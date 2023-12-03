// See https://aka.ms/new-console-template for more information

using File = Utility.File;

Console.WriteLine($"Puzzle 1: day3_test.txt = {DoPuzzle1("day3_test.txt")}");
Console.WriteLine($"Puzzle 1: day3.txt = {DoPuzzle1("day3.txt")}");

Console.WriteLine($"Puzzle 2: day3_test.txt = {DoPuzzle2("day3_test.txt")}");
Console.WriteLine($"Puzzle 2: day3.txt = {DoPuzzle2("day3.txt")}");

int DoPuzzle1(string file)
{
    var data = File.GetTextFileValues(file);
    var (numbers, symbols) = Parse(data);
    var sum = numbers.Where(n => n.IsPartNumber(symbols)).Sum(n => n.Value);

    return sum;
}


int DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);
    var (numbers, symbols) = Parse(data);
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
                    numberAcc = "";
                }

                if (c != '.')
                {
                    symbols1.Add(new Symbol { Value = c, Location = new Coordinate { X = x, Y = y } });
                }
            }
        }

        // close off end of line numbers
        if (numberAcc != "")
        {
            currentNumber.end = new Coordinate { X = line.Length - 1, Y = y };
            currentNumber.Value = int.Parse(numberAcc);
            list.Add(currentNumber);
            numberAcc = "";
        }

        // next line
        y++;
    }

    return (list, symbols1);
}