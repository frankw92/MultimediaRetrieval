using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    class Emotion
    {
        private double pitch, loudness;
        private int zeroCrossings;
        private string name;

        public Emotion(string name, double pitch, double loudness, int zeroCrossings)
        {
            this.name = name;
            this.pitch = pitch;
            this.loudness = loudness;
            this.zeroCrossings = zeroCrossings;
        }

        /// <summary>
        /// Returns the name of the emotions.
        /// </summary>
        /// <returns> The name of the emotion. </returns>
        public string getName()
        {
            return name;
        }

        /// <summary>
        /// Returns the pitch of the emotion.
        /// </summary>
        /// <returns> The pitch of the emotion. </returns>
        public double getPitch()
        {
            return pitch;
        }

        /// <summary>
        /// Returns the loudness of the emotion.
        /// </summary>
        /// <returns> The loudness of the emotion. </returns>
        public double getLoudness()
        {
            return loudness;
        }

        /// <summary>
        /// Returns the number of zero crossings of the emotion.
        /// </summary>
        /// <returns> The number of zero crossings. </returns>
        public int getZeroCrossings()
        {
            return zeroCrossings;
        }
    }
}
