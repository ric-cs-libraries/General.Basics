using Xunit;


using General.Basics.Trees.GenericTree.Abstracts;
using General.Basics.Trees.GenericTree;


namespace General.Basics.Trees.GenericTree.UnitTests;

public class LeafTests
{
    [Fact]
    public void Leaf_WhenInstanciated_ShouldHaveANoParent()
    {
        //--- Arrange ---
        var leaf = Leaf<int>.Create();


        //--- Act ---
        var result = leaf.HasParent;

        //--- Assert ---
        var expected = false;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Leaf_WhenInstanciated_ShouldHaveANullParentId()
    {
        //--- Arrange ---
        var leaf = Leaf<int>.Create();


        //--- Act ---
        var result = leaf.ParentId;

        //--- Assert ---
        int? expected = null;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Leaf_WhenInstanciated_ShouldHaveAPositiveId()
    {
        //--- Arrange ---
        var leaf = Leaf<int>.Create();


        //--- Act ---
        var result = leaf.Id;

        //--- Assert ---
        Assert.True(result >= 0);
    }

    [Fact]
    public void GetStateAsString__ShouldReturnTheCorrectStateAsString()
    {
        //--- Arrange ---
        var leaf = Leaf<string>.Create();
        leaf.Data = "leafData";


        //--- Act ---
        var result = leaf.GetStateAsString();
        //File.WriteAllText("T:/zdat.txt", result);

        //--- Assert ---
        var expected = $"Leaf<String>: Id={leaf.Id}; ParentId=; Data='leafData'";
        Assert.Equal(expected, result);
    }
}
