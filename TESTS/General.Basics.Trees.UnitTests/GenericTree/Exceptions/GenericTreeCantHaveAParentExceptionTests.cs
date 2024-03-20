using Xunit;


using General.Basics.Trees.GenericTree;


namespace General.Basics.Trees.GenericTree.UnitTests;

public class GenericTreeCantHaveAParentExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var ex = new GenericTreeCantHaveAParentException();

        var expectedMessage = GenericTreeCantHaveAParentException.MESSAGE;
        Assert.Equal(expectedMessage, ex.Message);
    }
}
