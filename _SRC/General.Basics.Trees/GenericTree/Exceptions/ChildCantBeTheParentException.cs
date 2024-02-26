namespace General.Basics.Trees.GenericTree;

public class ChildCantBeTheParentException : Exception
{
    public const string MESSAGE_FORMAT = "Le noeud d'Id '{0}' ne peut pas être parent de lui-même.";

    public override string Message { get; }

    public ChildCantBeTheParentException(int Id) : base("")
    {
        Message = string.Format(MESSAGE_FORMAT, Id);
    }
}