using Utility;

struct Race
{
    public long RaceDuration;
    public long RecordDistance;

    public long GetWinningPermutations()
    {
        var current = this;
        var result = Linq.Sequence(1, current.RaceDuration)
            .AsParallel().WithDegreeOfParallelism(8)
            .Count(t => current.GetRacePermutation(t) > current.RecordDistance);
        return result;
    }
    
    public long GetRacePermutation(long accelerationTime)
    {
        var distance = accelerationTime * (this.RaceDuration - accelerationTime);
        return distance;
    }
}