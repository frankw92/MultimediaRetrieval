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
        private static int windowSize = 3;
        private static int voiceLow = 300;
        private static int voiceHigh = 3400;
        public static char Gender;
        public static Chart WaveChart;

        public static readonly EmotionVector Anger = new EmotionVector(-0.41, 0.78);
        public static readonly EmotionVector Happy = new EmotionVector(0.9, 0.16);
        public static readonly EmotionVector Sad = new EmotionVector(-0.82, -0.4);
        public static readonly EmotionVector Fear = new EmotionVector(0.11, 0.78);
        public static readonly EmotionVector Neutral = new EmotionVector(0, 0);

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
