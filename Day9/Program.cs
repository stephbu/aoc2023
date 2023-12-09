using Utility;
using File = Utility.File;

Log.Level = 3;

Log.Line($"Puzzle 1 Test: {DoPuzzle1("day9_test.txt")}");
Log.Line($"Puzzle 1 : {DoPuzzle1("day9_puzzle1.txt")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day9_test2.txt")}");
Log.Line($"Puzzle 2 : {DoPuzzle2("day9_puzzle1.txt")}");

long DoPuzzle1(string file)
{
    
    var sensorData = Parse(file).ToArray();

    // make the predictions
    foreach(var sensor in sensorData)
    {
        // add a zero to the last row
        sensor.Last().Add(0);
        
        for(var i = sensor.Count - 1; i > 0; i--)
        {
            var currentRow = sensor[i];
            var priorRow = sensor[i - 1];
            priorRow.Add(currentRow[^1] + priorRow[^1]);
        }
    }

    var sum = sensorData.Sum(s => s[0][^1]);
    
    return sum;
}

long DoPuzzle2(string file)
{
    var sensors = Parse(file).ToArray();

    // make the predictions
    foreach(var sensor in sensors)
    {
        // add a zero to the last row
        sensor.Last().Insert(0,0);
        
        for(var i = sensor.Count - 1; i > 0; i--)
        {
            var currentRow = sensor[i];
            var priorRow = sensor[i - 1];
            priorRow.Insert(0,priorRow[0] - currentRow[0]);
        }
        
        foreach(var line in sensor)
        {
            Log.Line(string.Join(" ", line));
        }
        Log.Line("");
    }
    
    var sum = sensors.Sum(s => s[0][0]);
    
    return sum;
}

IEnumerable<SensorHistory> Parse(string file)
{
    foreach (var line in File.GetTextFileValues(file))
    {

        var sensorHistory = new SensorHistory();
        
        var last = line
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
        
        sensorHistory.Add(last); ;
        
        for(int index = 0; index < sensorHistory.Count; index++)
        {
            var nextRow = sensorHistory[index].Differences().ToList();
            sensorHistory.Add(nextRow);
            if(nextRow.All(x => x == 0))
            {
                break;
            }
        }
        
        yield return sensorHistory;
        foreach(var row in sensorHistory)
        {
            Log.Line(string.Join(" ", row));            
        }
        Log.Line("");
    }
}

public static class Extensions
{
    public static IEnumerable<long> Differences(this IEnumerable<long> values)
    {

        var enumerator = values.GetEnumerator();
        enumerator.MoveNext();
        var lastValue = enumerator.Current;
        while (enumerator.MoveNext())
        {
            var currentValue = enumerator.Current;
            yield return currentValue - lastValue;
            lastValue = currentValue;
        }
    }
}