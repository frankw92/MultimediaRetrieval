using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    public class Emotion
    {
        public char Name;
        public double Arousal;
        public double Valence;

        public Emotion(char name)
        {
            Name = name;
        }

        public Emotion(double arousal, double valence)
        {
            Arousal = arousal;
            Valence = valence;
        }
    }
}