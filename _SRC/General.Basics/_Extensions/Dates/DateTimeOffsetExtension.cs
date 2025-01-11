namespace General.Basics.Extensions.Dates;

public static class DateTimeOffsetExtension
{
    public static long GetCurrentTimeStamp_()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

}
