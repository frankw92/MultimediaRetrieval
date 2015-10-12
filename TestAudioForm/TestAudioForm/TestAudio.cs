using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Dsp;
//using VoiceRecorder.Audio;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;


namespace TestAudioForm
{
    public partial class TestAudio : Form
    {
        private WaveIn waveIn;
        private BufferedWaveProvider bufferedWaveProvider;
        private float[] waveBuffer;
        private Complex[] fftBuffer;
        //private int arrayPointer;
        private int bufferPointer = 0;
        private int bufferSize = 8192; //Must be power of 2!
        private int sampleRate = 44100;
        private List<double> maxIntensities = new List<double>();
        private List<int> maxIntensityIndices = new List<int>();
        //private SampleAggregator sampleAggregator;
        //private AutoCorrelator pitchDetector;
        //private FftPitchDetector pDetector;
        //private FFTCalculator fftCalculator;


        public TestAudio()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            var series1 = new Series
            {
                Name = "Series1",
                Color = Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                //ChartType = SeriesChartType.Line
            };

            waveChart.ChartAreas[0].AxisX.Maximum = 4000;
            waveChart.ChartAreas[0].AxisX.Minimum = 0;
            waveChart.ChartAreas[0].AxisY.Maximum = 0.05f;
            waveChart.ChartAreas[0].AxisY.Minimum = 0;
            waveChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            
            waveBuffer = new float[bufferSize];
            
            //sampleAggregator = new SampleAggregator();

            fftBuffer = new Complex[bufferSize];            
        }

        public void AddToBuffer(float f)
        {
            waveBuffer[bufferPointer] = f;
            fftBuffer[bufferPointer].X = (float)(f * FastFourierTransform.HammingWindow(bufferPointer, bufferSize));

            int xOffset = sampleRate / (bufferSize / 2);
            double maxIntensity = 0;
            int maxIntensityIndex = 0;

            bufferPointer = (bufferPointer + 1) % bufferSize;

            if (bufferPointer == 0) //Buffer full, send to FFT for calculation!
            {
                //float pitch = pitchDetector.DetectPitch(waveBuffer, (int)bufferSize / 4);
                FastFourierTransform.FFT(true, (int)Math.Log(bufferSize, 2.0), fftBuffer);

                waveChart.Series[0].Points.Clear();

                try
                {
                    for (int i = 0; i < fftBuffer.Length / 2; i++)
                    {
                        double intensity = Math.Sqrt((Math.Pow(fftBuffer[i].X, 2) + Math.Pow(fftBuffer[i].Y, 2)));
                        int index = i * xOffset;

                        maxIntensity = Math.Max(maxIntensity, intensity);
                        maxIntensityIndex = (maxIntensity == intensity) ? index : maxIntensityIndex;
 
                        waveChart.Series[0].Points.AddXY(i * xOffset, intensity);
                    }

                    maxIntensities.Add(maxIntensity);
                    maxIntensityIndices.Add(maxIntensityIndex);
                }
                catch { }
                //waveChart.Series[0].Points.Add(pitch);
                //Debug.Write("Pitch: " + pitch);
            }

        }

        public void NewDataAvailable(object sender, WaveInEventArgs e)
        {
            //Shit hier als er nieuwe data is!!!
            //for (int i = 0; i < e.BytesRecorded; i += 2)
            //{

            //    short sample = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i + 0]);

            //    waveArray[arrayPointer] = sample;
            //    arrayPointer = (arrayPointer + 1) % waveArray.Length;
            //}

            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;

            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((buffer[index + 1] << 8) |
                                        buffer[index + 0]);
                float sample32 = sample / 32768f;
                AddToBuffer(sample32);
            }

        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            //arrayPointer = 0;
            waveIn = new WaveIn();
            waveIn.WaveFormat = new WaveFormat(sampleRate, 2);

            //pitchDetector = new AutoCorrelator(waveIn.WaveFormat.SampleRate);
            //pDetector = new FftPitchDetector(waveIn.WaveFormat.SampleRate);
            
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(NewDataAvailable);
            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

            try
            {
                waveIn.StartRecording();
            }
            catch
            {
                MessageBox.Show("Ja echt dom om te recorden zonder microfoon...");
            }

            stopRecordingButton.Enabled = true;
            startRecordingButton.Enabled = false;
        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            try
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
                waveChart.Series[0].Points.Clear();
                CalculatePitch();
            }
            catch { }

            
            stopRecordingButton.Enabled = false;
            startRecordingButton.Enabled = true;
        }

        private void CalculatePitch()
        {
            double mxIntensity = 0;
            double mxIndex = 0;
            for (int i = 0; i < maxIntensities.Count; i++)
            {
                //if (humanvoiceMin < maxIntensityIndices[i] > humanvoiceMax)
                //{

                mxIntensity = Math.Max(mxIntensity, maxIntensities[i]);
                mxIndex = (mxIntensity == maxIntensities[i]) ? maxIntensityIndices[i] : mxIndex;

                //}
            }
        }


        //public int ZeroCrossings()
        //{
        //    if (arrayPointer == 0)
        //        return 0;


        //    int start = 0;

        //    while (waveArray[start] == 0 && start <= arrayPointer)
        //    {
        //        start++;
        //    }

        //    if (start == arrayPointer)
        //        return 0;

        //    short current = waveArray[start];

        //    int crossings = 0;

        //    for (int i = 1; i <= arrayPointer; i++)
        //    {
        //        if (waveArray[i] != 0 && (waveArray[i] * current) < 0)
        //        {
        //            crossings++;
        //            current = waveArray[i];
        //        }
        //    }

        //    return crossings / waveIn.WaveFormat.SampleRate;
        //}
    }
}
