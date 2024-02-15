namespace Transverse._Common.General.Trees.GenericTree;

public class ChildAlreadyExistsException : Exception
{
    public override string Message { get; }

    public ChildAlreadyExistsException(int childId, int parentNodeId) : base("")
    {
        Message = $"Le noeud d'Id '{parentNodeId}' a d�j� pour enfant l'�l�ment d'Id '{childId}'.";
    }
}