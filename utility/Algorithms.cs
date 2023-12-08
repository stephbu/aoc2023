namespace Utility;

public static class Algorithm
{
    
    public static long LowestCommonMultiple(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(LowestCommonMultiple);
    }   
    
    private static long LowestCommonMultiple(long a, long b)
    {
        return (a / GreatestCommonDivisor(a, b)) * b;
    }
    
    
    public static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            long t = b;
            b = a % b;
            a = t;
        }
        return a;
    }
}