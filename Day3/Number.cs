public struct Number
{
    public int Value;
    public Coordinate start;
    public Coordinate end;

    public bool IsPartNumber(List<Symbol> parts)
    {
        foreach (var part in parts)
            // perimeter check
            if (part.Location.X >= start.X - 1 && part.Location.X <= end.X + 1 && part.Location.Y >= start.Y - 1 &&
                part.Location.Y <= end.Y + 1)
                return true;
        return false;
    }
}