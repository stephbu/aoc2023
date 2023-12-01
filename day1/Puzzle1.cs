namespace Day1;

using Utility;

public static class Puzzle1
{
    public static void Do()
    {
        Console.WriteLine("Day1, Puzzle 1");

        Console.WriteLine($"Test Data: {Day1.GetSum("puzzle1_data_test.txt")}");
        Console.WriteLine($"Puzzle 1 Data: {Day1.GetSum("day1.txt")}");
    }
}