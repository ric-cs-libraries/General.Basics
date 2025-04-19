using System.Diagnostics.CodeAnalysis; //[ExcludeFromCodeCoverage]
using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using General.Basics.Generators.Interfaces;


namespace General.Basics.Generators.Extensions;

public static partial class IEnumerableExtension
{
    
    //returns null if enumerable is empty
    internal static IIntsRandomizer? GetDefaultRandomizer<T>(IEnumerable<T> enumerable)
    {
        IIntsRandomizer? randomizer = (enumerable.Any())? DefaultIntsRandomizer.Create(minValue: 0, maxValue: enumerable.GetLastIndex_()!.Value) : null;
        return randomizer;
    }

    /// <exception cref="CannotSearchElementBecauseEmptyException"></exception>
    [ExcludeFromCodeCoverage]
    public static T GetRandomElement_<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable.Any())
        {
            IIntsRandomizer randomizer = GetDefaultRandomizer(enumerable)!;
            int randomIndex = randomizer.GetRandomValue();
            return enumerable.ElementAt(randomIndex);

        }
        throw new CannotSearchElementBecauseEmptyException(nameof(enumerable));
    }
}
