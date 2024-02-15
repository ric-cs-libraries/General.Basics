namespace Transverse._Common.General.Trees.TreeWithExternalIds;

public class UnexistingParentNodeIdException : Exception
{
    public override string Message { get; }

    public UnexistingParentNodeIdException(string parentNodeId) : base("")
    {
        Message = $"Le noeud parent d'id '{parentNodeId}' n'existe pas.";
    }
}