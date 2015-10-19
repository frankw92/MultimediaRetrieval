using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace TestAudioForm
{
    class DataTrainingManager
    {
        private Window activeWindow;
        private List<Window> windows;

        public DataTrainingManager()
        {

        }

        /// <summary>
        /// Read audio-file and return measurements
        /// </summary>
        /// <returns> Measurements of audio-file </returns>
        public Measurements[] measureInput(string fileName)
        {
            activeWindow = new Window();
            windows = new List<Window>();

            if (fileName.EndsWith(".mp3"))
            {
                using (Mp3FileReader reader = new Mp3FileReader(fileName))
                {
                    ProcessFile(reader);
                }
            }

            else if (fileName.EndsWith(".wav"))
            {
                using (WaveFileReader reader = new WaveFileReader(fileName))
                {
                    ProcessFile(reader);
                }
            } 
            return GetFileMeasurements();
        }

        private void ProcessFile(WaveStream reader)
        {
            int fileLength = (int)reader.Length;
            byte[] buffer = new byte[fileLength];
            reader.Read(buffer, 0, fileLength);
            ProcessData(buffer, fileLength);
        }

        private Measurements[] GetFileMeasurements()
        {
            if (windows.Count == 0)
                return null;

            int numberOfWindows = windows.Count;
            Measurements[] fileMeasurements = new Measurements[numberOfWindows];

            for (int i = 0; i < numberOfWindows; i++) //-1 because we always throw away the last window
            {
                fileMeasurements[i] = windows[i].Measurements;
            }

            return fileMeasurements;
        }

        private void ProcessData(byte[] buffer, int bytesRecorded)
        {
            for (int index = 0; index < bytesRecorded; index += 2)
            {
                activeWindow.AddSample(buffer, index);
                if (activeWindow.Done)
                {
                    windows.Add(activeWindow);
                    activeWindow = new Window();
                }
            }
        }
    }
}
