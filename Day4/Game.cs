struct Game
{
    public int CardId;
    public int[] WinningNumbers;
    public int[] YourNumbers;
    
    public IEnumerable<int> MatchingNumbers => YourNumbers.Intersect(WinningNumbers);
    
    public static IEnumerable<Game> Parse(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            var segments = line.Split(':');
            var cardId = int.Parse(segments[0].Replace("Card ","").Trim());
            var numbers = segments[1].Split('|');
            var winningNumbers = numbers[0].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToArray();
            var yourNumbers = numbers[1].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToArray();
        
            yield return new Game
            {
                CardId = cardId,
                WinningNumbers = winningNumbers,
                YourNumbers = yourNumbers
            };
        }
    }
}