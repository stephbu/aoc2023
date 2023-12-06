// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using Utility;
using File = Utility.File;

Log.Line($"Puzzle 1 Test: {DoPuzzle1("day6_puzzle1_test.txt")}");
Log.Line($"Puzzle 1: {DoPuzzle1("day6_puzzle1.txt")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day6_puzzle2_test.txt")}");
Log.Line($"Puzzle 2: {DoPuzzle2("day6_puzzle2.txt")}");


long DoPuzzle1(string file)
{
    var data = File.GetTextFileValues(file);
    var races = Parse(data);
    var sum = races.Select(r => r.GetWinningPermutations()).Aggregate((item, acc) => item * acc);
    
    return sum;
}

long DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);
    var races = Parse(data);
    
    string strDuration = races.Select(r => r.RaceDuration).Aggregate<long, string>("",(acc, item) => acc + item.ToString());
    string strDistance = races.Select(r => r.RecordDistance).Aggregate<long, string>("",(acc, item) => acc + item.ToString());;

    var race = new Race() { RaceDuration = long.Parse(strDuration), RecordDistance = long.Parse(strDistance) };
    
    var permutations = Linq.Sequence(1, race.RaceDuration)
        .AsParallel().WithDegreeOfParallelism(8)
        .Count(t => race.GetRacePermutation(t) > race.RecordDistance);

    return permutations;
}

Race[] Parse(IEnumerable<string> lines)
{
    List<Race> races = new List<Race>();

    var timesRaw = lines
        .Take(1)
        .First()
        .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        [1..]
        .Select(s => long.Parse(s))
        .ToArray();
    var distancesRaw = lines
        .Skip(1)
        .Take(1)
        .First()
        .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        [1..]
        .Select(s => long.Parse(s))
        .ToArray();

    for (int i = 0; i < timesRaw.Length; i++)
    {
        races.Add(new Race{RaceDuration = timesRaw[i], RecordDistance = distancesRaw[i]});
    }

    return races.ToArray();
}