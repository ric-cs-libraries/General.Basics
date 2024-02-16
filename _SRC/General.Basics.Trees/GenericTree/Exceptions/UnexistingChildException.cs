namespace General.Basics.Trees.GenericTree;

public class UnexistingChildException : Exception
{
    public override string Message { get; }

    public UnexistingChildException(int childId, int parentNodeId) : base("")
    {
        Message = $"Le noeud d'Id '{parentNodeId}' n'a pas pour enfant l'élément d'Id '{childId}'.";
    }
}