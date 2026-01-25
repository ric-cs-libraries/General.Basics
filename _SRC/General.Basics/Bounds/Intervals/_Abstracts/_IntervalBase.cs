using General.Basics.Bounds.Exceptions;
using General.Basics.Extensions;

namespace General.Basics.Bounds.Intervals.Abstracts;

public abstract record IntervalBase<T>
    where T : struct, IComparable<T> // struct => type valeur non nullable
{
    public MinAndMax<T> Bounds { get; }

    public T? MinValue => Bounds.MinValue;
    public T? MaxValue => Bounds.MaxValue;


    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public IntervalBase(MinAndMax<T> bounds)
    {
        Bounds = bounds;
    }

    /// <exception cref="ValueShouldBeLowerOrEqualToException{T}">When minValue(defined) &gt; maxValue(defined)</exception>
    public IntervalBase(T? minValue, T? maxValue) : this(new MinAndMax<T>(minValue, maxValue))
	{
    }

    /// <summary>
    /// Retourne true si : this.MinValue &lt;= value &lt;= this.MaxValue.
    /// </summary>
    public bool Contains(T? value)
    {
        var result = Bounds.IsMinLowerOrEqualTo(value) && Bounds.IsMaxGreaterOrEqualTo(value);
        return result;
    }

    /// <summary>
    /// Retourne true si this et interval comportent au moins une valeur en commun.
    /// </summary>
    public bool Intersects(IntervalBase<T> interval) =>
            interval.Bounds.IsMinGreaterOrEqualTo(MinValue) && interval.Bounds.IsMinLowerOrEqualTo(MaxValue)
        || interval.Bounds.IsMaxGreaterOrEqualTo(MinValue) && interval.Bounds.IsMaxLowerOrEqualTo(MaxValue)
        || interval.Contains(this);
                   //!(   /*Bounds.IsMinDefined && interval.Bounds.IsMaxDefined &&*/ Bounds.IsMinGreaterThan(interval.MaxValue)
           //   ||
    /*Bounds.IsMaxDefined && interval.Bounds.IsMinDefined &&*/
    //);

    /// <summary>
    /// Retourne true si interval est inclus dans this.
    /// Rem.: si interval a une borne égale à celle correspondante de this, alors cette borne est considérée comme incluse dans this.
    /// </summary>
    public bool Contains(IntervalBase<T> interval) =>
        interval.Bounds.IsMinGreaterOrEqualTo(MinValue) && interval.Bounds.IsMaxLowerOrEqualTo(MaxValue);
            //(/*Bounds.IsMaxDefined && interval.Bounds.IsMaxDefined &&*/ interval.Bounds.IsMaxLowerOrEqualTo(MaxValue) /* || !Bounds.IsMaxDefined*/)
            //&&
            //(/*Bounds.IsMinDefined && interval.Bounds.IsMinDefined &&*/ Bounds.IsMinLowerOrEqualTo(interval.MinValue) /*|| !Bounds.IsMinDefined*/)
            //;


    // >>>>>>> TESTER TOUTES les méthodes : ISMinX.... et IsMaxX... de la classe MinAndMax. <<<<<<<<<<<<<<<<<<<<<<<<<<<<

    ///// <summary>
    ///// Retourne true si this commence avant interval ou à la borne de début d'interval.
    ///// </summary>
    //public bool StartsBeforeOrOnStart(IntervalBase<T> interval) => !HasLowerBound || interval.HasLowerBound && IsLowerOrEqual(MinValue, interval.MinValue);

    ///// <summary>
    ///// Retourne true si this commence après interval ou à la borne de fin d'interval.
    ///// </summary>
    //public bool StartsAfterOrOnEnd(IntervalBase<T> interval) => HasLowerBound && interval.HasHigherBound && IsLowerOrEqual(interval.MaxValue, MinValue);

    ///// <summary>
    ///// Retourne true si this finit avant le début d'interval ou à la borne de début d'interval.
    ///// </summary>
    //public bool EndsBeforeOrOnStart(IntervalBase<T> interval) => HasHigherBound && interval.HasLowerBound && IsLowerOrEqual(MaxValue, interval.MinValue);

    ///// <summary>
    ///// Retourne true si this finit après la fin d'interval ou à la borne de fin d'interval.
    ///// </summary>
    //public bool EndsAfterOrOnEnd(IntervalBase<T> interval) => !HasHigherBound || interval.HasHigherBound && IsLowerOrEqual(interval.MaxValue, MaxValue);

    ///// <summary>
    ///// Retourne la borne min. la plus élevée (compare : interval1.MinValue et interval2.MinValue). 
    ///// </summary>
    //public static T GetMaxMinValue(IntervalBase<T> interval1, IntervalBase<T> interval2)
    //    => interval1.StartsBeforeOrOnStart(interval2) ? interval2.MinValue : interval1.MinValue;

    ///// <summary>
    ///// Retourne la borne max. la plus petite (compare : interval1.MaxValue et interval2.MaxValue). 
    ///// </summary>
    //public static T GetMinMaxValue(IntervalBase<T> interval1, IntervalBase<T> interval2)
    //    => interval1.EndsAfterOrOnEnd(interval2) ? interval2.MaxValue : interval1.MaxValue;

    
    /// <summary>
    /// Retourne les bornes min. et max. de l'éventuelle zone d'intersection, entre this et interval.
    /// </summary>
    /// <returns>null si pas d'intersection</returns>
    protected MinAndMax<T>? GetIntersectionMinAndMax(IntervalBase<T> interval)
    {
        T? maxMinValue = interval.Bounds.IsMinGreaterThan(MinValue) ? interval.MinValue : MinValue;
        T? minMaxValue = interval.Bounds.IsMaxLowerThan(MaxValue) ? interval.MaxValue : MaxValue;

        if (maxMinValue.HasValue && minMaxValue.HasValue && maxMinValue.Value.IsGreaterThan(minMaxValue.Value))
            return null;

        MinAndMax<T>? retour = new MinAndMax<T>(maxMinValue, minMaxValue);
        return retour;
    }
}
