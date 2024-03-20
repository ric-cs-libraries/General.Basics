using Xunit;


using General.Basics.ErrorHandling;


namespace General.Basics.ErrorHandling.UnitTests;

public class CheckTests
{
    #region NotNull
    [Fact]
    public void NotNull_WhenNull_ShouldThrowANullReferenceExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        string? myNullableString = null;
        

        //--- Act && Assert ---
        var ex = Assert.Throws<NullReferenceException>(() => Check.NotNull(myNullableString!, nameof(myNullableString)));

        var expectedMessage = string.Format(Check.NOT_NULL_MESSAGE_FORMAT, nameof(myNullableString));
        Assert.Equal(expectedMessage, ex.Message);

    }

    [Fact]
    public void NotNull_WhenNotNull_ShouldNotThrowAnException()
    {
        //--- Arrange ---
        string? myNullableString = "";


        //--- Act ---
        Check.NotNull(myNullableString, nameof(myNullableString));

        //--- Assert ---
        Assert.True(true);
    }
    #endregion NotNull
}
