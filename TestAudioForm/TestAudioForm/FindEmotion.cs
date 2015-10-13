using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    class FindEmotion
    {
        private double pitch, loudness;
        private int zeroCrossings;
        public List<Emotion> emotionDatabase;


        public FindEmotion(double pitch, double loudness, int zeroCrossings)
        {
            this.pitch = pitch;
            this.loudness = loudness;
            this.zeroCrossings = zeroCrossings;
            createEmotionDatabase();
        }

        // Fills a list with all the emotion we consider.
        private void createEmotionDatabase()
        {
            emotionDatabase.Add(new Emotion("Neutral", 65, 0, 0));
            emotionDatabase.Add(new Emotion("Happiness", 155, 0, 0));
            emotionDatabase.Add(new Emotion("Anger", 110, 0, 0));
            emotionDatabase.Add(new Emotion("Sadness", 102, 0, 0));
            emotionDatabase.Add(new Emotion("Fear", 200, 0, 0));
        }

        /// <summary>
        /// Returns the list containing all emotions currently in the database.
        /// </summary>
        /// <returns> The list of all emotions. </returns>
        public List<Emotion> getEmotionDatabase()
        {
            return emotionDatabase;
        }
    }
}
