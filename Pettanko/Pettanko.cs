using System;
using Pettanko.Calculators;
using Pettanko.Difficulty;
using Pettanko.Performance;

namespace Pettanko
{
    public static class Pettanko
    {
        public static PerformanceAttributes Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)
        {
            switch (scoreData.RulesetId)
            {
                case 0:
                    return new OsuPerformanceCalculator().Calculate(difficultyAttributes, scoreData);
                case 1:
                    return new TaikoPerformanceCalculator().Calculate(difficultyAttributes, scoreData);
                case 2:
                    return new CatchPerformanceCalculator().Calculate(difficultyAttributes, scoreData);
                case 3:
                    return new ManiaPerformanceCalculator().Calculate(difficultyAttributes, scoreData);
                default:
                    throw new ArgumentException("Ruleset is not supported");
            }
        }
    }
}
