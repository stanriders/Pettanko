using Pettanko.Difficulty;
using Pettanko.Mods;
using Pettanko.Performance;
using Xunit;

namespace Pettanko.Tests
{
    public class CalculatorTests
    {
        // Mapset used for testing: https://osu.ppy.sh/beatmapsets/367631

        [Fact]
        public void TestOsuCalculator()
        {
            var result = Pettanko.Calculate(
                new OsuDifficultyAttributes
                {
                    StarRating = 8.826809883117676,
                    AimDifficulty = 3.8473799228668213,
                    SpeedDifficulty = 4.578939914703369,
                    FlashlightDifficulty = 5.34306001663208,
                    SliderFactor = 0.9949229955673218,
                    ApproachRate = 10.0,
                    OverallDifficulty = 10.0,
                    MaxCombo = 6892,
                    HitCircleCount = 5202,
                    SliderCount = 557,
                    SpinnerCount = 3
                }, 
                new Score
                {
                    RulesetId = 0,
                    Accuracy = 1.0,
                    MaxCombo = 6892,
                    Statistics = new Statistics
                    {
                        Count300 = 5762
                    }
                });

            var osuPerfAttributes = result as OsuPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(1246.7257065148297, osuPerfAttributes.Total);
            Assert.Equal(357.49683420670175, osuPerfAttributes.Aim);
            Assert.Equal(644.4564298221886, osuPerfAttributes.Speed);
            Assert.Equal(216.5583315748771, osuPerfAttributes.Accuracy);
            Assert.Equal(0.0000, osuPerfAttributes.Flashlight);
            Assert.Equal(0.0000, osuPerfAttributes.EffectiveMissCount);
        }

        [Fact]
        public void TestOsuCalculatorWithMods()
        {
            var result = Pettanko.Calculate(
                new OsuDifficultyAttributes
                {
                    StarRating = 9.017889976501465,
                    AimDifficulty = 3.0377299785614014,
                    SpeedDifficulty = 3.2300000190734863,
                    FlashlightDifficulty = 4.070400238037109,
                    SliderFactor = 0.9952549934387207,
                    ApproachRate = 9,
                    OverallDifficulty = 8.888890266418457,
                    MaxCombo = 6892,
                    HitCircleCount = 5202,
                    SliderCount = 557,
                    SpinnerCount = 3
                },
                new Score
                {
                    RulesetId = 0,
                    Accuracy = 0.9853349531412704,
                    MaxCombo = 5727,
                    Mods = new Mod[] { new ModFlashlight(), new ModHalfTime() },
                    Statistics = new Statistics
                    {
                        Count300 = 5639,
                        Count100 = 120,
                        Count50 = 3,
                        CountMiss = 2
                    }
                });

            var osuPerfAttributes = result as OsuPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(753.9625448969466, osuPerfAttributes.Total);
            Assert.Equal(140.94425606198382, osuPerfAttributes.Aim);
            Assert.Equal(173.5510098990755, osuPerfAttributes.Speed); 
            Assert.Equal(93.5211370839687, osuPerfAttributes.Accuracy);
            Assert.Equal(346.58137474440906, osuPerfAttributes.Flashlight);
            Assert.Equal(2.0000, osuPerfAttributes.EffectiveMissCount);
        }

        [Fact]
        public void TestTaikoCalculator()
        {
            var result = Pettanko.Calculate(
                new TaikoDifficultyAttributes
                {
                    StarRating = 7.979609966278076,
                    MaxCombo = 6546,
                    GreatHitWindow = 27.0
                },
                new Score
                {
                    RulesetId = 1,
                    Accuracy = 1.0,
                    MaxCombo = 6546,
                    Statistics = new Statistics
                    {
                        Count300 = 6546
                    }
                });

            var osuPerfAttributes = result as TaikoPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(495.6258202909107, osuPerfAttributes.Total);
            Assert.Equal(310.8280065944978, osuPerfAttributes.Difficulty);
            Assert.Equal(166.8479079539143, osuPerfAttributes.Accuracy);
        }

        [Fact]
        public void TestCatchCalculator()
        {
            var result = Pettanko.Calculate(
                new CatchDifficultyAttributes
                {
                    StarRating = 8.226490020751953,
                    ApproachRate = 9.800000190734863, // uhhhh thats what API gave me, probably a rounding error
                    MaxCombo = 7179
                },
                new Score
                {
                    RulesetId = 2,
                    Accuracy = 1.0,
                    MaxCombo = 7179,
                    Statistics = new Statistics
                    {
                        Count300 = 6919,
                        Count100 = 260
                    }
                });

            var osuPerfAttributes = result as CatchPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(1115.8276057869323, osuPerfAttributes.Total);
        }

        [Fact]
        public void TestManiaCalculator()
        {
            var result = Pettanko.Calculate(
                new ManiaDifficultyAttributes
                {
                    StarRating = 6.04725980758667,
                    ScoreMultipler = 1.0,
                    GreatHitWindow = 37
                },
                new Score
                {
                    RulesetId = 3,
                    Accuracy = 1.0,
                    MaxCombo = 11787,
                    TotalScore = 1000000,
                    Statistics = new Statistics
                    {
                        Count300 = 11457
                    }
                });

            var osuPerfAttributes = result as ManiaPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(435.6788348929514, osuPerfAttributes.Total);
            Assert.Equal(479.00051442681973, osuPerfAttributes.Difficulty);
            Assert.Equal(86.21961359631314, osuPerfAttributes.Accuracy);
            Assert.Equal(1000000, osuPerfAttributes.ScaledScore);
        }

        [Fact]
        public void TestManiaCalculatorWithMods()
        {
            var result = Pettanko.Calculate(
                new ManiaDifficultyAttributes
                {
                    StarRating = 4.725860118865967,
                    ScoreMultipler = 0.25,
                    GreatHitWindow = 51
                },
                new Score
                {
                    RulesetId = 3,
                    Accuracy = 1.0,
                    MaxCombo = 11787,
                    TotalScore = 1000000,
                    Mods = new Mod[] { new ModHalfTime(), new ModEasy() },
                    Statistics = new Statistics
                    {
                        Count300 = 11457
                    }
                });

            var osuPerfAttributes = result as ManiaPerformanceAttributes;

            Assert.NotNull(osuPerfAttributes);
            Assert.Equal(116.26749349526716, osuPerfAttributes.Total);
            Assert.Equal(273.82724724712375, osuPerfAttributes.Difficulty);
            Assert.Equal(23.730143073682992, osuPerfAttributes.Accuracy);
            Assert.Equal(1000000, osuPerfAttributes.ScaledScore);
        }
    }
}
