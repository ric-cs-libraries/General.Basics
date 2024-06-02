using Xunit;


using General.Basics.ErrorHandling;

using General.Basics.Generators.Interfaces;
using General.Basics.Generators.Extensions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace General.Basics.Generators.Extensions.UnitTests;

// >>>> PARTIALLY TESTED because : Randomness is not testable !
public class IEnumerableExtensionTests
{
    #region GetRandomElement_

    //[Fact] //Randomness Non testable, mais sert pour debugage éventuel.
    //public void GetRandomElement_WhenEnumerableIsNotEmpty_ShouldReturnARandomElement()
    //{
    //    //--- Arrange ---
    //    IEnumerable<string> list = new List<string>() { "AA", "BB", "CCC", "D", "EEE", "FF", "GGGGG", "HH", "II", "JJJ", "KKKKK" };

    //    //--- Act & Assert
    //    var randomElements = Enumerable.Range(1, 30).Select(n =>  list.GetRandomElement_());

    //    //
    //    Assert.True(true);
    //}

    [Fact]
    public void GetRandomElement_WhenEnumerableIsEmpty_ShouldThrowACannotSearchElementBecauseEmptyException()
    {
        //--- Arrange ---
        IEnumerable<string> list = new List<string>();

        //--- Act & Assert
        var ex = Assert.Throws<CannotSearchElementBecauseEmptyException>(() => list.GetRandomElement_());

        var expectedMessage = string.Format(CannotSearchElementBecauseEmptyException.MESSAGE_FORMAT, "enumerable");
        Assert.Equal(expectedMessage, ex.Message);

    }
    #endregion GetRandomElement_

}
