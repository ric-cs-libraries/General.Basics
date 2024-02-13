using Xunit;


using Transverse._Common.General.Basics.Extensions;



namespace Transverse._Common.General.Basics.Extensions.Type.UnitTests;

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
