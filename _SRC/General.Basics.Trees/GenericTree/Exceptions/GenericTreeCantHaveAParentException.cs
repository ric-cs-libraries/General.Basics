namespace General.Basics.Trees.GenericTree;

public class GenericTreeCantHaveAParentException : Exception
{
    public const string MESSAGE = "Un GenericTree ne peut avoir de parent.";

    public override string Message { get; }

    public GenericTreeCantHaveAParentException() : base("")
    {
        Message = MESSAGE;
    }
}