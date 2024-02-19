using Xunit;

using General.Basics.Exceptions;


namespace General.Basics.Exceptions.UnitTests;


public class OutOfRangeIntegerExceptionTests
{
    [Theory]
    //[MemberData(nameof(badIndexes))]
    [ClassData(typeof(InstanciationTestsData))]
    public void Instanciation_WhenSubjectIsNotGiven_TheExceptionShouldContainTheCorrectMessage(int index)
    {
        var subject = "Number";
        var minIndex = 0;
        var maxIndex = 50;
        var ex = new OutOfRangeIntegerException(index, minIndex, maxIndex);

        var expectedMessage = $"Invalid {subject} : '{index}', possible range : [{minIndex},{maxIndex}].";
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    //[MemberData(nameof(badIndexes))]
    [ClassData(typeof(InstanciationTestsData))]
    public void Instanciation_WhenSubjectIsGiven_TheExceptionShouldContainTheCorrectMessage(int index)
    {
        var subject = "Index";
        var minIndex = 0;
        var maxIndex = 50;
        var ex = new OutOfRangeIntegerException(index, minIndex, maxIndex, subject);

        var expectedMessage = $"Invalid {subject} : '{index}', possible range : [{minIndex},{maxIndex}].";
        Assert.Equal(expectedMessage, ex.Message);
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