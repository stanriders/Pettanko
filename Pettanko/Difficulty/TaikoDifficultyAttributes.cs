// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

namespace Pettanko.Difficulty
{
    public class TaikoDifficultyAttributes : DifficultyAttributes
    {
        /// <summary>
        /// The difficulty corresponding to the stamina skill.
        /// </summary>
        public double StaminaDifficulty { get; set; }

        /// <summary>
        /// The difficulty corresponding to the rhythm skill.
        /// </summary>
        public double RhythmDifficulty { get; set; }

        /// <summary>
        /// The difficulty corresponding to the colour skill.
        /// </summary>
        public double ColourDifficulty { get; set; }

        /// <summary>
        /// The difficulty corresponding to the hardest parts of the map.
        /// </summary>
        public double PeakDifficulty { get; set; }

        /// <summary>
        /// The perceived hit window for a GREAT hit inclusive of rate-adjusting mods (DT/HT/etc).
        /// </summary>
        /// <remarks>
        /// Rate-adjusting mods don't directly affect the hit window, but have a perceived effect as a result of adjusting audio timing.
        /// </remarks>
        public double GreatHitWindow { get; set; }

        /// <summary>
        /// The perceived hit window for an OK hit inclusive of rate-adjusting mods (DT/HT/etc).
        /// </summary>
        /// <remarks>
        /// Rate-adjusting mods don't directly affect the hit window, but have a perceived effect as a result of adjusting audio timing.
        /// </remarks>
        public double OkHitWindow { get; set; }
    }
}
