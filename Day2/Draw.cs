public struct Draw
{
    public int Blue;
    public int Red;
    public int Green;

    public static Draw Parse(string draw)
    {
        var result = new Draw();

        var segments = draw.Split(',');
        foreach (var segment in segments)
        {
            var color = segment.Trim().Split(' ')[1];
            var count = int.Parse(segment.Trim().Split(' ')[0]);
            switch (color)
            {
                case "blue":
                    result.Blue = count;
                    break;
                case "red":
                    result.Red = count;
                    break;
                case "green":
                    result.Green = count;
                    break;
            }
        }

        return result;
    }
}