using System.Diagnostics.CodeAnalysis; //[ExcludeFromCodeCoverage]

using General.Basics.Extensions;
using General.Basics.Generators.Interfaces;


namespace General.Basics.Generators;

[ExcludeFromCodeCoverage]
public record IntsTuplesGenerator
{
    private readonly IIntsRandomizer randomizer;

    private IntsTuplesGenerator(IIntsRandomizer randomizer)
    {
        this.randomizer = randomizer;
    }

    public static IntsTuplesGenerator Create(IIntsRandomizer randomizer)
    {
        var result = new IntsTuplesGenerator(randomizer);
        return result;
    }

    //distinctValue: true if we don't want any pair to contain twice the same value : (value1, value1).
    public IEnumerable<(int, int)> GetRandomPairs(int nbPairs, bool distinctValue = false)
    {
        nbPairs.CheckIsGreaterOrEqualTo_(1, nameof(nbPairs));

        var result = Enumerable.Range(1, nbPairs).Select(n => GetRandomPair(distinctValue));
        return result;
    }

    public (int, int) GetRandomPair(bool distinctValue = false)
    {
        int randomValue1, randomValue2;
        do {
            randomValue1 = randomizer.GetRandomValue();
            randomValue2 = randomizer.GetRandomValue();
        } while (distinctValue && (randomValue1 == randomValue2));

        return (randomValue1, randomValue2);
    }
}
