using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using System.Text;
using Xunit;



namespace General.Basics.UnitTests.Extensions.String;

public class SringExtensionTests
{
    #region CheckIsNotEqualToAnyOf_
    [Fact]
    public void CheckIsNotEqualToAnyOf_WhenEquals_ShouldThrowAStringShouldNotBeEqualToAnyOfStringsException()
    {
        //--- Arrange ---
        var str = "hé";
        string[] strings = { "hi", "you!", "HÉ" };
        StringComparison comparisonMode = StringComparison.InvariantCultureIgnoreCase;


        //--- Act & Assert ---
        Assert.Throws<StringShouldNotBeEqualToAnyOfStringsException>(() => str.CheckIsNotEqualToAnyOf_(strings, comparisonMode));
    }
    [Fact]
    public void CheckIsNotEqualToAnyOf_WhenNotEquals_ShouldNotThrowAnException()
    {
        //--- Arrange ---
        var str = "h";
        string[] strings = { "hi", "you" };
        StringComparison comparisonMode = StringComparison.InvariantCulture;


        //--- Act ---
        str.CheckIsNotEqualToAnyOf_(strings, comparisonMode);

        //--- Assert ---
        Assert.True(true);
    }
    [Fact]
    public void CheckIsNotEqualToAnyOf_WhenNoComparisonModeIsProvided_ShouldCompareCaseSensitiveAndNotThrowAnExceptionIfNotEquals()
    {
        //--- Arrange ---
        var str = "hé";
        string[] strings = { "hi", "you!", "HÉ" };

        //--- Act ---
        str.CheckIsNotEqualToAnyOf_(strings);

        //--- Assert ---
        Assert.True(true);
    }
    #endregion CheckIsNotEqualToAnyOf_


    #region EqualsOneOf_
    [Fact]
    public void EqualsOneOf_WhenEqualsByCaseInsensitive_ShouldReturnTrue()
    {
        //--- Arrange ---
        var str = "hé";
        string[] strings = { "hi", "you!", "HÉ" };
        StringComparison comparisonMode = StringComparison.InvariantCultureIgnoreCase;


        //--- Act ---
        var result = str.EqualsOneOf_(strings, comparisonMode);

        //--- Assert ---
        Assert.True(result);
    }
    [Fact]
    public void EqualsOneOf_WhenNotEquals_ShouldReturnFalse()
    {
        //--- Arrange ---
        var str = "hé";
        string[] strings = { "hÉ", "hi", "you" };
        StringComparison comparisonMode = StringComparison.InvariantCulture; //Case sensitive


        //--- Act ---
        var result = str.EqualsOneOf_(strings, comparisonMode);

        //--- Assert ---
        Assert.False(result);
    }
    [Fact]
    public void EqualsOneOf_WhenNoComparisonModeIsProvided_ShouldCompareCaseSensitive()
    {
        //--- Arrange ---
        var str = "hé";
        string[] strings = { "hÉ", "hi", "you" };


        //--- Act ---
        var result = str.EqualsOneOf_(strings);

        //--- Assert ---
        Assert.False(result);
    }
    #endregion EqualsOneOf_

    #region SuffixWithNumberFromAToB_
    [Fact]
    public void SuffixWithNumberFromAToB_WhenBGreaterThanA_ShouldReturnTheCorrectEnumerableContent()
    {
        //--- Arrange ---
        var intSuffixA = -2;
        var intSuffixB = 3;
        var prefix = "LePrefix";


        //--- Act ---
        IEnumerable<string> result = prefix.SuffixWithNumberFromAToB_(intSuffixA, intSuffixB);

        //--- Assert ---
        Assert.Equal(new string[] { $"{prefix}-2", $"{prefix}-1", $"{prefix}0", $"{prefix}1", $"{prefix}2", $"{prefix}3" }, result);
    }

    [Fact]
    public void SuffixWithNumberFromAToB_WhenBEqualsA_ShouldReturnAnEnumerableOf1Element()
    {
        //--- Arrange ---
        var intSuffixA = 2;
        var intSuffixB = intSuffixA;
        var prefix = "LePrefix";


        //--- Act ---
        IEnumerable<string> result = prefix.SuffixWithNumberFromAToB_(intSuffixA, intSuffixB);

        //--- Assert ---
        //Assert.Equal(1, result.Count());
        Assert.Equal(new string[] { $"{prefix}2" }, result);
    }
    [Fact]
    public void SuffixWithNumberFromAToB_WhenBLowerThanA_ShouldReturnAnIntShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        var intSuffixB = 2;
        var intSuffixA = intSuffixB + 1;
        var prefix = "LePrefix";

        //--- Act & Assert ---
        var ex = Assert.Throws<IntShouldBeGreaterOrEqualException>(() => prefix.SuffixWithNumberFromAToB_(intSuffixA, intSuffixB));

        var expectedMessage = string.Format(IntShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "index", intSuffixB, intSuffixA);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion SuffixWithNumberFromAToB_

    #region CheckDoesntOnlyContainSpaces
    [Fact]
    public void CheckDoesntOnlyContainSpaces_WhenOnlyContainsSpaces_ShouldThrowAnStringOnlyContainsSpacesException()
    {
        //--- Arrange ---
        var str1 = " ";
        var str2 = "      ";

        //--- Act & Assert ---
        Assert.Throws<StringOnlyContainsSpacesException>(() => str1.CheckDoesntOnlyContainSpaces_());
        Assert.Throws<StringOnlyContainsSpacesException>(() => str2.CheckDoesntOnlyContainSpaces_());
    }
    [Fact]
    public void CheckDoesntOnlyContainSpaces_WhenDoesntContainTooMany_ShouldNotThrowAnException()
    {
        //--- Arrange ---
        var str1 = "";
        var str2 = "     .   ";
        var str3 = $"    {Environment.NewLine}   ";

        //--- Act ---
        str1.CheckDoesntOnlyContainSpaces_();
        str2.CheckDoesntOnlyContainSpaces_();
        str3.CheckDoesntOnlyContainSpaces_();

        //--- Assert ---
        Assert.True(true);
    }
    #endregion CheckDoesntOnlyContainSpaces

    #region CheckDoesntContainTooManyOfAChar
    [Fact]
    public void CheckDoesntContainTooManyOfAChar_WhenContainsTooMany_ShouldThrowAnStringContainsTooManyOfACharException()
    {
        //--- Arrange ---
        var str = "abcxDxa5x8x";
        char theChar = 'x';
        int maxNbOccurrencesOfTheChar = 4 - 1;

        //--- Act & Assert ---
        var ex = Assert.Throws<StringContainsTooManyOfACharException>(() => str.CheckDoesntContainTooManyOfAChar_(theChar, maxNbOccurrencesOfTheChar));
    }
    [Fact]
    public void CheckDoesntContainTooManyOfAChar_WhenDoesntContainTooMany_ShouldNotThrowAnException()
    {
        //--- Arrange ---
        var str = "abcxDxa5x8";
        char theChar = 'x';
        int maxNbOccurrencesOfTheChar = 3 + 0;

        //--- Act ---
        str.CheckDoesntContainTooManyOfAChar_(theChar, maxNbOccurrencesOfTheChar);

        //--- Assert ---
        Assert.True(true);
    }
    #endregion CheckDoesntContainTooManyOfAChar

    #region CheckDoesntContainIllegalChar
    [Fact]
    public void CheckDoesntContainIllegalChar_WhenContainsOneOrMoreIllegalChars_ShouldThrowAnStringWithIllegalCharException()
    {
        //--- Arrange ---
        var str = "abcxDxa5";
        char[] illegalChars = { '4', 'z', 'x' };

        //--- Act & Assert ---
        var ex = Assert.Throws<StringWithIllegalCharException>(() => str.CheckDoesntContainIllegalChar_(illegalChars));
        Assert.Equal(string.Format(StringWithIllegalCharException.MESSAGE_FORMAT, str, str[3]), ex.Message);
    }
    [Fact]
    public void CheckDoesntContainIllegalChar_WhenDoesntContainIllegalChars_ShouldNotThrowAnException()
    {
        //--- Arrange ---
        var str = "abcxDxa5";
        char[] illegalChars = { '4', 'z' };

        //--- Act ---
        str.CheckDoesntContainIllegalChar_(illegalChars);

        //--- Assert ---
        Assert.True(true);
    }
    #endregion CheckDoesntContainIllegalChar

    #region IsEmptyOrOnlySpaces
    [Fact]
    public void IsEmptyOrOnlySpaces_WhenIsEmptyOrOnlyContainsSpaces_ShouldReturnTrue()
    {
        //--- Arrange ---
        var str1 = "";
        var str2 = "   ";

        //--- Act ---
        var result1 = str1.IsEmptyOrOnlySpaces_();
        var result2 = str2.IsEmptyOrOnlySpaces_();

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void IsEmptyOrOnlySpaces_WhenContainsNotOnlySpaces_ShouldReturnFalse()
    {
        //--- Arrange ---
        var str = "   . ";

        //--- Act ---
        var result = str.IsEmptyOrOnlySpaces_();

        //--- Assert ---
        Assert.False(result);
    }
    #endregion IsEmptyOrOnlySpaces

    #region IsEmpty_
    [Fact]
    public void IsEmpty__WhenFullyEmpty_ShouldReturnTrue()
    {
        //--- Arrange ---
        string str1 = string.Empty;
        string str2 = "";


        //--- Act ---
        var result1 = str1.IsEmpty_();
        var result2 = str2.IsEmpty_();

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
    }
    [Fact]
    public void IsEmpty__WhenNotFullyEmpty_ShouldReturnFalse()
    {
        //--- Arrange ---
        string str1 = " ";
        string str2 = "x";


        //--- Act ---
        var result1 = str1.IsEmpty_();
        var result2 = str2.IsEmpty_();

        //--- Assert ---
        Assert.False(result1);
        Assert.False(result2);
    }
    #endregion IsEmpty_

    #region OnlyContains_
    [Fact]
    public void OnlyContains_WhenOnlyContainsTheChar_ShouldReturnTrue()
    {
        //--- Arrange ---
        char onlyCharToFind1 = 'a';
        var str1 = onlyCharToFind1.ToString().Repeat_(3);

        char onlyCharToFind2 = ' ';
        var str2 = onlyCharToFind2.ToString().Repeat_(3);

        //--- Act ---
        var result1 = str1.OnlyContains_(onlyCharToFind1);
        var result2 = str2.OnlyContains_(onlyCharToFind2);

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void OnlyContains_WhenDoesntOnlyContainsTheChar_ShouldReturnFalse()
    {
        //--- Arrange ---
        char onlyCharToFind = 'a';
        var str = onlyCharToFind.ToString().Repeat_(3) + "A";

        //--- Act ---
        var result = str.OnlyContains_(onlyCharToFind);

        //--- Assert ---
        Assert.False(result);
    }

    [Fact]
    public void OnlyContains_WhenStringIsEmpty_ShouldReturnFalse()
    {
        //--- Arrange ---
        char onlyCharToFind = 'a';
        var str = string.Empty;

        //--- Act ---
        var result = str.OnlyContains_(onlyCharToFind);

        //--- Assert ---
        Assert.False(result);
    }
    #endregion OnlyContains_

    #region GetLastIndex_
    [Fact]
    public void GetLastIndex_WhenStringIsEmpty_ShouldReturnNull()
    {
        var str = "";
        Assert.Null(str.GetLastIndex_());

        str = string.Empty;
        Assert.Null(str.GetLastIndex_());
    }
    [Fact]
    public void GetLastIndex_WhenStringIsNotEmpty_ShouldReturnTheCorrectIndex()
    {
        var str = "123";
        var lastIndex = str.Length - 1;
        Assert.Equal(lastIndex, str.GetLastIndex_());
    }
    #endregion GetLastIndex_


    #region Repeat_
    [Theory]
    [InlineData(3)]
    [InlineData(1000)]
    public void Repeat_WhenNbRepeatIsGreaterThan1_ShouldReturnTheCorrectDuplication(int nbRepeat)
    {
        var str = "a b,";

        var result = str.Repeat_(nbRepeat);

        StringBuilder sb = new StringBuilder();
        foreach (int _ in Enumerable.Range(1, nbRepeat))
        {
            sb.Append(str);
        }
        var expected = sb.ToString();
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Repeat_WhenNbRepeatIs1_ShouldReturnACopyOfTheString()
    {
        var str = "a b,";
        var nbRepeat = 1;

        var result = str.Repeat_(nbRepeat);

        var expected = $"{str}";
        Assert.Equal(expected, result);
        Assert.False(ReferenceEquals(str, result));
    }

    [Fact]
    public void Repeat_WhenNbRepeatIs0_ShouldReturnAnEmptyString()
    {
        var str = "a b,";
        var nbRepeat = 0;

        var result = str.Repeat_(nbRepeat);

        var expected = string.Empty;
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Repeat_WhenNbRepeatIsNegative_ShouldThrowAMustBePositiveIntegerException()
    {
        var str = "a b,";
        var nbRepeat = -1;

        var ex = Assert.Throws<MustBePositiveIntegerException>(() => str.Repeat_(nbRepeat));

        var expectedMessage = string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, "nbRepeat", nbRepeat);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Repeat_

    #region IsValidIndex_
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void IsValidIndex_WhenStringIsEmpty_ShouldReturnFalse(int index)
    {
        var str = "";
        var result = str.IsValidIndex_(index);

        Assert.False(result);
    }

    [Fact]
    public void IsValidIndex_WhenIsValidIndex_ShouldReturnTrue()
    {
        var index = 4;
        var str = "a".Repeat_(index + 1);

        var result = str.IsValidIndex_(index);

        Assert.True(result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(null)] //Cas où il devra mettre pour index : str.Length
    public void IsValidIndex_WhenIsInvalidIndex_ShouldReturnFalse(int? index = null, string str = "0123456789")
    {
        if (!index.HasValue)
        {
            IsValidIndex_WhenIsInvalidIndex_ShouldReturnFalse(str.Length);
        }
        else
        {
            var result = str.IsValidIndex_(index!.Value);
            Assert.False(result);

        }
    }
    #endregion IsValidIndex_

    #region CheckIsValidIndex_
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void CheckIsValidIndex_WhenStringIsEmpty_ShouldThrowAnUnexistingIndexBecauseEmptyException(int index)
    {
        var str = "";

        var ex = Assert.Throws<UnexistingIndexBecauseEmptyException>(() => str.CheckIsValidIndex_(index));

        var expectedMessage = string.Format(UnexistingIndexBecauseEmptyException.MESSAGE_FORMAT, "string", index);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsValidIndex_WhenIsValidIndex_ShouldNotThrowAnException()
    {
        var str = "123";
        var validIndex = str.Length - 1;
        str.CheckIsValidIndex_(validIndex);
        Assert.True(true);
    }

    [Fact]
    public void CheckIsValidIndex_WhenIsInvalidIndex_ShouldThrowAnOutOfRangeIntegerException()
    {
        var str = "123";
        var invalidIndex = str.Length;
        var ex = Assert.Throws<OutOfRangeIntegerException>(() => str.CheckIsValidIndex_(invalidIndex));

        var expectedMessage = string.Format(OutOfRangeIntegerException.MESSAGE_FORMAT, "string Index", invalidIndex, 0, str.Length - 1);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckIsValidIndex_

    #region EndsWith_
    [Fact]
    public void EndsWith_WhenDoesntEndWithAndShould_ShouldEndWith()
    {
        var s = "toto";
        var expectedEnd = "X";

        var result = s.EndsWith_(true, expectedEnd);
        var expected = $"{s}{expectedEnd}";

        Assert.Equal(result, expected);
    }

    [Fact]
    public void EndsWith_WhenDoesntEndWithAndShouldnt_ShouldntEndWith()
    {
        var s = "toto";
        var unexpectedEnd = "X";

        var result = s.EndsWith_(false, unexpectedEnd);
        var expected = s;

        Assert.Equal(result, expected);
    }

    [Fact]
    public void EndsWith_WhenEndsWithAndShouldnt_ShouldntEndWith()
    {
        var unexpectedEnd = "X";
        var withoutEnding = "toto";
        var s = $"{withoutEnding}{unexpectedEnd}";

        var result = s.EndsWith_(false, unexpectedEnd);
        var expected = withoutEnding;

        Assert.Equal(result, expected);
    }

    [Fact]
    public void EndsWith_WhenEndsWithAndShould_ShouldEndWith()
    {
        var expectedEnd = "X";
        var s = $"toto{expectedEnd}";

        var result = s.EndsWith_(true, expectedEnd);
        var expected = s;

        Assert.Equal(result, expected);
    }
    #endregion EndsWith_


    #region StartsWith_
    [Fact]
    public void StartsWith_WhenDoesntStartWithAndShould_ShouldStartWith()
    {
        var s = "toto";
        var expectedStart = "X";

        var result = s.StartsWith_(true, expectedStart);
        var expected = $"{expectedStart}{s}";

        Assert.Equal(result, expected);
    }

    [Fact]
    public void StartsWith_WhenDoesntStartWithAndShouldnt_ShouldntStartWith()
    {
        var s = "toto";
        var unexpectedStart = "X";

        var result = s.StartsWith_(false, unexpectedStart);
        var expected = s;

        Assert.Equal(result, expected);
    }

    [Fact]
    public void StartsWith_WhenStartsWithAndShouldnt_ShouldntStartWith()
    {
        var unexpectedStart = "X";
        var withoutStarting = "toto";
        var s = $"{unexpectedStart}{withoutStarting}";

        var result = s.StartsWith_(false, unexpectedStart);
        var expected = withoutStarting;

        Assert.Equal(result, expected);
    }

    [Fact]
    public void StartsWith_WhenStartsWithAndShould_ShouldStartWith()
    {
        var expectedStart = "X";
        var s = $"{expectedStart}toto";

        var result = s.StartsWith_(true, expectedStart);
        var expected = s;

        Assert.Equal(result, expected);
    }
    #endregion StartsWith_


    #region Substring_
    [Theory]
    [InlineData(3, 400)]
    [InlineData(1, 10)]
    [InlineData(9, 2)]
    public void Substring_WhenSubstringLengthIsMentionnedAndIsTooHighAccordingToAValidStartIndex_ShouldReturnStringFromStartIndexToTheEnd(int validStartIndex, int tooHighSubstringLength)
    {
        var s = "0123456789";

        var result = s.Substring_(validStartIndex, tooHighSubstringLength);
        var expected = s.AsSpan(validStartIndex).ToString();

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(10, 0)]
    [InlineData(400, 0)]
    public void Substring_WhenSubstringLengthIsMentionnedAndStartIndexExceedsStringMaxIndex_ShouldReturnAnEmptyString(int exceedingStartIndex, int anySubstringLength)
    {
        var s = "0123456789";

        var result = s.Substring_(exceedingStartIndex, anySubstringLength);
        var expected = string.Empty;

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(3, 5)]
    [InlineData(3, 7)]
    [InlineData(0, 10)]
    [InlineData(9, 1)]
    public void Substring_WhenSubstringLengthIsMentionnedAndIsValidAccordingToAValidStartIndex_ShouldReturnTheSelectedPart(int validStartIndex, int validSubstringLength)
    {
        var s = "0123456789";

        var result = s.Substring_(validStartIndex, validSubstringLength);
        var expected = s.AsSpan(validStartIndex, validSubstringLength).ToString();

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(9)]
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexIsValid_ShouldReturnStringFromStartIndexToTheEn(int validStartIndex)
    {
        var s = "0123456789";

        var result = s.Substring_(validStartIndex);
        var expected = s.AsSpan(validStartIndex).ToString();

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(400)]
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexExceedsStringMaxIndex_ShouldReturnAnEmptyString(int exceedingStartIndex)
    {
        var s = "0123456789";

        var result = s.Substring_(exceedingStartIndex);
        var expected = string.Empty;

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-400)]
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexIsNegative_ShouldThrowAMustBePositiveIntegerException(int negativeStartIndex)
    {
        var ex = Assert.Throws<MustBePositiveIntegerException>(() => "anyString".Substring_(negativeStartIndex));
        Assert.Equal(ex.Message, string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, "The index", negativeStartIndex));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-400)]
    public void Substring_WhenSubstringLengthIsMentionnedAndStartIndexIsNegative_ShouldThrowAMustBePositiveIntegerException(int negativeStartIndex)
    {
        var ex = Assert.Throws<MustBePositiveIntegerException>(() => "anyString".Substring_(negativeStartIndex, 1));
        Assert.Equal(ex.Message, string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, "The index", negativeStartIndex));
    }
    #endregion Substring_


    #region GetAsShorten_
    [Fact]
    public void GetAsShorten_WhenStringHas10Chars_ShouldReturnTheGoodResult()
    {
        string str = "ABCDEFGHIJ";

        Assert.True(str.GetAsShorten_(0) == string.Empty);
        Assert.True(str.GetAsShorten_(1) == "A");
        Assert.True(str.GetAsShorten_(2) == "AB");
        Assert.True(str.GetAsShorten_(3) == "ABC");
        Assert.True(str.GetAsShorten_(4) == "A...");
        Assert.True(str.GetAsShorten_(5) == "AB...");
        Assert.True(str.GetAsShorten_(6) == "ABC...");
        Assert.True(str.GetAsShorten_(7) == "ABCD...");
        Assert.True(str.GetAsShorten_(8) == "ABCDE...");
        Assert.True(str.GetAsShorten_(9) == "ABCDEF...");
        Assert.True(str.GetAsShorten_(10) == str);
        Assert.True(str.GetAsShorten_(11) == str);
        Assert.True(str.GetAsShorten_(12) == str);
    }

    [Fact]
    public void GetAsShorten_WhenStringHas7Chars_ShouldReturnTheGoodResult()
    {
        string str = "ABCDEFG";

        Assert.True(str.GetAsShorten_(0) == string.Empty);
        Assert.True(str.GetAsShorten_(1) == "A");
        Assert.True(str.GetAsShorten_(2) == "AB");
        Assert.True(str.GetAsShorten_(3) == "ABC");
        Assert.True(str.GetAsShorten_(4) == "A...");
        Assert.True(str.GetAsShorten_(5) == "AB...");
        Assert.True(str.GetAsShorten_(6) == "ABC...");
        Assert.True(str.GetAsShorten_(7) == str);
        Assert.True(str.GetAsShorten_(8) == str);
    }

    [Fact]
    public void GetAsShorten_WhenStringHas4Chars_ShouldReturnTheGoodResult()
    {
        string str = "ABCD";

        Assert.True(str.GetAsShorten_(0) == string.Empty);
        Assert.True(str.GetAsShorten_(1) == "A");
        Assert.True(str.GetAsShorten_(2) == "AB");
        Assert.True(str.GetAsShorten_(3) == "ABC");
        Assert.True(str.GetAsShorten_(4) == str);
        Assert.True(str.GetAsShorten_(5) == str);
        Assert.True(str.GetAsShorten_(6) == str);
        Assert.True(str.GetAsShorten_(7) == str);
        Assert.True(str.GetAsShorten_(8) == str);
    }

    [Fact]
    public void GetAsShorten_WhenStringHas2Chars_ShouldReturnTheGoodResult()
    {
        string str = "AB";

        Assert.True(str.GetAsShorten_(0) == string.Empty);
        Assert.True(str.GetAsShorten_(1) == "A");
        Assert.True(str.GetAsShorten_(2) == str);
        Assert.True(str.GetAsShorten_(3) == str);
        Assert.True(str.GetAsShorten_(4) == str);
    }

    [Fact]
    public void GetAsShorten_WhenStringHas1Char_ShouldReturnTheGoodResult()
    {
        string str = "A";

        Assert.True(str.GetAsShorten_(0) == string.Empty);
        Assert.True(str.GetAsShorten_(1) == str);
        Assert.True(str.GetAsShorten_(2) == str);
        Assert.True(str.GetAsShorten_(3) == str);
        Assert.True(str.GetAsShorten_(4) == str);
    }

    [Fact]
    public void GetAsShorten_WhenStringIsEmpty_ShouldReturnAnEmptystring()
    {
        string str = string.Empty;

        Assert.True(str.GetAsShorten_(0) == string.Empty);
        Assert.True(str.GetAsShorten_(1) == str);
        Assert.True(str.GetAsShorten_(2) == str);
        Assert.True(str.GetAsShorten_(3) == str);
        Assert.True(str.GetAsShorten_(4) == str);
    }
    #endregion GetAsShorten_


    #region HtmlEntities...
    [Fact]
    public void GetWithHtmlEntities_WhenReceivingString_ShouldTransformInHtmlEntitiesWhatCanBe()
    {
        string str = "éàA'èz😀îç";

        string result = str.GetWithHtmlEntities_();
        Assert.Equal("&#233;&#224;A&#39;&#232;z&#128512;&#238;&#231;", result);
    }

    [Fact]
    public void GetWithoutHtmlEntities_WhenReceivingHTMLEntitiesString_ShouldTransformInNonHtmlEntitiesString()
    {
        string str = "&#233;&#224;A&#39;&#232;z&#128512;&#238;&#231;";

        string result = str.GetWithoutHtmlEntities_();
        Assert.Equal("éàA'èz😀îç", result);
    }
    #endregion HtmlEntities...


    #region ChunkExists_
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void ChunkExists_WhenStringIsEmpty_ShouldReturnFalse(int startIndex, int endIndex)
    {
        var str = "";

        var result = str.ChunkExists_(startIndex, endIndex);

        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(Fixtures.UnexistingChunkBoundsData))]
    public void ChunkExists_WhenChunkDoesntExist_ShouldReturnFalse(int startIndex, int endIndex)
    {
        var str = "0123456";

        var result = str.ChunkExists_(startIndex, endIndex);

        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(Fixtures.ExistingChunkBoundsData))]
    public void ChunkExists_WhenChunkExists_ShouldReturnTrue(int startIndex, int endIndex)
    {
        var str = "0123456";

        var result = str.ChunkExists_(startIndex, endIndex);

        Assert.True(result);
    }
    #endregion ChunkExists_

    #region CheckChunkExists_
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void ChunkExists_WhenStringIsEmpty_ShouldThrowAnUnexistingChunkBecauseEmptyException(int startIndex, int endIndex)
    {
        var str = "";

        var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => str.CheckChunkExists_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "string", startIndex, endIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [ClassData(typeof(Fixtures.ExistingChunkBoundsData))]
    public void CheckChunkExists_WhenChunkExists_ShouldNotThrowAnException(int startIndex, int endIndex)
    {
        var str = "0123456";

        str.CheckChunkExists_(startIndex, endIndex);

        Assert.True(true);
    }

    [Theory]
    [ClassData(typeof(Fixtures.UnexistingChunkBoundsData))]
    public void CheckChunkExists_WhenChunkDoesntExist_ShouldThrowAnUnexistingChunkException(int startIndex, int endIndex)
    {
        string str = "0123456";
        var minIndex = 0;
        var maxIndex = str.Length - 1;

        var ex = Assert.Throws<UnexistingChunkException>(() => str.CheckChunkExists_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "string", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion CheckChunkExists_

    #region GetChunk_
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    public void GetChunk_WhenStringIsEmpty_ShouldThrowAnUnexistingChunkBecauseEmptyException(int startIndex, int endIndex)
    {
        string str = "";

        var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => str.GetChunk_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "string", startIndex, endIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [ClassData(typeof(Fixtures.ExistingChunkBoundsDataWithResult))]
    public void GetChunk_WhenChunkExists_ShouldReturnTheCorrectChunk(int startIndex, int endIndex, string expectedChunk)
    {
        string str = "0123456";

        var result = str.GetChunk_(startIndex, endIndex);

        Assert.Equal(expectedChunk, result);
    }

    [Theory]
    [ClassData(typeof(Fixtures.UnexistingChunkBoundsData))]
    public void GetChunk_WhenChunkDoesntExistAndStringNotEmpty_ShouldThrowAnUnexistingChunkException(int startIndex, int endIndex)
    {
        string str = "0123456";
        var minIndex = 0;
        var maxIndex = str.Length - 1;

        var ex = Assert.Throws<UnexistingChunkException>(() => str.GetChunk_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "string", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetChunk_


    #region CapitalizeFirst_
    [Fact]
    public void CapitalizeFirst_WhenStringIsEmpty_ShouldReturnAnEmptyString()
    {
        //--- Arrange ---
        var str = "";


        //--- Act ---
        var result = str.CapitalizeFirst_();

        //--- Assert ---
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("abcd", "Abcd")]
    [InlineData(" bcd", " bcd")]
    [InlineData("Abcd", "Abcd")]
    public void CapitalizeFirst_WhenStringIsNotEmpty_ShouldReturnTheCorrectString(string str, string expectedResult)
    {
        //--- Act ---
        var result = str.CapitalizeFirst_();

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }
    #endregion Capitalize_

    #region ToList_
    [Fact]
    public void ToList_WhenTheStringIsNotEmpty_ShouldReturnTheCorrectListOfMonoCharStrings()
    {
        //--- Arrange ---
        var str = "ABCD";


        //--- Act ---
        var result = str.ToList_();

        //--- Assert ---
        Assert.Equal(new List<string>() { "A", "B", "C", "D" }, result);
    }
    [Fact]
    public void ToList_WhenTheStringIsEmpty_ShouldReturnAnEmptyList()
    {
        //--- Arrange ---
        var str = string.Empty;


        //--- Act ---
        var result = str.ToList_();

        //--- Assert ---
        Assert.Equal(new List<string>() { }, result);
        Assert.Equal(Enumerable.Empty<string>(), result);
    }
    #endregion ToList_

    #region GetFromEndUntil_
    [Theory]
    [ClassData(typeof(Fixtures.GetFromEndUntilData))]
    public void GetFromEndUntil__ShouldReturnTheCorrectChunk(string str, Predicate<char> predicate, string expectedResult)
    {
        //--- Act ---
        var result = str.GetFromEndUntil_(predicate);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }
    #endregion GetFromEndUntil_

    #region GetFromEnd_
    [Fact]
    public void GetFromEnd_WhenFullChunkExists_ShouldReturnTheCorrectChunk()
    {
        var str = "0123456";
        var chunkLength = 4;

        var result = str.GetFromEnd_(chunkLength);

        Assert.Equal("3456", result);
    }

    [Fact]
    public void GetFromEnd_WhenFullChunkExistsAndChunkLengthIsEqualToTheStringLength_ShouldReturnTheFullString()
    {
        var str = "0123456";
        var chunkLength = str.Length;

        var result = str.GetFromEnd_(chunkLength);

        Assert.Equal(str, result);
        Assert.True(str == result);
        Assert.False(ReferenceEquals(str, result));
    }

    [Fact]
    public void GetFromEnd_WhenChunkLengthIsLongerThanTheStringLength_ShouldThrowAnUnexistingChunkException()
    {
        var str = "01234";
        var chunkLength = str.Length + 1;
        var minIndex = 0;
        var maxIndex = str.GetLastIndex_();
        int endIndex = maxIndex!.Value;
        int startIndex = endIndex - (chunkLength - 1);

        var ex = Assert.Throws<UnexistingChunkException>(() => str.GetFromEnd_(chunkLength));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "String", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }

    // I don't want this behavior anymore.
    //[Theory]
    //[InlineData(0)]
    //[InlineData(1)]
    //public void GetFromEnd_WhenStringIsEmpty_ShouldAlwaysThrowAnUnexistingChunkBecauseEmptyException(int chunkLength)
    //{
    //    var str = string.Empty;
    //    int? startIndex = null;
    //    int? endIndex = null;

    //    var ex = Assert.Throws<UnexistingChunkBecauseEmptyException>(() => str.GetFromEnd_(chunkLength));

    //    var expectedMessage = string.Format(UnexistingChunkBecauseEmptyException.MESSAGE_FORMAT, "String", startIndex, endIndex);
    //    Assert.Equal(expectedMessage, ex.Message);
    //}


    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GetFromEnd_WhenStringIsEmptyAndChunkLengthIsNotNegative_ShouldAlwaysReturnAnEmptyString(int chunkLength)
    {
        var str = string.Empty;

        var result = str.GetFromEnd_(chunkLength);

        Assert.Empty(result);
    }

    [Fact]
    public void GetFromEnd_WhenChunkLengthIsZero_ShouldAlwaysReturnAnEmptyString()
    {
        var chunkLength = 0;
        var str1 = "01234";
        var str2 = string.Empty;

        var result1 = str1.GetFromEnd_(chunkLength);
        var result2 = str2.GetFromEnd_(chunkLength);

        Assert.Empty(result1);
        Assert.Empty(result2);
    }

    [Fact]
    public void GetFromEnd_WhenChunkLengthIsNegative_ShouldAlwaysThrowMustBePositiveIntegerException()
    {
        var str1 = "01234";
        var str2 = string.Empty;
        var chunkLength = -1;

        var ex1 = Assert.Throws<MustBePositiveIntegerException>(() => str1.GetFromEnd_(chunkLength));
        var ex2 = Assert.Throws<MustBePositiveIntegerException>(() => str2.GetFromEnd_(chunkLength));

        var expectedMessage1 = string.Format(MustBePositiveIntegerException.MESSAGE_FORMAT, "Chunk length", chunkLength);
        var expectedMessage2 = expectedMessage1;
        Assert.Equal(expectedMessage1, ex1.Message);
        Assert.Equal(expectedMessage2, ex2.Message);
    }
    #endregion GetFromEnd_


    #region ReplacePlaceHolders_
    [Fact]
    public void ReplacePlaceHolders_WhenEmpty_ShouldReturnTheOriginalString()
    {
        //--- Arrange ---
        string originalString = "aBCDEF GZH";
        string replacement1 = "qq";
        string replacement2 = "u";
        string replacement3 = "0x20";
        Dictionary<string, string> replacements = new() { { "Z", replacement1 }, { "a", replacement2 }, { " ", replacement3 } };

        //--- Act ---
        string result = originalString.ReplacePlaceHolders_(replacements);

        //--- Assert ---
        string expectedResult = $"{replacement2}BCDEF{replacement3}G{replacement1}H";
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ReplacePlaceHolders_WhenDicoContainsNoMatchWithOriginalString_ShouldReturnTheOriginalString()
    {
        //--- Arrange ---
        string originalString = "ABCDEFGH";
        Dictionary<string, string> replacements = new() { {"a", "x" }, {"Z", "qq" } };

        //--- Act ---
        string result = originalString.ReplacePlaceHolders_(replacements);

        //--- Assert ---
        Assert.Equal(originalString, result);
        Assert.True(object.ReferenceEquals(originalString, result));
    }

    [Fact]
    public void ReplacePlaceHolders_WhenDicoIsEmpty_ShouldReturnTheOriginalString()
    {
        //--- Arrange ---
        string originalString = "ABCDEFGH";
        Dictionary<string, string> replacements = new();

        //--- Act ---
        string result = originalString.ReplacePlaceHolders_(replacements);

        //--- Assert ---
        Assert.Equal(originalString, result);
        Assert.True(object.ReferenceEquals(originalString, result));
    }
    #endregion ReplacePlaceHolders_

    #region Backslash_
    [Fact]
    public void Backslash_ShouldReturnTheCorrectString()
    {
        //--- Arrange ---
        string originalString = @"https://aaa\bbb/cc\uu\kk/ll/xxx.com";

        //--- Act ---
        var result = originalString.Backslash_();

        //--- Assert ---
        var expectedResult = @"https:\\aaa\bbb\cc\uu\kk\ll\xxx.com";
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }
    #endregion Backslash_

    #region Slash_
    [Fact]
    public void Slash_ShouldReturnTheCorrectString()
    {
        //--- Arrange ---
        string originalString = @"https:\\aaa/bbb\cc/uu/kk\ll\xxx.com";

        //--- Act ---
        var result = originalString.Slash_();

        //--- Assert ---
        var expectedResult = @"https://aaa/bbb/cc/uu/kk/ll/xxx.com";
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }
    #endregion Slash_

    #region Split_(stringSeparator)
    [Fact]
    public void Split_WithStringSeparator_WhenMatchFoundForSplitting_ShouldReturnAnArrayWithSplittedPartsInSameOrder()
    {
        //--- Arrange ---
        string separatorString = " :!";
        string part1 = "AX";
        string part2 = "B ";
        string part3 = "CD:!";
        string part4 = "  ";
        string subjectString = $"{part1}{separatorString}{part2}{separatorString}{part3}{separatorString}{part4}";

        //--- Act ---
        string[] result = subjectString.Split_(separatorString);

        //--- Assert ---
        string[] expectedResult = new[] { part1, part2, part3, part4 };
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }

    [Fact]
    public void Split_WithStringSeparator_WhenSubjectStringIsEmpty_ShouldReturnAnEmptyArrayOfString()
    {
        //--- Arrange ---
        string subjectString = string.Empty;
        string separatorString = " :!";

        //--- Act ---
        string[] result = subjectString.Split_(separatorString);

        //--- Assert ---
        string[] expectedResult = Array.Empty<string>();
        Assert.Equal(expectedResult, result);
        Assert.True(object.ReferenceEquals(expectedResult, result));
        Assert.False(object.ReferenceEquals(new string[] { }, result));
    }

    [Fact]
    public void Split_WithStringSeparator_WhenNoMatchForSplitting_ShouldReturnAnArrayOnlyContainingTheSubjectString()
    {
        //--- Arrange ---
        string separatorString = " :!";
        string subjectString = "A:!B";

        //--- Act ---
        string[] result = subjectString.Split_(separatorString);

        //--- Assert ---
        string[] expectedResult = new[] { subjectString };
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }

    [Fact]
    public void Split_WithStringSeparator_WhenSeparatorIsEmpty_ShouldReturnAnArrayOnlyContainingTheSubjectString()
    {
        //--- Arrange ---
        string separatorString = string.Empty;
        string subjectString = "A:!B";

        //--- Act ---
        string[] result = subjectString.Split_(separatorString);

        //--- Assert ---
        string[] expectedResult = new[] { subjectString };
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }
    #endregion Split_(stringSeparator)

    #region Split_(charSeparator)
    [Fact]
    public void Split_WithCharSeparator_WhenMatchFoundForSplitting_ShouldReturnAnArrayWithSplittedPartsInSameOrder()
    {
        //--- Arrange ---
        char charSeparator = 'x';
        string part1 = "AX";
        string part2 = "B ";
        string part3 = "CD:!";
        string part4 = "  ";
        string subjectString = $"{part1}{charSeparator}{part2}{charSeparator}{part3}{charSeparator}{part4}";

        //--- Act ---
        string[] result = subjectString.Split_(charSeparator);

        //--- Assert ---
        string[] expectedResult = new[] { part1, part2, part3, part4 };
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }

    [Fact]
    public void Split_WithCharSeparator_WhenSubjectStringIsEmpty_ShouldReturnAnEmptyArrayOfString()
    {
        //--- Arrange ---
        string subjectString = string.Empty;
        char charSeparator = 'x';

        //--- Act ---
        string[] result = subjectString.Split_(charSeparator);

        //--- Assert ---
        string[] expectedResult = Array.Empty<string>();
        Assert.Equal(expectedResult, result);
        Assert.True(object.ReferenceEquals(expectedResult, result));
        Assert.False(object.ReferenceEquals(new string[] {}, result));
    }

    [Fact]
    public void Split_WithCharSeparator_WhenNoMatchForSplitting_ShouldReturnAnArrayOnlyContainingTheSubjectString()
    {
        //--- Arrange ---
        char charSeparator = 'x';
        string subjectString = "A:!B";

        //--- Act ---
        string[] result = subjectString.Split_(charSeparator);

        //--- Assert ---
        string[] expectedResult = new[] { subjectString };
        Assert.Equal(expectedResult, result);
        Assert.False(object.ReferenceEquals(expectedResult, result));
    }
    #endregion Split_(charSeparator)

    //===========================================================
    class Fixtures
    {
        internal class GetFromEndUntilData : TheoryData<string, Predicate<char>, string>
        {
            public GetFromEndUntilData()
            {
                var fullStr = "012345689";

                Add(fullStr, (c) => c == '4', fullStr.Substring(5));
                Add("", (c) => c == '4', "");
                Add(fullStr, (c) => c == 'A', fullStr);
                Add(fullStr, (c) => c == fullStr.Last(), "");
            }
        }

        internal class UnexistingChunkBoundsData : TheoryData<int, int>
        {
            public UnexistingChunkBoundsData()
            {
                Add(-1, 0);
                Add(1, 7);
                Add(7, 8);
                Add(3, 1);
                Add(1, 0);
            }
        }
        internal class ExistingChunkBoundsData : TheoryData<int, int>
        {
            public ExistingChunkBoundsData()
            {
                Add(0, 0);
                Add(0, 1);
                Add(0, 6);
                Add(1, 6);
                Add(3, 5);
                Add(6, 6);
            }
        }
        internal class ExistingChunkBoundsDataWithResult : TheoryData<int, int, string>
        {
            public ExistingChunkBoundsDataWithResult()
            {
                Add(0, 0, "0");
                Add(0, 1, "01");
                Add(0, 6, "0123456");
                Add(1, 6, "123456");
                Add(3, 5, "345");
                Add(6, 6, "6");
            }
        }

    }
}
