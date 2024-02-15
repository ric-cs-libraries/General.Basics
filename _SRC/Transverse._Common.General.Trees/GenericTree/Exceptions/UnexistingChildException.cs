namespace Transverse._Common.General.Trees.GenericTree;

public class UnexistingChildException : Exception
{
    public override string Message { get; }

    public UnexistingChildException(int childId, int parentNodeId) : base("")
    {
        Message = $"L'élément d'Id '{childId}' n'a pas pour parent le noeud d'Id '{parentNodeId}'.";
    }
}