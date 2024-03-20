using Xunit;


using General.Basics.Generators;


namespace General.Basics.Generators.UnitTests;

public class IdsGeneratorTests
{
    [Fact]
    public void Create_WhenCalledWithNoParams_ShouldInstanciateAndInitializeWithDefaultValues()
    {
        //--- Act ---
        var IdsGenerator = Generators.IdsGenerator.Create();

        //--- Assert ---
        Assert.IsType<IdsGenerator>(IdsGenerator);
        Assert.Equal(Generators.IdsGenerator.DEFAULT_INITIAL_INT_ID, IdsGenerator.CurrentIntId);
        Assert.Equal(Generators.IdsGenerator.DEFAULT_INT_ID_STEP, IdsGenerator.IntIdStep);
    }

    [Fact]
    public void Create_WhenCalledWithParams_ShouldInstanciateAndInitializeWithParams()
    {
        //--- Arrange ---
        var initialId = 10;
        var idStep = 2;

        //--- Act ---
        var IdsGenerator = Generators.IdsGenerator.Create(initialId, idStep);

        //--- Assert ---
        Assert.Equal(initialId, IdsGenerator.CurrentIntId);
        Assert.Equal(idStep, IdsGenerator.IntIdStep);
    }

    [Fact]
    public void ResetIntId_WhenCalled_ShouldSetCurrentIdToTheInitialId()
    {
        //--- Arrange ---
        var initialId = 10;
        var IdsGenerator = Generators.IdsGenerator.Create(initialId);
        var id = IdsGenerator.GetNextIntId();
        id = IdsGenerator.GetNextIntId();


        //--- Act ---
        IdsGenerator.ResetIntId();

        //--- Assert ---
        Assert.NotEqual(id, IdsGenerator.CurrentIntId);
        Assert.Equal(initialId, IdsGenerator.CurrentIntId);
    }

    [Fact]
    public void GetNextIntId_WhenCalled_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var initialId = 10;
        var idStep = 4;
        var IdsGenerator = Generators.IdsGenerator.Create(initialId, idStep);


        //--- Act ---
        var result1 = IdsGenerator.GetNextIntId();
        var result2 = IdsGenerator.GetNextIntId();
        var result3 = IdsGenerator.GetNextIntId();
        var result4 = IdsGenerator.GetNextIntId();

        //--- Assert ---
        Assert.Equal(initialId, result1);
        Assert.Equal(initialId + 1 * idStep, result2);
        Assert.Equal(initialId + 2 * idStep, result3);
        Assert.Equal(initialId + 3 * idStep, result4);
    }

    [Fact]
    public void GetNextGuidId_WhenCalled_ShouldReturnAnUniqueGuid()
    {
        //--- Arrange ---
        var IdsGenerator = Generators.IdsGenerator.Create();

        //--- Act & Assert ---
        List<string> guids = new();
        foreach(var _ in Enumerable.Range(1,100))
        {
            var guid = IdsGenerator.GetNextGuidId;
            Assert.DoesNotContain(guid, guids);
            guids.Add(guid);
        }
    }
}
