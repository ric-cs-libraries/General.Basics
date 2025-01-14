using General.Basics.ReflectionExtended.POO;
using General.Basics.ReflectionExtended.POO.Extensions;
using Xunit;

namespace General.Basics.ReflectionExtended.UnitTests.POO.Extensions;

public partial class TypeExtensionTests
{
    #region Implements_
    [Fact]
    public void Implements_WhenClassOrRecordImplementsTheInterface_ShouldReturnTrue()
    {
        //--- Arrange ---
        Type classType = typeof(MyClass);
        Type recordType = typeof(SomeRecord);
        Type genericClassType = typeof(MyClass<string, int>);
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

        Type parentClass0Type = typeof(ParentClass0<string, int>);
        Type parentClass1Type = typeof(ParentClass1<bool>);

        Type parentRecord0Type = typeof(ParentRecord0<string, int>);
        Type parentRecord1Type = typeof(ParentRecord1<bool>);

        Type parentInterfaceType = typeof(ISomeInterface00<string, long>);



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
        Type nonParentInterfaceType2b = typeof(ISomeInterface00<DateTime, int>);



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


