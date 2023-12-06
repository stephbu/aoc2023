namespace Utility;

public class Linq
{
    public static IEnumerable<long> Sequence(long start, long end) 
    {
        for (long i = start; i <= end; i++)
        {
            yield return i;
        }
    }
}