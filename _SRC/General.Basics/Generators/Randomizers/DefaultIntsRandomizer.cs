using System.Diagnostics.CodeAnalysis; //[ExcludeFromCodeCoverage]

using General.Basics.Generators.Interfaces;
using General.Basics.Generators.Abstracts;


namespace General.Basics.Generators;


[ExcludeFromCodeCoverage]
public record DefaultIntsRandomizer : IntsRandomizerBase, IIntsRandomizer
{
    private readonly Random randomizer;

    protected DefaultIntsRandomizer(int minValue, int maxValue) : base(minValue, maxValue)
    {
        randomizer = new Random();
    }

    public static DefaultIntsRandomizer Create(int minValue, int maxValue)
    {
        var result = new DefaultIntsRandomizer(minValue, maxValue);
        return result;
    }

    public int GetRandomValue()
    {
        int result = randomizer.Next(minValue, maxValue+1);   // +1 because the second param. of Next(), is an excluded bound !
        return result;
    }
}
