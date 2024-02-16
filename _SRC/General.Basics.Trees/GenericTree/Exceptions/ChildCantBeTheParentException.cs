namespace General.Basics.Trees.GenericTree;

public class ChildCantBeTheParentException : Exception
{
    public override string Message { get; }

    public ChildCantBeTheParentException(int Id) : base("")
    {
        Message = $"Le noeud d'Id '{Id}' ne peut pas �tre parent de lui-m�me.";
    }
}