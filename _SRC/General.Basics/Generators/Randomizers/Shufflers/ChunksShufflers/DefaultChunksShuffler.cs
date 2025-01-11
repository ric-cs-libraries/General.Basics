
using General.Basics.Generators.Shufflers.Interfaces;
using General.Basics.Generators.Shufflers.ChunksShufflers.Abstracts;


namespace General.Basics.Generators.Shufflers.ChunksShufflers;

public class DefaultChunksShuffler<TItems> : ChunksShufflerBase<TItems>
{
    public DefaultChunksShuffler(IShuffler shuffler, IEnumerable<TItems> itemsToShuffleOrUnshuffle) : base(shuffler, itemsToShuffleOrUnshuffle)
    {
    }

    protected override int TotalNbItemsPer_NbItemsPerChunk() => 1000/3;  //Kind of Shuffle Granularity : here for a total of 1000 items, we ideally want chunks to contain 3 items.
}

