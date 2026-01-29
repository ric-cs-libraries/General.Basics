using System.Diagnostics.CodeAnalysis;

namespace General.Basics.Extensions.Dates.Static;

[ExcludeFromCodeCoverage]
public static class DateTimeOffsetExtension
{
    /// <summary>
    ///  Current Timestamp in MILLISECONDS.
    /// </summary>
    public static long GetCurrentMsTimeStamp_()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// Current Timestamp in SECONDS.
    /// </summary>
    public static long GetCurrentTimeStamp_()
    {
        return GetCurrentMsTimeStamp_() / 1000;
    }

}
