using Pettanko.Difficulty;
using Pettanko.Performance;

namespace Pettanko.Calculators
{
    public interface IPerformanceCalculator
    {
        PerformanceAttributes Calculate(DifficultyAttributes difficultyAttributes, Score scoreData);
    }
}
