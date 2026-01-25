using General.Basics.Bounds.Exceptions;
using General.Basics.Extensions;



namespace General.Basics.Bounds;



public record MinAndMax<T>
    where T : struct, IComparable<T> // struct => type valeur non nullable
{
    public T? MinValue { get; }
    public T? MaxValue { get; }

    public bool IsMinDefined => MinValue.HasValue;
    public bool IsMaxDefined => MaxValue.HasValue;

    public bool AreMinAndMaxDefined => IsMinDefined && IsMaxDefined;

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public MinAndMax(T? minValue, T? maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        if (AreMinAndMaxDefined)
        {
            bool minIsGreaterThanMax = minValue!.Value.IsGreaterThan(maxValue!.Value);
            if (minIsGreaterThanMax)
            {
                throw new ValueShouldBeLowerOrEqualToException<T>("minValue", minValue.Value!, maxValue.Value!);
            }
        }
    }

    #region Comparaisons du Min, à ...
    public bool IsMinLowerThan(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMinDefined)
            return !valueIsNull; //MinValue est alors inf. à toute value définie(non null).
                                 //(Si value est null il y a égalité avec MinValue).

        return (valueIsNull) ? false : MinValue!.Value.IsLowerThan(value!.Value); //Si value is null, rien ne peut lui être inf.
    }

    public bool IsMinLowerOrEqualTo(T? value)
    {
        if (!IsMinDefined)
            return true; //MinValue est alors inf. ou égale(null) à toute value.

        bool valueIsNull = value is null; //value non définie

        return (valueIsNull) ? false : MinValue!.Value.IsLowerOrEqualTo(value!.Value); //Si value is null, MinValue(définie) ne peut que lui être sup.
    }

    public bool IsMinGreaterThan(T? value)
    {
        if (!IsMinDefined)
            return false; //MinValue ne peut être sup. à rien !

        bool valueIsNull = value is null; //value non définie

        return (valueIsNull) ? true : MinValue!.Value.IsGreaterThan(value!.Value); //Si value is null, MinValue lui est forcément sup.
    }

    public bool IsMinGreaterOrEqualTo(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMinDefined)
            return valueIsNull; //Egalité que si value vaut null

        return (valueIsNull) ? true : MinValue!.Value.IsGreaterOrEqualTo(value!.Value); //Si value is null, MinValue lui est forcément sup.
    }

    public bool IsMinEqualTo(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMinDefined)
            return valueIsNull; //Egalité que si value vaut null

        return (valueIsNull) ? false : MinValue!.Value.IsEqualTo(value!.Value); //Si value is null, MinValue lui est forcément sup.
    }
    #endregion Comparaisons du Min, à ...

    #region Comparaisons du Max, à ...
    public bool IsMaxGreaterThan(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMaxDefined)
            return !valueIsNull; //MaxValue est alors sup. à toute value définie(non null).
                                 //(Si value est null il y a égalité avec MaxValue).

        return (valueIsNull) ? false : MaxValue!.Value.IsGreaterThan(value!.Value); //Si value is null, rien ne peut lui être sup.
    }

    public bool IsMaxGreaterOrEqualTo(T? value)
    {
        if (!IsMaxDefined)
            return true; //MaxValue est alors sup. ou égale(null) à toute value.

        bool valueIsNull = value is null; //value non définie

        return (valueIsNull) ? false : MaxValue!.Value.IsGreaterOrEqualTo(value!.Value); //Si value is null, MaxValue(définie) ne peut que lui être inf.
    }

    public bool IsMaxLowerThan(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMaxDefined)
            return false; //MaxValue ne peut être inf. à qqch !

        return (valueIsNull) ? true : MaxValue!.Value.IsLowerThan(value!.Value); //Si value is null, MaxValue(définie) lui est forcément inf.
    }

    public bool IsMaxLowerOrEqualTo(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMaxDefined)
            return valueIsNull; //Egalité que si value vaut null

        return (valueIsNull) ? true : MaxValue!.Value.IsLowerOrEqualTo(value!.Value); //Si value is null, MaxValue(définie) lui est forcément inf.
    }

    public bool IsMaxEqualTo(T? value)
    {
        bool valueIsNull = value is null; //value non définie

        if (!IsMaxDefined)
            return valueIsNull; //Egalité que si value vaut null

        return (valueIsNull) ? false : MaxValue!.Value.IsEqualTo(value!.Value); //Si value is null, MaxValue lui est forcément sup.
    }
    #endregion Comparaisons du Max, à ...
}