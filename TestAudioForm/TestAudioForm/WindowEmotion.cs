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
        public readonly double AveragePitch, PitchSTD, EnergySTD;

        
        public WindowEmotion(char gender, char emotion, double averagePitch, double pitchSTD, double energySTD)
        {
            Gender = gender;
            Emotion = emotion;
            AveragePitch = averagePitch;
            PitchSTD = pitchSTD;
            EnergySTD = energySTD;
        }
    }
}
