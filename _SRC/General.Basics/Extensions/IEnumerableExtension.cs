using System;
using System.Reflection;
using General.Basics.Exceptions;

namespace General.Basics.Extensions;

public static class IEnumerableExtension
{
    public static int? GetLastIndex_<T>(this IEnumerable<T> enumerable)
    {
        int? result = null;
        if (enumerable.Any())
        {
            result = enumerable.Count() - 1;
        }
        return result;
    }

        public static bool IsValidIndex_<T>(this IEnumerable<T> enumerable, int index)
    {
        int minIndex = 0;
        int maxIndex = enumerable.Count() - 1;
        var result = (index >= minIndex && index <= maxIndex);
        return result;
    }

    public static void CheckIsValidIndex_<T>(this IEnumerable<T> enumerable, int index)
    {
        if (!enumerable.IsValidIndex_(index))
        {
            int minIndex = 0;
            int maxIndex = enumerable.Count() - 1;

            throw new OutOfRangeIntegerException(index, minIndex, maxIndex, "IEnumerable Index");
        }
    }

    public static IEnumerable<T> GetChunk_<T>(this IEnumerable<T> enumerable, int startIndex, int endIndex)
    {
        enumerable.CheckChunkExists_(startIndex, endIndex);
        var chunkLength = endIndex - startIndex + 1;
        var result = enumerable.Skip(startIndex).Take(chunkLength);
        return result;
    }

    public static bool ChunkExists_<T>(this IEnumerable<T> enumerable, int startIndex, int endIndex)
    {
        var result = (endIndex >= startIndex );
        result = result && enumerable.IsValidIndex_(startIndex) && enumerable.IsValidIndex_(endIndex);
        return result;
    }

    public static void CheckChunkExists_<T>(this IEnumerable<T> enumerable, int startIndex, int endIndex)
    {
        if (!enumerable.ChunkExists_(startIndex, endIndex))
        {
            int minIndex = 0;
            int maxIndex = enumerable.Count() - 1;

            throw new UnexistingChunkException(startIndex, endIndex, minIndex, maxIndex, "IEnumerable");
        }
    }
}
