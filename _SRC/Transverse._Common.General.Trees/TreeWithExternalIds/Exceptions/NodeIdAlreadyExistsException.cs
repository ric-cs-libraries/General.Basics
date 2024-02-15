namespace Transverse._Common.General.Trees.TreeWithExternalIds;

public class NodeIdAlreadyExistsException : Exception
{
    public override string Message { get; }

    public NodeIdAlreadyExistsException(string nodeId) : base("")
    {
        Message = $"Un noeud d'Id '{nodeId}' existe déjà.";
    }
}