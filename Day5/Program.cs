using Utility;
using File = Utility.File;

Log.Level = 1;

Log.Line($"Puzzle 1 Test: {DoPuzzle1("day5_test.txt")}");
Log.Line($"Puzzle 1: {DoPuzzle1("day5.txt")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day5_test.txt")}");
Log.Line($"Puzzle 2: {DoPuzzle2("day5.txt")}");

long DoPuzzle1(string file)
{
    var (seeds, maps) = Parse(file);
    
    // do puzzle computation
    var min = seeds.Select(s => GetSeedLocation(s, maps)).Min();
    return min;
}

long DoPuzzle2(string file)
{
    var (seeds, maps) = Parse(file);
    var min = seeds.Select((value, index) => new { value, index })
        .GroupBy(x => x.index / 2, x => x.value)  // group into pairs
        .AsParallel()
        .SelectMany(g => GenerateSeeds(g.First(), g.Last())) // flatten results into IEnum<long>
        .Select(s => GetSeedLocation(s, maps)) 
        .Min();
    
    return min;
}

IEnumerable<long> GenerateSeeds(long start, long count)
{
    var startTime = DateTime.Now;
    Log.Line($"Generating Seeds: {count} ({start} - {start + count - 1})");
    for (long x = 0; x < count; x++)
    {
        yield return start + x;
    }
    var endTime = DateTime.Now;
    Log.Line($"Generated Seeds: {count} {count/(endTime - startTime).TotalSeconds} seeds/second");
}   

(IEnumerable<long>, Dictionary<string, Mapping[]>) Parse(string file)
{
    // dictionary source -> destination -> source id -> destination id
    var seeds = new List<long>();
    var maps = new Dictionary<string, List<Mapping>>();
    
    var data = File.GetTextFileValues(file);
    
    var inMapSection = false;
    var sectionName = "";
    foreach(var line in data)
    {
        if (line.StartsWith("seeds:"))
        {
            var seedsRaw = line.Replace("seeds:", "").Trim().Split(" ");
            seeds.AddRange(seedsRaw.Select(i => long.Parse(i)));
            Log.Info($"Seeds Added: {seedsRaw.Length}");
            continue;
        }

        if (line == "" && inMapSection)
        {
            // map footer
            Log.Info($"End Map Section: {sectionName}");
            inMapSection = false;
            continue;
        }
        
        if (line == "" && !inMapSection)
        {
            // blank line
            continue;
        }
        
        if(line != "" && !inMapSection && line.EndsWith(" map:"))
        {
            // scrub line for transformation
            sectionName = line.Replace(" map:", "").Trim();
            
            // add map if not exists
            if(!maps.ContainsKey(sectionName))
            {
                maps.Add(sectionName, new List<Mapping>());
            }

            Log.Info($"Map Section: {sectionName}");
            inMapSection = true;
            continue;
        }
        
        // map value
        Log.Info($"Map Values: {line}");
        var mapSegments = line
            .Split(" ", StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries)
            .Select(i => long.Parse(i))
            .ToArray();

        var range = new Mapping()
        {
            Range = new Range{Start=mapSegments[1], End=mapSegments[1] + mapSegments[2] - 1},
            Offset = mapSegments[0] - mapSegments[1],
        };
        maps[sectionName].Add(range);
        Log.Info($"{sectionName} -> {range.Range} ({range.Offset})");
    }
    
    var result = maps.ToDictionary(k => k.Key, v => v.Value.ToArray());
    
    return (seeds, result);
}

long GetSeedLocation(long seed, Dictionary<string, Mapping[]> maps)
{
    var seedSoil = GetValueOrDefault(maps, seed, "seed-to-soil");
    var soilFertilizer = GetValueOrDefault(maps, seedSoil, "soil-to-fertilizer");
    var fertilizerWater = GetValueOrDefault(maps, soilFertilizer, "fertilizer-to-water");
    var waterLight = GetValueOrDefault(maps, fertilizerWater, "water-to-light");
    var lightTemperature = GetValueOrDefault(maps, waterLight, "light-to-temperature");
    var temperatureHumidity = GetValueOrDefault(maps, lightTemperature, "temperature-to-humidity");
    var humidityLocation = GetValueOrDefault(maps, temperatureHumidity, "humidity-to-location");
    
    #if DEBUG
    Log.Info($"Seed {seed}, soil {seedSoil}, fertilizer {soilFertilizer}, water {fertilizerWater}, light {waterLight}, temperature {lightTemperature}, humidity {temperatureHumidity}, location: {humidityLocation}");
    #endif
    
    return humidityLocation;
}

long GetValueOrDefault(Dictionary<string, Mapping[]> maps, long from, string fromTo)
{
    var matchingMapping = maps[fromTo].Where(m => from.Between(m.Range.Start,m.Range.End)).ToArray();
    if (matchingMapping.Length == 0)
    {
        // identity transformation for missing ranges
        return from;
    }
    return from + matchingMapping[0].Offset;
}

