using Xunit;

using General.Basics.Exceptions;


namespace General.Basics.Exceptions.UnitTests;


public class UnexistingChunkExceptionTests
{
    [Theory]
    [ClassData(typeof(InstanciationTestsChunkBoundsData))]
    public void Instanciation___TheExceptionShouldContainTheCorrectMessage(int startIndex, int endIndex)
    {
        var subject = "List";
        var minIndex = 0;
        var maxIndex = 50;
        var ex = new UnexistingChunkException(startIndex, endIndex, minIndex, maxIndex, subject);

        var expectedMessage = $"In {subject}, Unexisting Chunk [startIndex='{startIndex}'; endIndex='{endIndex}'] ; possible range : [{minIndex},{maxIndex}].";
        Assert.Equal(expectedMessage, ex.Message);
    }

    //---------------------------------------------------------
    class InstanciationTestsChunkBoundsData : TheoryData<int, int>
    {
        public InstanciationTestsChunkBoundsData()
        {
            Add(-1, 0);
            Add(1, 51);
            Add(51, 52);
            Add(5, 1);
        }
    }
}
