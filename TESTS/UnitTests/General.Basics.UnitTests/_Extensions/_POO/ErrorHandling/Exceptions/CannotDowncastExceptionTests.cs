using General.Basics.Bounds.Exceptions;
using General.Basics.Extensions.ErrorHandling;
using Xunit;

namespace General.Basics.UnitTests.Extensions.POO.ErrorHandling.Exceptions;

public class CannotDowncastExceptionTests
{
    [Fact]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage()
    {
        string fromTypeName = "IVehicle";
        string toTypeName = "Car";
        string subject = $"Item[1] in List<IVehicle>";
        var ex = new CannotDowncastException(fromTypeName, toTypeName, subject);

        var expectedMessage = string.Format(CannotDowncastException.MESSAGE_FORMAT, fromTypeName, toTypeName, subject);
        Assert.Equal(expectedMessage, ex.Message);
    }
}
