using System;
using System.IO;
using System.Text;

namespace TestAudioForm
{
    class OutputManager
    {
        string file;

        public OutputManager(UserSettings settings)
        {
            // Create a new directory if it isn't there yet
            if (!Directory.Exists("output"))
                Directory.CreateDirectory("output");

            // Create a new file for the current subject
            CreateNewFile(settings);
        }

        private void CreateNewFile(UserSettings settings)
        {
            // Create a file name
            file = "output/output-" + settings.Subject + ".csv";

            // Make sure file name is unique
            int i = 1;
            while (File.Exists(file))
            {
                file = "output/output-" + settings.Subject + " (" + i + ").csv";
                i++;
            }

            // Put all initial user settings and headings in the file
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine("Test subject;" + settings.Subject);
                sw.WriteLine("Gender;" + (settings.Gender == 'm' ? "Male" : "Female"));
                sw.WriteLine("Gender preferences;" + string.Join(", ", settings.GenrePreferences.ToArray()));
                sw.WriteLine("Goal emotion;" + CharToEmotion(settings.GoalEmotion) + "\n");
                sw.WriteLine("Iteration;");
            }
        }

        private string CharToEmotion(char c)
        {
            switch (c)
            {
                case 'A': return "Anger";
                case 'F': return "Fear";
                case 'H': return "Happiness";
                case 'S': return "Sadness";
                default: return "Neutral";
            }
        }

        public void OutputIteration(/* variables */)
        {
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine(/* variables + */ ";");
            }
        }
    }
}
