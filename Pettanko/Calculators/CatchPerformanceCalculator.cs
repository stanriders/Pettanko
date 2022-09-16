using System;
using System.Linq;
using Pettanko.Difficulty;
using Pettanko.Mods;
using Pettanko.Performance;

namespace Pettanko.Calculators
{
    public class CatchPerformanceCalculator : IPerformanceCalculator
    {
        private int fruitsHit;
        private int ticksHit;
        private int tinyTicksHit;
        private int tinyTicksMissed;
        private int misses;

        public PerformanceAttributes Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)
        {
            var catchAttributes = (CatchDifficultyAttributes)difficultyAttributes;

            fruitsHit = scoreData.Statistics.Count300;
            ticksHit = scoreData.Statistics.Count100;
            tinyTicksHit = scoreData.Statistics.Count50;
            tinyTicksMissed = scoreData.Statistics.CountKatu;
            misses = scoreData.Statistics.CountMiss;

            // We are heavily relying on aim in catch the beat
            double value = Math.Pow(5.0 * Math.Max(1.0, catchAttributes.StarRating / 0.0049) - 4.0, 2.0) / 100000.0;

            // Longer maps are worth more. "Longer" means how many hits there are which can contribute to combo
            int numTotalHits = totalComboHits();

            double lengthBonus =
                0.95 + 0.3 * Math.Min(1.0, numTotalHits / 2500.0) +
                (numTotalHits > 2500 ? Math.Log10(numTotalHits / 2500.0) * 0.475 : 0.0);
            value *= lengthBonus;

            value *= Math.Pow(0.97, misses);

            // Combo scaling
            if (catchAttributes.MaxCombo > 0)
                value *= Math.Min(Math.Pow(scoreData.MaxCombo, 0.8) / Math.Pow(catchAttributes.MaxCombo, 0.8), 1.0);

            double approachRate = catchAttributes.ApproachRate;
            double approachRateFactor = 1.0;
            if (approachRate > 9.0)
                approachRateFactor += 0.1 * (approachRate - 9.0); // 10% for each AR above 9
            if (approachRate > 10.0)
                approachRateFactor += 0.1 * (approachRate - 10.0); // Additional 10% at AR 11, 30% total
            else if (approachRate < 8.0)
                approachRateFactor += 0.025 * (8.0 - approachRate); // 2.5% for each AR below 8

            value *= approachRateFactor;

            if (scoreData.Mods.Any(m => m is ModHidden))
            {
                // Hiddens gives almost nothing on max approach rate, and more the lower it is
                if (approachRate <= 10.0)
                    value *= 1.05 + 0.075 * (10.0 - approachRate); // 7.5% for each AR below 10
                else if (approachRate > 10.0)
                    value *= 1.01 + 0.04 * (11.0 - Math.Min(11.0, approachRate)); // 5% at AR 10, 1% at AR 11
            }

            if (scoreData.Mods.Any(m => m is ModFlashlight))
                value *= 1.35 * lengthBonus;

            value *= Math.Pow(accuracy(), 5.5);

            if (scoreData.Mods.Any(m => m is ModNoFail))
                value *= 0.90;

            return new CatchPerformanceAttributes
            {
                Total = value
            };
        }
        private double accuracy() => totalHits() == 0 ? 0 : Extensions.Clamp((double)totalSuccessfulHits() / totalHits(), 0, 1);
        private int totalHits() => tinyTicksHit + ticksHit + fruitsHit + misses + tinyTicksMissed;
        private int totalSuccessfulHits() => tinyTicksHit + ticksHit + fruitsHit;
        private int totalComboHits() => misses + ticksHit + fruitsHit;
    }
}
