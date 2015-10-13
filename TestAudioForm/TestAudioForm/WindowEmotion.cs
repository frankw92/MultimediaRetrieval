using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    class WindowEmotion
    {
        public readonly char Gender;
        public readonly char Emotion;
        // pitch
        // energy
        
        public WindowEmotion(char gender, char emotion)
        {
            Gender = gender;
            Emotion = emotion;
        }
    }
}
