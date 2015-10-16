using NAudio.Dsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    public class Window
    {
        private int windowSize = GlobalVariables.WindowSize;
        private int windowPointer = 0;
        public Block[] blocks { get; private set; }
        private Measurements measurements;

        public Window()
        {
            blocks = new Block[windowSize];
        }

        public void AddSample(byte[] buffer, int index)
        {
            if (blocks[windowPointer] == null)
                blocks[windowPointer] = new Block();

            blocks[windowPointer].AddSample(buffer, index);

            if (blocks[windowPointer].Done)
            {
                windowPointer++;
                if (Done)
                {
                    SetMeasurements();
                }
            }
        }

        private void SetMeasurements()
        {
            if (!Done)
                return;

            double averagePitch = 0;
            double averageEnergy = 0;

            for (int i = 0; i < windowSize; i++)
            {
                averagePitch += blocks[i].pitch;
                averageEnergy += blocks[i].energy;
            }

            averagePitch /= windowSize;
            averageEnergy /= windowSize;

            double pitchSTD = 0;
            double energySTD = 0;

            for (int i = 0; i < windowSize; i++)
            {
                pitchSTD += Math.Pow(blocks[i].pitch - averagePitch, 2);
                energySTD += Math.Pow(blocks[i].energy - averageEnergy, 2);
            }

            pitchSTD /= windowSize;
            energySTD /= windowSize;

            measurements = new Measurements(averagePitch, Math.Sqrt(pitchSTD), Math.Sqrt(energySTD));
        }

        public Measurements Measurements
        {
            get { return measurements; }
        }

        public bool Done
        {
            get { return windowPointer >= windowSize; }
        }
    }

    public class Block
    {
        private int blockSize = GlobalVariables.BlockSize; //Must be power of 2!!
        private int blockPointer = 0;
        private short[] dataArray;
        private Wave[] waveArray;
        private Complex[] fftArray;
        public double[] intensities { get; private set; }
        public double pitch { get; private set; }
        public double energy { get; private set; }

        public Block()
        {
            dataArray = new short[blockSize];
            waveArray = new Wave[blockSize / 2];
            fftArray = new Complex[blockSize];
            pitch = 0;
            energy = 0;
        }

        public void AddSample(byte[] buffer, int index)
        {
            short sample = (short)((buffer[index + 1] << 8) |
                                        buffer[index + 0]);
            dataArray[blockPointer] = sample;
            fftArray[blockPointer].X = (float)((sample / 32768f) * FastFourierTransform.HammingWindow(blockPointer, blockSize));
            blockPointer++;

            if (Done)
            {
                FastFourierTransform.FFT(true, (int)Math.Log(blockSize, 2.0), fftArray);

                intensities = new double[blockSize / 2];

                //GlobalVariables.WaveChart.Series[0].Points.Clear();

                for (int i = 0; i < blockSize / 2; i++)
                {
                    intensities[i] = Math.Sqrt((Math.Pow(fftArray[i].X, 2) + Math.Pow(fftArray[i].Y, 2)));

                    //GlobalVariables.WaveChart.Series[0].Points.AddXY(i * GlobalVariables.FrequencyOffset, intensities[i]);
                }

                Measure();
            }
        }

        private void Measure()
        {
            if (!Done)
                return;

            double maxIntensity = double.MinValue;
            double sum = 0;
            int index = 0;
            for (int i = GlobalVariables.VoiceLow / GlobalVariables.FrequencyOffset; i < GlobalVariables.VoiceHigh / GlobalVariables.FrequencyOffset; i++)
            {
                sum += intensities[i];
                if (intensities[i] > maxIntensity)
                {
                    maxIntensity = intensities[i];
                    index = i;
                }
            }

            energy = sum / ((GlobalVariables.VoiceHigh / GlobalVariables.FrequencyOffset) - (GlobalVariables.VoiceLow / GlobalVariables.FrequencyOffset));
            pitch = index * GlobalVariables.FrequencyOffset;
        }

        public bool Done
        {
            get { return blockPointer >= blockSize; }
        }
    }

    public class Wave
    {
        public int frequency { get; private set; }
        public float intensity { get; private set; }


        public Wave(int frequency, float intensity)
        {
            this.frequency = frequency;
            this.intensity = intensity;
        }
    }

    public class Measurements
    {
        public double averagePitch { get; private set; }
        public double pitchSTD { get; private set; }
        public double energySTD { get; private set; }

        public Measurements(double aPitch, double pSTD, double eSTD)
        {
            this.averagePitch = aPitch;
            this.pitchSTD = pSTD;
            this.energySTD = eSTD;
        }
    }
}
