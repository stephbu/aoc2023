// See https://aka.ms/new-console-template for more information

using File = Utility.File;

Console.WriteLine($"{DoPuzzle1("day3_test.txt")}");
Console.WriteLine($"{DoPuzzle1("day3.txt")}");

Console.WriteLine($"{DoPuzzle2("day3_test.txt")}");
Console.WriteLine($"{DoPuzzle2("day3.txt")}");

int DoPuzzle1(string file)
{
    var data = File.GetTextFileValues(file);

    var (numbers, symbols) = Parse(data);

    var parts = numbers.Where(n => n.IsPartNumber(symbols)).OrderBy(p => p.start.Y).ThenBy(p => p.start.X).ToArray();
    foreach (var part in parts) Console.WriteLine($"Part: {part.Value} ({part.start.X},{part.start.Y})");

    var sum = numbers.Where(n => n.IsPartNumber(symbols)).Sum(n => n.Value);

    return sum;
}


int DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);

    var (numbers, symbols) = Parse(data);

    var parts = numbers.Where(n => n.IsPartNumber(symbols)).OrderBy(p => p.start.Y).ThenBy(p => p.start.X).ToArray();
    foreach (var part in parts) Console.WriteLine($"Part: {part.Value} ({part.start.X},{part.start.Y})");

    var sum = symbols.Sum(s => s.GearRatio(numbers));

    return sum;
}

(List<Number> numbers, List<Symbol> symbols) Parse(IEnumerable<string> enumerable)
{
    var list = new List<Number>();
    var symbols1 = new List<Symbol>();

    var currentNumber = new Number();
    var numberAcc = "";

    var y = 0;
    foreach (var line in enumerable)
    {
        Console.WriteLine($"{line}");
        for (var x = 0; x < line.Length; x++)
        {
            var c = line[x];

            if (char.IsDigit(c))
            {
                if (numberAcc == "")
                    currentNumber = new Number
                    {
                        start = new Coordinate { X = x, Y = y }
                    };

                numberAcc += c;
            }
            else
            {
                if (numberAcc != "")
                {
                    currentNumber.end = new Coordinate { X = x - 1, Y = y };
                    currentNumber.Value = int.Parse(numberAcc);
                    list.Add(currentNumber);
                    Console.WriteLine($"Number: {currentNumber.Value}");
                    numberAcc = "";
                }

                if (c != '.')
                {
                    symbols1.Add(new Symbol { Value = c, Location = new Coordinate { X = x, Y = y } });
                    Console.WriteLine($"Symbol: {c}({x},{y})");
                }
            }
        }

        // close off end of line numbers
        if (numberAcc != "")
        {
            currentNumber.end = new Coordinate { X = line.Length - 1, Y = y };
            currentNumber.Value = int.Parse(numberAcc);
            list.Add(currentNumber);
            Console.WriteLine($"Number: {currentNumber.Value}");
            numberAcc = "";
        }

        // next line
        y++;
    }

    return (list, symbols1);
}

public struct Number
{
    public int Value;
    public Coordinate start;
    public Coordinate end;

    public bool IsPartNumber(List<Symbol> parts)
    {
        foreach (var part in parts)
            // perimeter check
            if (part.Location.X >= start.X - 1 && part.Location.X <= end.X + 1 && part.Location.Y >= start.Y - 1 &&
                part.Location.Y <= end.Y + 1)
                return true;
        return false;
    }
}

public struct Coordinate
{
    public int X;
    public int Y;
}

public struct Symbol
{
    public char Value;
    public Coordinate Location;

    public int GearRatio(List<Number> numbers)
    {
        var discovered = new List<Number>();

        foreach (var number in numbers)
            // number perimeter check
            if (Location.X >= number.start.X - 1 && Location.X <= number.end.X + 1 &&
                Location.Y >= number.start.Y - 1 && Location.Y <= number.end.Y + 1)
                discovered.Add(number);

        if (discovered.Count == 2) return discovered[0].Value * discovered[1].Value;

        return 0;
    }
}