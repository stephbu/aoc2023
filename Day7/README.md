# Day 7

## Reference

[https://adventofcode.com/2023/day/7](https://adventofcode.com/2023/day/7)

## Diary

State of mind: tired.
Started around 9:30pm.

## Puzzle 1
Card game with a twist. Created structs to contain Hand(s) of Card(s).
Implemented ```IComparable<T>``` for ```Hand``` and ```Card``` to enable sorting and grouping.
Created a ranking system using LINQ ```GroupBy<T>``` to group cards into sets, ordered by card-value descending.
Careful reading of the rules was required to understand the fallbacks.
Completed part 1 in around an hour.

## Puzzle 2
Part 2 was a little more involved.  I reimplemented ```IComparable<Hand>``` and ```IComparable<Card>``` as ```IHandComparer<T>``` which implements both interfaces.
Moved Puzzle1 comparers out into ```Puzzle1Comparer```, and stitched ```IHandComparer``` back through the code.  
Implemented an updated ```Puzzle2Comparer``` to handle the change in rules.  Completed part 2 in around 45mins.
Interesting corner case around ```JJJJJ```.

## Summary
AoC seems to be alternating tasks between easy and hard.  This was probably more fiddly than difficult.