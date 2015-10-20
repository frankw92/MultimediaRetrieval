using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    class EmotionVector
    {
        public double Arousal;
        public double Valence;

        public EmotionVector(double valence, double arousal)
        {
            Arousal = arousal;
            Valence = valence;
        }
    }
}
