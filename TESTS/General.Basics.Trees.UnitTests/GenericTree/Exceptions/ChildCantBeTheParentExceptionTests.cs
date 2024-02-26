using Xunit;

using General.Basics.Trees.GenericTree;


namespace General.Basics.Trees.UnitTests.GenericTree;


public class ChildCantBeTheParentExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        var id = 10;
        var ex = new ChildCantBeTheParentException(id);

        var expectedMessage = string.Format(ChildCantBeTheParentException.MESSAGE_FORMAT, id);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
