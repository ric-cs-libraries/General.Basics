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


    #region GetInstancesFromClassesImplementing_<TInterface>
    [Fact]
    public void GetInstancesFromClassesImplementing_WhenProvidedTypeIsNotAnInterfaceType_ShouldThrowATypeShouldBeAnInterfaceTypeException()
    {
        //--- Act & Assert ---
        var ex = Assert.Throws<TypeShouldBeAnInterfaceTypeException>(() => currentAssembly.GetInstancesFromClassesImplementing_<Vehicle>());

        var expectedMessage = string.Format(TypeShouldBeAnInterfaceTypeException.MESSAGE_FORMAT, "Vehicle", TypeShouldBeAnInterfaceTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstancesFromClassesImplementing_WhenWithParamsConstructorIsFound_ShouldReturnTheCorrectlyInitializedInstancesOfMatchingNonGenericClassesImplementingTheInterface()
    {
        //--- Arrange ---
        var modelName = "SuperModele";
        var constructorParams = new object[] { modelName };

        //--- Act ---
        IReadOnlyList<IVehicle> results = currentAssembly.GetInstancesFromClassesImplementing_<IVehicle>(constructorParams);

        //--- Assert ---
        Assert.Equal(2, results.Count);
        Assert.IsAssignableFrom<IVehicle>(results[0]);
        Assert.IsAssignableFrom<IVehicle>(results[1]);
        Assert.Equal(modelName, results[0].Modele);
        Assert.Equal(modelName, results[1].Modele);
        Assert.NotNull(results[0] as Car);
        Assert.NotNull(results[1] as Moto);
    }

    [Fact]
    public void GetInstancesFromClassesImplementing_WhenNoConstructorParamAndClassesHaveNoConstructorOrHaveWithNoParamConstructor_ShouldReturnTheCorrectlyInitializedInstancesOfMatchingNonGenericClassesImplementingTheInterface()
    {
        //--- Arrange ---
        var constructorParams1 = new object[] { };
        object[]? constructorParams2 = null;

        //--- Act ---
        IReadOnlyList<IVehicle0> results1 = currentAssembly.GetInstancesFromClassesImplementing_<IVehicle0>(constructorParams1);
        IReadOnlyList<IVehicle0> results2 = currentAssembly.GetInstancesFromClassesImplementing_<IVehicle0>(constructorParams2);
        IReadOnlyList<IVehicle0> results3 = currentAssembly.GetInstancesFromClassesImplementing_<IVehicle0>();

        //--- Assert ---
        Assert.Equal(3, results1.Count);
        Assert.IsAssignableFrom<IVehicle0>(results1[0]);
        Assert.IsAssignableFrom<IVehicle0>(results1[1]);
        Assert.IsAssignableFrom<IVehicle0>(results1[2]);
        Assert.Null(results1[0].Modele);
        Assert.Null(results1[1].Modele);
        Assert.Null(results1[2].Modele);
        Assert.NotNull(results1[0] as Car);
        Assert.NotNull(results1[1] as Moto);
        Assert.NotNull(results1[2] as Velo);

        Assert.Equivalent(results1, results2);
        Assert.Equivalent(results2, results3);
    }

    [Fact]
    public void GetInstancesFromClassesImplementing_WhenNoMatchingConstructorIsFound_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var unExpectedParamType = 100;
        var constructorParams1 = new object[] { unExpectedParamType };


        //--- Act & Assert ---
        var ex1 = Assert.Throws<MissingMethodException>(() => currentAssembly.GetInstancesFromClassesImplementing_<IVehicle>(constructorParams1));
        var ex2 = Assert.Throws<MissingMethodException>(() => currentAssembly.GetInstancesFromClassesImplementing_<IVehicle1>());
    }
    #endregion GetInstancesFromClassesImplementing_<TInterface>



    #region GetInstancesFromGenericClassesImplementing_<TInterface>
    [Fact]
    public void GetInstancesFromGenericClassesImplementing_WhenProvidedTypeIsNotAnInterfaceType_ShouldThrowATypeShouldBeAnInterfaceTypeException()
    {
        //--- Arrange ---
        Type[] genericParametersType = new Type[] { typeof(bool) };

        //--- Act & Assert ---
        var ex = Assert.Throws<TypeShouldBeAnInterfaceTypeException>(() => currentAssembly.GetInstancesFromGenericClassesImplementing_<Vehicle<int>>(genericParametersType));

        var expectedMessage = string.Format(TypeShouldBeAnInterfaceTypeException.MESSAGE_FORMAT, "Vehicle`1", TypeShouldBeAnInterfaceTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstancesFromGenericClassesImplementing_WhenWithParamsConstructorIsFound_ShouldReturnTheCorrectlyInitializedInstancesOfMatchingGenericClassesImplementingTheInterface()
    {
        //--- Arrange ---
        string modelName = "SuperModele";
        bool uParam = true;
        var constructorParams = new object[] { modelName, uParam };
        Type[] genericParametersType = new Type[] { typeof(bool) }; //U

        //--- Act ---
        IReadOnlyList<IVehicle<int>> results = currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle<int>>(genericParametersType, constructorParams);

        //--- Assert ---
        Assert.Equal(2, results.Count);
        Assert.IsAssignableFrom<IVehicle<int>>(results[0]);
        Assert.IsAssignableFrom<IVehicle<int>>(results[1]);
        Assert.Equal(modelName, results[0].Modele);
        Assert.Equal(modelName, results[1].Modele);
        Assert.Equal(100, results[0].SomeData);
        Assert.Equal(10, results[1].SomeData);
        Assert.NotNull(results[0] as Car<bool>);
        Assert.NotNull(results[1] as Moto<bool>);
        Assert.Equal(uParam, (results[0] as Car<bool>)!.info);
        Assert.Equal(uParam, (results[1] as Moto<bool>)!.info);
    }

    [Fact]
    public void GetInstancesFromGenericClassesImplementing_WhenNoConstructorParamAndClassesHaveNoConstructorOrHaveWithNoParamConstructor_ShouldReturnTheCorrectlyInitializedInstancesOfMatchingGenericClassesImplementingTheInterface()
    {
        //--- Arrange ---
        var constructorParams1 = new object[] { };
        object[]? constructorParams2 = null;

        Type[] genericParametersType = new Type[] { typeof(DateTime) };

        //--- Act ---
        IReadOnlyList<IVehicle0_> results1 = currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle0_>(genericParametersType, constructorParams1);
        IReadOnlyList<IVehicle0_> results2 = currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle0_>(genericParametersType, constructorParams2);
        IReadOnlyList<IVehicle0_> results3 = currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle0_>(genericParametersType);

        //--- Assert ---
        Assert.Equal(3, results1.Count);
        Assert.IsAssignableFrom<IVehicle0_>(results1[0]);
        Assert.IsAssignableFrom<IVehicle0_>(results1[1]);
        Assert.IsAssignableFrom<IVehicle0_>(results1[2]);
        Assert.Null(results1[0].Modele);
        Assert.Null(results1[1].Modele);
        Assert.Null(results1[2].Modele);
        Assert.NotNull(results1[0] as Car<DateTime>);
        Assert.NotNull(results1[1] as Moto<DateTime>);
        Assert.NotNull(results1[2] as Velo<DateTime>);

        Assert.Equivalent(results1, results2);
        Assert.Equivalent(results2, results3);
    }

    [Fact]
    public void GetInstancesFromGenericClassesImplementing_WhenNoMatchingConstructorIsFound_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var unExpectedParamType = 100;
        var constructorParams1 = new object[] { unExpectedParamType };

        Type[] genericParametersType = new Type[] { typeof(long) };

        //--- Act & Assert ---
        var ex1 = Assert.Throws<MissingMethodException>(() => currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle<int>>(genericParametersType, constructorParams1));
        var ex2 = Assert.Throws<MissingMethodException>(() => currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle1_>(genericParametersType));
    }

    [Fact]
    public void GetInstancesFromGenericClassesImplementing_WhenGenericParameterTypeDoesNotComplyToSomeConstraint_ShouldThrowAGenericParameterTypeViolatingSomeConstraintException()
    {
        //--- Arrange ---
        Type[] genericParametersType = new Type[] { typeof(long), typeof(int), typeof(Avion<long,string>) };  //Pour Helico<W,X,Y> where Y: Avion<W,X>
                                                                                                              //Y attendu étant alors: Avion<long,int> 

        //--- Act & Assert ---
        var ex1 = Assert.Throws<GenericParameterTypeViolatingSomeConstraintException>(() => currentAssembly.GetInstancesFromGenericClassesImplementing_<IVehicle<int>>(genericParametersType));
    }
    #endregion GetInstancesFromGenericClassesImplementing_<TInterface>

}


#region GetClassesImplementingTheInterface_  et GetConcreteClassesImplementingTheInterface_
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

abstract class AbstractClassImplementing2 : SomeGenericAbstractClass<int, bool>
{

}

abstract class SomeGenericAbstractClass<T, U> : IMyInterface
{

}
#endregion GetClassesImplementingTheInterface_  et GetConcreteClassesImplementingTheInterface_

#region GetInstancesFromClassesImplementing_<TInterface>
interface IVehicle0
{
    string? Modele { get; }
}
interface IVehicle : IVehicle0
{
}
abstract class Vehicle : IVehicle
{
    public string? Modele { get; }

    protected Vehicle()
    {

    }
    protected Vehicle(string modele)
    {
        Modele = modele;
    }
}
class Car : Vehicle, IVehicle
{
    public Car() : base()
    {

    }
    public Car(string modele) : base(modele)
    {
    }
}
class Moto : Vehicle  //Implémente de façon indirecte IVehicle
{
    public Moto() : base()
    {

    }
    public Moto(string modele) : base(modele)
    {
    }
}
class Camion<T> : IVehicle
{
    public string? Modele { get; }
}

class Velo : IVehicle0
{
    public string? Modele { get; }
}
interface IVehicle1
{

}
class Bateau : IVehicle1
{
    public Bateau(DateTime dt)
    {

    }
}

abstract class Transporter : IVehicle
{
    public string? Modele { get; }
}
#endregion GetInstancesFromClassesImplementing_<TInterface>


#region GetInstancesFromGenericClassesImplementing_<TInterface>
interface IVehicle0_
{
    string? Modele { get; }
}
interface IVehicle<T> : IVehicle0_
{
    T? SomeData { get; }
}
abstract class Vehicle<T> : IVehicle<T>
{
    public string? Modele { get; }
    public T? SomeData { get; }

    protected Vehicle()
    {

    }
    protected Vehicle(string modele, T? someData)
    {
        Modele = modele;
        SomeData = someData;
    }
}
class Car<U> : Vehicle<int>, IVehicle<int>
{
    public U? info = default(U);
    public Car() : base()
    {

    }
    public Car(string modele, U info) : base(modele, 100)
    {
        this.info = info;
    }
}
class Moto<U> : Vehicle<int>  //Implémente de façon indirecte IVehicle<int>
{
    public U? info = default(U);

    public Moto() : base()
    {

    }
    public Moto(string modele, U info) : base(modele, 10)
    {
        this.info = info;
    }
}
class Camion : IVehicle<int>
{
    public int SomeData { get; }
    public string? Modele { get; }
}

class Avion<W, X> : IVehicle<int>
{
    public int SomeData { get; }
    public string? Modele { get; }
}
class Helico<W,X,Y> : IVehicle<int>
    where Y : Avion<W,X>
{
    public int SomeData { get; }
    public string? Modele { get; }
}

class Velo<K> : IVehicle0_
{
    public string? Modele { get; }
}

interface IVehicle1_
{

}
class Bateau<T> : IVehicle1_
{
    public Bateau(DateTime dt)
    {

    }
}

abstract class Transporter<U> : IVehicle<int>
{
    public string? Modele { get; }
    public int SomeData { get; }
}
#endregion GetInstancesFromGenericClassesImplementing_<TInterface>