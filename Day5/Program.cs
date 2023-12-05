using File = Utility.File;

//Console.WriteLine($"Puzzle 1 Test: {DoPuzzle1("day5_test.txt")}");
//Console.WriteLine($"Puzzle 1: {DoPuzzle1("day5.txt")}");

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
        .GroupBy(x => x.index / 2, x => x.value)
        .AsParallel()
        .SelectMany(g => GenerateSeeds(g.First(), g.Last()))
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

// long DoPuzzle2(string file)
// {
//     var (seeds, maps) = Parse(file);
//
//     var newSeeds = seeds.Select((value, index) => new { value, index })
//         .GroupBy(x => x.index / 2, x => x.value)
//         .Select(g => new Range()
//         {
//             Start = g.First(),
//             End = g.First() + g.Last() - 1,
//         }).ToArray();
//     
//     
//     var seedSoil = RemapRanges(newSeeds, maps["seed"]["soil"]).ToList();
//     var soilFertilizer = RemapRanges(seedSoil, maps["soil"]["fertilizer"]).ToList();
//     var fertilizerWater = RemapRanges(soilFertilizer, maps["fertilizer"]["water"]).ToList();
//     var waterLight = RemapRanges(fertilizerWater, maps["water"]["light"]).ToList();
//     var lightTemperature = RemapRanges(waterLight, maps["light"]["temperature"]).ToList();
//     var temperatureHumidity = RemapRanges(lightTemperature, maps["temperature"]["humidity"]).ToList();
//     var humidityLocation = RemapRanges(temperatureHumidity, maps["humidity"]["location"]).ToList();
//
//     var min = humidityLocation.MinBy(r => r.Start);
//     return 0;
// }

/*
Range[] RemapRanges(IEnumerable<Range> ranges, List<SourceRangeDestRange> map)
{
    var remapped = new List<Range>();
    var mappings = map.OrderBy(r => r.SourceStart).ToArray();

    foreach (var range in ranges)
    {
        var overlappingMappings = mappings.Where(r => (range.Start.Between(r.SourceStart, r.SourceEnd))
                                                       || (range.End.Between(r.SourceStart, r.SourceEnd))
                                                       || (r.SourceStart.Between(range.Start, range.End) &&
                                                           r.SourceEnd.Between(range.Start, range.End))
        ).ToArray();

        if (overlappingMappings.Any())
        {
            // there are overlaps
            foreach (var overlappedMap in overlappingMappings)
            {
                var overlapRange = new Range(){Start = overlappedMap.SourceStart, End = overlappedMap.SourceEnd};
                
                var join = range.Join(rnage, overlappedMap.Offset).ToArray();
                var zeroStart = join.Any(r => r.Start == 0);
                
                remapped.AddRange(join);
            }
        }
        else
        {
            // no overlaps
            remapped.Add(range);
        }
    }

    var result = remapped.OrderBy(r => r.Start).ToArray();
    
    return result;
}
*/

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

struct SourceRangeDestRange
{
    public long SourceStart;
    public long SourceEnd;
    public long Offset;
    public long DestStart;
    public long DestEnd;

    public override string ToString()
    {
        return $"{SourceStart}-{SourceEnd} ({Offset})";

    }

}

struct Range
{
    public Range Offset(long offset)
    {
        return new Range()
        {
            Start = this.Start + offset,
            End = this.End + offset,
        };
    }
    
    public override string ToString()
    {
        return $"[{Start},{End}]";
    }

    public long Start;
    public long End;

    public IEnumerable<Range> Join(IEnumerable<SourceRangeDestRange> sdranges, long offset)
    {
        var remainingRanges = new List<Range>();
        var results = new List<Range>();
        
        
        
        
        
        /*if (overlappingMappings.Any())
        {
            // there are overlaps
            foreach (var overlappedMap in overlappingMappings)
            {
                var overlapRange = new Range(){Start = overlappedMap.SourceStart, End = overlappedMap.SourceEnd};

                var join = currentRange.Join(overlapRange, overlappedMap.Offset).ToArray();
                var zeroStart = join.Any(r => r.Start == 0);

                results.AddRange(join);

                if (zeroStart)
                {
                    // we have a zero start, so we need to remap the remaining ranges
                    remainingRanges = remainingRanges.Select(r => r.Offset(overlappedMap.Offset)).ToList();
                }
                else
                {
                    // we have a non-zero start, so we need to remap the remaining ranges
                    remainingRanges = remainingRanges.Select(r => r.Offset(overlappedMap.Offset)).ToList();
                }
            }
        }
        else
        {
            // no overlaps
            results.Add(currentRange);
        }




        // if this range starts in the other range
        if(this.Start >= r.Start && this.End <= r.End)
        {
            yield return new Range()
            {
                Start = this.Start + offset,
                End = this.End + offset
            };
            yield break;
        }

        // if this range starts before the other range and ends in the middle of the other range
        if(this.Start < r.Start && this.End >= this.Start && this.End <= r.End)
        {
            // pass through mapping
            yield return new Range()
            {
                Start = this.Start,
                End = r.Start - 1,
            };

            // remapping
            yield return new Range()
            {
                Start = r.Start + offset,
                End = this.End + offset,
            };
            yield break;
        }

        // if this range starts in the middle of the other range and ends after the other range
        if(this.Start >= r.Start && this.Start <= r.End && this.End > r.End)
        {
            // remapping
            yield return new Range()
            {
                Start = this.Start + offset,
                End = r.End + offset,
            };

            // return balance of range without remapping
            yield return new Range()
            {
                Start = r.End + 1,
                End = this.End,
            };
            yield break;
        }

        // if this range starts before the other range and ends after the other range

        if(this.Start < r.Start && this.End > r.End)
        {
            // initial segment
            yield return new Range()
            {
                Start = this.Start,
                End = r.Start - 1,
            };

            yield return new Range()
            {
                Start = r.Start + offset,
                End = r.End + offset,
            };
            // final segment
            yield return new Range()
            {
                Start = r.End + 1,
                End = this.End,
            };
            yield break;
        }

        // no overlap
        yield return this;
    } */
        return results;
    }
}

public static class Extensions
{
    public static bool Between(this long value, long start, long end) => value >= start && value <= end;
}



