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
            var checksum = GetTransformedSumFromLine(line);
            totalSum += checksum;
        }

        return totalSum;
    }
    
    public static Dictionary<string,char> DigitMap = new Dictionary<string, char>()
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
                // var isMatch = false;
                foreach (var numberMapKey in DigitMap.Keys)
                {
                    if(i + numberMapKey.Length > line.Length)
                    {
                        // skip digit names that are too long
                        continue;
                    }
                    if (line.Substring(i).StartsWith(numberMapKey))
                    {
                        // found a match append digit and skip ahead
                        transformedStringBuilder.Append(DigitMap[numberMapKey]);
                        // isMatch = true;
                        break;
                    }
                }

                // if (isMatch == false)
                // {
                //     // no match 
                //     transformedStringBuilder.Append(line[i]);
                // }
            }
        }

        var transformedString = transformedStringBuilder.ToString();
        
        var sum = GetChecksumFromLine(transformedString);
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
                foreach (var numberMapKey in DigitMap.Keys)
                {
                    if(i + numberMapKey.Length > line.Length)
                    {
                        continue;
                    }
                    if (line.Substring(i, numberMapKey.Length).StartsWith(numberMapKey))
                    {
                        // found a match
                        transformedStringBuilder.Append(DigitMap[numberMapKey]);
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

        
        var sum = GetChecksumFromLine(transformedString);
        Console.WriteLine($"{line} -> {transformedString} = {sum}");
        return sum;
    }
    
    public static int GetSum(string file)
    {
        var lines = File.GetTextFileValues(file);

        var totalSum = 0;
        
        foreach (var line in lines)
        {
            var sum = GetChecksumFromLine(line);
            totalSum += sum;
        }

        return totalSum;
    }

    public static int GetChecksumFromLine(string line)
    {
        var firstDigit = line.First(char.IsDigit);
        var lastDigit = line.Last(char.IsDigit);
        var checksum = int.Parse(string.Concat(firstDigit, lastDigit));
        return checksum;
    }
}