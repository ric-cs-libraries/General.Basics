using General.Basics.ErrorHandling;


namespace General.Basics.Extensions;

public static class FuncExtension
{
    /// <exception cref="IntShouldBeGreaterOrEqualException"></exception>
    public static T Recurse<T>(this Func<T, T> func, T firstCallParam, int nbIterations)
    {
        const int nbRecurseMini = 1;
        if (nbIterations < nbRecurseMini)
            throw new IntShouldBeGreaterOrEqualException("nbIterations", nbIterations, minimalValue: nbRecurseMini);

        T result = firstCallParam;
        do
        {
            result = func(result);

        } while ((--nbIterations) > 0);

        return result;
    }
}
