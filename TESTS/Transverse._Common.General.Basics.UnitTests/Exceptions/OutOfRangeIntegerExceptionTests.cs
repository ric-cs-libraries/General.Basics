using Xunit;

using Transverse._Common.General.Basics.Exceptions;


namespace Transverse._Common.General.Basics.Exceptions.UnitTests;


public class OutOfRangeIntegerExceptionTests
{
    [Theory]
    //[MemberData(nameof(badIndexes))]
    [ClassData(typeof(InstanciationTestsData))]
    public void Instanciation_WhenSubjectIsNotGiven_TheExceptionShouldContainTheCorrectMessage(int indexValue)
    {
        var subject = "Number";
        var minIndexValue = 0;
        var maxIndexValue = 50;
        var ex = new OutOfRangeIntegerException(indexValue, minIndexValue, maxIndexValue);

        var expected = $"Invalid {subject} : '{indexValue}', possible range : [{minIndexValue},{maxIndexValue}].";
        Assert.Equal(expected, ex.Message);
    }

    [Theory]
    //[MemberData(nameof(badIndexes))]
    [ClassData(typeof(InstanciationTestsData))]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage(int indexValue)
    {
        var subject = "Index";
        var minIndexValue = 0;
        var maxIndexValue = 50;
        var ex = new OutOfRangeIntegerException(indexValue, minIndexValue, maxIndexValue, subject);

        var expected = $"Invalid {subject} : '{indexValue}', possible range : [{minIndexValue},{maxIndexValue}].";
        Assert.Equal(expected, ex.Message);
    }


    //---------------------------------------------------------
    //public static IEnumerable<object[]> badIndexes = new List<object[]>
    //{
    //    new object[]{-1}, new object[]{51},
    //};

    class InstanciationTestsData : TheoryData<int>
    {
        public InstanciationTestsData()
        {
            AddRange(new[]{ -1, 51 });
        }
    }
}
