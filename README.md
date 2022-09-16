# Pettanko - stripped down version of osu! performance calculators.

Pettanko is because you can't name something related to pp without using some kind of japanese boobs word. And also because it's *slim*.

## How to use

1) Add it to your project
2) Get beatmap difficulty attributes [from osu! API v2](https://osu.ppy.sh/docs/index.html#get-beatmap-attributes)
3) Call `Pettanko.Calculate(DifficultyAttributes difficultyAttributes, Score scoreData)`

Calculators don't need anything other than difficulty attributes and score data - no beatmaps, slow parsing or anything else.