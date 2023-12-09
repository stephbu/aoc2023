# Day 9

## Reference

[https://adventofcode.com/2023/day/9](https://adventofcode.com/2023/day/9)

## Diary
State of mind: flagging.
Started late at 10:30pm.

## Puzzle 1
Data-set manipulation and processing problem.  Wrote a parser to start extracting sensor data.
Picked a ```List<List<long>>``` structure to store the variable length arrays, figuring that the 
list manipulation functions would be helpful.  Wrote an extension method to generate the differences
between sequential values in an IEnumerable.  Called this recursively until all elements returned were zero.
Used the new slice operator to return the last value of the array e.g. ```array[^1]```

## Puzzle 2
Second part was quite easy too once I'd looked over the test data to validate assumptions.
Modifying the list manipulation code to prepend values was straightforward.  I dry ran the logic
a couple of times just to verify the results against as-written examples. Completed Puzzle 2 in 14mins
to make Day 9 Puzzle 2 the quickest to solve so far.

## Summary
Much easier than yesterday's LCM problem.  Array slicing was a nice addition to the language.