using Xunit;

using General.Basics.ErrorHandling;


using General.Basics.Trees.GenericTree.Abstracts;
using General.Basics.Trees.GenericTree;


namespace General.Basics.Trees.GenericTree.UnitTests;

public class NodeTests
{
    [Fact]
    public void Node_WhenInstanciated_ShouldContainNoElement()
    {
        //--- Arrange ---
        var node = Node<int>.Create();


        //--- Act ---
        var result1 = node.NbChildren;
        var result2 = node.HasChild;

        //--- Assert ---
        var expected1 = 0;
        var expected2 = false;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Node_WhenInstanciated_ShouldHaveANoParent()
    {
        //--- Arrange ---
        var node = Node<int>.Create();


        //--- Act ---
        var result1 = node.HasParent;
        var result2 = node.Parent;
        var result3 = node.ParentId;
        var result4 = node.IndexInParent;

        //--- Assert ---
        var expected1 = false;
        TreeElement<int>? expected2 = null;
        int? expected3 = null;
        int? expected4 = null;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
        Assert.Equal(expected3, result3);
        Assert.Equal(expected4, result4);
    }


    [Fact]
    public void Node_WhenInstanciated_ShouldHaveAPositiveId()
    {
        //--- Arrange ---
        var node = Node<int>.Create();


        //--- Act ---
        var result = node.Id;

        //--- Assert ---
        Assert.True(result >= 0);
    }

    [Fact]
    public void Add_WhenAddALeaf_ParentNodeShouldHaveAtLeastAChild()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var leaf = Leaf<int>.Create();


        //--- Act ---
        parentNode.Add(leaf);
        var result1 = parentNode.HasChild;
        var result2 = leaf.IndexInParent;

        //--- Assert ---
        var expected1 = true;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Add_WhenAddANode_ParentNodeShouldHaveAtLeastAChild()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode = Node<int>.Create();


        //--- Act ---
        parentNode.Add(childNode);
        var result1 = parentNode.HasChild;
        var result2 = childNode.IndexInParent;

        //--- Assert ---
        var expected1 = true;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Add_WhenAddALeaf_ParentNodeShouldContainOneMoreChild()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode = Node<int>.Create();
        parentNode.Add(childNode);
        var nbChildrenBeforeAddingOneMore = parentNode.NbChildren;

        var leaf = Leaf<int>.Create();

        //--- Act ---
        parentNode.Add(leaf);
        var result1 = parentNode.NbChildren;
        var result2 = leaf.IndexInParent;

        //--- Assert ---
        var expected1 = nbChildrenBeforeAddingOneMore + 1;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Add_WhenAddANode_ParentNodeShouldContainOneMoreChild()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var leaf = Leaf<int>.Create();
        parentNode.Add(leaf);
        var nbChildrenBeforeAddingOneMore = parentNode.NbChildren;

        var childNode = Node<int>.Create();

        //--- Act ---
        parentNode.Add(childNode);
        var result1 = parentNode.NbChildren;
        var result2 = childNode.IndexInParent;

        //--- Assert ---
        var expected1 = nbChildrenBeforeAddingOneMore + 1;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Add_WhenNodeAddedToAParentNode_NodeParentIdShouldBeParentNodeId()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode = Node<int>.Create();


        //--- Act ---
        parentNode.Add(childNode);
        var result = childNode.ParentId;

        //--- Assert ---
        var expected = parentNode.Id;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_WhenLeafAddedToAParentNode_LeafParentIdShouldBeParentNodeId()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var leaf = Leaf<int>.Create();


        //--- Act ---
        parentNode.Add(leaf);
        var result = leaf.ParentId;

        //--- Assert ---
        var expected = parentNode.Id;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_WhenLeafAddedToAParentNode_LeafShouldBeTheLastChildren()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var leaf = Leaf<int>.Create();


        //--- Act ---
        parentNode.Add(leaf);
        var result1 = parentNode.LastAddedElement;
        var result2 = leaf.IndexInParent;

        //--- Assert ---
        var expected1 = leaf;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Add_WhenNodeAddedToAParentNode_NodeShouldBeTheLastChildren()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode = Node<int>.Create();


        //--- Act ---
        parentNode.Add(childNode);
        var result1 = parentNode.LastAddedElement;
        var result2 = childNode.IndexInParent;

        //--- Assert ---
        var expected1 = childNode;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void Add_WhenTryingToAddItselfAsChild_ShouldThrowAChildCantBeTheParentException()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();


        //--- Act & Assert ---
        var ex = Assert.Throws<ChildCantBeTheParentException>(() => parentNode.Add(parentNode));
    }

    [Fact]
    public void Add_WhenTryingToAddAChildTheNodeAlreadyOwns_ShouldThrowAChildAlreadyExistsException()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode = Node<int>.Create();
        parentNode.Add(childNode);


        //--- Act & Assert ---
        var ex = Assert.Throws<ChildAlreadyExistsException>(() => parentNode.Add(childNode));
    }

    [Fact]
    public void Add_WhenNoError_ShouldReturnTheParentNodeItself()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });

        //--- Act ---
        var result = parentNode.Add(Node<string>.Create());

        //--- Assert ---
        var expected = parentNode;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_WhenTheChildHasAlreadyAParentNode_ShouldRemoveThisChildFromTheOldParentNodeChildren()
    {
        //--- Arrange ---
        var parentNode1 = Node<string>.Create();
        var childNode = Node<string>.Create();
        parentNode1.Add(childNode);
        //Assert.Equal(parentNode1, childNode.Parent);

        var parentNode2 = Node<string>.Create();

        //--- Act ---
        parentNode2.Add(childNode);
        var result1 = parentNode1.HasChild;
        var result2 = parentNode2.HasChild;
        var result3 = childNode.Parent;
        var result4 = parentNode2.GetChildById(childNode.Id);
        var result5 = parentNode2.GetChildByIndex(parentNode2.NbChildren - 1);


        //--- Assert ---
        Assert.False(result1);
        Assert.True(result2);
        Assert.Equal(parentNode2, result3);
        Assert.Equal(childNode, result4);
        Assert.Equal(childNode, result5);
    }

    [Fact]
    public void Add_WhenTryingToAddAGenericTreeAsChild_ShouldThrowAGenericTreeCantHaveAParentException()
    {
        //--- Arrange ---
        var tree = GenericTree<string>.Create();
        var node = Node<string>.Create();


        //--- Act & Assert ---
        var ex = Assert.Throws<GenericTreeCantHaveAParentException>(() => node.Add(tree));
    }


    [Fact]
    public void AddMany_WhenElementsAddedToAParentNode_TheNumberOfChildrenShouldIncreaseByTheSameAmount()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode1 = Node<int>.Create();
        parentNode.Add(childNode1);
        var nbChildrenBeforeAddingMore = parentNode.NbChildren;


        var childNode2 = Node<int>.Create();
        var leaf = Leaf<int>.Create();

        var newChildren = new TreeElement<int>[] { childNode2, leaf };

        //--- Act ---
        parentNode.AddMany(newChildren);
        var result = parentNode.NbChildren;

        //--- Assert ---
        var expected = nbChildrenBeforeAddingMore + newChildren.Length;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddMany_WhenElementsAddedToAParentNode_TheLastOfTheseElementsShouldBeTheLastChildren()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var childNode = Node<int>.Create();
        var leaf = Leaf<int>.Create();


        //--- Act ---
        parentNode.AddMany(new TreeElement<int>[] { childNode, leaf });
        var result1 = parentNode.LastAddedElement;
        var result2 = leaf.IndexInParent;

        //--- Assert ---
        var expected1 = leaf;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void AddMany_WhenElementsAddedToAParentNode_TheLastOfTheseElementsShouldBeTheLastChildren_2()
    {
        //--- Arrange ---
        var parentNode = Node<int>.Create();
        var leaf = Leaf<int>.Create();
        var childNode = Node<int>.Create();


        //--- Act ---
        parentNode.AddMany(new TreeElement<int>[] { leaf, childNode });
        var result1 = parentNode.LastAddedElement;
        var result2 = childNode.IndexInParent;

        //--- Assert ---
        var expected1 = childNode;
        var expected2 = parentNode.NbChildren - 1;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void AddMany_WhenNoError_ShouldReturnTheParentNodeItself()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();

        //--- Act ---
        var result = parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });

        //--- Assert ---
        var expected = parentNode;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetStateAsString__ShouldReturnTheCorrectStateAsString()
    {
        //--- Arrange ---
        TreeElement<string>.ResetId();

        var parentNode = Node<string>.Create();
        parentNode.Data = "parentNodeData";

        var childNode = Node<string>.Create();
        childNode.Data = "childNodeData";
        var leaf10 = Leaf<string>.Create();
        leaf10.Data = "leaf10Data";
        childNode.Add(leaf10);

        var leaf00 = Leaf<string>.Create();
        leaf00.Data = "leaf00Data";

        parentNode.AddMany(new TreeElement<string>[] { leaf00, childNode });


        //--- Act ---
        var result = parentNode.GetStateAsString();
        //File.WriteAllText("T:/zdat.txt", result);

        //--- Assert ---
        var expected = "{Node<String>: Id=0; ParentId=; Data='parentNodeData'; Elements(2)=[Leaf<String>: Id=3; ParentId=0; Data='leaf00Data',{Node<String>: Id=1; ParentId=0; Data='childNodeData'; Elements(1)=[Leaf<String>: Id=2; ParentId=1; Data='leaf10Data']}]}";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetChildByIndex_WhenChildExists_ShouldReturnTheChild()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act ---
        var result1 = parentNode.GetChildByIndex(0);
        var result2 = parentNode.GetChildByIndex(1);

        //--- Assert ---
        var expected1 = childNode;
        var expected2 = leaf;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void GetChildByIndex_WhenChildIndexIsOutOfRange_ShouldThrowAnOutOfRangeIntegerException()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act & Assert ---
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => parentNode.GetChildByIndex(parentNode.NbChildren));
    }

    [Fact]
    public void GetChildById_WhenChildExists_ShouldReturnTheChild()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act ---
        var result1 = parentNode.GetChildById(childNode.Id);
        var result2 = parentNode.GetChildById(leaf.Id);

        //--- Assert ---
        var expected1 = childNode;
        var expected2 = leaf;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }

    [Fact]
    public void GetChildById_WhenChildDoesntExist_ShouldReturnNull()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act ---
        var result = parentNode.GetChildById(350);

        //--- Assert ---
        Assert.Null(result);
    }

    [Fact]
    public void OwnsChildById_WhenOwns_ShouldReturnTrue()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act ---
        var result1 = parentNode.OwnsChildById(childNode.Id);
        var result2 = parentNode.OwnsChildById(leaf.Id);
        var result3 = parentNode.OwnsChildById(childNode);
        var result4 = parentNode.OwnsChildById(leaf);

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.True(result4);
    }

    [Fact]
    public void OwnsChildById_WhenDoesntOwn_ShouldReturnFalse()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act ---
        var result1 = parentNode.OwnsChildById(4500);
        var result2 = parentNode.OwnsChildById(500);
        var result3 = parentNode.OwnsChildById(Node<string>.Create());
        var result4 = parentNode.OwnsChildById(Leaf<string>.Create());

        //--- Assert ---
        Assert.False(result1);
        Assert.False(result2);
        Assert.False(result3);
        Assert.False(result4);
    }

    [Fact]
    public void RemoveChildById_WhenNodeOwnsTheChild_ShouldRemoveThisChildAndUpdateChildIndexesInParent()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });
        //Assert.Equal(2, parentNode.NbChildren);
        //Assert.True(parentNode.OwnsChildById(childNode));

        //--- Act ---
        parentNode.RemoveChildById(childNode);

        //--- Act ---
        Assert.Equal(1, parentNode.NbChildren);
        Assert.False(parentNode.OwnsChildById(childNode));
        Assert.Equal(leaf, parentNode.LastAddedElement);
        Assert.Equal(parentNode.NbChildren - 1, leaf.IndexInParent);
    }

    [Fact]
    public void RemoveChildById_WhenRemoveAllChildren_ShouldHaveNoChild()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });
        //Assert.True(parentNode.HasChild);


        //--- Act ---
        parentNode.RemoveChildById(childNode)
                  .RemoveChildById(leaf);

        //--- Assert ---
        Assert.False(parentNode.HasChild);
        Assert.False(leaf.HasParent);
        Assert.Null(leaf.Parent);
        Assert.Null(leaf.IndexInParent);
        Assert.False(childNode.HasParent);
        Assert.Null(childNode.Parent);
        Assert.Null(childNode.IndexInParent);
    }

    [Fact]
    public void RemoveChildById_WhenNodeDoesntOwnTheChild_ShouldThrowAnUnexistingChildException()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });


        //--- Act & Assert ---
        var ex = Assert.Throws<UnexistingChildException>(() => parentNode.RemoveChildById(Node<string>.Create()));
    }

    [Fact]
    public void RemoveChildById_WhenNoError_ShouldreturnTheParentNodeItself()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });

        //--- Act ---
        var result = parentNode.RemoveChildById(leaf);

        //--- Assert ---
        var expected = parentNode;
        Assert.Equal(expected, result);
    }


    [Fact]
    public void RemoveChildrenById_WhenNodeOwnsTheChildren_ShouldRemoveThoseChildrenAndUpdateEveryChildIndexesInParent()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        
        var childNode1 = Node<string>.Create();
        var childNode2 = Node<string>.Create();
        var leaf1 = Leaf<string>.Create();
        var leaf2 = Leaf<string>.Create();

        parentNode.AddMany(new TreeElement<string>[] { childNode1, leaf1, childNode2, leaf2 });


        //--- Act ---
        var result = parentNode.RemoveChildrenById(new TreeElement<string>[] { leaf1, childNode1 });

        //--- Assert ---
        Assert.Equal(0, childNode2.IndexInParent);
        Assert.Equal(parentNode.NbChildren - 1, leaf2.IndexInParent);
        Assert.Equal(2, parentNode.NbChildren);
        Assert.Equal(parentNode, childNode2.Parent);
        Assert.Equal(parentNode, leaf2.Parent);
        Assert.False(parentNode.OwnsChildById(childNode1));
        Assert.False(parentNode.OwnsChildById(leaf1));
        Assert.True(parentNode.OwnsChildById(childNode2));
        Assert.True(parentNode.OwnsChildById(leaf2));
        Assert.False(childNode1.HasParent);
        Assert.False(leaf1.HasParent);
        Assert.Null(childNode1.ParentId);
        Assert.Null(leaf1.ParentId);
        Assert.Null(childNode1.IndexInParent);
        Assert.Null(leaf1.IndexInParent);
    }

    [Fact]
    public void RemoveChildrenById_WhenNodeOwnsTheChildren_ShouldRemoveThoseChildrenAndUpdateEveryChildIndexesInParent_2()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();

        var childNode1 = Node<string>.Create();
        var childNode2 = Node<string>.Create();
        var leaf1 = Leaf<string>.Create();
        var leaf2 = Leaf<string>.Create();

        parentNode.AddMany(new TreeElement<string>[] { childNode1, leaf1, childNode2, leaf2 });


        //--- Act ---
        var result = parentNode.RemoveChildrenById(new int[] { leaf1.Id, childNode1.Id });

        //--- Assert ---
        Assert.Equal(0, childNode2.IndexInParent);
        Assert.Equal(parentNode.NbChildren - 1, leaf2.IndexInParent);
        Assert.Equal(2, parentNode.NbChildren);
        Assert.Equal(parentNode, childNode2.Parent);
        Assert.Equal(parentNode, leaf2.Parent);
        Assert.False(parentNode.OwnsChildById(childNode1));
        Assert.False(parentNode.OwnsChildById(leaf1));
        Assert.True(parentNode.OwnsChildById(childNode2));
        Assert.True(parentNode.OwnsChildById(leaf2));
        Assert.False(childNode1.HasParent);
        Assert.False(leaf1.HasParent);
        Assert.Null(childNode1.ParentId);
        Assert.Null(leaf1.ParentId);
        Assert.Null(childNode1.IndexInParent);
        Assert.Null(leaf1.IndexInParent);
    }

    [Fact]
    public void RemoveChildrenById_WhenRemoveAllChildren_ShouldHaveNoChild()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });
        //Assert.True(parentNode.HasChild);


        //--- Act ---
        parentNode.RemoveChildrenById(new int[] { childNode.Id, leaf.Id });

        //--- Assert ---
        Assert.False(parentNode.HasChild);
        Assert.False(leaf.HasParent);
        Assert.Null(leaf.Parent);
        Assert.Null(leaf.IndexInParent);
        Assert.False(childNode.HasParent);
        Assert.Null(childNode.Parent);
        Assert.Null(childNode.IndexInParent);
    }

    [Fact]
    public void RemoveChildrenById_WhenNodeDoesntOwnTheChild_ShouldThrowAnUnexistingChildException()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });
        var leaf2 = Leaf<string>.Create();


        //--- Act & Assert ---
        var ex = Assert.Throws<UnexistingChildException>(() => parentNode.RemoveChildrenById(new TreeElement<string>[]{ leaf, leaf2}));
    }

    [Fact]
    public void RemoveChildrenById_WhenNoError_ShouldreturnTheParentNodeItself()
    {
        //--- Arrange ---
        var parentNode = Node<string>.Create();
        var childNode = Node<string>.Create();
        var leaf = Leaf<string>.Create();
        parentNode.AddMany(new TreeElement<string>[] { childNode, leaf });

        //--- Act ---
        var result = parentNode.RemoveChildrenById(new int[] { leaf .Id });

        //--- Assert ---
        var expected = parentNode;
        Assert.Equal(expected, result);
    }


    [Fact]
    public void ResetId___ShouldSetNextIdTo0() //Ne pas mettre ce test dans un autre source sinon il y a "conflit" sur la valeur du membre static TreeElement<int>.CurrentId
    {                                          //Les tests de sources(de tests) différents semblant se lancer en parallèle, mais pas quand il s'agit du même source (ici NodeTests.cs).
        //--- Arrange ---
        TreeElement<int>.ResetId();
        var leaf1 = Leaf<int>.Create();
        TreeElement<int>.ResetId();
        var leaf2 = Leaf<int>.Create();

        //--- Act ---
        var result1 = leaf1.Id;
        var result2 = leaf2.Id;

        //--- Assert ---
        var expected1 = 0;
        var expected2 = 0;
        Assert.Equal(expected1, result1);
        Assert.Equal(expected2, result2);
    }
}
