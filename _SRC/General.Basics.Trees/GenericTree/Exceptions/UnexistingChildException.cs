namespace General.Basics.Trees.GenericTree;

public class UnexistingChildException : Exception
{
    public const string MESSAGE_FORMAT = "Le noeud d'Id '{0}' n'a pas pour enfant l'élément d'Id '{1}'.";

    public override string Message { get; }

    public UnexistingChildException(int parentNodeId, int childId) : base("")
    {
        Message = string.Format(MESSAGE_FORMAT, parentNodeId, childId);
    }
}