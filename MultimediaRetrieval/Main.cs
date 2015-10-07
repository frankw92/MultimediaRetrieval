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
    public partial class Main : Form
    {
        WaveIn waveIn;
        WaveOut waveOut;
        BufferedWaveProvider bufferedWaveProvider;
        bool recording, playing;

        public Main()
        {
            InitializeComponent();
            playButton.Visible = false;
            waveIn = new WaveIn();
            WaveFormat waveFormat = new WaveFormat(44100, 1);
            waveIn.WaveFormat = waveFormat;
            waveIn.DataAvailable += dataAvailable;

            bufferedWaveProvider = new BufferedWaveProvider(waveFormat);
            bufferedWaveProvider.BufferDuration = new TimeSpan(0,0,20); //20 seconds
            bufferedWaveProvider.DiscardOnBufferOverflow = true;
            waveOut = new WaveOut();
        }

        

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                bufferedWaveProvider.ClearBuffer();
                waveOut.Init(bufferedWaveProvider);

                playButton.Visible = false;

                recordButton.Text = "Stop Recording";
                waveIn.StartRecording();
                recording = true;
            }
            else
            {
                playButton.Visible = true;
                recordButton.Text = "Record";
                waveIn.StopRecording();
                recording = false;
            }
        }

        private void dataAvailable(object sender, WaveInEventArgs e)
        {
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (!playing)
            {
                StartPlaying();
            }
            else
            {
                StopPlaying();
            }
        }

        private void StartPlaying()
        {
            playing = true;
            playButton.Text = "Stop";
            recordButton.Visible = false;
            waveOut.Play();
        }

        private void StopPlaying()
        {
            playing = false;
            recordButton.Visible = true;
            playButton.Text = "Play";
            waveOut.Stop();
        }
    }
}
