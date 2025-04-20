using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis; //[ExcludeFromCodeCoverage]
using System.Xml.Linq;
using General.Basics.ErrorHandling;
using General.Basics.Extensions;
using General.Basics.Generators.Interfaces;


namespace General.Basics.Combinators.Extensions;

public static partial class IEnumerableExtension
{
    #region UnOrdered Combinations
    public static IEnumerable<IEnumerable<T>> GetAllUnOrderedCombinations_<T>(this IEnumerable<T> enumerable)
    {
        IEnumerable<IEnumerable<T>> allCombinations = new List<IEnumerable<T>>();
        IEnumerable<IEnumerable<T>> combinationsByGroupSize;

        long nbElements = enumerable.Count();
        for (long groupSize = 1L; groupSize <= nbElements; groupSize++)
        {
            combinationsByGroupSize = _GetUnOrderedCombinations_<T>(enumerable, groupSize);
            allCombinations = allCombinations.Concat(combinationsByGroupSize);
        }
        return allCombinations;
    }

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static IEnumerable<IEnumerable<T>> GetUnOrderedCombinationsOf_<T>(this IEnumerable<T> enumerable, long groupSize)
    {
        groupSize.CheckIsGreaterOrEqualTo_(0L, nameof(groupSize));

        long nbElements = enumerable.Count();
        nbElements.CheckIsGreaterOrEqualTo_(groupSize, "Enumerable size");

        IEnumerable<IEnumerable<T>> combinationsByGroupSize = (groupSize == 0L)? Enumerable.Empty<IEnumerable<T>>() 
                                                                                 : _GetUnOrderedCombinations_<T>(enumerable, groupSize);
        return combinationsByGroupSize;
    }

    private static IEnumerable<IEnumerable<T>> _GetUnOrderedCombinations_<T>(IEnumerable<T> enumerable, long groupSize, int indent = 0)
    {
        long nbElements = enumerable.Count();

        List<IEnumerable<T>> combinations = new List<IEnumerable<T>>();
        //var indentText = " ".Repeat_(indent);

        //Debug.WriteLine($"\n\n{indentText}*** ENTRÉE dans _GetUnOrderedCombinations_(enumerable:{{{string.Join(", ", enumerable)}}},   groupSize:{groupSize}) ***\n");
        if (groupSize > 0L && nbElements >= groupSize)
        {
            T element;
            IEnumerable<T> remainingElements;
            IEnumerable<IEnumerable<T>> remainingCombinations;

            long nextGroupSize;
            IEnumerable<T> elementBranches;
            for (int index = 0; index < nbElements; index++)
            {
                element = enumerable.ElementAt(index);
                //Debug.WriteLine($"\n\n\n\n{indentText}   -- Element[{index}]={element}    (GroupSize={groupSize}) --");
                remainingElements = enumerable.Skip(index + 1);
                //Debug.WriteLine($"{indentText}   remainingElements={{" + string.Join(", ", remainingElements) + "}");
                nextGroupSize = groupSize - 1L;
                remainingCombinations = _GetUnOrderedCombinations_(remainingElements, nextGroupSize, indent + 8);
                //Debug.WriteLine("\n" + indentText + "   remainingCombinations={ {" + string.Join("}, {", remainingCombinations.Select(list => string.Join(", ", list))) + "} }");

                foreach (IEnumerable<T> remainingCombination in remainingCombinations)
                {
                    elementBranches = new[] { element }.Concat(remainingCombination);
                    if (elementBranches.Count() == groupSize)
                    {
                        //Debug.WriteLine($"{indentText}   >>Ajout de : {{" + string.Join(", ", elementBranches) + "}" + $"           (GroupSize={groupSize})");
                        combinations.Add(elementBranches);
                    }
                }
            }
        }
        else
        {
            combinations.Add(Enumerable.Empty<T>());
        }

        //var resultAsText = "{" + string.Join("}, {", combinations.Select(list => string.Join(", ", list))) + "}";
        //Debug.WriteLine($"\n{indentText}*** SORTIE DE _GetUnOrderedCombinations_({{{string.Join(", ", enumerable)}}}, {groupSize})   / RESULT = {{ " + resultAsText + " } ***\n\n");

        return combinations;
    }
    #endregion UnOrdered Combinations

    #region Ordered Combinations
    public static IEnumerable<IEnumerable<T>> GetAllOrderedCombinations_<T>(this IEnumerable<T> enumerable)
    {
        IEnumerable<IEnumerable<T>> allCombinations = new List<IEnumerable<T>>();
        IEnumerable<IEnumerable<T>> combinationsByGroupSize;

        long nbElements = enumerable.Count();
        for (long groupSize = 1L; groupSize <= nbElements; groupSize++)
        {
            combinationsByGroupSize = _GetOrderedCombinations_<T>(enumerable, groupSize);
            allCombinations = allCombinations.Concat(combinationsByGroupSize);
        }
        return allCombinations;
    }

    /// <exception cref="LongShouldBeGreaterOrEqualException"></exception>
    public static IEnumerable<IEnumerable<T>> GetOrderedCombinationsOf_<T>(this IEnumerable<T> enumerable, long groupSize)
    {
        groupSize.CheckIsGreaterOrEqualTo_(0L, nameof(groupSize));

        long nbElements = enumerable.Count();
        nbElements.CheckIsGreaterOrEqualTo_(groupSize, "Enumerable size");

        IEnumerable<IEnumerable<T>> combinationsByGroupSize = (groupSize == 0L) ? Enumerable.Empty<IEnumerable<T>>()
                                                                                 : _GetOrderedCombinations_<T>(enumerable, groupSize);
        return combinationsByGroupSize;
    }

    private static IEnumerable<IEnumerable<T>> _GetOrderedCombinations_<T>(IEnumerable<T> enumerable, long groupSize, int indent = 0)
    {
        long nbElements = enumerable.Count();

        List<IEnumerable<T>> combinations = new List<IEnumerable<T>>();
        //var indentText = " ".Repeat_(indent);

        //Debug.WriteLine($"\n\n{indentText}*** ENTRÉE dans _GetOrderedCombinations_(elements:{{{string.Join(", ", enumerable)}}},   groupSize:{groupSize}) ***\n");
        if (groupSize > 0L && nbElements >= groupSize)
        {
            T element;
            List<T> remainingElements;
            IEnumerable<IEnumerable<T>> remainingCombinations;

            long nextGroupSize;
            IEnumerable<T> elementBranches;
            for (int index = 0; index < nbElements; index++)
            {
                element = enumerable.ElementAt(index);
                //Debug.WriteLine($"\n\n\n\n{indentText}   -- Element[{index}]={element}    (GroupSize={groupSize}) --");
                remainingElements = Enumerable.Empty<T>().Concat(enumerable).ToList();
                remainingElements.Remove(element);
                //Debug.WriteLine($"{indentText}   remainingElements={{" + string.Join(", ", remainingElements) + "}");
                nextGroupSize = groupSize - 1L;
                remainingCombinations = _GetOrderedCombinations_(remainingElements, nextGroupSize, indent + 8);
                //Debug.WriteLine("\n" + indentText + "   remainingCombinations={ {" + string.Join("}, {", remainingCombinations.Select(list => string.Join(", ", list))) + "} }");


                foreach (IEnumerable<T> remainingCombination in remainingCombinations)
                {
                    elementBranches = new[] { element }.Concat(remainingCombination);
                    if (elementBranches.Count() == groupSize)
                    {
                        //Debug.WriteLine($"{indentText}   >>Ajout de : {{" + string.Join(", ", elementBranches) + "}" + $"           (GroupSize={groupSize})");
                        combinations.Add(elementBranches);
                    }

                }
            }
        }
        else
        {
            combinations.Add(Enumerable.Empty<T>());
        }

        //var resultAsText = "{" + string.Join("}, {", combinations.Select(list => string.Join(", ", list))) + "}";
        //Debug.WriteLine($"\n{indentText}*** SORTIE DE _GetOrderedCombinations_({{{string.Join(", ", enumerable)}}}, {groupSize})   / RESULT = {{ " + resultAsText + " } ***\n\n");

        return combinations;
    }

    #endregion Ordered Combinations
}