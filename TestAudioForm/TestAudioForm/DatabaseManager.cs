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

        double pitchDifference, pitchSTDDifference, energySTDDifference;

        string file = "soundDb.txt";

        public DatabaseManager()
        {
            CreateDatabase();
            ReadFile();
        }

        void CreateDatabase()
        {
            if (File.Exists(file))
                return;
                
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
                    
                    if (measurements == null)                
                        continue;

                    // write to file, for each window
                    foreach (Measurements m in measurements)
                    {
                        sw.WriteLine(gender + " " + emotion + " " + m.averagePitch + " " + m.pitchSTD + " " + m.energySTD);
                    }
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
            double minPitch = double.MaxValue;
            double maxPitch = double.MinValue;
            double minPitchSTD = double.MaxValue;
            double maxPitchSTD = double.MinValue;
            double minEnergySTD = double.MaxValue;
            double maxEnergySTD = double.MinValue;

            using (StreamReader sr = new StreamReader(file))
            {
                string input = sr.ReadLine();
                while (input != null)
                { 
                    string[] line = input.Split(' ');

                    char gender = line[0][0];
                    char emotion = line[1][0];
                
                    // The values
                    double averagePitch = Double.Parse(line[2]);
                    double pitchSTD = Double.Parse(line[3]);
                    double energySTD = Double.Parse(line[4]);

                    // Check for all the variables if it's a new max or min
                    if (averagePitch < minPitch)
                        minPitch = averagePitch;
                    else if (averagePitch > maxPitch)
                        maxPitch = averagePitch;

                    if (pitchSTD < minPitchSTD)
                        minPitchSTD = pitchSTD;
                    else if (pitchSTD > maxPitchSTD)
                        maxPitchSTD = pitchSTD;

                    if (energySTD < minEnergySTD)
                        minEnergySTD = energySTD;
                    else if (energySTD > maxEnergySTD)
                        maxEnergySTD = energySTD;

                    // Create a new window emotion object and add it to the right list
                    WindowEmotion we = new WindowEmotion(gender, emotion, averagePitch, pitchSTD, energySTD);

                    if (gender == 'm')
                        DatabaseM.Add(we);
                    else
                        DatabaseF.Add(we);

                    // Read a new line
                    input = sr.ReadLine();
                }
            }

            // Calculate differences between min and max values
            pitchDifference = Math.Abs(maxPitch - minPitch);
            pitchSTDDifference = Math.Abs(maxPitchSTD - minPitchSTD);
            energySTDDifference = Math.Abs(maxEnergySTD - minEnergySTD);
        }

        /// <summary>
        /// Searches in the corresponding databse for the emotional state of the user.
        /// </summary>
        /// <param name="measurements"> A WindowEmotion measured from the user's speech.</param>
        /// <returns>The emotional state of the user.</returns>
        public char SearchDatabaseForEmotion (List<Window> windows)
        {
            double tHappy = 0.0f, tAnger = 0.0f, tNeutral = 0.0f, tSad = 0.0f, tFear = 0.0f;

            List<WindowEmotion> db = GlobalVariables.Gender == 'm' ? DatabaseM : DatabaseF;

            foreach (Window window in windows)
            {
                double sHappy = Double.MaxValue, sAnger = Double.MaxValue, sNeutral = Double.MaxValue, sSad = Double.MaxValue, sFear = Double.MaxValue;

                foreach(WindowEmotion dataBaseEntry in db)
                {
                    // Calculate distance based on sum of squared errors
                    double averagePitchDistance = window.Measurements.averagePitch - dataBaseEntry.AveragePitch;
                    double pitchSTDDistance = window.Measurements.pitchSTD - dataBaseEntry.PitchSTD;
                    double energySTDDistance = window.Measurements.energySTD - dataBaseEntry.EnergySTD;

                    double currentDistance = Math.Pow(averagePitchDistance / pitchDifference, 2) + Math.Pow(pitchSTDDistance / pitchSTDDifference, 2) + Math.Pow(energySTDDistance / energySTDDifference, 2);

                    switch (dataBaseEntry.Emotion)
                    {
                        case 'H':
                            if (currentDistance < sHappy)
                                sHappy = currentDistance;
                            break;
                        case 'A':
                            if (currentDistance < sAnger)
                                sAnger = currentDistance;
                            break;
                        case 'S':
                            if (currentDistance < sSad)
                                sSad = currentDistance;
                            break;
                        case 'F':
                            if (currentDistance < sFear)
                                sFear = currentDistance;
                            break;
                        default:
                            if (currentDistance < sNeutral)
                                sNeutral = currentDistance;
                            break;
                    }
                }

                tHappy += sHappy;
                tSad += sSad;
                tAnger += sAnger;
                tFear += sFear;
                tNeutral += sNeutral;
            }

            char emotion = 'N';

            if (tHappy < tNeutral)
            {
                emotion = 'H';
                tNeutral = tHappy;
            }
            if (tSad < tNeutral)
            {
                emotion = 'S';
                tNeutral = tSad;
            } 
            if (tAnger < tNeutral)
            {
                emotion = 'A';
                tNeutral = tAnger;
            } 
            if (tFear < tNeutral)
            {
                emotion = 'F';
                tNeutral = tFear;
            }

            System.Diagnostics.Debug.WriteLine("Your emotion is {0}", emotion);

            return emotion;
        }
    }
}
