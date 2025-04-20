using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Extensions.Numbers;

public class LongExtensionTests
{
    #region CheckIsGreaterOrEqualTo_
    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenLongIntHasNotTheMinimalValueAndSubjectNameNotGiven_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myLong = 9L;
        var minimalValue = 10L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => myLong.CheckIsGreaterOrEqualTo_(minimalValue));

        string subjectName = "long int";
        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, subjectName, myLong, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenLongIntHasNotTheMinimalValueAndSubjectNameGiven_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myLong = 9L;
        var minimalValue = 10L;
        string subjectName = nameof(myLong);

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => myLong.CheckIsGreaterOrEqualTo_(minimalValue, subjectName));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, subjectName, myLong, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CheckIsGreaterOrEqualTo_WhenLongIntHasTheMinimalValueOrGreater_ShouldNotThrowAnException()
    {
        var minimalValue = 10L;
        var myLong1 = minimalValue;
        var myLong2 = minimalValue + 1L;

        myLong1.CheckIsGreaterOrEqualTo_(minimalValue);
        myLong2.CheckIsGreaterOrEqualTo_(minimalValue);
        myLong1.CheckIsGreaterOrEqualTo_(minimalValue, "xxx");
        myLong2.CheckIsGreaterOrEqualTo_(minimalValue, "zzz");

        Assert.True(true);
    }
    #endregion CheckIsGreaterOrEqualTo_

    #region GetNbAllOrderedCombinations_
    [Theory]
    [ClassData(typeof(Fixtures.GetNbAllOrderedCombinationsData))]
    public void GetNbAllOrderedCombinations__ShouldReturnTheCorrectValue(long nbElements, long expectedResult)
    {
        //--- Act ---
        var result = nbElements.GetNbAllOrderedCombinations_();

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void GetNbAllOrderedCombinations_WhenNbElementsIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var nbElems = -1L;
        var minimalValue = 0L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => nbElems.GetNbAllOrderedCombinations_());

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbElements", nbElems, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetNbAllOrderedCombinations_

    #region GetNbOrderedCombinationsOf_
    [Theory]
    [ClassData(typeof(Fixtures.GetNbOrderedCombinationsOfData))]
    public void GetNbOrderedCombinationsOf__ShouldReturnTheCorrectValue(long nbElements, long groupSize, long expectedResult)
    {
        //--- Act ---
        var result = nbElements.GetNbOrderedCombinationsOf_(groupSize);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void GetNbOrderedCombinationsOf__WhenGroupSizeIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var groupSize = -1L;
        var groupSizeMinimalValue = 0L;
        var nbElems = 1L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => nbElems.GetNbOrderedCombinationsOf_(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "groupSize", groupSize, groupSizeMinimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetNbOrderedCombinationsOf__WhenGroupSizeIsGreaterThanNbElements_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var nbElems = 1L;
        var groupSize = 2L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => nbElems.GetNbOrderedCombinationsOf_(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbElements", nbElems, groupSize);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetNbOrderedCombinationsOf_



    #region GetNbAllUnOrderedCombinations_
    [Theory]
    [ClassData(typeof(Fixtures.GetNbAllUnOrderedCombinationsData))]
    public void GetNbAllUnOrderedCombinations__ShouldReturnTheCorrectValue(long nbElements, long expectedResult)
    {
        //--- Act ---
        var result = nbElements.GetNbAllUnOrderedCombinations_();

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void GetNbAllUnOrderedCombinations_WhenNbElementsIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var nbElems = -1L;
        var minimalValue = 0L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => nbElems.GetNbAllUnOrderedCombinations_());

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbElements", nbElems, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetNbAllUnOrderedCombinations_

    #region GetNbUnOrderedCombinationsOf_
    [Theory]
    [ClassData(typeof(Fixtures.GetNbUnOrderedCombinationsOfData))]
    public void GetNbUnOrderedCombinationsOf__ShouldReturnTheCorrectValue(long nbElements, long groupSize, long expectedResult)
    {
        //--- Act ---
        var result = nbElements.GetNbUnOrderedCombinationsOf_(groupSize);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void GetNbUnOrderedCombinationsOf__WhenGroupSizeIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var groupSize = -1L;
        var groupSizeMinimalValue = 0L;
        var nbElems = 1L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => nbElems.GetNbUnOrderedCombinationsOf_(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "groupSize", groupSize, groupSizeMinimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetNbUnOrderedCombinationsOf__WhenGroupSizeIsGreaterThanNbElements_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var nbElems = 1L;
        var groupSize = 2L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => nbElems.GetNbUnOrderedCombinationsOf_(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "nbElements", nbElems, groupSize);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetNbUnOrderedCombinationsOf_


    #region Factorial_
    [Theory]
    [ClassData(typeof(Fixtures.FactorialData))]
    public void Factorial__ShouldReturnTheCorrectValue(long myLong, long expectedResult)
    {
        //--- Act ---
        var result = myLong.Factorial_();

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Factorial_WhenNumberIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        var myLong = -1L;
        var minimalValue = 0L;

        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => myLong.Factorial_());

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "number", myLong, minimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Factorial_




    //==============================================================================================

    static class Fixtures
    {
        internal class GetNbAllOrderedCombinationsData : TheoryData<long, long>
        {
            public GetNbAllOrderedCombinationsData()
            {
                Add(0L, 0L);
                Add(1L, 1L);
                Add(2L, 2L + 2L);
                Add(3L, 3L + 6L + 6L);
                Add(4L, 4L + 4L*3L + 4L*6L + 4L*6L);
            }
        }

        internal class GetNbOrderedCombinationsOfData : TheoryData<long, long, long>
        {
            public GetNbOrderedCombinationsOfData()
            {
                Add(0L, 0L, 0L);

                Add(1L, 0L, 0L);
                Add(1L, 1L, 1L);

                Add(2L, 0L, 0L);
                Add(2L, 1L, 2L);
                Add(2L, 2L, 2L);

                Add(3L, 0L, 0L);
                Add(3L, 1L, 3L);
                Add(3L, 2L, 6L);
                Add(3L, 3L, 6L);

                Add(4L, 0L, 0L);
                Add(4L, 1L, 4L);
                Add(4L, 2L, 4L * 3L);
                Add(4L, 3L, 4L * 6L);
                Add(4L, 4L, 4L * 6L);
            }
        }

        internal class GetNbAllUnOrderedCombinationsData : TheoryData<long, long>
        {
            public GetNbAllUnOrderedCombinationsData()
            {
                Add(0L, 0L);

                Add(1L, 1L);

                Add(2L, 2L + 1L);

                Add(3L, 3L + 3L + 1L);

                Add(4L, 4L + 6L + 4L + 1L);

                Add(5L, 5L + 10L + 10L + 5L + 1L);
            }
        }

        internal class GetNbUnOrderedCombinationsOfData : TheoryData<long, long, long>
        {
            public GetNbUnOrderedCombinationsOfData()
            {
                Add(0L, 0L, 0L);

                Add(1L, 0L, 0L);
                Add(1L, 1L, 1L);

                Add(2L, 0L, 0L);
                Add(2L, 1L, 2L);
                Add(2L, 2L, 1L);

                Add(3L, 0L, 0L);
                Add(3L, 1L, 3L);
                Add(3L, 2L, 3L);
                Add(3L, 3L, 1L);

                Add(4L, 0L, 0L);
                Add(4L, 1L, 4L);
                Add(4L, 2L, 6L);
                Add(4L, 3L, 4L);
                Add(4L, 4L, 1L);

                Add(5L, 0L, 0L);
                Add(5L, 1L, 5L);
                Add(5L, 2L, 10L);
                Add(5L, 3L, 10L);
                Add(5L, 4L, 5L);
                Add(5L, 5L, 1L);
            }
        }

        internal class FactorialData : TheoryData<long, long>
        {
            public FactorialData()
            {
                Add(0L, 1L);
                Add(1L, 1L*1L);
                Add(2L, 1L*2L);
                Add(3L, 2L*3L);
                Add(4L, 6L*4L);
                Add(5L, 24L*5L);
                Add(6L, 120L*6L);
                Add(7L, 720L*7L);
            }
        }
    }
}