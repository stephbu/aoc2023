using Day7;
using Utility;
using File = Utility.File;

Log.Level = 1;


Log.Line($"Puzzle 1 Test: {DoPuzzle1("day7_puzzle1_test.txt")}");
Log.Line($"Puzzle 1: {DoPuzzle1("day7_puzzle1.txt")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day7_puzzle1_test.txt")}");
Log.Line($"Puzzle 2: {DoPuzzle2("day7_puzzle1.txt")}");

long DoPuzzle1(string file)
{
    var data = File.GetTextFileValues(file);
    var hands = ParsePuzzle(data, new Puzzle1Comparer());
    var handsByRank = hands.Order().ToArray();
    var sum = handsByRank.Select((h, i) => h.Bid * (i + 1)).Sum();
    
    return sum;
}

long DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);
    var hands = ParsePuzzle(data, new Puzzle2Comparer());
    var handsByRank = hands.Order().ToArray();
    var sum = handsByRank.Select((h, i) => h.Bid * (i + 1)).Sum();
    
    return sum;
}

Hand[] ParsePuzzle(IEnumerable<string> lines, IHandComparer comparer)
{
    var hands = new List<Hand>();

    foreach (var line in lines)
    {
        var segments = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var hand = new Hand(
            segments[0], 
            long.Parse(segments[1]), 
            comparer);
        
        hands.Add(hand);
    }

    return hands.ToArray();
}