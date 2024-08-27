﻿using General.Basics.Extensions;
using General.Basics.Others;
using General.Basics.ErrorHandling;

using General.Basics.Generators.TupleGenerators.Interfaces;

namespace General.Basics.Generators.TupleGenerators;

public record ComputedIntsPairsGenerator : IIntsPairsGenerator
{
    private readonly Func<int?,int,int,int, int> pairsLeftValueComputer;
    private readonly Func<int?,int,int,int,int, int> pairsRightValueComputer;
        

    private ComputedIntsPairsGenerator(Func<int?,int,int,int, int> pairsLeftValueComputer, Func<int?,int,int,int,int, int> pairsRightValueComputer)
    {
        this.pairsLeftValueComputer = pairsLeftValueComputer;
        this.pairsRightValueComputer = pairsRightValueComputer;
    }

    public static ComputedIntsPairsGenerator Create(Func<int?,int,int,int, int> pairsLeftValueComputer, Func<int?,int,int,int,int, int> pairsRightValueComputer)
    {
        var result = new ComputedIntsPairsGenerator(pairsLeftValueComputer, pairsRightValueComputer);
        return result;
    }


    //distinctValue: true if we don't want any pair to contain twice the same value : (value1, value1).
    public IEnumerable<(int, int)> GetPairs(int maxNbPairs, IntsInterval leftValueAuthorizedInterval, IntsInterval rightValueAuthorizedInterval, bool distinctValue = false)
    {
        maxNbPairs.CheckIsGreaterOrEqualTo_(1, nameof(maxNbPairs));

        Check.NotNull(leftValueAuthorizedInterval.MaxValue!, $"{nameof(leftValueAuthorizedInterval)}.MaxValue");
        Check.NotNull(rightValueAuthorizedInterval.MaxValue!, $"{nameof(rightValueAuthorizedInterval)}.MaxValue");

        List<(int LeftValue, int RightValue)> pairs = new();

        int iterationNumber = 0;
        int? leftValue = null;
        int? rightValue = null;
        int maxLeftValue = leftValueAuthorizedInterval.MaxValue!.Value;
        int maxRightValue = rightValueAuthorizedInterval.MaxValue!.Value;
        bool validPair;
        do
        {
            leftValue = pairsLeftValueComputer(leftValue, iterationNumber, maxNbPairs, maxLeftValue);
            if (!leftValueAuthorizedInterval.Contains(leftValue.Value))
                break;

            rightValue = pairsRightValueComputer(rightValue, iterationNumber, maxNbPairs, maxRightValue, leftValue.Value);
            if (!rightValueAuthorizedInterval.Contains(rightValue.Value))
                break;


            validPair = !distinctValue || (leftValue.Value != rightValue.Value);

            if (validPair)
            {
                pairs.Add((leftValue.Value, rightValue.Value));
            }
            iterationNumber++;
           

        } while (pairs.Count < maxNbPairs);

        return pairs;
    }

}
