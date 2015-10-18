using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestAudioForm
{
    public static class GlobalVariables
    {

        private static int sampleRate = 44100;
        private static int blockSize = 8192; //Must be power of 2!!!
        private static int windowSize = 2;
        private static int voiceLow = 300;
        private static int voiceHigh = 3400;
        public static Chart WaveChart;

        public static int SampleRate
        {
            get { return sampleRate; }
        }

        public static int BlockSize
        {
            get { return blockSize; }
        }

        public static int WindowSize
        {
            get { return windowSize; }
        }

        public static int FrequencyOffset
        {
            get { return sampleRate / (blockSize / 2); }
        }

        public static int VoiceLow
        {
            get { return voiceLow; }
        }

        public static int VoiceHigh
        {
            get { return voiceHigh; }
        }
    }
}
