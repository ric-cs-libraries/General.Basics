using Xunit;

using Transverse._Common.General.Trees.GenericTree;



namespace Transverse._Common.General.Trees.UnitTests.GenericTree;

public class GenericTreeTests
{
    [Fact]
    public void Create_WhenInstanciated_ShouldBeCorrectlyInitialized()
    {
        //--- Act ---
        var tree = GenericTree<string>.Create();

        //--- Assert ---
        Assert.True(tree.Id >=0);
        Assert.False(tree.HasChild);
        Assert.Equal(0 ,tree.NbChildren);
        Assert.False(tree.HasParent);
        Assert.Null(tree.Parent);
        Assert.Null(tree.ParentId);
    }
}
