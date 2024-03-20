using Xunit;


using General.Basics.ErrorHandling;

using General.Basics.Extensions;


namespace General.Basics.Extensions.UnitTests;

public class IntExtensionTests
{
    #region CheckIsGreaterOrEqualTo_
    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenIntegerHasNotTheMinimalValueAndSubjectNameNotGiven_ShouldThrowAnIntShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myInt = 9;
        var minimalValue = 10;

        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => myInt.CheckIsGreaterOrEqualTo_(minimalValue));

        string subjectName = "number";
        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, subjectName, myInt, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenIntegerHasNotTheMinimalValueAndSubjectNameGiven_ShouldThrowAnIntShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myInt = 9;
        var minimalValue = 10;
        string subjectName = nameof(myInt);

        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => myInt.CheckIsGreaterOrEqualTo_(minimalValue, subjectName));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, subjectName, myInt, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenIntegerHasTheMinimalValueOrGreater_ShouldNotThrowAnException()
    {
        var minimalValue = 10;
        var myInt1 = minimalValue;
        var myInt2 = minimalValue + 1;

        myInt1.CheckIsGreaterOrEqualTo_(minimalValue);
        myInt2.CheckIsGreaterOrEqualTo_(minimalValue);
        myInt1.CheckIsGreaterOrEqualTo_(minimalValue, "xxx");
        myInt2.CheckIsGreaterOrEqualTo_(minimalValue, "zzz");

        Assert.True(true);
    }
    #endregion CheckIsGreaterOrEqualTo_

}
