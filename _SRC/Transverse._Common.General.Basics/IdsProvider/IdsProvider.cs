using System.Diagnostics;


namespace Transverse._Common.General.Basics.IdsProvider;

[DebuggerDisplay("CurrentIntId={CurrentIntId}, InitialIntId={InitialIntId}, IntIdStep={IntIdStep}, DEFAULT_INITIAL_INT_ID={DEFAULT_INITIAL_INT_ID}, DEFAULT_INT_ID_STEP={DEFAULT_INT_ID_STEP}")]
public class IdsProvider
{
    public const int DEFAULT_INITIAL_INT_ID = 0;
    public const int DEFAULT_INT_ID_STEP = 1;


    public string GetNextGuidId => Guid.NewGuid().ToString();


    public int InitialIntId { get; }
    public int CurrentIntId { get; private set;  }
    public int IntIdStep { get; }

    private IdsProvider(int initialIntId = DEFAULT_INITIAL_INT_ID, int intIdStep = DEFAULT_INT_ID_STEP)
    {
        InitialIntId = initialIntId;
        IntIdStep = intIdStep;

        ResetIntId();
    }

    public static IdsProvider Create(int initialIntId = DEFAULT_INITIAL_INT_ID, int intIdStep = DEFAULT_INT_ID_STEP)
    {
        var result = new IdsProvider(initialIntId, intIdStep);
        return result;
    }

    public int GetNextIntId()
    {
        var result = CurrentIntId;
        CurrentIntId += IntIdStep;
        return result;
    }

    public void ResetIntId()
    {
        CurrentIntId = InitialIntId;
    }
}
