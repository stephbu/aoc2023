public struct GameRecord
{
    public int Id;
    public Draw[] Draws;

    public static GameRecord Parse(string record)
    {
        // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        var result = new GameRecord();

        var segments = record.Split(':');
        result.Id = int.Parse(segments[0].Replace("Game ","").Trim());

        var drawSegments = segments[1].Split(';');
        result.Draws = new Draw[drawSegments.Length];
        for (var i = 0; i < drawSegments.Length; i++)
        {
            var drawSegment = drawSegments[i];
            result.Draws[i] = Draw.Parse(drawSegment);
        }

        return result;
    }
    
    public bool IsPossible(Draw limit1)
    {
        var b = true;
        foreach (var draw in this.Draws)
        {
            if (draw.Blue > limit1.Blue || draw.Red > limit1.Red || draw.Green > limit1.Green)
            {
                b = false;
                break;
            }
        }

        return b;
    }
}