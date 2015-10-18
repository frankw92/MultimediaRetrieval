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
            else
            {
                File.Delete(file);
                CreateDatabase();
            }
        }

        void CreateDatabase()
        {
            string[] files = Directory.GetFiles("wav");
            DataTrainingManager dtm = new DataTrainingManager();

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
                    Measurements[] measurements = dtm.measureInput(files[i]);
                    
                    // write to file, for each window
                    // for each window
                    foreach (Measurements m in measurements)
                    {
                        sw.WriteLine(gender + " " + emotion + " " + m.averagePitch + " " + m.pitchSTD + " " + m.energySTD);
                    }
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
                
                // The values
                double averagePitch = Double.Parse(line[2]);
                double pitchSTD = Double.Parse(line[3]);
                double energySTD = Double.Parse(line[4]); 

                WindowEmotion we = new WindowEmotion(gender, emotion, averagePitch, pitchSTD, energySTD);

                if (gender == 'm')
                    DatabaseM.Add(we);
                else
                    DatabaseF.Add(we);
            }
        }

        /// <summary>
        /// Searches in the corresponding databse for the emotional state of the user.
        /// </summary>
        /// <param name="measurements"> A WindowEmotion measured from the user's speech.</param>
        /// <returns>The emotional state of the user.</returns>
        char SearchDatabaseForEmotion (WindowEmotion measurements)
        {
            if (measurements.Gender == 'm')
                return SearchDatabaseM(measurements);
            else
                return SearchDatabaseF(measurements);
        }

        char SearchDatabaseM(WindowEmotion measurements)
        {
            char emotion = 'N'; // Instantiate as neutral emotion, will change in loop.
            double smallestDistance = Double.MaxValue; // Highest value possible

            foreach (WindowEmotion dataBaseEntry in DatabaseM)
            {
                // Calculate distance based on sum of squared errors
                double averagePitchDistance = measurements.AveragePitch - dataBaseEntry.AveragePitch;
                double pitchSTDDistance = measurements.PitchSTD - dataBaseEntry.PitchSTD;
                double energySTDDistance = measurements.EnergySTD - dataBaseEntry.EnergySTD;

                double currentDistance = Math.Pow(averagePitchDistance, 2) + Math.Pow(pitchSTDDistance, 2) + Math.Pow(energySTDDistance, 2);

                if (currentDistance < smallestDistance)
                {
                    smallestDistance = currentDistance;
                    emotion = dataBaseEntry.Emotion;
                }
            }

            return emotion;
        }

        char SearchDatabaseF(WindowEmotion measurements)
        {
            char emotion = 'N'; // Instantiate as neutral emotion, will change in loop.
            double smallestDistance = Double.MaxValue; // Highest value possible

            foreach (WindowEmotion dataBaseEntry in DatabaseF)
            {
                // Calculate distance based on sum of squared errors
                double averagePitchDistance = measurements.AveragePitch - dataBaseEntry.AveragePitch;
                double pitchSTDDistance = measurements.PitchSTD - dataBaseEntry.PitchSTD;
                double energySTDDistance = measurements.EnergySTD - dataBaseEntry.EnergySTD;

                double currentDistance = Math.Pow(averagePitchDistance, 2) + Math.Pow(pitchSTDDistance, 2) + Math.Pow(energySTDDistance, 2);

                if (currentDistance < smallestDistance)
                {
                    smallestDistance = currentDistance;
                    emotion = dataBaseEntry.Emotion;
                }
            }

            return emotion;
        }
    }
}
