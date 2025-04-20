using General.Basics.Combinators.Extensions;
using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using Xunit;

namespace General.Basics.UnitTests.Combinators.Extensions;

public class IEnumerableExtensionTests
{
    #region GetAllUnOrderedCombinations_
    [Theory]
    [ClassData(typeof(Fixtures.GetAllUnOrderedCombinationsData))]
    public void GetAllUnOrderedCombinations_ShouldReturnTheCorrectValue
        (IEnumerable<string> elements, IEnumerable<IEnumerable<string>> expectedCombinations)
    {
        //--- Act ---
        IEnumerable<IEnumerable<string>> combinations = elements.GetAllUnOrderedCombinations_<string>();

        //--- Assert ---
        Assert.Equal(expectedCombinations, combinations);
    }
    #endregion GetAllUnOrderedCombinations_

    #region GetUnOrderedCombinationsOf_
    [Theory]
    [ClassData(typeof(Fixtures.GetUnOrderedCombinationsOfData))]
    public void GetUnOrderedCombinationsOf_ShouldReturnTheCorrectValue
        (IEnumerable<string> elements, long groupSize, IEnumerable<IEnumerable<string>> expectedCombinations)
    {
        //--- Act ---
        IEnumerable<IEnumerable<string>> combinations = elements.GetUnOrderedCombinationsOf_<string>(groupSize);

        //--- Assert ---
        Assert.Equal(expectedCombinations, combinations);
    }

    [Fact]
    public void GetUnOrderedCombinationsOf_WhenGroupSizeIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        var groupSize = -1L;
        var groupSizeMinimalValue = 0L;
        var elems = new[] { "BYE" };

        //--- Act & Assert ---
        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => elems.GetUnOrderedCombinationsOf_(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "groupSize", groupSize, groupSizeMinimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetUnOrderedCombinationsOf_WhenGroupSizeIsGreaterThanNbElements_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        var elems = new[] { "BYE" };
        var groupSize = 2L;

        //--- Act & Assert ---
        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => elems.GetUnOrderedCombinationsOf_<string>(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "Enumerable size", elems.Count(), groupSize);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetUnOrderedCombinationsOf_


    #region GetAllOrderedCombinations_
    [Theory]
    [ClassData(typeof(Fixtures.GetAllOrderedCombinationsData))]
    public void GetAllOrderedCombinations__ShouldReturnTheCorrectValue(IEnumerable<string> elements, IEnumerable<IEnumerable<string>> expectedCombinations)
    {
        //--- Act ---
        IEnumerable<IEnumerable<string>> combinations = elements.GetAllOrderedCombinations_<string>();

        //--- Assert ---
        Assert.Equal(expectedCombinations, combinations);
    }
    #endregion GetAllOrderedCombinations_

    #region GetOrderedCombinationsOf_
    [Theory]
    [ClassData(typeof(Fixtures.GetOrderedCombinations_WithGroupSize_Data))]
    public void GetOrderedCombinationsOf_ShouldReturnTheCorrectValue
        (IEnumerable<string> elements, long groupSize, IEnumerable<IEnumerable<string>> expectedCombinations)
    {
        //--- Act ---
        IEnumerable<IEnumerable<string>> combinations = elements.GetOrderedCombinationsOf_<string>(groupSize);

        //--- Assert ---
        Assert.Equal(expectedCombinations, combinations);
    }

    [Fact]
    public void GetOrderedCombinationsOf_WhenGroupSizeIsNegative_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        var groupSize = -1L;
        var groupSizeMinimalValue = 0L;
        var elems = new[] { "BYE" };

        //--- Act & Assert ---
        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => elems.GetOrderedCombinationsOf_(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "groupSize", groupSize, groupSizeMinimalValue);
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetOrderedCombinationsOf_WhenGroupSizeIsGreaterThanNbElements_ShouldThrowALongShouldBeGreaterOrEqualExceptionWithTheCorrectMessage()
    {
        //--- Arrange ---
        var elems = new[] { "BYE" };
        var groupSize = 2L;

        //--- Act & Assert ---
        var ex = Assert.Throws<LongShouldBeGreaterOrEqualException>(() => elems.GetOrderedCombinationsOf_<string>(groupSize));

        var expectedMessage = string.Format(LongShouldBeGreaterOrEqualException.MESSAGE_FORMAT, "Enumerable size", elems.Count(), groupSize);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetOrderedCombinationsOf_

    //==================================================================================
    static class Fixtures
    {
        internal class GetAllUnOrderedCombinationsData : TheoryData<IEnumerable<string>, IEnumerable<IEnumerable<string>>>
        {
            public GetAllUnOrderedCombinationsData()
            {
                //--- 0 element ---
                Add(new string[0], Enumerable.Empty<IEnumerable<string>>());


                //--- 1 element ---
                Add(new[] { "A" }, new[] { new[] { "A" } });


                //--- 2 elements ---
                Add(new[] { "A", "B" }, new[] { new[] { "A" }, new[] { "B" }, new[] { "A", "B" } });


                //--- 3 elements ---
                Add(new[] { "A", "B", "C" }, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" },
                                                     new[] { "A", "B" }, new[] { "A", "C" }, new[] { "B", "C" },
                                                     new[] { "A", "B", "C" } });


                ////--- 4 elements ---
                Add(new[] { "A", "B", "C", "D" }, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" }, new[] { "D" },
                                                          new[] { "A", "B" }, new[] { "A", "C" }, new[] { "A", "D" },
                                                          new[] { "B", "C" }, new[] { "B", "D" }, new[] { "C", "D" },
                                                          new[] { "A", "B", "C" }, new[] { "A", "B", "D" }, new[] { "A", "C", "D" }, new[] { "B", "C", "D" },
                                                          new[] { "A", "B", "C", "D" } });

            }
        }
        internal class GetUnOrderedCombinationsOfData : TheoryData<IEnumerable<string>, long, IEnumerable<IEnumerable<string>>>
        {
            public GetUnOrderedCombinationsOfData()
            {
                //--- 0 element ---
                Add(new string[0], 0L, Enumerable.Empty<IEnumerable<string>>());


                //--- 1 element ---
                Add(new[] { "A" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A" }, 1L, new[] { new[] { "A" } });


                //--- 2 elements ---
                Add(new[] { "A", "B" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A", "B" }, 1L, new[] { new[] { "A" }, new[] { "B" } });
                Add(new[] { "A", "B" }, 2L, new[] { new[] { "A", "B" } });


                //--- 3 elements ---
                Add(new[] { "A", "B", "C" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A", "B", "C" }, 1L, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" } });
                Add(new[] { "A", "B", "C" }, 2L, new[] { new[] { "A", "B" }, new[] { "A", "C" }, new[] { "B", "C" } });
                Add(new[] { "A", "B", "C" }, 3L, new[] { new[] { "A", "B", "C" } });


                //--- 4 elements ---
                Add(new[] { "A", "B", "C", "D" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A", "B", "C", "D" }, 1L, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" }, new[] { "D" } });
                Add(new[] { "A", "B", "C", "D" }, 2L, new[] { new[] { "A", "B" }, new[] { "A", "C" }, new[] { "A", "D" },
                                                              new[] { "B", "C" }, new[] { "B", "D" }, new[] { "C", "D" } });
                Add(new[] { "A", "B", "C", "D" }, 3L, new[] { new[] { "A", "B", "C" }, new[] { "A", "B", "D" }, new[] { "A", "C", "D" },
                                                              new[] { "B", "C", "D" } });
                Add(new[] { "A", "B", "C", "D" }, 4L, new[] { new[] { "A", "B", "C", "D" } });


                //--- 5 elements ---
                Add(new[] { "A", "B", "C", "D", "E" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A", "B", "C", "D", "E" }, 1L, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" }, new[] { "D" }, new[] { "E" } });
                Add(new[] { "A", "B", "C", "D", "E" }, 2L, new[] { new[] { "A", "B" }, new[] { "A", "C" }, new[] { "A", "D" }, new[] { "A", "E" },
                                                                   new[] { "B", "C" }, new[] { "B", "D" }, new[] { "B", "E" },
                                                                   new[] { "C", "D" }, new[] { "C", "E" },
                                                                   new[] { "D", "E" } });
                Add(new[] { "A", "B", "C", "D", "E" }, 3L, new[] { new[] { "A", "B", "C" }, new[] { "A", "B", "D" }, new[] { "A", "B", "E" },
                                                                   new[] { "A", "C", "D" }, new[] { "A", "C", "E" }, new[] { "A", "D", "E" },
                                                                   new[] { "B", "C", "D" }, new[] { "B", "C", "E" }, new[] { "B", "D", "E" },
                                                                   new[] { "C", "D", "E" } });
                Add(new[] { "A", "B", "C", "D", "E" }, 4L, new[] { new[] { "A", "B", "C", "D" }, new[] { "A", "B", "C", "E" }, new[] { "A", "B", "D", "E" },
                                                                   new[] { "A", "C", "D", "E" },
                                                                   new[] { "B", "C", "D", "E" } });
                Add(new[] { "A", "B", "C", "D", "E" }, 5L, new[] { new[] { "A", "B", "C", "D", "E" } });
            }
        }


        internal class GetAllOrderedCombinationsData : TheoryData<IEnumerable<string>, IEnumerable<IEnumerable<string>>>
        {
            public GetAllOrderedCombinationsData()
            {
                //--- 0 element ---
                Add(new string[0], Enumerable.Empty<IEnumerable<string>>());


                //--- 1 element ---
                Add(new[] { "A" }, new[] { new[] { "A" } });


                ////--- 2 elements ---
                Add(new[] { "A", "B" }, new[] { new[] { "A" }, new[] { "B" }, new[] { "A", "B" }, new[] { "B", "A" } });


                ////--- 3 elements ---
                Add(new[] { "A", "B", "C" }, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" }, 
                                                     new[] { "A", "B" }, new[] { "A", "C" }, 
                                                     new[] { "B", "A" }, new[] { "B", "C" },
                                                     new[] { "C", "A" }, new[] { "C", "B" },
                                                     new[] { "A", "B", "C" }, new[] { "A", "C", "B" },
                                                     new[] { "B", "A", "C" }, new[] { "B", "C", "A" },
                                                     new[] { "C", "A", "B" }, new[] { "C", "B", "A" } });
            }
        }

        internal class GetOrderedCombinations_WithGroupSize_Data : TheoryData<IEnumerable<string>, long, IEnumerable<IEnumerable<string>>>
        {
            public GetOrderedCombinations_WithGroupSize_Data()
            {
                //--- 0 element ---
                Add(new string[0], 0L, Enumerable.Empty<IEnumerable<string>>());


                //--- 1 element ---
                Add(new[] { "A" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A" }, 1L, new[] { new[] { "A" } });


                //--- 2 elements ---
                Add(new[] { "A", "B" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A", "B" }, 1L, new[] { new[] { "A" }, new[] { "B" } });
                Add(new[] { "A", "B" }, 2L, new[] { new[] { "A", "B" }, new[] { "B", "A" } });


                //--- 3 elements ---
                Add(new[] { "A", "B", "C" }, 0L, Enumerable.Empty<IEnumerable<string>>());
                Add(new[] { "A", "B", "C" }, 1L, new[] { new[] { "A" }, new[] { "B" }, new[] { "C" } });
                Add(new[] { "A", "B", "C" }, 2L, new[] { new[] { "A", "B" }, new[] { "A", "C" },
                                                         new[] { "B", "A" }, new[] { "B", "C" },
                                                         new[] { "C", "A" }, new[] { "C", "B" } });
                Add(new[] { "A", "B", "C" }, 3L, new[] { new[] { "A", "B", "C" }, new[] { "A", "C", "B" },
                                                         new[] { "B", "A", "C" }, new[] { "B", "C", "A" },
                                                         new[] { "C", "A", "B" }, new[] { "C", "B", "A" } });
            }
        }
    }
}
