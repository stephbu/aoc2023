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
Love the concise method group syntax for First<char>(Char.IsDigit) and Last<char>(Char.IsDigit).
String concatenation into an int.parse got it done.

## Puzzle 2
I was able to reuse the code from Puzzle 1, but had to refactor it to enable a line preprocessor.
Got hung up on ```oneight``` becoming ```18``` instead of ```1ight```.  Despite this, the test data worked like a charm, 
but the specification of how to preprocess the data was not clear.  

The test data case that should have shown expected behavior was ```eightwothree``` unfortunately 
with method one generated ```8wo3``` vs the expected ```823```.  Luckily I figured out.

I brought through non-digit characters, so I could visually debug the preprocessor issue.  The "no digit, no match"
case could have been skipped, but I left it in for clarity.

## Summary
Done by 1am EST on Day 1 - I'm happy with that, and ready for Day 2.
