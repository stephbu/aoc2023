using Utility;
using File = Utility.File;

Log.Level = 3;

Log.Line($"Puzzle 1 Test: {DoPuzzle1("day8_test.txt", "AAA","ZZZ")}");
Log.Line($"Puzzle 1 Test2: {DoPuzzle1("day8_test2.txt", "AAA","ZZZ")}");
Log.Line($"Puzzle 1: {DoPuzzle1("day8_puzzle1.txt", "AAA","ZZZ")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day8_test3.txt", 'A','Z')}");
Log.Line($"Puzzle 2: {DoPuzzle2("day8_puzzle2.txt", 'A','Z')}");


long DoPuzzle1(string file, string start, string goal)
{
    (var instructions, var nodeDictionary) = Parse(file);
    
    var instructionList = InstructionList(instructions).GetEnumerator();
    var currentNode = nodeDictionary[start];
    var stepCount = 0;
    
    while (currentNode.Label != goal)
    {
        instructionList.MoveNext();
        
        if (instructionList.Current == 'L')
        {
            currentNode = nodeDictionary[currentNode.Left];
        }
        else
        {
            currentNode = nodeDictionary[currentNode.Right];
        }

        stepCount++;
    }

    return stepCount;
}

long DoPuzzle2(string file, char start, char goal)
{
    (var instructions, var nodeDictionary) = Parse(file);
    
    var stepCount = 0l;

    var nodewalkers = nodeDictionary
        .Keys
        .Where(k => k.EndsWith(start))
        .Select(k => new NodeWalker(k, goal, nodeDictionary))
        .ToArray();
    
    var instructionIndex = 0;
    int index = 0;
    int max = nodewalkers.Length;
    
    while(true)
    {
        var instruction = instructions[instructionIndex];
        if(++instructionIndex == instructions.Length)
        {
            instructionIndex = 0;
        }
        
        stepCount++;
        var hitGoal = 0;
        for(index = 0; index < max; index++)
        {
            var nw = nodewalkers[index];
            nw.Move(instruction);
            if (nw.LCM != 0)
            {
                hitGoal++;
            }

            if (hitGoal == max)
            {
                long lcm = Algorithm.LowestCommonMultiple(nodewalkers.Select(n => n.LCM));
                return lcm;
            }
        }
    }
}


IEnumerable<char> InstructionList(string instructions)
{
    var index = 0;
    while (true)
    {
        yield return instructions[index];
        index++;
        if (index >= instructions.Length)
        {
            index = 0;
        }
    }
}

(string, Dictionary<string, Node>) Parse(string file)
{
    var data = File.GetTextFileValues(file);
    var nodeDictionary = new Dictionary<string, Node>();

    var enumerator = data.GetEnumerator();

    enumerator.MoveNext();
    var instructions = enumerator.Current;

    enumerator.MoveNext();
    // skip blank line
    
    // load nodes
    while (enumerator.MoveNext())
    {
        var line = enumerator.Current;
        
        var segments = line.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var nodeList = segments[1].Substring(1, segments[1].Length - 2);
        var nodes = nodeList.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
        var node = new Node
        {
            Label = segments[0],
            Left = nodes[0],
            Right = nodes[1]
        };
        nodeDictionary.Add(segments[0], node);
    }
    
    return (instructions, nodeDictionary);
}