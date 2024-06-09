using System.Diagnostics.CodeAnalysis; //ExcludeFromCodeCoverage


using General.Basics.Extensions;
using General.Basics.Generators.Interfaces;

namespace General.Basics.Generators.Extensions;

public static partial class IListExtension
{
    private const int DEFAULT_RANDOM_SHUFFLE_NB_SWAPS = 500;

    [ExcludeFromCodeCoverage]
    public static void RandomShuffle_<T>(this IList<T> list, int nbSwaps = DEFAULT_RANDOM_SHUFFLE_NB_SWAPS)
    {
        nbSwaps.CheckIsGreaterOrEqualTo_(0, nameof(nbSwaps));

        if (list.Any())
        {
            IIntsRandomizer randomizer = IEnumerableExtension.GetDefaultRandomizer(list)!;

            RandomIntsPairsGenerator intsPairsGenerator = RandomIntsPairsGenerator.Create(randomizer);
            IEnumerable<(int index, int otherIndex)> indexesToSwap = intsPairsGenerator.GetPairs(nbPairs: nbSwaps, distinctValue: true);
            list.Shuffle_(indexesToSwap);
        }
    }

}
