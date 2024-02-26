namespace General.Basics.Trees.GenericTree;

public class ChildAlreadyExistsException : Exception
{
    public const string MESSAGE_FORMAT = "Le noeud d'Id '{0}' a d�j� pour enfant l'�l�ment d'Id '{1}'.";
    public override string Message { get; }

    public ChildAlreadyExistsException(int parentNodeId, int childId) : base("")
    {
        Message = string.Format(MESSAGE_FORMAT, parentNodeId, childId);
    }
}