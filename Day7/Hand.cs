namespace Day7;

public struct Hand(string cards, long bid, IHandComparer comparer) : IComparable<Hand>
{
    private readonly IHandComparer _comparer = comparer;
    public string Cards = cards;
    public long Bid = bid;

    public int CompareTo(Hand other)
    {
        return this._comparer.Compare(this, other);
    }

    public override string ToString()
    {
        return $"{String.Concat(this.Cards)} ({this.Bid})";
    }
}