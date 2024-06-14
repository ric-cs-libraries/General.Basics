


namespace General.Basics.Extensions;

public static partial class IListExtension
{
    public static void Swap_<T>(this IList<T> list, int index, int otherIndex)
    {
        list.CheckIsValidIndex_(index);
        list.CheckIsValidIndex_(otherIndex);

        if (index != otherIndex)
        {
            T initialValAtIndex = list.ElementAt(index);
            T initialValAtOtherIndex = list.ElementAt(otherIndex);

            list.RemoveAt(index);
            list.Insert(index, initialValAtOtherIndex);

            list.RemoveAt(otherIndex);
            list.Insert(otherIndex, initialValAtIndex);

        }

    }

    public static void Shuffle_<T>(this IList<T> list, IEnumerable<(int Index, int OtherIndex)> indexesToSwap)
    {
        if (list.Any())
        {
            foreach ((int index, int otherIndex) in indexesToSwap)
            {
                list.Swap_(index, otherIndex);
            }
        }
    }

    public static void ReverseShuffle_<T>(this IList<T> list, IEnumerable<(int Index, int OtherIndex)> indexesToSwap)
    {
        if (list.Any())
        {
            IEnumerable<(int Index, int OtherIndex)> reversedIndexesToSwap = indexesToSwap.Reverse(); //REM.: indexesToSwap.Reverse() doesn't modify indexesToSwap.
            list.Shuffle_(reversedIndexesToSwap);
        }
    }

    public static void InsertAt_<T>(this IList<T> list, IEnumerable<(int Index, T Value)> elementsToInsert)
    {
        elementsToInsert = elementsToInsert.OrderBy(e => e.Index);

        foreach (var elementToInsert in elementsToInsert)
        {
            list.Insert(elementToInsert.Index, elementToInsert.Value);
        }
    }

    public static void RemoveAt_<T>(this IList<T> list, IEnumerable<int> elementsToRemoveIndex)
    {
        list.CheckAreValidIndexes_(elementsToRemoveIndex);

        var elementsToRemoveIndex_ = elementsToRemoveIndex.OrderByDescending(e => e); //Will start by the highest index of elementsToRemoveIndex.

        foreach (var elementToRemoveIndex in elementsToRemoveIndex_)
        {
            list.RemoveAt(elementToRemoveIndex);
        }
    }
}
