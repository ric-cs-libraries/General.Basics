using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;

namespace General.Basics.ReflectionExtended.POO.UnitTests;

public class AbstractClassTests
{
    [Fact]
    public void Instanciation_WhenParamDescribesTheAppropriateType_ShouldCreateAndInitializeCorrectlyTheInstance()
    {
        //--- Arrange ---
        Type type = typeof(MyAbstractClass);
        Type type2 = typeof(MyAbstractRecord);

        //--- Act ---
        AbstractClass abstractClass = new(type);
        AbstractClass abstractRecord = new(type2);

        //--- Assert ---
        Assert.Equal(abstractClass.Type, type);
        Assert.Equal(abstractRecord.Type, type2);
    }

    [Fact]
    public void Instanciation_WhenParamDoesntDescribeTheAppropriateType_ShouldThrowATypeShouldBeAClassTypeException()
    {
        //--- Arrange ---
        Type type1 = typeof(MyConcreteClass);
        Type type2 = typeof(IMyInterface);

        //--- Act & Assert ---
        var ex1 = Assert.Throws<TypeShouldBeAnAbstractClassTypeException>(() => new AbstractClass(type1));
        var expectedMessage1 = string.Format(TypeShouldBeAnAbstractClassTypeException.MESSAGE_FORMAT, type1.GetFullName_(), TypeShouldBeAnAbstractClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage1, ex1.Message);

        var ex2 = Assert.Throws<TypeShouldBeAnAbstractClassTypeException>(() => new AbstractClass(type2));
        var expectedMessage2 = string.Format(TypeShouldBeAnAbstractClassTypeException.MESSAGE_FORMAT, type2.GetFullName_(), TypeShouldBeAnAbstractClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public void Equality_When2AbstractClassInstancesDescribeTheExactSameType_TheyShouldBeEqual()
    {
        //--- Arrange ---
        Type type = typeof(MyAbstractClass);
        Type type2 = typeof(MyAbstractRecord);

        //--- Act ---
        AbstractClass abstractClass1 = new(type);
        AbstractClass abstractClass2 = new(type);
        AbstractClass abstractRecord1 = new(type2);
        AbstractClass abstractRecord2 = new(type2);


        //--- Assert ---
        Assert.Equal(abstractClass1, abstractClass2);
        Assert.True(abstractClass1 == abstractClass2);
        //Assert.True(abstractClass1.Equals(abstractClass2));
        Assert.False(object.ReferenceEquals(abstractClass1, abstractClass2));

        Assert.Equal(abstractRecord1, abstractRecord2);
        Assert.True(abstractRecord1 == abstractRecord2);
        //Assert.True(abstractRecord1.Equals(abstractRecord2));
        Assert.False(object.ReferenceEquals(abstractRecord1, abstractRecord2));
    }

    [Fact]
    public void Equality_When2AbstractClassInstancesDescribeNotTheExactSameType_TheyShouldNotBeEqual()
    {
        //--- Arrange ---
        Type type1 = typeof(MyAbstractClass);
        Type type2 = typeof(MyAbstractClass2); //Hérite directement de MyAbstractClass

        //--- Act ---
        AbstractClass abstractClass1 = new(type1);
        AbstractClass abstractClass2 = new(type2);

        //--- Assert ---
        Assert.NotEqual(abstractClass1, abstractClass2);
        Assert.True(abstractClass1 != abstractClass2);
        //Assert.True(!abstractClass1.Equals(abstractClass2));
    }
}


abstract class MyAbstractClass2 : MyAbstractClass
{

}

abstract record MyAbstractRecord
{

}
abstract record MyAbstractRecord2 : MyAbstractRecord
{

}