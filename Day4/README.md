# Day 4

## Reference

[https://adventofcode.com/2023/day/4](https://adventofcode.com/2023/day/4)

## Diary

State of mind: ready.
Started just after 9pm.

## Puzzle 1
Continued in the same vein as yesterday's puzzles.  Build a parser and structures.
Line parsing fairly easy - but some little gotchas like double blanks that need to be ignored.
This time I used ```Intersect<T>(IEnumerable<T>)``` to generate matching numbers and a 
nice inline conditional to generate a 2 ^ (n-1) points.  Got part 1 done in 13mins.

## Puzzle 2
Reused the same parser and structures in second part, this time creating a temporary structure
to keep count of instances. Materialized the temporary structures, made it easy to recursively enumerated
through the array adding instance count to subsequent elements.  Array math hygiene needed to make sure you don't overrun
the end of the array.  Completed part 2 in about 24mins inc. needing to duck out for a few minutes.

## Summary
Completed day 4 in 54mins, including refactoring and nice code quality.  Pretty quick tonight.