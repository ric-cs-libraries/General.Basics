using System.Reflection;

using Xunit;



using General.Basics.Reflection.Extensions;

namespace General.Basics.Reflection.Extensions.UnitTests;

public class ParameterInfoExtensionTests
{
    readonly string currentNamespace;

    public ParameterInfoExtensionTests()
    {
        Type currentClassType = typeof(TypeExtensionTests);
        currentNamespace = currentClassType.Namespace!;
    }

    #region GetTypeInfo_
    [Fact]
    public void GetTypeInfo__ShouldReturnTheParameterTypeFullName()
    {
        //--- Arrange ---
        Type myClass = typeof(MyClass);
        MethodInfo method1 = myClass.GetMethod("Method1")!;
        MethodInfo method2 = myClass.GetMethod("Method2")!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool, string, DateTime>>);
        MethodInfo genericClassMethod = myGenericClass.GetMethod("Method")!;

        ParameterInfo[] methodParameters1 = method1.GetParameters();
        ParameterInfo[] methodParameters2 = method2.GetParameters();
        ParameterInfo[] methodParameters3 = genericClassMethod.GetParameters();


        //--- Act && Assert ---
        //Assert.Equal(4, methodParameters1.Length);
        Assert.Equal("System.String", methodParameters1[0].GetTypeInfo_());
        Assert.Equal("System.Nullable<Boolean>", methodParameters1[1].GetTypeInfo_());
        Assert.Equal($"{currentNamespace}.MyClass", methodParameters1[2].GetTypeInfo_());
        Assert.Equal("System.Int32", methodParameters1[3].GetTypeInfo_());

        //Assert.Equal(1, methodParameters2.Length);
        Assert.Equal("System.DateTime", methodParameters2[0].GetTypeInfo_());

        //Assert.Equal(3, methodParameters3.Length);
        Assert.Equal("System.Int32", methodParameters3[0].GetTypeInfo_());
        Assert.Equal($"{currentNamespace}.IMyClass<Boolean,String,DateTime>", methodParameters3[1].GetTypeInfo_());
        Assert.Equal("System.Boolean", methodParameters3[2].GetTypeInfo_());
    }
    #endregion GetTypeInfo_


    #region GetFullInfo_
    [Fact]
    public void GetFullInfo__ShouldReturnTheParameterFullInfo()
    {
        //--- Arrange ---
        Type myClass = typeof(MyClass);
        MethodInfo method1 = myClass.GetMethod("Method1")!;
        MethodInfo method2 = myClass.GetMethod("Method2")!;

        Type myInterface = typeof(IMyClass);
        MethodInfo method0 = myInterface.GetMethod("Method1")!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool, string, DateTime>>);
        MethodInfo genericClassMethod = myGenericClass.GetMethod("Method")!;

        ParameterInfo[] methodParameters0 = method0.GetParameters();
        ParameterInfo[] methodParameters1 = method1.GetParameters();
        ParameterInfo[] methodParameters2 = method2.GetParameters();
        ParameterInfo[] methodParameters3 = genericClassMethod.GetParameters();



        //--- Act && Assert ---
        Assert.Equal("System.String p1", methodParameters1[0].GetFullInfo_());
        Assert.Equal("System.Nullable<Boolean> p2", methodParameters1[1].GetFullInfo_());
        Assert.Equal($"{currentNamespace}.MyClass p3", methodParameters1[2].GetFullInfo_());
        Assert.Equal("Optional System.Int32 p4", methodParameters1[3].GetFullInfo_()); //<<<<

        Assert.Equal("System.DateTime dt", methodParameters2[0].GetFullInfo_());

        Assert.Equal("System.Int32 p40", methodParameters0[3].GetFullInfo_()); //<<<<

        Assert.Equal("System.Int32 p1", methodParameters3[0].GetFullInfo_());
        Assert.Equal($"{currentNamespace}.IMyClass<Boolean,String,DateTime> p2", methodParameters3[1].GetFullInfo_());
        Assert.Equal("System.Boolean p3", methodParameters3[2].GetFullInfo_());
    }
    #endregion GetFullInfo_
}