using System.Diagnostics;

using Transverse._Common.General.Basics.Extensions;

using Transverse._Common.General.Trees.TreeWithExternalIds.Interfaces;


namespace Transverse._Common.General.Trees.TreeWithExternalIds;


[DebuggerDisplay("{RootNode.GetStateAsString()}")]
public class TreeWithExternalIds : ITreeWithExternalIds
{
    private TreeRootNode RootNode { get; }

    private Dictionary<string, TreeNode> AllTreeNodesById = new();
    private Dictionary<string, TreeLeaf> AllTreeLeavesById = new();

    public TreeWithExternalIds(string rootNodeDescription)
    {
        RootNode = new TreeRootNode(rootNodeDescription);
        AllTreeNodesById.Add(GetRootNodeId(), RootNode);
    }

    public string GetRootNodeId()
    {
        return RootNode.Id;
    }


    public void AddNode(string id, string parentNodeId, string description)
    {
        checkElementDoesntExist(id);

        TreeNode parentNode = GetParentNodeById(parentNodeId);

        TreeNode newChildNode = new(id, parentNodeId, description);
        parentNode.AddElement(newChildNode);
        AllTreeNodesById.Add(id, newChildNode);
    }

    public void AddLeaf(string id, string parentNodeId, string description)
    {
        checkElementDoesntExist(id);

        TreeNode parentNode = GetParentNodeById(parentNodeId);

        TreeLeaf newLeaf = new(id, parentNodeId, description);
        parentNode.AddElement(newLeaf);
        AllTreeLeavesById.Add(id, newLeaf);
    }

    private TreeNode GetParentNodeById(string parentNodeId)
    {
        TreeNode? parentNode = GetNodeById(parentNodeId);
        if (parentNode is null)
        {
            throw new UnexistingParentNodeIdException(parentNodeId);
        }
        return parentNode;

    }

    private void checkElementDoesntExist(string elementId)
    {
        checkNodeDoesntExist(elementId);
        checkLeafDoesntExist(elementId);
    }

    private void checkNodeDoesntExist(string nodeId)
    {
        if (GetNodeById(nodeId) is not null)
        {
            throw new NodeIdAlreadyExistsException(nodeId);
        }
    }

    private TreeNode? GetNodeById(string nodeId)
    {
        AllTreeNodesById.TryGetValue(nodeId, out TreeNode? node);
        return node;
    }

    private void checkLeafDoesntExist(string leafId)
    {
        if (AllTreeLeavesById.TryGetValue(leafId, out TreeLeaf? leaf))
        {
            throw new LeafIdAlreadyExistsException(leafId);
        }
    }


    //===================================

    [DebuggerDisplay("{GetStateAsString()}")]
    private abstract record TreeElement(string Id, string ParentNodeId, string Description)
    {
        internal const int DEBUG_DESCRIPTION_MAX_LENGTH = 100;
        internal int ChildNumber { get; set; }

        protected virtual List<string> GetStateAsStringsList(int maxDescriptionLength = DEBUG_DESCRIPTION_MAX_LENGTH)  //POUR DEBUG
        {
            var state = new List<string>
            {
                $"{GetType().GetSimpleName()}"
            };

            state.AddRange(GetIdsStateAsStringsList());

            state.AddRange(new List<string>
            {
                $"Description='{Description.GetAsShorten_(DEBUG_DESCRIPTION_MAX_LENGTH)}'"
            });
            return state;
        }
        protected virtual List<string> GetIdsStateAsStringsList()
        {
            var idsState = new List<string>
            {
                $"Id='{Id}'",
                $"ParentNodeId='{ParentNodeId}'",
                $"ChildNumber='{ChildNumber}'",
            };
            return idsState;
        }

        private string GetStateAsString(int maxDescriptionLength = DEBUG_DESCRIPTION_MAX_LENGTH)
        {
            var result = string.Join("; ", GetStateAsStringsList(maxDescriptionLength));
            return result;
        }
    }

    private record TreeNode(string Id, string ParentNodeId, string Description) : TreeElement(Id, ParentNodeId, Description)
    {
        private List<TreeElement> Elements { get; } = new();

        internal void AddElement(TreeElement element)
        {
            element.ChildNumber = Elements.Count;
            Elements.Add(element);
        }

        protected override List<string> GetStateAsStringsList(int maxDescriptionLength = DEBUG_DESCRIPTION_MAX_LENGTH)  //POUR DEBUG
        {
            var state = base.GetStateAsStringsList();
            state.AddRange(new List<string>
            {
                $"Elements({Elements.Count})=["+string.Join(", ", Elements.Select(e => $"{e.GetType().GetSimpleName()}(Id={e.Id})"))+"]"
            });
            return state;
        }
    }
    private record TreeRootNode(string Description) : TreeNode(NO_ID, NO_PARENT_ID, Description)
    {
        private const string NO_ID = "ROOT_ID";
        private const string NO_PARENT_ID = "";
        protected override List<string> GetIdsStateAsStringsList()
        {
            var idsState = new List<string>
            {
            };
            return idsState;
        }
    }


    private record TreeLeaf(string Id, string ParentNodeId, string Description) : TreeElement(Id, ParentNodeId, Description)
    {
    }

}


