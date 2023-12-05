struct Range
{
    public Range Offset(long offset)
    {
        return new Range()
        {
            Start = this.Start + offset,
            End = this.End + offset,
        };
    }
    
    public override string ToString()
    {
        return $"[{Start},{End}]";
    }

    public long Start;
    public long End;
}