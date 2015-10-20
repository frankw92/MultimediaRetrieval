using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudioForm
{
    public class EmotionCalculator
    {
        const double minX = -1, minY = -1, maxX = 1, maxY = 1;
        const int iterations = 10;

        public EmotionVector CalculateVector(EmotionAnalysis analysis)
        {
            double wMinX = minX, wMinY = minY, wMaxX = maxX, wMaxY = maxY;
            double xMid, yMid, xStep, yStep, current, best;

            for (int i = 0; i < iterations; i++)
            {
                xStep = (wMaxX - wMinX) / 4;
                xMid = wMinX + xStep;
                best = DistanceBetweenEmotions(analysis, CalculateDistanceToPoint(wMinX + xStep, (wMaxY + wMinY) / 2));

                current = DistanceBetweenEmotions(analysis, CalculateDistanceToPoint(wMinX + xStep * 2, (wMaxY + wMinY) / 2));
                if (current < best)
                {
                    best = current;
                    xMid = wMinX + xStep * 2;
                }

                current = DistanceBetweenEmotions(analysis, CalculateDistanceToPoint(wMinX + xStep * 3, (wMaxY + wMinY) / 2));
                if (current < best)
                {
                    best = current;
                    xMid = wMinX + xStep * 3;
                }

                yStep = (wMaxY - wMinY) / 4;
                yMid = wMinY + yStep;
                best = DistanceBetweenEmotions(analysis, CalculateDistanceToPoint((wMaxX + wMinX) / 2, wMinY + yStep));

                current = DistanceBetweenEmotions(analysis, CalculateDistanceToPoint((wMaxX + wMinX) / 2, wMinY + yStep * 2));
                if (current < best)
                {
                    best = current;
                    yMid = wMinY + yStep * 2;
                }

                current = DistanceBetweenEmotions(analysis, CalculateDistanceToPoint((wMaxX + wMinX) / 2, wMinY + yStep * 3));
                if (current < best)
                {
                    best = current;
                    yMid = wMinY + yStep * 3;
                }

                wMinX = xMid - xStep;
                wMaxX = xMid + xStep;
                wMinY = yMid - yStep;
                wMaxY = yMid + yStep;
            }

            return new EmotionVector((wMinX + wMaxX) / 2, (wMinY + wMaxY) / 2);
        }

        private EmotionAnalysis NormalizeAnalysis(EmotionAnalysis analysis)
        {
            double total = analysis.Anger + analysis.Fear + analysis.Happy + analysis.Neutral + analysis.Sad;
            return new EmotionAnalysis(analysis.Happy / total, analysis.Sad / total, analysis.Anger / total, analysis.Fear / total, analysis.Neutral / total);
        }

        private double DistanceBetweenEmotions(EmotionAnalysis one, EmotionAnalysis two)
        {
            EmotionAnalysis a = NormalizeAnalysis(one);
            EmotionAnalysis b = NormalizeAnalysis(two);
            return Math.Abs(a.Anger - b.Anger) + Math.Abs(a.Fear - b.Fear) + Math.Abs(a.Happy - b.Happy) + Math.Abs(a.Neutral - b.Neutral) + Math.Abs(a.Sad - b.Sad);
        }

        private EmotionAnalysis CalculateDistanceToPoint(double x, double y)
        {
            double angerD = Math.Sqrt(Math.Pow(GlobalVariables.Anger.Valence - x, 2) + Math.Pow(GlobalVariables.Anger.Arousal - y, 2));
            double fearD = Math.Sqrt(Math.Pow(GlobalVariables.Fear.Valence - x, 2) + Math.Pow(GlobalVariables.Fear.Arousal - y, 2));
            double happyD = Math.Sqrt(Math.Pow(GlobalVariables.Happy.Valence - x, 2) + Math.Pow(GlobalVariables.Happy.Arousal - y, 2));
            double sadD = Math.Sqrt(Math.Pow(GlobalVariables.Sad.Valence - x, 2) + Math.Pow(GlobalVariables.Sad.Arousal - y, 2));
            double neutralD = Math.Sqrt(Math.Pow(GlobalVariables.Neutral.Valence - x, 2) + Math.Pow(GlobalVariables.Neutral.Arousal - y, 2));
            return new EmotionAnalysis(happyD, sadD, angerD, fearD, neutralD);
        }
    }
}
