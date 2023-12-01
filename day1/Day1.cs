using System.Text;
using File = Utility.File;

namespace Day1;

public class Day1
{
    public static int GetTransformedSum(string file)
    {
        var lines = File.GetTextFileValues(file);

        var totalSum = 0;
        
        foreach (var line in lines)
        {
            var sum = GetTransformedSumFromLine(line);
            totalSum += sum;
        }

        return totalSum;
    }
    
    public static Dictionary<string,char> NumberMap = new Dictionary<string, char>()
    {
        {"one", '1'},
        {"two", '2'},
        {"three", '3'},
        {"four", '4'},
        {"five", '5'},
        {"six", '6'},
        {"seven", '7'},
        {"eight", '8'},
        {"nine", '9'},
    };
    
    public static int GetTransformedSumFromLine(string line)
    {
        
        var transformedStringBuilder = new StringBuilder();
        for(int i=0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                transformedStringBuilder.Append(line[i]);
            }
            else
            {
                var isMatch = false;
                foreach (var numberMapKey in NumberMap.Keys)
                {
                    if(i + numberMapKey.Length > line.Length)
                    {
                        continue;
                    }
                    if (line.Substring(i, numberMapKey.Length).StartsWith(numberMapKey))
                    {
                        // found a match
                        transformedStringBuilder.Append(NumberMap[numberMapKey]);
                        isMatch = true;
                        break;
                    }
                }

                if (isMatch == false)
                {
                    // no match 
                    transformedStringBuilder.Append(line[i]);
                }
            }
        }

        var transformedString = transformedStringBuilder.ToString();
        
        var sum = GetSumFromLine(transformedString);
        Console.WriteLine($"{line} -> {transformedString} = {sum}");
        return sum;
    }

    
    public static int GetTransformedSumFromLineOld(string line)
    {
        
        var transformedStringBuilder = new StringBuilder();
        for(int i=0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                transformedStringBuilder.Append(line[i]);
            }
            else
            {
                var isMatch = false;
                foreach (var numberMapKey in NumberMap.Keys)
                {
                    if(i + numberMapKey.Length > line.Length)
                    {
                        continue;
                    }
                    if (line.Substring(i, numberMapKey.Length).StartsWith(numberMapKey))
                    {
                        // found a match
                        transformedStringBuilder.Append(NumberMap[numberMapKey]);
                        i = i + numberMapKey.Length - 1;
                        isMatch = true;
                        break;
                    }
                }

                if (isMatch == false)
                {
                    // no match 
                    transformedStringBuilder.Append(line[i]);
                }
            }
        }

        var transformedString = transformedStringBuilder.ToString();

        
        var sum = GetSumFromLine(transformedString);
        Console.WriteLine($"{line} -> {transformedString} = {sum}");
        return sum;
    }
    
    public static int GetSum(string file)
    {
        var lines = File.GetTextFileValues(file);

        var totalSum = 0;
        
        foreach (var line in lines)
        {
            var sum = GetSumFromLine(line);
            totalSum += sum;
        }

        return totalSum;
    }

    public static int GetSumFromLine(string line)
    {
        var firstDigit = line.First(char.IsDigit);
        var lastDigit = line.Last(char.IsDigit);
        var sum = int.Parse(string.Concat(firstDigit, lastDigit));
        return sum;
    }
}