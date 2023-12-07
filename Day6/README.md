# Day 6

## Reference

[https://adventofcode.com/2023/day/6](https://adventofcode.com/2023/day/6)

## Diary

State of mind: tired.
Started just after 9:30pm.

## Puzzle 1
The recipe is familiar, parse into structs, build function to process the data.  Initial puzzle was pretty easy
once I validated the parser.  Quick napkin diagram to validate the assumptions.  Used LINQ to rainbow out the permutations and 
gather up the "Power" calc.  Completed part 1 in less than 30mins.

## Puzzle 2
Used a reprocessor in part 2 to generate a derivative input of the original parsed data result.  Was wondering if the magnitudes were
going to be an issue, turned out to not be a problem.  Created a neat LINQ number sequence generate because the
intrinsic ```Enumerable.Sequence``` doesn't deal with ```long```.  Completed part 2 in less than 15mins.

## Summary
Quick tonight, especially when compared to the night before.