using Xunit;


using General.Basics.Extensions;

using General.Basics.Reflection.POO;


using General.Basics.Reflection.Extensions;
using General.Basics.Reflection.DynamicCalls.UnitTests;

namespace General.Basics.Reflection.Extensions.UnitTests;

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
        Type classType3 = typeof(SomeGenericClass2<Task<long>,double>);
        Type classType4 = typeof(Task< SomeGenericClass2<string,Task<DateTime>> >);
        Type classType5 = typeof(SomeGenericClass< SomeGenericClass2<SomeClass, SomeGenericClass2<int,bool>> >);

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

    #region Implements_
    [Fact]
    public void Implements_WhenClassOrRecordImplementsTheInterface_ShouldReturnTrue()
    {
        //--- Arrange ---
        Type classType = typeof(MyClass);
        Type recordType = typeof(SomeRecord);
        Type genericClassType = typeof(MyClass<string,int>);
        Type childClassType = typeof(ChildClass);

        Type interface1 = typeof(IMyClass);
        Interface interface1b = new(interface1);
        Type interface2 = typeof(ISomeInterface);
        Interface interface2b = new(interface2);
        Type interface3 = typeof(ISomeInterface0<string>);
        Interface interface3b = new(interface3);
        Type interface4 = typeof(ISomeInterface0<int>);
        Interface interface4b = new(interface4);
        Type genericInterface = typeof(IMyClass<string, int, bool>);
        Interface genericInterface_ = new(genericInterface);

        //--- Act ---
        var result1 = classType.Implements_(interface1b);
        var result2 = recordType.Implements_(interface2b);
        var result3 = recordType.Implements_(interface3b);
        var result4 = childClassType.Implements_(interface4b);
        var resultGenericInterface = genericClassType.Implements_(genericInterface_);

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        Assert.True(result4);
        Assert.True(resultGenericInterface);
    }

    [Fact]
    public void Implements_WhenClassOrRecordDoesNotImplementTheInterface_ShouldReturnFalse()
    {
        //--- Arrange ---
        Type classType = typeof(MyClass);
        Type recordType = typeof(SomeRecord);
        Type genericClassType = typeof(MyClass<string, int>);

        Type interface1 = typeof(IMyClass);
        Interface interface1b = new(interface1);
        Type interface2 = typeof(ISomeInterface);
        Interface interface2b = new(interface2);
        Type genericInterface = typeof(IMyClass<string, DateTime, bool>);
        Interface genericInterface_ = new(genericInterface);

        //--- Act ---
        var result1 = classType.Implements_(interface2b);
        var result2 = recordType.Implements_(interface1b);
        var resultGenericInterface = genericClassType.Implements_(genericInterface_);

        //--- Assert ---
        Assert.False(result1);
        Assert.False(result2);
        Assert.False(resultGenericInterface);
    }
    #endregion Implements_

    #region InheritsFrom_
    [Fact]
    public void InheritsFrom__WhenClassOrRecordOrInterfaceInherits_ShouldReturnTrue()
    {
        //--- Arrange ---
        Type childClassType = typeof(ChildClass);
        Type childRecordType = typeof(ChildRecord);
        Type interfaceType = typeof(ISomeInterface);

        Type parentClass0Type = typeof(ParentClass0<string,int>);
        Type parentClass1Type = typeof(ParentClass1<bool>);

        Type parentRecord0Type = typeof(ParentRecord0<string,int>);
        Type parentRecord1Type = typeof(ParentRecord1<bool>);

        Type parentInterfaceType = typeof(ISomeInterface00<string,long>);



        //--- Act ---
        var resultC0 = childClassType.InheritsFrom_(new Class(parentClass0Type));
        var resultC1 = childClassType.InheritsFrom_(new Class(parentClass1Type));

        var resultR0 = childRecordType.InheritsFrom_(new Class(parentRecord0Type));
        var resultR1 = childRecordType.InheritsFrom_(new Class(parentRecord1Type));

        var resultI0 = interfaceType.InheritsFrom_(new Interface(parentInterfaceType));


        //--- Assert ---
        Assert.True(resultC0);
        Assert.True(resultC1);

        Assert.True(resultR0);
        Assert.True(resultR1);

        Assert.True(resultI0);
    }

    [Fact]
    public void InheritsFrom__WhenClassOrRecordOrInterfaceDoesNotInherit_ShouldReturnFalse()
    {
        //--- Arrange ---
        Type classType = typeof(ChildClass);
        Type recordType = typeof(ChildRecord);
        Type interfaceType = typeof(ISomeInterface0<DateTime>);

        Type nonParentClassType = typeof(SomeClass);
        Type nonParentRecordType = typeof(SomeRecord);

        Type nonParentInterfaceType = typeof(ISomeInterface);
        Type nonParentInterfaceType2b = typeof(ISomeInterface00<DateTime,int>);



        //--- Act ---
        var resultC1 = classType.InheritsFrom_(new Class(nonParentClassType));
        var resultC2 = classType.InheritsFrom_(new Class(nonParentRecordType));
        var resultC3 = classType.InheritsFrom_(new Interface(nonParentInterfaceType));

        var resultI = interfaceType.InheritsFrom_(new Interface(nonParentInterfaceType2b));

        var resultR1 = recordType.InheritsFrom_(new Class(nonParentRecordType));
        var resultR2 = recordType.InheritsFrom_(new Class(nonParentClassType));


        //--- Assert ---
        Assert.False(resultC1);
        Assert.False(resultC2);
        Assert.False(resultC3);

        Assert.False(resultI);

        Assert.False(resultR1);
        Assert.False(resultR2);
    }
    #endregion InheritsFrom_
}



class SomeGenericClass<T> { }

class SomeGenericClass2<T,U> { }

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
