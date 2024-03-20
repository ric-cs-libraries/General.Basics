using System.Diagnostics;


namespace General.Basics.Generators;

[DebuggerDisplay("CurrentIntId={CurrentIntId}, InitialIntId={InitialIntId}, IntIdStep={IntIdStep}, DEFAULT_INITIAL_INT_ID={DEFAULT_INITIAL_INT_ID}, DEFAULT_INT_ID_STEP={DEFAULT_INT_ID_STEP}")]
public class IdsGenerator
{
    public const int DEFAULT_INITIAL_INT_ID = 0;
    public const int DEFAULT_INT_ID_STEP = 1;


    public string GetNextGuidId => Guid.NewGuid().ToString();


    public int InitialIntId { get; }
    public int CurrentIntId { get; private set;  }
    public int IntIdStep { get; }

    private IdsGenerator(int initialIntId = DEFAULT_INITIAL_INT_ID, int intIdStep = DEFAULT_INT_ID_STEP)
    {
        InitialIntId = initialIntId;
        IntIdStep = intIdStep;

        ResetIntId();
    }

    public static IdsGenerator Create(int initialIntId = DEFAULT_INITIAL_INT_ID, int intIdStep = DEFAULT_INT_ID_STEP)
    {
        var result = new IdsGenerator(initialIntId, intIdStep);
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
