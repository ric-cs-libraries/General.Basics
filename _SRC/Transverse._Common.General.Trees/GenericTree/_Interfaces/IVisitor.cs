namespace Transverse._Common.General.Trees.GenericTree.Interfaces;

public interface IVisitor<TData>
{
    void Visit(Leaf<TData> leaf);
    void Visit(Node<TData> node);
}
