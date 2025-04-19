using System.Diagnostics.CodeAnalysis; //[ExcludeFromCodeCoverage]

using General.Basics.Extensions;
using General.Basics.Generators.Interfaces;


namespace General.Basics.Generators;

[ExcludeFromCodeCoverage]
public record RandomIntsPairsGenerator
{
    private readonly IIntsRandomizer randomizer;

    private RandomIntsPairsGenerator(IIntsRandomizer randomizer)
    {
        this.randomizer = randomizer;
    }

    public static RandomIntsPairsGenerator Create(IIntsRandomizer randomizer)
    {
        var result = new RandomIntsPairsGenerator(randomizer);
        return result;
    }

    /// <exception cref="IntShouldBeGreaterOrEqualException"></exception>
    //distinctValue: true if we don't want any pair to contain twice the same value : (value1, value1).
    public IEnumerable<(int, int)> GetPairs(int nbPairs, bool distinctValue = false)
    {
        nbPairs.CheckIsGreaterOrEqualTo_(1, nameof(nbPairs));

        var result = Enumerable.Range(1, nbPairs).Select(n => GetPair(distinctValue));
        return result;
    }

    private (int, int) GetPair(bool distinctValue = false)
    {
        int randomValue1, randomValue2;
        do {
            randomValue1 = randomizer.GetRandomValue();
            randomValue2 = randomizer.GetRandomValue();
        } while (distinctValue && (randomValue1 == randomValue2));

        return (randomValue1, randomValue2);
    }
}
