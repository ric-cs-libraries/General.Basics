using General.Basics.ErrorHandling;


namespace General.Basics.Extensions;

public static partial class IEnumerableExtension
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

    public static bool IsEmpty_<T>(this IEnumerable<T> enumerable) => (enumerable.Count() == 0);

    public static bool IsValidIndex_<T>(this IEnumerable<T> enumerable, int index)
    {
        int maxIndex = enumerable.Count() - 1;

        if (maxIndex < 0)
            return false;

        int minIndex = 0;
        var result = (index >= minIndex && index <= maxIndex);
        return result;
    }

    public static void CheckIsValidIndex_<T>(this IEnumerable<T> enumerable, int index)
    {
        if (!enumerable.IsValidIndex_(index))
        {
            var subject = "IEnumerable";
            int maxIndex = enumerable.Count() - 1;

            if (maxIndex < 0)
            {
                throw new UnexistingIndexBecauseEmptyException(index, subject);
            }

            int minIndex = 0;
            throw new OutOfRangeIntegerException(index, minIndex, maxIndex, $"{subject} Index");
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
        var result = (endIndex >= startIndex);
        result = result && enumerable.IsValidIndex_(startIndex) && enumerable.IsValidIndex_(endIndex);
        return result;
    }

    public static void CheckChunkExists_<T>(this IEnumerable<T> enumerable, int startIndex, int endIndex)
    {
        if (!enumerable.ChunkExists_(startIndex, endIndex))
        {
            var subject = "IEnumerable";
            if (enumerable.IsEmpty_())
            {
                throw new UnexistingChunkBecauseEmptyException(startIndex, endIndex, subject);
            }

            int minIndex = 0;
            int maxIndex = enumerable.Count() - 1;
            throw new UnexistingChunkException(startIndex, endIndex, minIndex, maxIndex, subject);
        }
    }

}
