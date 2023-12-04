using File = Utility.File;

Console.WriteLine($"{DoPuzzle1("day4_test.txt")}");
Console.WriteLine($"{DoPuzzle1("day4.txt")}");

Console.WriteLine($"{DoPuzzle2("day4_test.txt")}");
Console.WriteLine($"{DoPuzzle2("day4.txt")}");

double DoPuzzle1(string file)
{
    var data = File.GetTextFileValues(file);
    var games = Game.Parse(data);
    var sum = games.Select(g => !g.MatchingNumbers.Any() ? 0 : Math.Pow(2,g.MatchingNumbers.Count() - 1)).Sum();
    
    return sum;
}

int DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);
    var games = Game.Parse(data).Select(g => new GameCardInstance{Game=g,Instance=1}).ToArray();
    
    for(int x = 0; x < games.Length; x++)
    {
        var matchingNumbers = games[x].Game.MatchingNumbers.Count();
        if(matchingNumbers > 0)
        {
            for(int y = 1; y <= matchingNumbers && x + y < games.Length; y++)
            {
                games[x + y].Instance += games[x].Instance;
            }
        }
    }
    
    return games.Sum(g => g.Instance);
}

