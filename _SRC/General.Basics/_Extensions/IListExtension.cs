


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

    public static void Shuffle_<T>(this IList<T> list, IEnumerable<(int index, int otherIndex)> indexesToSwap)
    {
        if (list.Any())
        {
            foreach ((int index, int otherIndex) in indexesToSwap)
            {
                list.Swap_(index, otherIndex);
            }
        }
    }

    public static void ReverseShuffle_<T>(this IList<T> list, IEnumerable<(int index, int otherIndex)> indexesToSwap)
    {
        if (list.Any())
        {
            IEnumerable<(int index, int otherIndex)> reversedIndexesToSwap = indexesToSwap.Reverse(); //REM.: indexesToSwap.Reverse() doesn't modify indexesToSwap.
            list.Shuffle_(reversedIndexesToSwap); 
        }
    }

}