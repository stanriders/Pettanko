// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

namespace Pettanko.Difficulty
{
    public class DifficultyAttributes
    {
        /// <summary>
        /// The combined star rating of all skills.
        /// </summary>
        public double StarRating { get; set; }

        /// <summary>
        /// The maximum achievable combo.
        /// </summary>
        public int MaxCombo { get; set; }
    }
}
