namespace General.Basics.Trees.GenericTree;

public class ChildCantBeTheParentException : Exception
{
    public const string MESSAGE_FORMAT = "Le noeud d'Id '{0}' ne peut pas �tre parent de lui-m�me.";

    public override string Message { get; }

    public ChildCantBeTheParentException(int Id) : base("")
    {
        Message = string.Format(MESSAGE_FORMAT, Id);
    }
}