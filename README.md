# Pettanko - stripped down version of osu! performance calculators.

It's Pettanko because you can't name something related to pp calculation without using some kind of a japanese boobs word.  
And also because it's *slim*.

## How to use

1) Add it to your project using [Nuget](https://www.nuget.org/packages/StanR.Pettanko)
2) Get beatmap difficulty attributes [from osu! API v2](https://osu.ppy.sh/docs/index.html#get-beatmap-attributes)
3) Call `Pettanko.Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)`

Calculators don't need anything other than difficulty attributes and score data. No beatmap means no slow parsing.