using Utility;
using File = Utility.File;

const int MAX_PARALLELISM = 8;

Log.Level = 1;

Log.Line($"Puzzle 1 Test: {DoPuzzle1("day5_test.txt")}");
Log.Line($"Puzzle 1: {DoPuzzle1("day5.txt")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day5_test.txt")}");
Log.Line($"Puzzle 2: {DoPuzzle2("day5.txt")}");

long DoPuzzle1(string file)
{
    var (seeds, maps) = Parse(file);
    
    var staticMap = new StaticMaps()
    {
        SeedToSoil = maps["seed-to-soil"],
        SoilToFertilizer = maps["soil-to-fertilizer"],
        FertilizerToWater = maps["fertilizer-to-water"],
        WaterToLight = maps["water-to-light"],
        LightToTemperature = maps["light-to-temperature"],
        TemperatureToHumidity = maps["temperature-to-humidity"],
        HumidityToLocation = maps["humidity-to-location"],
    };
    
    // do puzzle computation
    var min = seeds.Select(s => GetSeedLocation(s, staticMap)).Min();
    return min;
}

long DoPuzzle2(string file)
{
    var (seeds, maps) = Parse(file);

    var staticMap = new StaticMaps()
    {
        SeedToSoil = maps["seed-to-soil"],
        SoilToFertilizer = maps["soil-to-fertilizer"],
        FertilizerToWater = maps["fertilizer-to-water"],
        WaterToLight = maps["water-to-light"],
        LightToTemperature = maps["light-to-temperature"],
        TemperatureToHumidity = maps["temperature-to-humidity"],
        HumidityToLocation = maps["humidity-to-location"],
    };
    
    var min = seeds.Select((value, index) => new { value, index })
        .GroupBy(x => x.index / 2, x => x.value)  // group into pairs
        .OrderByDescending(g => g.Last()) // schedule the largest seed groups first
        .AsParallel().WithDegreeOfParallelism(MAX_PARALLELISM) // execute in parallel pinned to Performance Core Count
        .SelectMany(g => GenerateSeeds(g.First(), g.Last())) // flatten results into IEnum<long>
        .Select(s => GetSeedLocation(s, staticMap)) 
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
    
    var result = maps.ToDictionary(k => k.Key, v => v.Value.OrderBy(v => v.Range.Start).ToArray());
    
    return (seeds, result);
}

long GetSeedLocation(long seed, StaticMaps maps)
{
    var seedSoil = GetValueOrDefault(maps.SeedToSoil, seed);
    var soilFertilizer = GetValueOrDefault(maps.SoilToFertilizer, seedSoil);
    var fertilizerWater = GetValueOrDefault(maps.FertilizerToWater, soilFertilizer);
    var waterLight = GetValueOrDefault(maps.WaterToLight, fertilizerWater);
    var lightTemperature = GetValueOrDefault(maps.LightToTemperature, waterLight);
    var temperatureHumidity = GetValueOrDefault(maps.TemperatureToHumidity, lightTemperature);
    var humidityLocation = GetValueOrDefault(maps.HumidityToLocation, temperatureHumidity);
    
    #if DEBUG
    Log.Info($"Seed {seed}, soil {seedSoil}, fertilizer {soilFertilizer}, water {fertilizerWater}, light {waterLight}, temperature {lightTemperature}, humidity {temperatureHumidity}, location: {humidityLocation}");
    #endif
    
    return humidityLocation;
}

long GetValueOrDefault(Mapping[] maps, long from)
{
    for(int i = 0; i < maps.Length; i++)
    {
        if (from.Between(maps[i].Range.Start, maps[i].Range.End))
        {
            return from + maps[i].Offset;
        }
    }
    // identity transformation for missing ranges
    return from;
}

struct StaticMaps()
{
    public Mapping[] SeedToSoil;
    public Mapping[] SoilToFertilizer;
    public Mapping[] FertilizerToWater;
    public Mapping[] WaterToLight;
    public Mapping[] LightToTemperature;
    public Mapping[] TemperatureToHumidity;
    public Mapping[] HumidityToLocation;
}

