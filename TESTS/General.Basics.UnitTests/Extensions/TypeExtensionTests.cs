using Xunit;


using General.Basics.Extensions;



namespace General.Basics.Extensions.UnitTests;

public class TypeExtensionTests
{

    class AGenericClass<T> { }

    [Fact]
    public void GetSimpleName__ShouldReturnTheCorrectClassName()
    {
        var o = new AGenericClass<int>();

        var result = o.GetType().GetSimpleName();

        Assert.Equal("AGenericClass", result);
        Assert.NotEqual(o.GetType().Name, result);
    }

}
