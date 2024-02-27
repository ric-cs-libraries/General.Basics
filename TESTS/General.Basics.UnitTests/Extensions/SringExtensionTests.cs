﻿using System.Text;
using Xunit;


using General.Basics.Extensions;
using General.Basics.Exceptions;
using System.Security.Cryptography;

namespace General.Basics.Extensions.UnitTests;

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
        int maxNbOccurrencesOfTheChar = 3+0;

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
    public void GetLastIndex_WhenEnumerableIsNotEmpty_ShouldReturnTheCorrectIndex()
    {
        var str = "123";
        var lastIndex = str.Length - 1;
        Assert.Equal(lastIndex, str.GetLastIndex_());
    }
    [Fact]
    public void GetLastIndex_WhenEnumerableIsEmpty_ShouldReturnNull()
    {
        var str = "";
        Assert.Null(str.GetLastIndex_());

        str = string.Empty;
        Assert.Null(str.GetLastIndex_());
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
        Assert.False(object.ReferenceEquals(str, result));
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
    [ClassData(typeof(UnexistingChunkBoundsData))]
    public void ChunkExists_WhenChunkDoesntExist_ShouldReturnFalse(int startIndex, int endIndex)
    {
        var str = "0123456";

        var result = str.ChunkExists_(startIndex, endIndex);

        Assert.False(result);
    }

    [Theory]
    [ClassData(typeof(ExistingChunkBoundsData))]
    public void ChunkExists_WhenChunkExists_ShouldReturnTrue(int startIndex, int endIndex)
    {
        var str = "0123456";

        var result = str.ChunkExists_(startIndex, endIndex);

        Assert.True(result);
    }
    #endregion ChunkExists_


    #region CheckChunkExists_
    [Theory]
    [ClassData(typeof(ExistingChunkBoundsData))]
    public void CheckChunkExists_WhenChunkExists_ShouldNotThrowAnException(int startIndex, int endIndex)
    {
        var str = "0123456";

        str.CheckChunkExists_(startIndex, endIndex);

        Assert.True(true);
    }

    [Theory]
    [ClassData(typeof(UnexistingChunkBoundsData))]
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
    [ClassData(typeof(ExistingChunkBoundsDataWithResult))]
    public void GetChunk_WhenChunkExists_ShouldReturnTheCorrectChunk(int startIndex, int endIndex, string expectedChunk)
    {
        string str = "0123456";

        var result = str.GetChunk_(startIndex, endIndex);

        Assert.Equal(expectedChunk, result);
    }

    [Theory]
    [ClassData(typeof(UnexistingChunkBoundsData))]
    public void GetChunk_WhenChunkDoesntExist_ShouldThrowAnUnexistingChunkException(int startIndex, int endIndex)
    {
        string str = "0123456";
        var minIndex = 0;
        var maxIndex = str.Length - 1;

        var ex = Assert.Throws<UnexistingChunkException>(() => str.GetChunk_(startIndex, endIndex));

        var expectedMessage = string.Format(UnexistingChunkException.MESSAGE_FORMAT, "string", startIndex, endIndex, minIndex, maxIndex);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetChunk_


    //---------------------------------------------------------
    class UnexistingChunkBoundsData : TheoryData<int, int>
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
    class ExistingChunkBoundsData : TheoryData<int, int>
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
    class ExistingChunkBoundsDataWithResult : TheoryData<int, int, string>
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