using System;
using System.Linq;
using Pettanko.Difficulty;
using Pettanko.Mods;
using Pettanko.Performance;

namespace Pettanko.Calculators
{
    public class ManiaPerformanceCalculator : IPerformanceCalculator
    {
        // Score after being scaled by non-difficulty-increasing mods
        private double scaledScore;

        private int countPerfect;
        private int countGreat;
        private int countGood;
        private int countOk;
        private int countMeh;
        private int countMiss;

        public PerformanceAttributes Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)
        {
            var maniaAttributes = (ManiaDifficultyAttributes)difficultyAttributes;

            scaledScore = scoreData.TotalScore;

            // FIXME: this is probably incorrect, fix when mania switches to using hit results
            countPerfect = scoreData.Statistics.Count300;
            countGreat = scoreData.Statistics.CountGeki;
            countGood = scoreData.Statistics.Count100;
            countOk = scoreData.Statistics.CountKatu;
            countMeh = scoreData.Statistics.Count50;
            countMiss = scoreData.Statistics.CountMiss;

            // TODO:
            if (scoreData.Mods.Length > 0)
                throw new NotImplementedException("Mania mods are not implemented yet!");

            //IEnumerable<Mod> scoreIncreaseMods = Ruleset.GetModsFor(ModType.DifficultyIncrease);

            double scoreMultiplier = 1.0;
            //foreach (var m in scoreData.Mods.Where(m => !scoreIncreaseMods.Contains(m)))
            //    scoreMultiplier *= m.ScoreMultiplier;
            
            // Scale score up, so it's comparable to other keymods
            scaledScore *= 1.0 / scoreMultiplier;

            // Arbitrary initial value for scaling pp in order to standardize distributions across game modes.
            // The specific number has no intrinsic meaning and can be adjusted as needed.
            double multiplier = 0.8;

            if (scoreData.Mods.Any(m => m is ModNoFail))
                multiplier *= 0.9;
            if (scoreData.Mods.Any(m => m is ModEasy))
                multiplier *= 0.5;

            double difficultyValue = computeDifficultyValue(maniaAttributes);
            double accValue = computeAccuracyValue(difficultyValue, maniaAttributes);
            double totalValue =
                Math.Pow(
                    Math.Pow(difficultyValue, 1.1) +
                    Math.Pow(accValue, 1.1), 1.0 / 1.1
                ) * multiplier;

            return new ManiaPerformanceAttributes
            {
                Difficulty = difficultyValue,
                Accuracy = accValue,
                ScaledScore = scaledScore,
                Total = totalValue
            };
        }
        private double computeDifficultyValue(ManiaDifficultyAttributes attributes)
        {
            double difficultyValue = Math.Pow(5 * Math.Max(1, attributes.StarRating / 0.2) - 4.0, 2.2) / 135.0;

            difficultyValue *= 1.0 + 0.1 * Math.Min(1.0, totalHits / 1500.0);

            if (scaledScore <= 500000)
                difficultyValue = 0;
            else if (scaledScore <= 600000)
                difficultyValue *= (scaledScore - 500000) / 100000 * 0.3;
            else if (scaledScore <= 700000)
                difficultyValue *= 0.3 + (scaledScore - 600000) / 100000 * 0.25;
            else if (scaledScore <= 800000)
                difficultyValue *= 0.55 + (scaledScore - 700000) / 100000 * 0.20;
            else if (scaledScore <= 900000)
                difficultyValue *= 0.75 + (scaledScore - 800000) / 100000 * 0.15;
            else
                difficultyValue *= 0.90 + (scaledScore - 900000) / 100000 * 0.1;

            return difficultyValue;
        }

        private double computeAccuracyValue(double difficultyValue, ManiaDifficultyAttributes attributes)
        {
            if (attributes.GreatHitWindow <= 0)
                return 0;

            // Lots of arbitrary values from testing.
            // Considering to use derivation from perfect accuracy in a probabilistic manner - assume normal distribution
            double accuracyValue = Math.Max(0.0, 0.2 - (attributes.GreatHitWindow - 34) * 0.006667)
                                   * difficultyValue
                                   * Math.Pow(Math.Max(0.0, scaledScore - 960000) / 40000, 1.1);

            return accuracyValue;
        }

        private double totalHits => countPerfect + countOk + countGreat + countGood + countMeh + countMiss;
    }
}
