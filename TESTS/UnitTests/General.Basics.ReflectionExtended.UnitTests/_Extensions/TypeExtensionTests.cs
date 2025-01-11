using General.Basics.Extensions;
using Xunit;

namespace General.Basics.ReflectionExtended.Extensions.UnitTests;

public partial class TypeExtensionTests
{
    readonly string currentAssemblyName;
    readonly string currentNamespace;

    public TypeExtensionTests()
    {
        Type currentClassType = typeof(TypeExtensionTests);
        currentAssemblyName = currentClassType.Module.Name.EndsWith_(false, ".dll");
        currentNamespace = currentClassType.Namespace!;
    }

    #region GetAssemblyName_
    [Fact]
    public void GetAssemblyName__ShouldReturnTheCorrectName()
    {
        //--- Act ---
        var classAssemblyName = typeof(SomeGenericClass<>).GetAssemblyName_();

        //--- Assert ---
        Assert.Equal(classAssemblyName, currentAssemblyName);
    }
    #endregion GetAssemblyName_

}