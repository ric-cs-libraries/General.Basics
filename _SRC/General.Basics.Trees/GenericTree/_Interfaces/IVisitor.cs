namespace General.Basics.Trees.GenericTree.Interfaces;

public interface IVisitor<TData>
{
    void Visit(Leaf<TData> leaf);
    void Visit(Node<TData> node);
}
