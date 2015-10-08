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
using VoiceRecorder.Audio;
using System.Windows.Forms.DataVisualization.Charting;


namespace TestAudioForm
{
    public partial class TestAudio : Form
    {
        private WaveIn waveIn;
        private BufferedWaveProvider bufferedWaveProvider;
        private short[] waveArray;
        private int arrayPointer;
        private int arraySize = 100;
        private SampleAggregator sampleAggregator;


        public TestAudio()
        {
            InitializeComponent();

            var series1 = new Series
            {
                Name = "Series1",
                Color = Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            waveChart.ChartAreas[0].AxisX.Maximum = 100;
            waveChart.ChartAreas[0].AxisX.Minimum = 0;
            waveChart.ChartAreas[0].AxisY.Maximum = 100;
            waveChart.ChartAreas[0].AxisY.Minimum = -100;

            //waveChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            //waveChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;

            
            
            sampleAggregator = new SampleAggregator();
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
                sampleAggregator.Add(sample32);

                arrayPointer++;
                if (arrayPointer >= 99)
                {
                    arrayPointer = 0;
                    waveChart.Series[0].Points.Clear();
                }

                waveChart.Series[0].Points.AddXY(arrayPointer - 1, sample32 * 100);
                
            }

        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            arrayPointer = 0;
            waveIn = new WaveIn();
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
        }

        private void FFT()
        {

        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            waveIn = null;
        }

        public int ZeroCrossings()
        {
            if (arrayPointer == 0)
                return 0;


            int start = 0;

            while (waveArray[start] == 0 && start <= arrayPointer)
            {
                start++;
            }

            if (start == arrayPointer)
                return 0;

            short current = waveArray[start];

            int crossings = 0;

            for (int i = 1; i <= arrayPointer; i++)
            {
                if (waveArray[i] != 0 && (waveArray[i] * current) < 0)
                {
                    crossings++;
                    current = waveArray[i];
                }
            }

            return crossings / waveIn.WaveFormat.SampleRate;
        }
    }
}
