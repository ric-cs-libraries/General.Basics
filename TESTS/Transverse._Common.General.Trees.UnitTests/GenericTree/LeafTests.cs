using Xunit;

using Transverse._Common.General.Trees.GenericTree;



namespace Transverse._Common.General.Trees.UnitTests.GenericTree;

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
        TreeElement<string>.ResetId();
        var leaf = Leaf<string>.Create();
        leaf.Data = "leafData";


        //--- Act ---
        var result = leaf.GetStateAsString();
        //File.WriteAllText("T:/zdat.txt", result);

        //--- Assert ---
        var expected = "Leaf: Id=0; ParentId=; Data='leafData'";
        Assert.Equal(expected, result);
    }
}
