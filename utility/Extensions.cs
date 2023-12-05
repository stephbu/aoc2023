namespace Utility;

public static class Extensions
{
    public static bool Between(this long value, long start, long end) => value >= start && value <= end;
}
