using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    class DatabaseManager
    {
        public List<WindowEmotion> DatabaseM = new List<WindowEmotion>();
        public List<WindowEmotion> DatabaseF = new List<WindowEmotion>();

        string file = "soundDb.txt";

        public DatabaseManager()
        {
            if (!File.Exists(file))
                CreateDatabase();
        }

        void CreateDatabase()
        {
            string[] files = Directory.GetFiles("wav");

            using (StreamWriter sw = new StreamWriter(file))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    // no boredom & disgust
                    char gEmotion = files[i][9];
                    if (gEmotion == 'L' || gEmotion == 'E')
                        continue;

                    char gender = GetGender(files[i].Substring(4, 2));
                    char emotion = GetEmotion(gEmotion);

                    // get values pitch, energy per window
                    
                    // write to file, for each window
                    // for each window
                    sw.WriteLine(gender + " " + emotion + " "); // + values
                    // end foreach
                }
            }
        }

        char GetGender(string fileName)
        {
            switch (fileName)
            {
                case "03":
                case "11":
                case "12":
                case "13":
                case "15":
                    return 'm';
                default:
                    return 'f';
            }
        }
        
        char GetEmotion(char c)
        {
            switch (c)
            {
                case 'W': return 'A';
                case 'A': return 'F';
                case 'F': return 'H';
                case 'T': return 'S';
                default: return 'N';
            }
        }

        void ReadFile()
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string[] line = sr.ReadLine().Split(' ');

                char gender = line[0][0];
                char emotion = line[1][0];
                // + values

                WindowEmotion we = new WindowEmotion(gender, emotion); // + values

                if (gender == 'm')
                    DatabaseM.Add(we);
                else
                    DatabaseF.Add(we);
            }
        }
    }
}
