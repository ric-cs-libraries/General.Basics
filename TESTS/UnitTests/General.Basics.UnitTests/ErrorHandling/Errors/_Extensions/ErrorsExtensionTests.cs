using General.Basics.ErrorHandling;
using General.Basics.ErrorHandling.Errors.Extensions;
using Xunit;

namespace General.Basics.UnitTests.ErrorHandling.Errors.Extensions
{
    public class ErrorsExtensionTests
    {
        [Fact]
        public void ToException_ForErrors_ShouldReturnTheCorrectlyInitializedException()
        {
            //--- Arrange ---
            string errCode = "someErrCode";
            string debugMessageTemplate = "Herer it is : {0}, {1}";
            IEnumerable<string> placeholderValues = new[] { "val1", "val2" };

            Error error1 = new(errCode, debugMessageTemplate, placeholderValues);
            Error error1b = new(errCode, debugMessageTemplate, placeholderValues);
            ErrorWithOptionalCode error2 = new(debugMessageTemplate, placeholderValues);
            Error error3 = new("otherCode", debugMessageTemplate, placeholderValues);

            var otherSeparator = " ; ";

            //--- Act ---
            Exception ex = new[] { error1, error1b, error2, error3 }.ToException();
            Exception ex2 = new[] { error1, error2 }.ToException(otherSeparator);

            //--- Assert ---
            var defaultSeparator = ErrorsExtension.DEFAULT_SEPARATOR_BETWEEN_ERRORS;
            Assert.Equal($"{error1}{defaultSeparator}{error1b}{defaultSeparator}{error2}{defaultSeparator}{error3}", ex.Message);
            Assert.Equal(new Dictionary<object, object>() {
                { error1.Code, error1.ToString() },
                { error3.Code, error3.ToString() },
            }, ex.Data); //error1b not added because has same key (code) as error1 !
                         //error2 not added because has no code.
            Assert.Equal(2, ex.Data.Count);

            //

            Assert.Equal($"{error1}{otherSeparator}{error2}", ex2.Message);
            Assert.Equal(new Dictionary<object, object>() { { error1.Code, error1.ToString() } }, ex2.Data); //error2 not added because has no code.
            Assert.Single(ex2.Data);
        }

        [Fact]
        public void ToException_ForError_ShouldReturnTheCorrectlyInitializedException()
        {
            //--- Arrange ---
            string debugMessageTemplate = "Herer it is : {0}, {1}";
            IEnumerable<string> placeholderValues = new[] { "val1", "val2" };
            var someIntegerCode = "-1800";

            Error error1 = new("someErrCode", debugMessageTemplate, placeholderValues);
            ErrorWithOptionalCode error2 = new(debugMessageTemplate, placeholderValues);
            Error error3 = new(someIntegerCode, debugMessageTemplate, placeholderValues);

            //--- Act ---
            Exception ex1 = error1.ToException();
            Exception ex2 = error2.ToException();
            Exception ex3 = error3.ToException();

            //--- Assert ---
            Assert.Equal($"{error1}", ex1.Message);
            Assert.Equal(new Dictionary<object, object>() { { error1.Code, error1.ToString() } }, ex1.Data);
            Assert.Single(ex1.Data);

            Assert.Equal($"{error2}", ex2.Message);
            Assert.Empty(ex2.Data);//error2 has no code so not added to ex2.Data. 

            Assert.Equal($"{error3}", ex3.Message);
            Assert.Equal(new Dictionary<object, object>() { { error3.Code, error3.ToString() } }, ex3.Data);
            Assert.Single(ex3.Data);
            Assert.Equal(int.Parse(someIntegerCode), ex3.HResult); //Because error3 Code is an int.
        }
    }
}
