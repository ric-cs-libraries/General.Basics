namespace Transverse._Common.General.Trees.TreeWithExternalIds.Interfaces;

public interface ITreeWithExternalIds
{
    void AddLeaf(string id, string parentNodeId, string description);
    void AddNode(string id, string parentNodeId, string description);
    string GetRootNodeId();
}