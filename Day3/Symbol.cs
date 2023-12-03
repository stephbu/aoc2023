public struct Symbol
{
    public char Value;
    public Coordinate Location;

    public int GearRatio(List<Number> numbers)
    {
        var discovered = new List<Number>();

        foreach (var number in numbers)
        {
            // number perimeter check
            if (Location.X >= number.start.X - 1 && Location.X <= number.end.X + 1 &&
                Location.Y >= number.start.Y - 1 && Location.Y <= number.end.Y + 1)
            {
                // covers discovered.Count > 2
                if (discovered.Count == 2)
                    return 0;

                discovered.Add(number);
            }
        }

        if (discovered.Count == 2) 
            return discovered[0].Value * discovered[1].Value;

        // covers discovered.Count == 0 and discovered.Count == 1
        return 0;
    }
}