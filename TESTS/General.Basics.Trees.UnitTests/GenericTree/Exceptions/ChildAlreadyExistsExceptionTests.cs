using Xunit;

using General.Basics.Trees.GenericTree;


namespace General.Basics.Trees.UnitTests.GenericTree;


public class ChildAlreadyExistsExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var parentNodeId = 10;
        var childId = 20;
        var ex = new ChildAlreadyExistsException(parentNodeId, childId);

        var expectedMessage = string.Format(ChildAlreadyExistsException.MESSAGE_FORMAT, parentNodeId, childId);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
