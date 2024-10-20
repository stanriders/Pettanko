// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System;
using System.Linq;
using Pettanko.Difficulty;
using Pettanko.Mods;
using Pettanko.Performance;

namespace Pettanko.Calculators
{
    public class ManiaPerformanceCalculator : IPerformanceCalculator
    {
        private int countPerfect;
        private int countGreat;
        private int countGood;
        private int countOk;
        private int countMeh;
        private int countMiss;
        private double scoreAccuracy;

        public PerformanceAttributes Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)
        {
            var maniaAttributes = (ManiaDifficultyAttributes)difficultyAttributes;

            // FIXME: this is probably incorrect, fix when mania switches to using hit results
            countPerfect = scoreData.Statistics.Count300;
            countGreat = scoreData.Statistics.CountGeki;
            countGood = scoreData.Statistics.Count100;
            countOk = scoreData.Statistics.CountKatu;
            countMeh = scoreData.Statistics.Count50;
            countMiss = scoreData.Statistics.CountMiss;
            scoreAccuracy = calculateCustomAccuracy();

            double multiplier = 1.0;

            if (scoreData.Mods.Any(m => m is ModNoFail))
                multiplier *= 0.75;
            if (scoreData.Mods.Any(m => m is ModEasy))
                multiplier *= 0.5;

            double difficultyValue = computeDifficultyValue(maniaAttributes);
            double totalValue = difficultyValue * multiplier;

            return new ManiaPerformanceAttributes
            {
                Difficulty = difficultyValue,
                Total = totalValue
            };
        }
        private double computeDifficultyValue(ManiaDifficultyAttributes attributes)
        {
            double difficultyValue = 8.0 * Math.Pow(Math.Max(attributes.StarRating - 0.15, 0.05), 2.2) // Star rating to pp curve
                                         * Math.Max(0, 5 * scoreAccuracy - 4) // From 80% accuracy, 1/20th of total pp is awarded per additional 1% accuracy
                                         * (1 + 0.1 * Math.Min(1, totalHits / 1500)); // Length bonus, capped at 1500 notes

            return difficultyValue;
        }

        private double totalHits => countPerfect + countOk + countGreat + countGood + countMeh + countMiss;

        /// <summary>
        /// Accuracy used to weight judgements independently from the score's actual accuracy.
        /// </summary>
        private double calculateCustomAccuracy()
        {
            if (totalHits == 0)
                return 0;

            return (countPerfect * 320 + countGreat * 300 + countGood * 200 + countOk * 100 + countMeh * 50) / (totalHits * 320);
        }
    }
}
