using System;
using System.Collections.Generic;
using System.Text;

namespace TestAudioForm
{
    public class UserSettings
    {
        public readonly string Subject;
        public readonly char Gender;
        public readonly List<string> GenrePreferences;
        public readonly char GoalEmotion;

        public UserSettings(string subject, char gender, List<string> genrePreferences, char goalEmotion)
        {
            Subject = subject;
            Gender = gender;
            GenrePreferences = genrePreferences;
            GoalEmotion = goalEmotion;
        }
    }
}