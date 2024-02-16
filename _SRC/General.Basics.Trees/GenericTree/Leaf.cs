using System.Diagnostics;

using General.Basics.Trees.GenericTree.Interfaces;


namespace General.Basics.Trees.GenericTree;


[DebuggerDisplay("Leaf: DataToString='{Data.ToString()}', Id={Id}, ParentId={ParentId}, IndexInParent={IndexInParent}")]
public class Leaf<TData> : TreeElement<TData>
{
    private Leaf() : base()
    {
    }

    public static Leaf<TData> Create()
    {
        var result = new Leaf<TData>();
        return result;
    }

    public override void AcceptVisitor(IVisitor<TData> visitor)
    {
        visitor.Visit(this);
    }
}
