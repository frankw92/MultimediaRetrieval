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


namespace TestAudioForm
{
    public partial class TestAudio : Form
    {
        private WaveIn waveIn;
        private BufferedWaveProvider bufferedWaveProvider;
        private short[] waveArray;
        private int arrayPointer;


        public TestAudio()
        {
            InitializeComponent();

            waveArray = new short[10000000];
            arrayPointer = 0;
        }


        public void NewDataAvailable(object sender, WaveInEventArgs e)
        {
            //Shit hier als er nieuwe data is!!!
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {

                short sample = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i + 0]);
      
                waveArray[arrayPointer] = sample;
                arrayPointer = (arrayPointer + 1) % waveArray.Length;
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

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            int x = ZeroCrossings();
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
