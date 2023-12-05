using File = Utility.File;

Console.WriteLine($"Puzzle 1 Test: {DoPuzzle1("day5_test.txt")}");
Console.WriteLine($"Puzzle 1: {DoPuzzle1("day5.txt")}");

Console.WriteLine($"Puzzle 2 Test: {DoPuzzle2("day5_test.txt")}");
Console.WriteLine($"Puzzle 2: {DoPuzzle2("day5.txt")}");

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
    Console.WriteLine($"[{DateTime.UtcNow}] Seeds: {start} - {start + count - 1}");
    for (long x = 0; x < count; x++)
    {
        yield return start + x;
    }
}   

(IEnumerable<long>, Dictionary<string, Dictionary<string, List<SourceRangeDestRange>>>) Parse(string file)
{
    // dictionary source -> destination -> source id -> destination id
    var seeds = new List<long>();
    var maps = new Dictionary<string, Dictionary<string, List<SourceRangeDestRange>>>();
    
    var data = File.GetTextFileValues(file);
    
    var inMapSection = false;
    var mapFrom = "";
    var mapTo = "";
    foreach(var line in data)
    {
        if (line.StartsWith("seeds:"))
        {
            var seedsRaw = line.Replace("seeds:", "").Trim().Split(" ");
            seeds.AddRange(seedsRaw.Select(i => long.Parse(i)));
            // Console.WriteLine($"Seeds Added: {seedsRaw.Length}");
            continue;
        }

        if (line == "" && inMapSection)
        {
            // map footer
            // Console.WriteLine($"End Map Section: {mapFrom} -> {mapTo}");

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
            var mapLine = line.Replace(" map:", "").Trim();
            var mapLineSegments = mapLine.Split("-to-");
            mapFrom = mapLineSegments[0].Trim();
            mapTo = mapLineSegments[1].Trim();
            
            // add map if not exists
            if(!maps.ContainsKey(mapFrom))
            {
                maps.Add(mapFrom, new Dictionary<string, List<SourceRangeDestRange>>());
            }
            
            // add map if not exists
            if(!maps[mapFrom].ContainsKey(mapTo))
            {
                maps[mapFrom].Add(mapTo, new List<SourceRangeDestRange>());
            }

            Console.WriteLine($"Map Section: {mapFrom} -> {mapTo}");
            inMapSection = true;
            continue;
        }
        
        // map value
        // Console.WriteLine($"Map Values: {line}");
        var mapSegments = line
            .Split(" ", StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries)
            .Select(i => long.Parse(i))
            .ToArray();

        var range = new SourceRangeDestRange()
        {
            SourceStart = mapSegments[1], 
            SourceEnd = mapSegments[1] + mapSegments[2] - 1, 
            DestStart = mapSegments[0],
            DestEnd = mapSegments[0] + mapSegments[2] - 1,
            Offset = mapSegments[0] - mapSegments[1],
        };
        maps[mapFrom][mapTo].Add(range);
        Console.WriteLine($"{mapFrom} -> {mapTo}: {range.SourceStart} - {range.SourceEnd} -> {range.DestStart} - {range.DestEnd}");
    }
    return (seeds, maps);
}

long GetSeedLocation(long seed, Dictionary<string, Dictionary<string, List<SourceRangeDestRange>>> maps)
{
    var seedSoil = GetValueOrDefault(maps, seed, "seed", "soil");
    var soilFertilizer = GetValueOrDefault(maps, seedSoil, "soil", "fertilizer");
    var fertilizerWater = GetValueOrDefault(maps, soilFertilizer, "fertilizer", "water");
    var waterLight = GetValueOrDefault(maps, fertilizerWater, "water", "light");
    var lightTemperature = GetValueOrDefault(maps, waterLight, "light", "temperature");
    var temperatureHumidity = GetValueOrDefault(maps, lightTemperature, "temperature", "humidity");
    var humidityLocation = GetValueOrDefault(maps, temperatureHumidity, "humidity", "location");
    
    // Console.WriteLine($"Seed {seed}, soil {seedSoil}, fertilizer {soilFertilizer}, water {fertilizerWater}, light {waterLight}, temperature {lightTemperature}, humidity {temperatureHumidity}, location: {humidityLocation}");
    
    return humidityLocation;
}

long GetValueOrDefault(Dictionary<string, Dictionary<string, List<SourceRangeDestRange>>> maps, long from, string fromType, string toType)
{
    if (maps[fromType].ContainsKey(toType) && maps[fromType][toType].Any(r => r.SourceStart <= from && r.SourceEnd >= from))
    {
        var r = maps[fromType][toType].First(r => r.SourceStart <= from && r.SourceEnd >= from);
        
        var offset = from - r.SourceStart;
        
        return r.DestStart + offset;
    }
    // identity transformation for missing ranges
    return from;
}


