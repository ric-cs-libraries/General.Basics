using Xunit;

using General.Basics.IdsProviders;


namespace General.Basics.IdsProviders.UnitTests;


public class IdsProviderTests
{
    [Fact]
    public void Create_WhenCalledWithNoParams_ShouldInstanciateAndInitializeWithDefaultValues()
    {
        //--- Act ---
        var idsProvider = IdsProvider.Create();

        //--- Assert ---
        Assert.IsType<IdsProvider>(idsProvider);
        Assert.Equal(IdsProvider.DEFAULT_INITIAL_INT_ID, idsProvider.CurrentIntId);
        Assert.Equal(IdsProvider.DEFAULT_INT_ID_STEP, idsProvider.IntIdStep);
    }

    [Fact]
    public void Create_WhenCalledWithParams_ShouldInstanciateAndInitializeWithParams()
    {
        //--- Arrange ---
        var initialId = 10;
        var idStep = 2;

        //--- Act ---
        var idsProvider = IdsProvider.Create(initialId, idStep);

        //--- Assert ---
        Assert.Equal(initialId, idsProvider.CurrentIntId);
        Assert.Equal(idStep, idsProvider.IntIdStep);
    }

    [Fact]
    public void ResetIntId_WhenCalled_ShouldSetCurrentIdToTheInitialId()
    {
        //--- Arrange ---
        var initialId = 10;
        var idsProvider = IdsProvider.Create(initialId);
        var id = idsProvider.GetNextIntId();
        id = idsProvider.GetNextIntId();


        //--- Act ---
        idsProvider.ResetIntId();

        //--- Assert ---
        Assert.NotEqual(id, idsProvider.CurrentIntId);
        Assert.Equal(initialId, idsProvider.CurrentIntId);
    }

    [Fact]
    public void GetNextIntId_WhenCalled_ShouldReturnTheCorrectValue()
    {
        //--- Arrange ---
        var initialId = 10;
        var idStep = 4;
        var idsProvider = IdsProvider.Create(initialId, idStep);


        //--- Act ---
        var result1 = idsProvider.GetNextIntId();
        var result2 = idsProvider.GetNextIntId();
        var result3 = idsProvider.GetNextIntId();
        var result4 = idsProvider.GetNextIntId();

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
        var idsProvider = IdsProvider.Create();

        //--- Act & Assert ---
        List<string> guids = new();
        foreach(var _ in Enumerable.Range(1,100))
        {
            var guid = idsProvider.GetNextGuidId;
            Assert.DoesNotContain(guid, guids);
            guids.Add(guid);
        }
    }
}
