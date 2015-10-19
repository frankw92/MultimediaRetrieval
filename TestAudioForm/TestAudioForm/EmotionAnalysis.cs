using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    class EmotionAnalysis
    {
        public double Happy, Sad, Anger, Fear, Neutral;
        public char Emotion;

        public EmotionAnalysis(double happy, double sad, double anger, double fear, double neutral)
        {
            Happy = happy;
            Sad = sad;
            Anger = anger;
            Fear = fear;
            Neutral = neutral;
        }
    }
}
