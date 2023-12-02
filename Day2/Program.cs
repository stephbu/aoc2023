// See https://aka.ms/new-console-template for more information

using File = Utility.File;

var bagContents = new Draw { Red = 12, Green = 13, Blue = 14 };

Console.WriteLine(DoPuzzle1("day2_test.txt", bagContents));
Console.WriteLine(DoPuzzle1("day2.txt", bagContents));

Console.WriteLine(DoPuzzle2("day2_test.txt"));
Console.WriteLine(DoPuzzle2("day2.txt"));

int DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);
    var gameRecords = data.Select(GameRecord.Parse);
    var sum = gameRecords.Sum(CalculatePower);
    return sum;
}

int DoPuzzle1(string file, Draw limit)
{
    var data = File.GetTextFileValues(file);
    var gameRecords = data.Select(GameRecord.Parse);
    var sum = gameRecords.Where(g => g.IsPossible(limit)).Sum(g => g.Id);

    return sum;
}

int CalculatePower(GameRecord g)
{
    var draws = g.Draws;
    
    Draw minimumDraw = new Draw { Red = 0, Green = 0, Blue = 0};
    foreach (var draw in draws)
    {
        if(draw.Red > minimumDraw.Red)
        {
            minimumDraw.Red = draw.Red;
        }
        if(draw.Green > minimumDraw.Green)
        {
            minimumDraw.Green = draw.Green;
        }
        if(draw.Blue > minimumDraw.Blue)
        {
            minimumDraw.Blue = draw.Blue;
        }
    }
    
    return minimumDraw.Red * minimumDraw.Green * minimumDraw.Blue;
}