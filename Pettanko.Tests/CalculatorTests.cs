using Pettanko.Difficulty;
using Pettanko.Performance;
using Xunit;

namespace Pettanko.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void TestOsuCalculator()
        {
            var result = Pettanko.Calculate(
                new OsuDifficultyAttributes
                {
                    AimDifficulty = 2.9864489908605893,
                    SpeedDifficulty = 3.1111283473891254,
                    FlashlightDifficulty = 3.54341969421949,
                    SliderFactor = 0.99796469139376365,
                    ApproachRate = 9.50,
                    OverallDifficulty = 9.00,
                    MaxCombo = 3220,
                    HitCircleCount = 1534,
                    SliderCount = 587,
                    SpinnerCount = 5
                }, 
                new Score
                {
                    RulesetId = 0,
                    Accuracy = 1.0,
                    MaxCombo = 3220,
                    Statistics = new Statistics
                    {
                        Count300 = 2126
                    }
                });

            var osuPerfAttributes = result as OsuPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(455.90599187959032, osuPerfAttributes.Total);
            Assert.Equal(141.45654132294621, osuPerfAttributes.Aim);
            Assert.Equal(167.49667236961548, osuPerfAttributes.Speed);
            Assert.Equal(140.70696717850745, osuPerfAttributes.Accuracy);
            Assert.Equal(0.0000, osuPerfAttributes.Flashlight);
            Assert.Equal(0.0000, osuPerfAttributes.EffectiveMissCount);
        }
    }
}
