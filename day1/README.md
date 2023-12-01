# Day 1

## Reference
[https://adventofcode.com/2023/day/1](https://adventofcode.com/2023/day/1)

## Diary
State of mind: Excited, Ready to go

I had a productive day coding before which helps a lot.  Did some prework to get ready for AoC 2023.

- Dusted off my AoC Account
- Created a new private team for myself and new coworkers.
- Created a new repo for [AoC 2023 on GitHub](https://github.com/stephbu/aoc2023)
- Invested in some C# scaffolding, dusted off previous Utility class
- Upgraded MacOS to .NET 8.0 SDK
- Updated to Jetbrains Rider 2023.2.3

Figure that most of the time I'll be writing at night on my Mac, with Rider as primary IDE.
Rider does a great job of being compatible with VS/VSCode.

## Puzzle 1
Got going pretty quick, used an enumerator to read lines.  Used LINQ enumerate characters to find first and last digits.
Love the concise method group syntax for ```First<char>(Char.IsDigit)``` and ```Last<char>(Char.IsDigit)```.
String concatenation into an int.parse got it done.

## Puzzle 2
Got hung up greedy preprocessing the string LTR to minimize code changes - ```oneight``` becoming ```1ight``` instead of the expected ```18```.  
I simplified the approach to go LTR until first, then RTL until last to figure it out.

## Summary
Done by 1am EST on Day 1 - I'm happy with that, and ready for Day 2.
