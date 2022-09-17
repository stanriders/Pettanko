// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

namespace Pettanko.Difficulty
{
    public class CatchDifficultyAttributes : DifficultyAttributes
    {
        /// <summary>
        /// The perceived approach rate inclusive of rate-adjusting mods (DT/HT/etc).
        /// </summary>
        /// <remarks>
        /// Rate-adjusting mods don't directly affect the approach rate difficulty value, but have a perceived effect as a result of adjusting audio timing.
        /// </remarks>
        public double ApproachRate { get; set; }
    }
}
