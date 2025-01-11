using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;


namespace General.Basics.ReflectionExtended.POO.UnitTests;

public class InterfaceTests
{
    [Fact]
    public void Instanciation_WhenParamDescribesTheAppropriateType_ShouldCreateAndInitializeCorrectlyTheInstance()
    {
        //--- Arrange ---
        Type type = typeof(IMyInterface);

        //--- Act ---
        Interface @interface = new(type);

        //--- Assert ---
        Assert.Equal(@interface.Type, type);
    }

    [Fact]
    public void Instanciation_WhenParamDoesntDescribeTheAppropriateType_ShouldThrowATypeShouldBeAClassTypeException()
    {
        //--- Arrange ---
        Type type1 = typeof(MyConcreteClass);
        Type type2 = typeof(MyAbstractClass);

        //--- Act & Assert ---
        var ex1 = Assert.Throws<TypeShouldBeAnInterfaceTypeException>(() => new Interface(type1));
        var expectedMessage1 = string.Format(TypeShouldBeAnInterfaceTypeException.MESSAGE_FORMAT, type1.GetFullName_(), TypeShouldBeAnInterfaceTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage1, ex1.Message);

        var ex2 = Assert.Throws<TypeShouldBeAnInterfaceTypeException>(() => new Interface(type2));
        var expectedMessage2 = string.Format(TypeShouldBeAnInterfaceTypeException.MESSAGE_FORMAT, type2.GetFullName_(), TypeShouldBeAnInterfaceTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public void Equality_When2InterfaceInstancesDescribeTheExactSameType_TheyShouldBeEqual()
    {
        //--- Arrange ---
        Type type = typeof(IMyInterface);

        //--- Act ---
        Interface @interface1 = new(type);
        Interface @interface2 = new(type);

        //--- Assert ---
        Assert.Equal(@interface1, @interface2);
        Assert.True(@interface1 == @interface2);
        //Assert.True(@interface1.Equals(@interface2));
        Assert.False(object.ReferenceEquals(@interface1, @interface2));
    }

    [Fact]
    public void Equality_When2InterfaceInstancesDescribeNotTheExactSameType_TheyShouldNotBeEqual()
    {
        //--- Arrange ---
        Type type1 = typeof(IMyInterface);
        Type type2 = typeof(IMyInterface2); //Hérite directement de IMyInterface

        //--- Act ---
        Interface @interface1 = new(type1);
        Interface @interface2 = new(type2);

        //--- Assert ---
        Assert.NotEqual(@interface1, @interface2);
        Assert.True(@interface1 != @interface2);
        //Assert.True(!@interface1.Equals(@interface2));
    }
}

interface IMyInterface2 : IMyInterface
{

}