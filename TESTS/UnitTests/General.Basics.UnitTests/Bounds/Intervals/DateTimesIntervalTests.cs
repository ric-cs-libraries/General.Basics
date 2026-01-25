using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.Intervals;
using Xunit;


namespace General.Basics.UnitTests.Bounds.Intervals;

public class DateTimesIntervalTests
{
    #region Instanciation
    [Theory]
    [ClassData(typeof(Fixtures.Instanciation_WhenDateDebIsLowerOrEqualToDateFin_Data))]
    public void Instanciation_WhenDateDebIsLowerOrEqualToDateFin_ShouldInitializeCorrectlyTheProperties(DateTime? dateDeb, DateTime? dateFin)
    {
        //--- Act ---
        DateTimesInterval dateTimesInterval = new(dateDeb, dateFin);

        //--- Assert ---
        Assert.Equal(dateTimesInterval.MinValue, dateDeb);
        Assert.Equal(dateTimesInterval.MaxValue, dateFin);
    }

    [Theory]
    [ClassData(typeof(Fixtures.Instanciation_WhenDateDebIsGreaterThanDateFin_Data))]
    public void Instanciation_WhenDateDebIsGreaterThanDateFin_ShouldThrowAValueShouldNotBeGreaterThanException(DateTime? dateDeb, DateTime? dateFin)
    {

        //--- Act & Assert ---
        var ex = Assert.Throws<ValueShouldBeLowerOrEqualToException<DateTime?>>(() => new DateTimesInterval(dateDeb, dateFin));

        var expectedMessage = string.Format(ValueShouldBeLowerOrEqualToException<int>.MESSAGE_FORMAT, "Interval minValue", dateDeb, dateFin);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Instanciation

    #region Intersects
    [Theory]
    [ClassData(typeof(Fixtures.Intersects__ShouldReturnTheCorrectBool_Data))]
    public void Intersects__ShouldReturnTheCorrectBool(DateTimesInterval dtInterval1, DateTimesInterval dtInterval2, bool expectedResult)
    {
        //--- Act ---
        bool result = dtInterval1.Intersects(dtInterval2);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }
    #endregion Intersects

    #region Contains(DateTimesInterval)
    [Theory]
    [ClassData(typeof(Fixtures.Contains__ShouldReturnTheCorrectBool_Data))]
    public void Contains__ShouldReturnTheCorrectBool(DateTimesInterval dtInterval1, DateTimesInterval dtInterval2, bool expectedResult)
    {
        //--- Act ---
        bool result = dtInterval1.Contains(dtInterval2);

        //--- Assert ---
        Assert.Equal(expectedResult, result);
    }
    #endregion Contains(DateTimesInterval)


    //==============================================================================================
    //==============================================================================================

    static class Fixtures
    {
        internal class Instanciation_WhenDateDebIsLowerOrEqualToDateFin_Data : TheoryData<DateTime?, DateTime?>
        {
            public Instanciation_WhenDateDebIsLowerOrEqualToDateFin_Data()
            {
                DateTime dateDeb = DateTime.UtcNow;
                DateTime dateFin = dateDeb.AddMicroseconds(1);
                DateTime dateFin2 = dateDeb.AddDays(1);

                Add(dateDeb, dateFin);
                Add(dateDeb, dateFin2);
                Add(dateDeb, null);
                Add(null, dateFin);
                Add(null, dateFin2);
                Add(null, null);
            }
        }

        internal class Instanciation_WhenDateDebIsGreaterThanDateFin_Data : TheoryData<DateTime?, DateTime?>
        {
            public Instanciation_WhenDateDebIsGreaterThanDateFin_Data()
            {
                DateTime dateFin = DateTime.UtcNow;
                DateTime dateDeb = dateFin.AddSeconds(1);
                DateTime dateDeb2 = dateFin.AddDays(1);

                Add(dateDeb, dateFin);
                Add(dateDeb2, dateFin);
            }
        }

        internal class Intersects__ShouldReturnTheCorrectBool_Data : TheoryData<DateTimesInterval, DateTimesInterval, bool>
        {
            public Intersects__ShouldReturnTheCorrectBool_Data()
            {
                DateTime dt01 = DateTime.Parse("28/02/2024");
                DateTime dt02 = DateTime.Parse("01/03/2025");

                DateTimesInterval dtIntervalRef0 = new(dt01, dt02);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-10), dt01.AddDays(-1)), false);
                Add(dtIntervalRef0, new DateTimesInterval(dt02.AddDays(1), dt02.AddDays(10)), false);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(1), dt02.AddDays(-1)), true);
                Add(dtIntervalRef0, new DateTimesInterval(dt01, dt02), true);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-1), dt01), true);
                Add(dtIntervalRef0, new DateTimesInterval(dt02, dt02.AddDays(1)), true);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-1), dt01.AddDays(2)), true);
                Add(dtIntervalRef0, new DateTimesInterval(dt02.AddDays(-1), dt02.AddDays(2)), true);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-1), dt02.AddDays(2)), true);

                Add(dtIntervalRef0, new DateTimesInterval(dt01, dt01), true);
                Add(dtIntervalRef0, new DateTimesInterval(dt02, dt02), true);

                DateTime dt03 = DateTime.Parse("12/03/2025");
                Add(new DateTimesInterval(null, null), dtIntervalRef0, true);
                Add(new DateTimesInterval(null, null), new DateTimesInterval(dt03, null), true);
                Add(new DateTimesInterval(null, null), new DateTimesInterval(null, dt03), true);
                Add(new DateTimesInterval(null, null), new DateTimesInterval(null, null), true);

                Add(new DateTimesInterval(dt01, null), dtIntervalRef0, true);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt03, null), true);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt03, dt03.AddDays(1)), true);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01, dt01), true);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01.AddDays(-1), dt01), true);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01.AddDays(-2), dt01.AddDays(-1)), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(null, dt01.AddDays(-1)), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01.AddDays(-10), null), true);

                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(-1), dt01), true);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(-1), dt01.AddDays(1)), true);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(-2), dt01.AddDays(-1)), true);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01, dt01.AddDays(1)), true);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01, dt01), true);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(1), dt01.AddDays(2)), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(1), null), false);
            }

            private new void Add(DateTimesInterval dtInterval1, DateTimesInterval dtInterval2, bool expectedResult)
            {
                base.Add(dtInterval1, dtInterval2, expectedResult);

                //La réciproque doit donner le même résultat
                base.Add(dtInterval2, dtInterval1, expectedResult);
            }
        }

        internal class Contains__ShouldReturnTheCorrectBool_Data : TheoryData<DateTimesInterval, DateTimesInterval, bool>
        {
            public Contains__ShouldReturnTheCorrectBool_Data()
            {
                DateTime dt01 = DateTime.Parse("28/02/2024");
                DateTime dt02 = DateTime.Parse("01/03/2025");

                DateTimesInterval dtIntervalRef0 = new(dt01, dt02);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-10), dt01.AddDays(-1)), false);
                Add(dtIntervalRef0, new DateTimesInterval(dt02.AddDays(1), dt02.AddDays(10)), false);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(1), dt02.AddDays(-1)), true);
                Add(new DateTimesInterval(dt01.AddDays(1), dt02.AddDays(-1)), dtIntervalRef0, false);
                Add(dtIntervalRef0, new DateTimesInterval(dt01, dt02), true);
                Add(new DateTimesInterval(dt01, dt02), dtIntervalRef0, true);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-1), dt01), false);
                Add(dtIntervalRef0, new DateTimesInterval(dt02, dt02.AddDays(1)), false);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-1), dt01.AddDays(2)), false);
                Add(dtIntervalRef0, new DateTimesInterval(dt02.AddDays(-1), dt02.AddDays(2)), false);

                Add(dtIntervalRef0, new DateTimesInterval(dt01.AddDays(-1), dt02.AddDays(2)), false);

                Add(dtIntervalRef0, new DateTimesInterval(dt01, dt01), true);
                Add(new DateTimesInterval(dt01, dt01), dtIntervalRef0, false);
                Add(dtIntervalRef0, new DateTimesInterval(dt02, dt02), true);
                Add(new DateTimesInterval(dt02, dt02), dtIntervalRef0, false);

                DateTime dt03 = DateTime.Parse("12/03/2025");
                Add(new DateTimesInterval(null, null), dtIntervalRef0, true);
                Add(dtIntervalRef0, new DateTimesInterval(null, null), false);
                Add(new DateTimesInterval(null, null), new DateTimesInterval(dt03, null), true);
                Add(new DateTimesInterval(dt03, null), new DateTimesInterval(null, null), false);
                Add(new DateTimesInterval(null, dt03), new DateTimesInterval(null, null), false);
                Add(new DateTimesInterval(null, null), new DateTimesInterval(null, null), true);

                Add(new DateTimesInterval(dt01, null), dtIntervalRef0, true);
                Add(dtIntervalRef0, new DateTimesInterval(dt01, null), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt03, null), true);
                Add(new DateTimesInterval(dt03, null), new DateTimesInterval(dt01, null), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt03, dt03.AddDays(1)), true);
                Add(new DateTimesInterval(dt03, dt03.AddDays(1)), new DateTimesInterval(dt01, null), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01, dt01), true);
                Add(new DateTimesInterval(dt01, dt01), new DateTimesInterval(dt01, null), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01.AddDays(-1), dt01), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01.AddDays(-2), dt01.AddDays(-1)), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(null, dt01.AddDays(-1)), false);
                Add(new DateTimesInterval(dt01, null), new DateTimesInterval(dt01.AddDays(-10), null), false);

                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(-1), dt01), true);
                Add(new DateTimesInterval(dt01.AddDays(-1), dt01), new DateTimesInterval(null, dt01), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(-1), dt01.AddDays(1)), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(-2), dt01.AddDays(-1)), true);
                Add(new DateTimesInterval(dt01.AddDays(-2), dt01.AddDays(-1)), new DateTimesInterval(null, dt01), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01, dt01.AddDays(1)), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01, dt01), true);
                Add(new DateTimesInterval(dt01, dt01), new DateTimesInterval(null, dt01), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(1), dt01.AddDays(2)), false);
                Add(new DateTimesInterval(null, dt01), new DateTimesInterval(dt01.AddDays(1), null), false);
            }
        }
    }
}



