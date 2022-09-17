// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

namespace Pettanko.Difficulty
{
    public class ManiaDifficultyAttributes : DifficultyAttributes
    {
        /// <summary>
        /// The hit window for a GREAT hit inclusive of rate-adjusting mods (DT/HT/etc).
        /// </summary>
        /// <remarks>
        /// Rate-adjusting mods do not affect the hit window at all in osu-stable.
        /// </remarks>
        public double GreatHitWindow { get; set; }

        /// <summary>
        /// This attribute is not part of the lazer performance calculators, but it is present in API attributes. Required if you want to calculate mania mods
        /// </summary>
        public double? ScoreMultipler { get; set; }
    }
}
