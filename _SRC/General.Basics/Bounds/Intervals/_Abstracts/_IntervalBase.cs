using General.Basics.Bounds.Exceptions;
using General.Basics.Bounds.MinAndMax;
using General.Basics.Extensions;

namespace General.Basics.Bounds.Intervals.Abstracts;

public abstract record IntervalBase<T>
    where T : struct, IComparable<T> // struct => type valeur non nullable
{
    public MinAndMax<T> Bounds { get; }

    public T MinValue => Bounds.MinValue;
    public T MaxValue => Bounds.MaxValue;


    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public IntervalBase(MinAndMax<T> bounds)
    {
        Bounds = bounds;
    }

    /// <summary>
    /// Retourne true si this et interval comportent au moins une valeur en commun.
    /// </summary>
    public bool Intersects(IntervalBase<T> interval) =>
        Contains(interval.MinValue) || Contains(interval.MaxValue)
        ||
        interval.Contains(MinValue) || interval.Contains(MaxValue);

    /// <summary>
    /// Retourne true si interval est inclus dans this.
    /// Rem.: si interval a une borne égale à celle correspondante de this, alors cette borne est considérée comme incluse dans this.
    /// </summary>
    public bool Contains(IntervalBase<T> interval) =>
        Contains(interval.MinValue) && Contains(interval.MaxValue);

    /// <summary>
    /// Retourne true si : this.MinValue &lt;= value &lt;= this.MaxValue.
    /// </summary>
    public bool Contains(T value)
    {
        var result = MinValue.IsLowerOrEqualTo(value) && MaxValue.IsGreaterOrEqualTo(value);
        return result;
    }

    /// <summary>
    /// Retourne les bornes min. et max. de l'éventuelle zone d'intersection, entre this et interval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    protected MinAndMax<T>? GetIntersectionMinAndMax(IntervalBase<T> interval)
    {
        T maxMinValue = interval.MinValue.IsGreaterThan(MinValue) ? interval.MinValue : MinValue;
        T minMaxValue = interval.MaxValue.IsLowerThan(MaxValue) ? interval.MaxValue : MaxValue;

        try
        {
            MinAndMax<T>? retour = new MinAndMax<T>(maxMinValue, minMaxValue);
            return retour;
        }
        catch (ValueShouldBeLowerOrEqualToException<T>)
        {
            // Pas d'intersection.
            return null;
        }
    }
}
