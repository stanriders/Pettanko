// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System;
using System.Linq;
using Pettanko.Difficulty;
using Pettanko.Mods;
using Pettanko.Performance;

namespace Pettanko.Calculators
{
    public class TaikoPerformanceCalculator : IPerformanceCalculator
    {
        private int countGreat;
        private int countOk;
        private int countMeh;
        private int countMiss;

        public PerformanceAttributes Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)
        {
            var taikoAttributes = (TaikoDifficultyAttributes)difficultyAttributes;

            countGreat = scoreData.Statistics.Count300;
            countOk = scoreData.Statistics.Count100;
            countMeh = scoreData.Statistics.Count50;
            countMiss = scoreData.Statistics.Count50;
            

            // Custom multipliers for NoFail and SpunOut.
            double multiplier = 1.1; // This is being adjusted to keep the final pp value scaled around what it used to be when changing things

            if (scoreData.Mods.Any(m => m is ModNoFail))
                multiplier *= 0.90;

            if (scoreData.Mods.Any(m => m is ModHidden))
                multiplier *= 1.10;

            double difficultyValue = computeDifficultyValue(scoreData, taikoAttributes);
            double accuracyValue = computeAccuracyValue(scoreData, taikoAttributes);
            double totalValue =
                Math.Pow(
                    Math.Pow(difficultyValue, 1.1) +
                    Math.Pow(accuracyValue, 1.1), 1.0 / 1.1
                ) * multiplier;

            return new TaikoPerformanceAttributes
            {
                Difficulty = difficultyValue,
                Accuracy = accuracyValue,
                Total = totalValue
            };
        }

        private double computeDifficultyValue(Score score, TaikoDifficultyAttributes attributes)
        {
            double strainValue = Math.Pow(5.0 * Math.Max(1.0, attributes.StarRating / 0.0075) - 4.0, 2.0) / 100000.0;

            // Longer maps are worth more
            double lengthBonus = 1 + 0.1 * Math.Min(1.0, totalHits / 1500.0);
            strainValue *= lengthBonus;

            // Penalize misses exponentially. This mainly fixes tag4 maps and the likes until a per-hitobject solution is available
            strainValue *= Math.Pow(0.985, countMiss);

            if (score.Mods.Any(m => m is ModHidden))
                strainValue *= 1.025;

            if (score.Mods.Any(m => m is ModFlashlight))
                // Apply length bonus again if flashlight is on simply because it becomes a lot harder on longer maps.
                strainValue *= 1.05 * lengthBonus;

            // Scale the speed value with accuracy _slightly_
            return strainValue * score.Accuracy;
        }

        private double computeAccuracyValue(Score score, TaikoDifficultyAttributes attributes)
        {
            if (attributes.GreatHitWindow <= 0)
                return 0;

            // Lots of arbitrary values from testing.
            // Considering to use derivation from perfect accuracy in a probabilistic manner - assume normal distribution
            double accValue = Math.Pow(150.0 / attributes.GreatHitWindow, 1.1) * Math.Pow(score.Accuracy, 15) * 22.0;

            // Bonus for many hitcircles - it's harder to keep good accuracy up for longer
            return accValue * Math.Min(1.15, Math.Pow(totalHits / 1500.0, 0.3));
        }

        private int totalHits => countGreat + countOk + countMeh + countMiss;
    }
}
