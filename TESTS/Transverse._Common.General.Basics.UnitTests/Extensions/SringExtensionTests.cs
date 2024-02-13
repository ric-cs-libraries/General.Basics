﻿using System.Text;
using Xunit;


using Transverse._Common.General.Basics.Extensions;
using Transverse._Common.General.Basics.Exceptions;


namespace Transverse._Common.General.Basics.Extensions.String.UnitTests;

public class SringExtensionTests
{
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

        var expectedMessage = $"nbRepeat must be a >=0 integer : '{nbRepeat}' unauthorized.";
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
    public void IsValidIndex_WhenIsInvalidIndex_ShouldReturnFalse(int? index = null, string str= "0123456789")
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
    public void CheckIsValidIndex_WhenIsValidIndex_ShouldnotThrowAnException()
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

        var expectedMessage = $"Invalid string Index : '{invalidIndex}', possible range : [{0},{str.Length - 1}].";
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
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexIsNegative_ShouldThrowAnInvalidNegativeIndexException(int negativeStartIndex)
    {
        var ex = Assert.Throws<MustBePositiveIntegerException>(() => "anyString".Substring_(negativeStartIndex));
        Assert.Equal(ex.Message, $"The index must be a >=0 integer : '{negativeStartIndex}' unauthorized.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-400)]
    public void Substring_WhenSubstringLengthIsMentionnedAndStartIndexIsNegative_ShouldThrowAnInvalidNegativeIndexException(int negativeStartIndex)
    {
        var ex = Assert.Throws<MustBePositiveIntegerException>(() => "anyString".Substring_(negativeStartIndex, 1));
        Assert.Equal(ex.Message, $"The index must be a >=0 integer : '{negativeStartIndex}' unauthorized.");
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
     
}