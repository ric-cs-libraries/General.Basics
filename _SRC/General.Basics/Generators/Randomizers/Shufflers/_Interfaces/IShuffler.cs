namespace General.Basics.Generators.Shufflers.Interfaces;

public interface IShuffler
{
    IEnumerable<(int Index, int OtherIndex)>? LastSwappedIndexes { get; }

    void Shuffle<T>(IList<T> list);

    IEnumerable<(int Index, int OtherIndex)> GetIndexesToSwap(int listLength);
}
