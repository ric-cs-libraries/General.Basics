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
            var subject = enumerable.GetType().Name;
            int maxIndex = enumerable.Count() - 1;

            if (maxIndex < 0)
            {
                throw new UnexistingIndexBecauseEmptyException(index, subject);
            }

            int minIndex = 0;
            throw new OutOfRangeIntegerException(index, minIndex, maxIndex, $"{subject} Index");
        }
    }

    public static void CheckAreValidIndexes_<T>(this IEnumerable<T> enumerable, IEnumerable<int> indexes)
    {
        foreach (var index in indexes)
        {
            enumerable.CheckIsValidIndex_(index);
        }
    }

    public static IEnumerable<T> GetChunk_<T>(this IEnumerable<T> enumerable, int startIndex, int endIndex)
    {
        enumerable.CheckChunkExists_(startIndex, endIndex);
        var chunkLength = endIndex - startIndex + 1;
        var result = enumerable.Skip(startIndex).Take(chunkLength);
        return result;
    }

    public static IEnumerable<T> GetChunk_<T>(this IEnumerable<T> enumerable, int startIndex)
    {
        if (enumerable.IsEmpty_())
        {
            var subject = enumerable.GetType().Name;
            throw new UnexistingChunkBecauseEmptyException(startIndex, endIndex: null, subject);
        }

        int? endIndex = enumerable.GetLastIndex_();
        var result = enumerable.GetChunk_(startIndex, endIndex!.Value);
        return result;
    }

    public static IEnumerable<T> GetChunkFromEnd_<T>(this IEnumerable<T> enumerable, int chunkLength)
    {
        //if (enumerable.IsEmpty_())
        //{
        //    var subject = enumerable.GetType().Name;
        //    throw new UnexistingChunkBecauseEmptyException(startIndex: null, endIndex: null, subject);
        //}

        if (chunkLength < 0)
        {
            throw new MustBePositiveIntegerException(chunkLength, "Chunk length");
        }

        if (chunkLength == 0 || enumerable.IsEmpty_())
            return Enumerable.Empty<T>();

        int startIndex = enumerable.GetLastIndex_()!.Value - (chunkLength - 1);
        var result = enumerable.GetChunk_(startIndex);
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
            var subject = enumerable.GetType().Name;
            if (enumerable.IsEmpty_())
            {
                throw new UnexistingChunkBecauseEmptyException(startIndex, endIndex, subject);
            }

            int minIndex = 0;
            int maxIndex = enumerable.Count() - 1;
            throw new UnexistingChunkException(startIndex, endIndex, minIndex, maxIndex, subject);
        }
    }

    public static List<IEnumerable<T>> ToChunks_<T>(this IEnumerable<T> enumerable, int idealNbElementsInAChunk)
    {
        List<IEnumerable<T>> result = new();

        idealNbElementsInAChunk.CheckIsGreaterOrEqualTo_(1, nameof(idealNbElementsInAChunk));

        int nbElements = enumerable.Count();
        if (nbElements > 0)
        {
            IEnumerable<T> chunk;
            int startIndex = 0;
            while (startIndex + idealNbElementsInAChunk <= nbElements)
            {
                chunk = enumerable.Skip(startIndex).Take(idealNbElementsInAChunk);
                result.Add(chunk);

                startIndex += idealNbElementsInAChunk;
            }

            if (startIndex != nbElements)
            {
                chunk = enumerable.Skip(startIndex); //Last remaining chunk
                result.Add(chunk);
            }
        }

        return result;
    }

    public static bool ContainsSameElementsAs_<T>(this IEnumerable<T> enumerable, IEnumerable<T> elements, IEqualityComparer<T>? comparer = null)
    {
        //?? VERIFIER si la méthode marche avec le : comparer !!?
        var enumerableAsHashSet = enumerable.ToHashSet<T>(comparer); //Sans doublons éventuels
        var elementsAsHashSet = elements.ToHashSet<T>(comparer); //Sans doublons éventuels

        var response = (enumerableAsHashSet.Count() == elementsAsHashSet.Count());
        if (response)
        {
            enumerableAsHashSet.SymmetricExceptWith(elementsAsHashSet); //vire les éléments communs, et ajoute ceux en plus de : elements.
            response = !enumerableAsHashSet.Any();
        }
        return response;
    }

    public static bool ContainsAll_<T>(this IEnumerable<T> enumerable, IEnumerable<T> elements, IEqualityComparer<T>? comparer = null)
    {
        var response = elements.All(element => enumerable.Contains(element, comparer));
        return response;
    }

    public static IList<T> GetExceedingElementsFrom_<T>(this IEnumerable<T> enumerable, IEnumerable<T> elements, IEqualityComparer<T>? comparer = null)
    {
        List<T> exceedingElements = new();
        foreach (var element in elements)
        {
            if (!enumerable.Contains(element, comparer))
            {
                exceedingElements.Add(element);
            }
        }
        return exceedingElements;
    }

    public static IEnumerable<T> RotateLeft_<T>(this IEnumerable<T> enumerable, int nbRotations)
    {
        if (nbRotations < 0)
        {
            throw new MustBePositiveIntegerException(nbRotations, nameof(nbRotations));
        }
        nbRotations %= enumerable.Count();

        return enumerable.Skip(nbRotations).Concat(enumerable.Take(nbRotations));
    }

    public static async Task<HashSet<T>> IntersectWithAsync_<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
    {
        HashSet<T> enumerable_ = enumerable.ToHashSet();

        enumerable_.IntersectWith(otherEnumerable); //Modifies enumerable_ HashSet.

        return await Task.FromResult(enumerable_);
    }
}
