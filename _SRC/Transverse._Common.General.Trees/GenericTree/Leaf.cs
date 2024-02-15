using System.Diagnostics;
using Transverse._Common.General.Trees.GenericTree.Interfaces;

namespace Transverse._Common.General.Trees.GenericTree;


[DebuggerDisplay("Leaf: DataToString='{Data.ToString()}', Id={Id}, ParentId={ParentId}")]
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
