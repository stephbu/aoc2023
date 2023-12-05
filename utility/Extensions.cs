namespace Utility;

public static class Extensions
{
    public static bool Between(this long value, long? start, long? end) => start != null && end != null && (value >= start && value <= end);
}
