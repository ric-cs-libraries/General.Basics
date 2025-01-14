using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using Xunit;


namespace General.Basics.ReflectionExtended.POO.UnitTests;

public class ClassTests
{
    [Fact]
    public void Instanciation_WhenParamDescribesTheAppropriateType_ShouldCreateAndInitializeCorrectlyTheInstance()
    {
        //--- Arrange ---
        Type type1 = typeof(MyConcreteClass);
        Type type2 = typeof(MyAbstractClass);
        Type type3 = typeof(MyConcreteRecord);


        //--- Act ---
        Class class1 = new(type1);
        Class class2 = new(type2);
        Class record3 = new(type3);

        //--- Assert ---
        Assert.Equal(class1.Type, type1);
        Assert.Equal(class2.Type, type2);
        Assert.Equal(record3.Type, type3);
    }

    [Fact]
    public void Instanciation_WhenParamDoesntDescribeTheAppropriateType_ShouldThrowATypeShouldBeAClassTypeException()
    {
        //--- Arrange ---
        Type type = typeof(IMyInterface);

        //--- Act & Assert ---
        var ex = Assert.Throws<TypeShouldBeAClassTypeException>(() => new Class(type));

        var expectedMessage = string.Format(TypeShouldBeAClassTypeException.MESSAGE_FORMAT, type.GetFullName_(), TypeShouldBeAClassTypeException.EXPECTED_TYPE_LABEL);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void Equality_When2ClassInstancesDescribeTheExactSameType_TheyShouldBeEqual()
    {
        //--- Arrange ---
        Type type = typeof(MyConcreteClass);
        Type type2 = typeof(MyConcreteRecord);

        //--- Act ---
        Class class1 = new(type);
        Class class2 = new(type);
        Class record3 = new(type2);
        Class record4 = new(type2);

        //--- Assert ---
        Assert.Equal(class1, class2);
        Assert.True(class1 == class2);
        //Assert.True(class1.Equals(class2));
        Assert.False(object.ReferenceEquals(class1, class2));

        Assert.Equal(record3, record4);
        Assert.True(record3 == record4);
        //Assert.True(record3.Equals(record4));
        Assert.False(object.ReferenceEquals(record3, record4));
    }

    [Fact]
    public void Equality_When2ClassInstancesDescribeNotTheExactSameType_TheyShouldNotBeEqual()
    {
        //--- Arrange ---
        Type type1 = typeof(MyConcreteClass);
        Type type2 = typeof(MyConcreteClass2); //Hérite directement de MyConcreteClass

        //--- Act ---
        Class class1 = new(type1);
        Class class2 = new(type2);

        //--- Assert ---
        Assert.NotEqual(class1, class2);
        Assert.True(class1 != class2);
        //Assert.True(!class1.Equals(class2));
    }

}

class MyConcreteClass2 : MyConcreteClass
{

}

record MyConcreteRecord
{

}
record MyConcreteRecord2 : MyConcreteRecord
{

}