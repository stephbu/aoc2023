# Day 3

## Reference

[https://adventofcode.com/2023/day/3](https://adventofcode.com/2023/day/3)

## Diary

State of mind: tired.
Started in PST morning, after friends-dinner xmas meal the night before.  Mind "weather forecast" was low clouds.

## Puzzle 1
Approached this as a two part exercise - first, parse the data into a pair of lists of structs - Whole Numbers, and Symbols.
Built a coordinate space around the Numbers and location of the Symbols, then built a Part-Number detector on the numbers
to find symbols inside the perimeter of the number.  Silly off-by-one error in the closing coordinate of the number took me a couple of minutes
to figure out. Numbers are detected as closed in the character *after* the number, or EOL, not the last character of the number itself.
Used LINQ and a boundary detection function on the number to join the Numbers and Symbol lists together, then aggregate the part numbers.
Spent about 55mins on Puzzle 1 inc. debugging the OB1 error.

## Puzzle 2
Mirrored worked done for Puzzle 1 in Number -> Symbol detection, to do Symbol -> Number detection.  
Created function that detected exactly two Numbers, and returned the gear ratio.  Used LINQ again to summarize the data.
Spent less than 10mins refactoring to complete Puzzle 2.

## Summary

Day 3 under my belt in about 70mins, about 40mins was writing the parse and structs.  Starting to a see a pattern of
test data probably deliberately lacking critical corner cases.  In this instance, the test data did not include numbers at end-of-line.
