using General.Basics.ErrorHandling;


namespace General.Basics.Extensions;

public static class FuncExtension
{
    /// <exception cref="IntShouldBeGreaterOrEqualException"></exception>
    public static T Recurse<T>(this Func<T, T> func, T firstCallParam, int nbRecurse)
    {
        const int nbRecurseMini = 1;
        if (nbRecurse < nbRecurseMini)
            throw new IntShouldBeGreaterOrEqualException("nbRecurse", nbRecurse, minimalValue: nbRecurseMini);

        T result = firstCallParam;
        do
        {
            result = func(result);

        } while ((--nbRecurse) > 0);

        return result;
    }
}
