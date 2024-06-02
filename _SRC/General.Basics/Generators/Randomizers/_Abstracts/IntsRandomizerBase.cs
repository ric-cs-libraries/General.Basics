using System.Diagnostics.CodeAnalysis; //[ExcludeFromCodeCoverage]

using General.Basics.Extensions;



namespace General.Basics.Generators.Abstracts;


[ExcludeFromCodeCoverage]
public abstract record IntsRandomizerBase
{
    protected readonly int minValue;
    protected readonly int maxValue;

    protected IntsRandomizerBase(int minValue, int maxValue)
    {
        maxValue.CheckIsGreaterOrEqualTo_(minValue, nameof(maxValue));

        this.minValue = minValue;
        this.maxValue = maxValue;
    }

}
