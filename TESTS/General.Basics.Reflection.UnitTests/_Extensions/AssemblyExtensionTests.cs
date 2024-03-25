using System.Reflection;

using Xunit;


using General.Basics.Reflection.POO;

using General.Basics.Reflection.Extensions;



namespace General.Basics.Reflection.Extensions.UnitTests;

public class AssemblyExtensionTests
{
    readonly Assembly currentAssembly;

    public AssemblyExtensionTests()
    {
        Type currentClassType = typeof(AssemblyExtensionTests);
        currentAssembly = currentClassType.Assembly;
    }

    #region GetClassesImplementingTheInterface_
    [Fact]
    public void GetClassesImplementingTheInterface_WhenSomeClassesImplementTheInterface_ShouldReturnAllTheClassesImplementingThisInterface()
    {
        //--- Arrange ---
        Type myInterfaceType = typeof(IMyInterface0);
        Interface @interface = new(myInterfaceType);
        Assembly assembly = currentAssembly;

        //--- Act ---
        List<Class> classes = assembly.GetClassesImplementingTheInterface_(@interface);

        //--- Assert ---
        Assert.Equal(6, classes.Count);
        Assert.Contains(new Class(typeof(ClassImplementing)), classes);
        Assert.Contains(new Class(typeof(ClassImplementing2)), classes);
        Assert.Contains(new Class(typeof(ClassImplementing3)), classes);
        Assert.Contains(new AbstractClass(typeof(AbstractClassImplementing)), classes);
        Assert.Contains(new AbstractClass(typeof(AbstractClassImplementing2)), classes);
        Assert.Contains(new AbstractClass(typeof(SomeGenericAbstractClass<,>)), classes); 
    }

    [Fact]
    public void GetClassesImplementingTheInterface_WhenNoClassImplementsTheInterface_ShouldReturnAnEmptyList()
    {
        //--- Arrange ---
        Type myInterfaceType = typeof(IMyInterfaceY);
        Interface @interface = new(myInterfaceType);
        Assembly assembly = currentAssembly;

        //--- Act ---
        List<Class> classes = assembly.GetClassesImplementingTheInterface_(@interface);

        //--- Assert ---
        Assert.Empty(classes);

    }
    #endregion GetClassesImplementingTheInterface_


    #region GetConcreteClassesImplementingTheInterface_
    [Fact]
    public void GetConcreteClassesImplementingTheInterface_WhenSomeConcreteClassesImplementTheInterface_ShouldReturnAllTheClassesImplementingThisInterface()
    {
        //--- Arrange ---
        Type myInterfaceType = typeof(IMyInterface0);
        Interface @interface = new(myInterfaceType);
        Assembly assembly = currentAssembly;

        //--- Act ---
        List<Class> classes = assembly.GetConcreteClassesImplementingTheInterface_(@interface);

        //--- Assert ---
        Assert.Equal(3, classes.Count);
        Assert.Contains(new Class(typeof(ClassImplementing)), classes);
        Assert.Contains(new Class(typeof(ClassImplementing2)), classes);
        Assert.Contains(new Class(typeof(ClassImplementing3)), classes);
    }

    [Fact]
    public void GetConcreteClassesImplementingTheInterface_WhenNoConcreteClassImplementsTheInterface_ShouldReturnAnEmptyList()
    {
        //--- Arrange ---
        Assembly assembly = currentAssembly;
        Type myInterfaceType1 = typeof(IMyInterfaceY);
        Interface interface1 = new(myInterfaceType1);
        Type myInterfaceType2 = typeof(IMyInterfaceX);
        Interface interface2 = new(myInterfaceType2);


        //--- Act ---
        List<Class> classes1 = assembly.GetConcreteClassesImplementingTheInterface_(interface1);
        List<Class> classes2 = assembly.GetConcreteClassesImplementingTheInterface_(interface2);

        //--- Assert ---
        Assert.Empty(classes1);
        Assert.Empty(classes2);

    }
    #endregion GetConcreteClassesImplementingTheInterface_

}


interface IMyInterface0
{

}

interface IMyInterface : IMyInterface0
{

}

interface IMyInterfaceX
{

}

interface IMyInterfaceY
{

}

class ClassImplementing : IMyInterface
{

}

class ClassImplementing2 : ClassImplementing
{

}

class ClassImplementing3 : ClassImplementing2
{

}


abstract class AbstractClassImplementing : ClassImplementing3, IMyInterfaceX
{

}

abstract class AbstractClassImplementing2 : SomeGenericAbstractClass<int,bool>
{

}

abstract class SomeGenericAbstractClass<T,U> : IMyInterface
{

}