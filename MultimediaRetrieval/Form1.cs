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


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        WaveIn waveIn;
        WaveOut waveOut;
        BufferedWaveProvider bufferedWaveProvider;
        bool recording, playing;

        public Form1()
        {
            InitializeComponent();
            button2.Visible = false;
            waveIn = new WaveIn();
            WaveFormat waveFormat = new WaveFormat(44100, 1);
            waveIn.WaveFormat = waveFormat;
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(dataAvailable);

            bufferedWaveProvider = new BufferedWaveProvider(waveFormat);

            waveOut = new WaveOut();
            waveOut.Init(bufferedWaveProvider);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                button1.Text = "Stop Recording";
                waveIn.StartRecording();
                recording = true;
            }
            else
            {
                button2.Visible = true;
                button1.Text = "Record";
                waveIn.StopRecording();
                recording = false;
            }
        }

        private void dataAvailable(object sender, WaveInEventArgs e)
        {
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!playing)
            {
                playing = true;
                button2.Text = "Stop";
                waveOut.Play();
            }
            else
            {
                playing = false;
                button2.Text = "Play";
                waveOut.Stop();
            }
        }
    }
}
