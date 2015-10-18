using System;
using System.Collections.Generic;
using System.Text;

namespace TestAudioForm
{
    public class UserSettings
    {
        public readonly char Gender;
        public readonly List<string> GenrePreferences;
        public readonly Emotion GoalEmotion;

        public UserSettings(char gender, List<string> genrePreferences, Emotion goalEmotion)
        {
            Gender = gender;
            GenrePreferences = genrePreferences;
            GoalEmotion = goalEmotion;
        }
    }
}