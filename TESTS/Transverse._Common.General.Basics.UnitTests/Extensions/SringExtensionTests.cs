using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Transverse._Common.General.Basics.Extensions;
using Transverse._Common.General.Basics.Exceptions;

//using Moq;


namespace Transverse._Common.String.UnitTests;

[TestClass]
public class SringExtensionTests
{
    [TestMethod]
    public void EndsWith_WhenDoesntEndWithAndShould_ShouldEndWith()
    {
        var s = "toto";
        var expectedEnd = "X";

        var result = s.EndsWith_(true, expectedEnd);
        var expected = $"{s}{expectedEnd}";

        result.Should().Be(expected);
    }

    [TestMethod]
    public void EndsWith_WhenDoesntEndWithAndShouldnt_ShouldntEndWith()
    {
        var s = "toto";
        var unexpectedEnd = "X";

        var result = s.EndsWith_(false, unexpectedEnd);
        var expected = s;

        result.Should().Be(expected);
    }

    [TestMethod]
    public void EndsWith_WhenEndsWithAndShouldnt_ShouldntEndWith()
    {
        var unexpectedEnd = "X";
        var withoutEnding = "toto";
        var s = $"{withoutEnding}{unexpectedEnd}";

        var result = s.EndsWith_(false, unexpectedEnd);
        var expected = withoutEnding;

        result.Should().Be(expected);
    }

    [TestMethod]
    public void EndsWith_WhenEndsWithAndShould_ShouldEndWith()
    {
        var expectedEnd = "X";
        var s = $"toto{expectedEnd}";

        var result = s.EndsWith_(true, expectedEnd);
        var expected = s;

        result.Should().Be(expected);
    }

    [TestMethod]
    public void StartsWith_WhenDoesntStartWithAndShould_ShouldStartWith()
    {
        var s = "toto";
        var expectedStart = "X";

        var result = s.StartsWith_(true, expectedStart);
        var expected = $"{expectedStart}{s}";

        result.Should().Be(expected);
    }

    [TestMethod]
    public void StartsWith_WhenDoesntStartWithAndShouldnt_ShouldntStartWith()
    {
        var s = "toto";
        var unexpectedStart = "X";

        var result = s.StartsWith_(false, unexpectedStart);
        var expected = s;

        result.Should().Be(expected);
    }

    [TestMethod]
    public void StartsWith_WhenStartsWithAndShouldnt_ShouldntStartWith()
    {
        var unexpectedStart = "X";
        var withoutStarting = "toto";
        var s = $"{unexpectedStart}{withoutStarting}";

        var result = s.StartsWith_(false, unexpectedStart);
        var expected = withoutStarting;

        result.Should().Be(expected);
    }

    [TestMethod]
    public void StartsWith_WhenStartsWithAndShould_ShouldStartWith()
    {
        var expectedStart = "X";
        var s = $"{expectedStart}toto";

        var result = s.StartsWith_(true, expectedStart);
        var expected = s;

        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(3, 400)]
    [DataRow(1, 10)]
    [DataRow(9, 2)]
    public void Substring_WhenSubstringLengthIsMentionnedAndIsTooHighAccordingToAValidStartIndex_ShouldReturnStringFromStartIndexToTheEnd(int validStartIndex, int tooHighSubstringLength)
    {
        var s = "0123456789";

        var result = s.Substring_(validStartIndex, tooHighSubstringLength);
        var expected = s.AsSpan(validStartIndex).ToString();

        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(10, 1)]
    [DataRow(10, 0)]
    [DataRow(400, 0)]
    public void Substring_WhenSubstringLengthIsMentionnedAndStartIndexExceedsStringMaxIndex_ShouldReturnAnEmptyString(int exceedingStartIndex, int anySubstringLength)
    {
        var s = "0123456789";

        var result = s.Substring_(exceedingStartIndex, anySubstringLength);
        var expected = string.Empty;

        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(3, 5)]
    [DataRow(3, 7)]
    [DataRow(0, 10)]
    [DataRow(9, 1)]
    public void Substring_WhenSubstringLengthIsMentionnedAndIsValidAccordingToAValidStartIndex_ShouldReturnTheSelectedPart(int validStartIndex, int validSubstringLength)
    {
        var s = "0123456789";

        var result = s.Substring_(validStartIndex, validSubstringLength);
        var expected = s.AsSpan(validStartIndex, validSubstringLength).ToString();

        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(5)]
    [DataRow(9)]
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexIsValid_ShouldReturnStringFromStartIndexToTheEn(int validStartIndex)
    {
        var s = "0123456789";

        var result = s.Substring_(validStartIndex);
        var expected = s.AsSpan(validStartIndex).ToString();

        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(10)]
    [DataRow(400)]
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexExceedsStringMaxIndex_ShouldReturnAnEmptyString(int exceedingStartIndex)
    {
        var s = "0123456789";

        var result = s.Substring_(exceedingStartIndex);
        var expected = string.Empty;

        result.Should().Be(expected);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(-400)]
    public void Substring_WhenSubstringLengthIsNotMentionnedAndStartIndexIsNegative_ShouldThrowAnInvalidNegativeIndexException(int negativeStartIndex)
    {
        "anyString".Invoking(str => str.Substring_(negativeStartIndex))
                   .Should().Throw<InvalidNegativeIndexException>()
                   .WithMessage($"Negative index({negativeStartIndex}) unauthorized.")
                   ;
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(-400)]
    public void Substring_WhenSubstringLengthIsMentionnedAndStartIndexIsNegative_ShouldThrowAnInvalidNegativeIndexException(int negativeStartIndex)
    {
        "aString".Invoking(str => str.Substring_(negativeStartIndex,1))
                 .Should().Throw<InvalidNegativeIndexException>()
                 .WithMessage($"Negative index({negativeStartIndex}) unauthorized.")
                 ;
    }

}
