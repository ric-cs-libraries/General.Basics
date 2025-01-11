using General.Basics.Extensions;
using General.Basics.Reflection.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Reflection.Extensions;

public class TypeExtensionTests
{
    readonly string currentAssemblyName;
    readonly string currentNamespace;

    public TypeExtensionTests()
    {
        Type currentClassType = typeof(TypeExtensionTests);
        currentAssemblyName = currentClassType.Module.Name.EndsWith_(false, ".dll");
        currentNamespace = currentClassType.Namespace!;
    }

    #region GetSimpleName_
    [Fact]
    public void GetSimpleName_WhenGenericClass_ShouldReturnTheCorrectSimpleName()
    {
        //--- Arrange ---
        Type classType1 = typeof(SomeGenericClass<>);
        Type classType2 = typeof(SomeGenericClass<string>);

        //--- Act ---
        var result1 = classType1.GetSimpleName_();
        var result2 = classType2.GetSimpleName_();

        //--- Assert ---
        Assert.Equal("SomeGenericClass", result1);
        Assert.NotEqual(classType1.Name, result1);

        Assert.Equal("SomeGenericClass", result2);
        Assert.NotEqual(classType2.Name, result2);
    }

    [Fact]
    public void GetSimpleName_WhenNonGenericClass_ShouldReturnTheCorrectSimpleName()
    {
        //--- Arrange ---
        Type classType = typeof(SomeClass);

        //--- Act ---
        var result = classType.GetSimpleName_();

        //--- Assert ---
        Assert.Equal("SomeClass", result);
        Assert.Equal(classType.Name, result);
    }
    #endregion GetSimpleName_


    #region GetName_
    [Fact]
    public void GetName_WhenGenericClass_ShouldReturnTheCorrectName()
    {
        //--- Arrange ---
        Type classType1 = typeof(SomeGenericClass<>);
        Type classType2 = typeof(SomeGenericClass<string>);
        Type classType3 = typeof(SomeGenericClass2<Task<long>, double>);
        Type classType4 = typeof(Task<SomeGenericClass2<string, Task<DateTime>>>);
        Type classType5 = typeof(SomeGenericClass<SomeGenericClass2<SomeClass, SomeGenericClass2<int, bool>>>);

        //--- Act ---
        var result1 = classType1.GetName_();
        var result2 = classType2.GetName_();
        var result3 = classType3.GetName_();
        var result4 = classType4.GetName_();
        var result5 = classType5.GetName_();


        //--- Assert ---
        Assert.Equal("SomeGenericClass<>", result1);
        Assert.NotEqual(classType1.Name, result1);

        Assert.Equal("SomeGenericClass<String>", result2);
        Assert.NotEqual(classType2.Name, result2);

        Assert.Equal("SomeGenericClass2<Task<Int64>,Double>", result3);
        Assert.NotEqual(classType3.Name, result3);

        Assert.Equal("Task<SomeGenericClass2<String,Task<DateTime>>>", result4);
        Assert.NotEqual(classType4.Name, result4);

        Assert.Equal("SomeGenericClass<SomeGenericClass2<SomeClass,SomeGenericClass2<Int32,Boolean>>>", result5);
        Assert.NotEqual(classType5.Name, result5);
    }

    [Fact]
    public void GetName_WhenNonGenericClass_ShouldReturnTheCorrectName()
    {
        //--- Arrange ---
        Type classType1 = typeof(SomeClass);
        Type classType2 = typeof(string);
        Type classType3 = typeof(Task);
        Type classType4 = typeof(uint);

        //--- Act ---
        var result1 = classType1.GetName_();
        var result2 = classType2.GetName_();
        var result3 = classType3.GetName_();
        var result4 = classType4.GetName_();


        //--- Assert ---
        Assert.Equal("SomeClass", result1);
        Assert.Equal(classType1.Name, result1);

        Assert.Equal("String", result2);
        Assert.Equal(classType2.Name, result2);

        Assert.Equal("Task", result3);
        Assert.Equal(classType3.Name, result3);

        Assert.Equal("UInt32", result4);
        Assert.Equal(classType4.Name, result4);
    }
    #endregion GetName_


    #region GetSimpleFullName_
    [Fact]
    public void GetSimpleFullName__ShouldReturnTheCorrectSimpleFullName()
    {
        //--- Arrange ---
        Type classType = typeof(SomeGenericClass<>);

        //--- Act ---
        var result = classType.GetSimpleFullName_();

        //--- Assert ---
        Assert.Equal($"{currentNamespace}.SomeGenericClass", result);
    }
    #endregion GetSimpleFullName_

    #region GetFullName_
    [Fact]
    public void GetFullName__ShouldReturnTheCorrectFullName()
    {
        //--- Arrange ---
        Type classType1 = typeof(SomeGenericClass<>);
        Type classType2 = typeof(SomeGenericClass2<Task<SomeGenericClass<string>>, Task<DateTime>>);

        //--- Act ---
        var result1 = classType1.GetFullName_();
        var result2 = classType2.GetFullName_();

        //--- Assert ---
        Assert.Equal($"{currentNamespace}.SomeGenericClass<>", result1);
        Assert.Equal($"{currentNamespace}.SomeGenericClass2<Task<SomeGenericClass<String>>,Task<DateTime>>", result2);
    }
    #endregion GetFullName_

}



class SomeGenericClass<T> { }

class SomeGenericClass2<T, U> { }

class SomeClass { }


interface ISomeInterface00<T, X>
{

}
interface ISomeInterface0<T> : ISomeInterface00<T, long>
{

}
interface ISomeInterface : ISomeInterface0<string>
{

}
class SomeRecord : ISomeInterface
{

}

//
class ParentClass0<T, U> : ISomeInterface0<U>
{

}
class ParentClass1<U> : ParentClass0<string, int>
{

}
class ChildClass : ParentClass1<bool>, ISomeInterface
{

}

record ParentRecord0<T, U>
{

}
record ParentRecord1<U> : ParentRecord0<string, int>
{

}
record ChildRecord : ParentRecord1<bool>
{

}
