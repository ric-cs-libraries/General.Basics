﻿namespace Transverse._Common.General.Trees.GenericTree;


public class GenericTree<TData> : Node<TData>
{
    public override TreeElement<TData>? Parent 
    {
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
