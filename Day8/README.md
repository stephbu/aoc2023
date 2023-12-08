# Day 8

## Reference

[https://adventofcode.com/2023/day/8](https://adventofcode.com/2023/day/8)

## Diary
State of mind: awake.
Started at 9:05pm.

## Puzzle 1
Graph/Path following problem.  Wrote parser to load instructions, and ```Dictionary<String,Node>```
Wrote a wrap-around instruction issuer, and simple code to follow instructions until hit goal.
Done in less than 20mins including validations.

## Puzzle 2
Puzzle two initially looked like a modification for of the follower logic, to have multiple followers.
Extracted logic from puzzle one into ```NodeWalker``` class that carried node-walking state.  Created an instance of
this class for each starting point, and a loop to run them in parallel.  Turns out the
paths were very big.  After over 2Bn iterations, I killed the process.
Looking at the data it appeared that each path eventually looped.  So I took a different approach of
Lowest Common Multiples.  I ran each ```NodeWalker``` path until it looped, then calculated the ```LowestCommonMultiple``` of the
combined set of NodeWalkers.  This gave me a pretty huge number - 13 trillion.

## Summary
The instructions were kind of poor, it wasn't obvious that the data actually looped, because often the 'Z's
pointed to non-'A' nodes as indirection back to 'Z'.  If anything the data had been manipulated to make that condition 
true.  If LCM didn't work, I'm not sure how I would have tackled the problem.