// See https://aka.ms/new-console-template for more information

using Utility;
using File = Utility.File;

Log.Line($"Puzzle 1 Test: {DoPuzzle1("day7_puzzle1_test.txt")}");
Log.Line($"Puzzle 1: {DoPuzzle1("day7_puzzle1.txt")}");

Log.Line($"Puzzle 2 Test: {DoPuzzle2("day7_puzzle2_test.txt")}");
Log.Line($"Puzzle 2: {DoPuzzle2("day7_puzzle2.txt")}");


long DoPuzzle1(string file)
{
    var data = File.GetTextFileValues(file);
    throw new NotImplementedException();
}

long DoPuzzle2(string file)
{
    var data = File.GetTextFileValues(file);
    throw new NotImplementedException();
}