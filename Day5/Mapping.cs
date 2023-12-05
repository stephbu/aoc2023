struct Mapping
{
    public Range Range;
    public long Offset;

    public override string ToString()
    {
        return $"{Range} ({Offset})";
    }
}