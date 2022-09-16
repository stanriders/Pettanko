
using System;

namespace Pettanko
{
    public class Score
    {
        public int RulesetId { get; set; }

        public long TotalScore { get; set; }

        public int MaxCombo { get; set; }

        public double Accuracy { get; set; }

        public Statistics Statistics { get; set; }

        public Mod[] Mods { get; set; } = Array.Empty<Mod>();
    }

    public class Statistics
    {
        public int Count300 { get; set; }
        public int CountGeki { get; set; }
        public int Count100 { get; set; }
        public int CountKatu { get; set; }
        public int Count50 { get; set; }
        public int CountMiss { get; set; }
    }
}
