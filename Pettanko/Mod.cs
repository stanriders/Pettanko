
using Pettanko.Mods;
using Pettanko.Mods.Osu;

namespace Pettanko
{
    public abstract class Mod
    {
        public abstract string Acronym { get; }

        public static Mod[] AllMods => new Mod[]
        {
            new ModDoubleTime(),
            new ModEasy(),
            new ModFlashlight(),
            new ModHalfTime(),
            new ModHardRock(),
            new ModHidden(),
            new ModNoFail(),
            new ModPerfect(),
            new ModRelax(),
            new OsuModBlinds(),
            new OsuModSpunOut(),
            new OsuModTouchDevice()
        };
    }
}
