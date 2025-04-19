using General.Basics.Trees.GenericTree.Abstracts;


namespace General.Basics.Trees.GenericTree;


public class GenericTree<TData> : Node<TData>
{
    public override TreeElement<TData>? Parent 
    {
        /// <exception cref="GenericTreeCantHaveAParentException"></exception>
        protected internal set 
        {
            throw new GenericTreeCantHaveAParentException(); //Viole LSP mais pas grave
        }
    }
    public override int? IndexInParent
    {
        /// <exception cref="GenericTreeCantHaveAParentException"></exception>
        protected internal set
        {
            throw new GenericTreeCantHaveAParentException(); //Viole LSP mais pas grave
        }
    }

    private GenericTree() : base()
	{

	}

    public new static GenericTree<TData> Create()
    {
        var result = new GenericTree<TData>();
        return result;
    }
}
