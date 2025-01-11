using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class CheckTests
{
    #region NotNull
    [Fact]
    public void NotNull_WhenNull_ShouldThrowAMustNotBeNullExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        string? myNullableString = null;
        int? myNullableInt = null;


        //--- Act && Assert ---
        var ex1 = Assert.Throws<MustNotBeNullException>(() => Check.NotNull(myNullableString, nameof(myNullableString)));
        var ex2 = Assert.Throws<MustNotBeNullException>(() => Check.NotNull(myNullableInt, nameof(myNullableInt)));

        var expectedMessage1 = string.Format(MustNotBeNullException.MESSAGE_FORMAT, nameof(myNullableString));
        var expectedMessage2 = string.Format(MustNotBeNullException.MESSAGE_FORMAT, nameof(myNullableInt));
        Assert.Equal(expectedMessage1, ex1.Message);
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public void NotNull_WhenNotNull_ShouldNotThrowAnException()
    {
        //--- Arrange ---
        string? myNullableString = "";
        int? myNullableInt = 0;


        //--- Act ---
        Check.NotNull(myNullableString, nameof(myNullableString));
        Check.NotNull(myNullableInt, nameof(myNullableInt));

        //--- Assert ---
        Assert.True(true);
    }
    #endregion NotNull
}
