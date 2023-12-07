namespace Day7;

public interface IHandComparer : IComparer<char>, IComparer<Hand>
{
    // Hand comparer needs to implement both Hand and Card comparisons
}