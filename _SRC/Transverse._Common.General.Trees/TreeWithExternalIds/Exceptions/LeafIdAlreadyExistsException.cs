namespace Transverse._Common.General.Trees.TreeWithExternalIds;

public class LeafIdAlreadyExistsException : Exception
{
    public override string Message { get; }

    public LeafIdAlreadyExistsException(string leafId) : base("")
    {
        Message = $"Une feuille d'Id '{leafId}' existe déjà.";
    }
}