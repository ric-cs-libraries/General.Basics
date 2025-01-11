using General.Basics.Intervals;


namespace General.Basics.Generators.TupleGenerators.Interfaces;

public interface IIntsPairsGenerator
{
    //distinctValue: true if we don't want any pair to contain twice the same value : (value1, value1).
    IEnumerable<(int, int)> GetPairs(int maxNbPairs, IntsInterval leftValueAuthorizedInterval, IntsInterval rightValueAuthorizedInterval, bool distinctValue);
}
