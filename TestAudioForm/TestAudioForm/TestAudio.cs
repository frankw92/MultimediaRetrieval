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
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;


namespace TestAudioForm
{
    public partial class TestAudio : Form
    {
        private WaveIn waveIn;
        private int bufferSize = GlobalVariables.BlockSize; //Must be power of 2!
        private int sampleRate = GlobalVariables.SampleRate;
        private Window activeWindow;
        private List<Window> windows;

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
            };

            GlobalVariables.WaveChart = waveChart;

            waveChart.ChartAreas[0].AxisX.Maximum = 4000;
            waveChart.ChartAreas[0].AxisX.Minimum = 0;
            waveChart.ChartAreas[0].AxisY.Maximum = 0.05f;
            waveChart.ChartAreas[0].AxisY.Minimum = 0;
            waveChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
        }

        public void NewDataAvailable(object sender, WaveInEventArgs e)
        {

            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;

            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                activeWindow.AddSample(buffer, index);
                if (activeWindow.Done)
                {
                    windows.Add(activeWindow);

                    waveChart.Series[0].Points.Clear();

                    for (int i = 0; i < activeWindow.blocks[0].intensities.Length; i++)
                    {
                        waveChart.Series[0].Points.AddXY(i * GlobalVariables.FrequencyOffset, activeWindow.blocks[0].intensities[i]);
                    }

                    activeWindow = new Window();
                }
            }
        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            //arrayPointer = 0;
            waveIn = new WaveIn();
            waveIn.WaveFormat = new WaveFormat(sampleRate, 2);

            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(NewDataAvailable);

            activeWindow = new Window();
            windows = new List<Window>();

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
            }
            catch { }


            stopRecordingButton.Enabled = false;
            startRecordingButton.Enabled = true;
        }

        private void TestAudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            catch { }
        }
    }
}
