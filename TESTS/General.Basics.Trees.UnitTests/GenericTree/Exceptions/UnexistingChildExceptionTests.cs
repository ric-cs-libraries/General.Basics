using Xunit;


using General.Basics.Trees.GenericTree;


namespace General.Basics.Trees.GenericTree.UnitTests;

public class UnexistingChildExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var parentNodeId = 10;
        var childId = 20;
        var ex = new UnexistingChildException(parentNodeId, childId);

        var expectedMessage = string.Format(UnexistingChildException.MESSAGE_FORMAT, parentNodeId, childId);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
