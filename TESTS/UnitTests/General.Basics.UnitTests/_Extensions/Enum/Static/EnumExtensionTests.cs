using General.Basics.Extensions;
using Xunit;


namespace General.Basics.UnitTests.Extensions.Numbers;

public class EnumExtensionTests
{
    #region GetMaxValue
    [Fact]
    public void GetMaxValue__ShouldReturnTheEnumMaxValue()
    {
        Enum1 enum1MaxValue = EnumExtension.GetMaxValue<Enum1>();
        Enum2 enum2MaxValue = EnumExtension.GetMaxValue<Enum2>();
        Enum3 enum3MaxValue = EnumExtension.GetMaxValue<Enum3>();
        Enum4 enum4MaxValue = EnumExtension.GetMaxValue<Enum4>();

        Assert.Equal(Enum1.d, enum1MaxValue);
        Assert.Equal(Enum2.K, enum2MaxValue);
        Assert.Equal(Enum3.b, enum3MaxValue);
        Assert.Equal(Enum4.C, enum4MaxValue);
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

        Enum3? enum3Value1 = EnumExtension.ToValueOf<Enum3>(-2);
        Enum3? enum3Value2 = EnumExtension.ToValueOf<Enum3>(-1);
        Enum3? enum3Value3 = EnumExtension.ToValueOf<Enum3>(-6);
        Enum3? enum3Value4 = EnumExtension.ToValueOf<Enum3>(-7);
        Enum3? enum3Value5 = EnumExtension.ToValueOf<Enum3>(Enum3.b);

        Enum4? enum4Value1 = EnumExtension.ToValueOf<Enum4>(11);
        Enum4? enum4Value2 = EnumExtension.ToValueOf<Enum4>(12);
        Enum4? enum4Value3 = EnumExtension.ToValueOf<Enum4>(-49);
        Enum4? enum4Value4 = EnumExtension.ToValueOf<Enum4>(-48);
        Enum4? enum4Value5 = EnumExtension.ToValueOf<Enum4>(6);
        Enum4? enum4Value6 = EnumExtension.ToValueOf<Enum4>(7);

        Assert.NotNull(enum1Value1);
        Assert.NotNull(enum1Value2);
        Assert.NotNull(enum1Value3);
        Assert.NotNull(enum1Value4);

        Assert.NotNull(enum2Value1);
        Assert.NotNull(enum2Value2);
        Assert.NotNull(enum2Value3);
        Assert.NotNull(enum2Value4);

        Assert.NotNull(enum3Value1);
        Assert.NotNull(enum3Value2);
        Assert.NotNull(enum3Value3);
        Assert.NotNull(enum3Value4);
        Assert.NotNull(enum3Value5);

        Assert.NotNull(enum4Value1);
        Assert.NotNull(enum4Value2);
        Assert.NotNull(enum4Value3);
        Assert.NotNull(enum4Value4);
        Assert.NotNull(enum4Value5);
        Assert.NotNull(enum4Value6);
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

        Enum3? enum3Value1 = EnumExtension.ToValueOf<Enum3>(0);
        Enum3? enum3Value2 = EnumExtension.ToValueOf<Enum3>(-5);
        Enum3? enum3Value3 = EnumExtension.ToValueOf<Enum3>(-3);
        Enum3? enum3Value4 = EnumExtension.ToValueOf<Enum3>(1);
        Enum3? enum3Value5 = EnumExtension.ToValueOf<Enum3>(7);

        Enum4? enum4Value1 = EnumExtension.ToValueOf<Enum4>(13);
        Enum4? enum4Value2 = EnumExtension.ToValueOf<Enum4>(9);
        Enum4? enum4Value3 = EnumExtension.ToValueOf<Enum4>(-51);
        Enum4? enum4Value4 = EnumExtension.ToValueOf<Enum4>(-47);
        Enum4? enum4Value5 = EnumExtension.ToValueOf<Enum4>(8);
        Enum4? enum4Value6 = EnumExtension.ToValueOf<Enum4>(0);

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

        Assert.Null(enum3Value1);
        Assert.Null(enum3Value2);
        Assert.Null(enum3Value3);
        Assert.Null(enum3Value4);
        Assert.Null(enum3Value5);

        Assert.Null(enum4Value1);
        Assert.Null(enum4Value2);
        Assert.Null(enum4Value3);
        Assert.Null(enum4Value4);
        Assert.Null(enum4Value5);
        Assert.Null(enum4Value6);
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

    enum Enum3
    {
        a = -2,
        b,
        c = -6,
        d = -7
    }

    enum Enum4
    {
        a = 10,
        b,
        C,
        c = -50,
        d,
        e,
        f = 5,
        g,
        h
    }
}
