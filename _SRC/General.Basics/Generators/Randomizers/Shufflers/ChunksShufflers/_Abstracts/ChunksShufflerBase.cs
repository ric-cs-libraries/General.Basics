using System.Diagnostics; //Debug.Assert

using General.Basics.Extensions;
using General.Basics.Generators.Shufflers.Interfaces;

namespace General.Basics.Generators.Shufflers.ChunksShufflers.Abstracts;

public abstract class ChunksShufflerBase<TItems>
{
    protected abstract int TotalNbItemsPer_NbItemsPerChunk();

    private int IdealNbItemsPerChunk => (TotalNbItemsPer_NbItemsPerChunk() == 0) ? 1 : Math.Max(nbItems / TotalNbItemsPer_NbItemsPerChunk(), 1);
    private int IdealNbChunks => nbItems / IdealNbItemsPerChunk;
    private int RealNbChunks => IdealNbChunks + ((LastChunkNbItems > 0) ? 1 : 0);
    private int LastChunkNbItems => nbItems - IdealNbChunks * IdealNbItemsPerChunk;
    private int? GetLastChunkIndex()
    {
        int lastChunkNbItems = LastChunkNbItems;
        if (lastChunkNbItems == 0)
            return null;

        var realNbChunks = RealNbChunks;
        var result = realNbChunks - 1;
        return result;
    }

    private readonly IShuffler shuffler;
    private readonly List<TItems> itemsToShuffleOrUnshuffle;
    private readonly int nbItems;

    public ChunksShufflerBase(IShuffler shuffler, IEnumerable<TItems> itemsToShuffleOrUnshuffle)
    {
        Debug.Assert(TotalNbItemsPer_NbItemsPerChunk() > 0, "Please, TotalNbItemsPer_NbItemsPerChunk must be >0");

        this.shuffler = shuffler;
        this.itemsToShuffleOrUnshuffle = itemsToShuffleOrUnshuffle.ToList();
        nbItems = this.itemsToShuffleOrUnshuffle.Count;

    }

    public List<IEnumerable<TItems>> ToShuffledChunks()
    {
        int idealNbItemsPerChunk = IdealNbItemsPerChunk;
        List<IEnumerable<TItems>> chunks = itemsToShuffleOrUnshuffle.ToChunks_(idealNbItemsPerChunk);

        shuffler.Shuffle(chunks);

        return chunks;
    }

    public List<IEnumerable<TItems>> ToUnshuffledChunks()
    {
        int nbChunks = RealNbChunks;
        IEnumerable<(int, int)> indexesToUnSwap = shuffler.GetIndexesToSwap(nbChunks);

        //---- Recreation of the shuffled chunks list, to unshuffle ----
        (IEnumerable<int> RegularChunksIndexes_BeforeChunkThatWasLast, int? chunkThatWasLast_ActualIndex, IEnumerable<int> RegularChunksIndexes_AfterChunkThatWasLast) chunksIndexes =
            GetChunksIndexes(indexesToUnSwap);

        int idealNbItemsPerChunk = IdealNbItemsPerChunk;
        int chunkThatWasLastNbItems = LastChunkNbItems;
        List<IEnumerable<TItems>> shuffledChunks = new();

        shuffledChunks.AddRange(
                                chunksIndexes.RegularChunksIndexes_BeforeChunkThatWasLast
                               .Select(index => itemsToShuffleOrUnshuffle.GetRange(index * idealNbItemsPerChunk, idealNbItemsPerChunk))
                               );
        if (chunksIndexes.chunkThatWasLast_ActualIndex is not null)
        {
            shuffledChunks.Add(
                                itemsToShuffleOrUnshuffle.GetRange(chunksIndexes.chunkThatWasLast_ActualIndex!.Value * idealNbItemsPerChunk, chunkThatWasLastNbItems)
                              );
        }
        shuffledChunks.AddRange(
                                chunksIndexes.RegularChunksIndexes_AfterChunkThatWasLast
                                .Select(index => itemsToShuffleOrUnshuffle.GetRange(chunkThatWasLastNbItems + (index - 1) * idealNbItemsPerChunk, idealNbItemsPerChunk))
                               );

        //Unshuffle
        shuffledChunks.ReverseShuffle_(indexesToUnSwap);

        var unshuffledChunks = shuffledChunks;
        return unshuffledChunks;
    }



    //
    private (IEnumerable<int> RegularChunksIndexes_BeforeChunkThatWasLast, int? chunkThatWasLast_ActualIndex, IEnumerable<int> RegularChunksIndexes_AfterChunkThatWasLast)
    GetChunksIndexes(IEnumerable<(int, int)> indexesToUnSwap)
    {
        (IEnumerable<int> RegularChunksIndexes_BeforeChunkThatWasLast, int? chunkThatWasLast_ActualIndex, IEnumerable<int> RegularChunksIndexes_AfterChunkThatWasLast)
            chunksIndexes = (null!, null, null!);

        int? chunkThatWasLast_ActualIndex = GetChunkThatWasLast_ActualIndex(indexesToUnSwap);
        int realNbChunks = RealNbChunks;
        if (chunkThatWasLast_ActualIndex.HasValue)
        {
            //- index of Chunks that are before chunkThatWasLastActualIndex -
            chunksIndexes.RegularChunksIndexes_BeforeChunkThatWasLast = Enumerable.Range(0, chunkThatWasLast_ActualIndex.Value);
            //- index of Chunks that are after chunkThatWasLastActualIndex -
            chunksIndexes.RegularChunksIndexes_AfterChunkThatWasLast = Enumerable.Range(chunkThatWasLast_ActualIndex.Value + 1, realNbChunks - (chunkThatWasLast_ActualIndex.Value + 1));

        }
        else
        {
            chunksIndexes.RegularChunksIndexes_BeforeChunkThatWasLast = Enumerable.Range(0, realNbChunks); //ALL chunks have the same size
            chunksIndexes.RegularChunksIndexes_AfterChunkThatWasLast = Enumerable.Empty<int>();
        }

        chunksIndexes.chunkThatWasLast_ActualIndex = chunkThatWasLast_ActualIndex;

        return chunksIndexes;
    }

    private int? GetChunkThatWasLast_ActualIndex(IEnumerable<(int, int)> indexesToUnSwap)
    {
        int? initialLastChunkIndex = GetLastChunkIndex(); //Index before shuffle
        if (initialLastChunkIndex.HasValue)
        {
            int chunkThatWasLast_ActualIndex = initialLastChunkIndex.Value;
            foreach ((int Index, int OtherIndex) swap in indexesToUnSwap)
            {
                if (swap.Index == chunkThatWasLast_ActualIndex)
                {
                    chunkThatWasLast_ActualIndex = swap.OtherIndex; //chunkThatWasLast was moved to index : swap.OtherIndex
                }
                else if (swap.OtherIndex == chunkThatWasLast_ActualIndex)
                {
                    chunkThatWasLast_ActualIndex = swap.Index; //chunkThatWasLast was moved to index : swap.Index
                }
            }
            return chunkThatWasLast_ActualIndex; //Index(after shuffle) of what was the last chunk.
        }
        return null;
    }
}

