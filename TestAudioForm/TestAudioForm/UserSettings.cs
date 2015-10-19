using System;
using System.Collections.Generic;
using System.Text;

namespace TestAudioForm
{
    public class UserSettings
    {
        public readonly char Gender;
        public readonly List<string> GenrePreferences;
        public readonly char GoalEmotion;

        public UserSettings(char gender, List<string> genrePreferences, char goalEmotion)
        {
            Gender = gender;
            GenrePreferences = genrePreferences;
            GoalEmotion = goalEmotion;
        }
    }
}