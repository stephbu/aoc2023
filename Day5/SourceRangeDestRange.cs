struct SourceRangeDestRange
{
    public long SourceStart;
    public long SourceEnd;
    public long Offset;
    public long DestStart;
    public long DestEnd;

    public override string ToString()
    {
        return $"{SourceStart}-{SourceEnd} ({Offset})";

    }
}