using General.Basics.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Extensions.Numbers;

public class EnumExtensionTests
{
    #region GetMaxValue
    [Fact]
    public void GetLastValue__ShouldReturnTheEnumLastValue()
    {
        Enum1 enum1LastValue = EnumExtension.GetMaxValue<Enum1>();
        Enum2 enum2LastValue = EnumExtension.GetMaxValue<Enum2>();

        Assert.Equal(Enum1.d, enum1LastValue);
        Assert.Equal(Enum2.K, enum2LastValue);
    }
    #endregion GetMaxValue

    #region ToValueOf
    [Fact]
    public void ToValueOf_WhenEnumValueExists_ShouldReturnThisEnumValue()
    {
        Enum1? enum1Value1 = EnumExtension.ToValueOf<Enum1>(0);
        Enum1? enum1Value2 = EnumExtension.ToValueOf<Enum1>(2);
        Enum1? enum1Value3 = EnumExtension.ToValueOf<Enum1>(3);
        Enum1? enum1Value4 = EnumExtension.ToValueOf<Enum1>(Enum1.B);

        Enum2? enum2Value1 = EnumExtension.ToValueOf<Enum2>(100);
        Enum2? enum2Value2 = EnumExtension.ToValueOf<Enum2>(101);
        Enum2? enum2Value3 = EnumExtension.ToValueOf<Enum2>(60);
        Enum2? enum2Value4 = EnumExtension.ToValueOf<Enum2>(Enum2.W);

        Assert.NotNull(enum1Value1);
        Assert.NotNull(enum1Value2);
        Assert.NotNull(enum1Value3);
        Assert.NotNull(enum1Value4);

        Assert.NotNull(enum2Value1);
        Assert.NotNull(enum2Value2);
        Assert.NotNull(enum2Value3);
        Assert.NotNull(enum2Value4);
    }

    [Fact]
    public void ToValueOf_WhenEnumValueDoesntExist_ShouldReturnNull()
    {
        Enum1? enum1Value1 = EnumExtension.ToValueOf<Enum1>(10);
        Enum1? enum1Value2 = EnumExtension.ToValueOf<Enum1>(4);
        Enum1? enum1Value3 = EnumExtension.ToValueOf<Enum1>("");
        Enum1? enum1Value4 = EnumExtension.ToValueOf<Enum1>(true);
        Enum1? enum1Value5 = EnumExtension.ToValueOf<Enum1>("any");

        Enum2? enum2Value1 = EnumExtension.ToValueOf<Enum2>(0);
        Enum2? enum2Value2 = EnumExtension.ToValueOf<Enum2>(14);
        Enum2? enum2Value3 = EnumExtension.ToValueOf<Enum2>(16);
        Enum2? enum2Value4 = EnumExtension.ToValueOf<Enum2>(99);
        Enum2? enum2Value5 = EnumExtension.ToValueOf<Enum2>(102);
        Enum2? enum2Value6 = EnumExtension.ToValueOf<Enum2>(-4);

        Assert.Null(enum1Value1);
        Assert.Null(enum1Value2);
        Assert.Null(enum1Value3);
        Assert.Null(enum1Value4);
        Assert.Null(enum1Value5);

        Assert.Null(enum2Value1);
        Assert.Null(enum2Value2);
        Assert.Null(enum2Value3);
        Assert.Null(enum2Value4);
        Assert.Null(enum2Value5);
        Assert.Null(enum2Value6);
    }
    #endregion ToValueOf

    //==============================================================================================

    enum Enum1
    {
        A, //0
        B, //1
        C, //2
        d //3
    }

    enum Enum2
    {
        W = 15,
        H = 100,
        x = 10,
        K = 101,
        Y = 20,
        Z = 60,
    }

}
