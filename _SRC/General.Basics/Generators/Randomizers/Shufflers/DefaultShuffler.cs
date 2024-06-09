
using General.Basics.Extensions;
using General.Basics.Others;

using General.Basics.Generators.TupleGenerators.Interfaces;
using General.Basics.Generators.Shufflers.Interfaces;
using General.Basics.Generators.TupleGenerators;


namespace General.Basics.Generators.Shufflers;

public class DefaultShuffler : IShuffler
{
    private readonly IIntsPairsGenerator intsPairsGenerator;
    public IEnumerable<(int Index, int OtherIndex)>? LastSwappedIndexes { get; private set; }

    public DefaultShuffler()
    {
        intsPairsGenerator = ComputedIntsPairsGenerator.Create(PairsLeftValueComputer, PairsRightValueComputer);
    }

    public void Shuffle<T>(IList<T> list)
    {
        if (list.Count > 1)
        {
            int listMaxIndex = list.GetLastIndex_()!.Value;
            IntsInterval leftValueAuthorizedInterval = new(minValue: 0, maxValue: listMaxIndex);
            IntsInterval rightValueAuthorizedInterval = leftValueAuthorizedInterval;
            var indexesToSwap = intsPairsGenerator.GetPairs(MaxNbSwaps, leftValueAuthorizedInterval, rightValueAuthorizedInterval, distinctValue: true);

            LastSwappedIndexes = indexesToSwap;
            list.Shuffle_(indexesToSwap);
        }
    }

    protected virtual int MaxNbSwaps => 500;

    protected virtual int PairsLeftValueComputer(int? previousLeftValue, int iterationNumber, int maxNbPairs, int maxLeftValue)
    {
        return maxLeftValue - iterationNumber;
    }

    protected virtual int PairsRightValueComputer(int? previousRightValue, int iterationNumber, int maxNbPairs, int maxRightValue, int currentLeftValue)
    {
        int delta1 = (maxRightValue + 1) / 4;
        int delta2 = delta1 / 2;

        int rightValue;
        if (!previousRightValue.HasValue)
        {
            rightValue = maxRightValue - delta2;
        }
        else
        {
            rightValue = previousRightValue.Value + ((iterationNumber.IsOdd_()) ? -delta1 : +delta2);
        }

        if (rightValue < 0)
        {
            rightValue = 0;
        }

        return rightValue;
    }

}
