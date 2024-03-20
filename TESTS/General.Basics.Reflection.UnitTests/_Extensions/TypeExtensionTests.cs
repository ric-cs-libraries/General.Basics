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

    #region ClassImplements_
    [Fact]
    public void ClassImplements_WhenClassOrRecordImplementsTheInterface_ShouldReturnTrue()
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
        Type interface2c = typeof(ISomeInterface0<string>);
        Type genericInterface = typeof(IMyClass<string,int,bool>);
        Interface genericInterface_ = new(genericInterface);
        Type interface4 = typeof(ISomeInterface0<int>);

        //--- Act ---
        var result1 = classType.ClassImplements_(interface1);
        var result1b = classType.ClassImplements_(interface1b);
        var result2 = recordType.ClassImplements_(interface2);
        var result2b = recordType.ClassImplements_(interface2b);
        var result2c = recordType.ClassImplements_(interface2c);
        var result3 = genericClassType.ClassImplements_(genericInterface);
        var result3b = genericClassType.ClassImplements_(genericInterface_);
        var result4 = childClassType.ClassImplements_(interface4);

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result1b);
        Assert.True(result2);
        Assert.True(result2b);
        Assert.True(result2c);
        Assert.True(result3);
        Assert.True(result3b);
        Assert.True(result4);
    }

    [Fact]
    public void ClassImplements_WhenClassOrRecordDoesNotImplementTheInterface_ShouldReturnFalse()
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
        var result1 = classType.ClassImplements_(interface2);
        var result1b = classType.ClassImplements_(interface2b);
        var result2 = recordType.ClassImplements_(interface1);
        var result2b = recordType.ClassImplements_(interface1b);
        var result3 = genericClassType.ClassImplements_(genericInterface);
        var result3b = genericClassType.ClassImplements_(genericInterface_);

        //--- Assert ---
        Assert.False(result1);
        Assert.False(result1b);
        Assert.False(result2);
        Assert.False(result2b);
        Assert.False(result3);
        Assert.False(result3b);
    }
    #endregion ClassImplements_

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
        var result01 = childClassType.InheritsFrom_(parentClass0Type);
        var result11 = childClassType.InheritsFrom_(new Class(parentClass0Type));
        var result02 = childClassType.InheritsFrom_(parentClass1Type);

        var result03 = childRecordType.InheritsFrom_(parentRecord0Type);
        var result13 = childRecordType.InheritsFrom_(new Class(parentRecord0Type));
        var result04 = childRecordType.InheritsFrom_(parentRecord1Type);

        var resultI00 = interfaceType.InheritsFrom_(parentInterfaceType);
        var resultI01 = interfaceType.InheritsFrom_(new Interface(parentInterfaceType));


        //--- Assert ---
        Assert.True(result01);
        Assert.True(result11);
        Assert.True(result02);

        Assert.True(result03);
        Assert.True(result13);
        Assert.True(result04);

        Assert.True(resultI00);
        Assert.True(resultI01);
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
        var result01 = classType.InheritsFrom_(nonParentClassType);
        var result11 = classType.InheritsFrom_(new Class(nonParentClassType));
        var result02 = classType.InheritsFrom_(nonParentRecordType);
        var result12 = classType.InheritsFrom_(nonParentInterfaceType);
        var result12b = interfaceType.InheritsFrom_(nonParentInterfaceType2b);


        var result03 = recordType.InheritsFrom_(nonParentRecordType);
        var result13 = recordType.InheritsFrom_(new Class(nonParentRecordType));
        var result04 = recordType.InheritsFrom_(nonParentClassType);


        //--- Assert ---
        Assert.False(result01);
        Assert.False(result11);
        Assert.False(result02);
        Assert.False(result12);
        Assert.False(result12b);

        Assert.False(result03);
        Assert.False(result13);
        Assert.False(result04);
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
