namespace Transverse._Common.General.Trees.GenericTree;

public class GenericTreeCantHaveAParentException : Exception
{
    public override string Message { get; }

    public GenericTreeCantHaveAParentException() : base("")
    {
        Message = $"Un GenericTree ne peut avoir de parent.";
    }
}