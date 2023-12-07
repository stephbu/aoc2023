namespace Day7;

public class Puzzle2Comparer : IHandComparer
{
    public int Compare(char x, char y)
    {
        const string rank = "J23456789TQKA";
        return rank.IndexOf(x).CompareTo(rank.IndexOf(y));
    }
    
    public int Compare(Hand h1, Hand h2)
    {
        var rank1 = GetRank(h1);
        var rank2 = GetRank(h2);
        
        if (rank1 != rank2)
        {
            return rank1.CompareTo(rank2);
        }

        for (int index = 0; index < 5; index++)
        {
            var card1 = h1.Cards[index];
            var card2 = h2.Cards[index];

            var cmp = this.Compare(card1, card2);
            if (cmp != 0)
            {
                return cmp;
            }
        }
        
        // They're identical hands
        return 0;
    }

    private HandRank GetRank(Hand hand)
    {
        var cardGroups = hand.Cards.GroupBy(c => c, c => c)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key);

        var jokerCount = hand.Cards.Count(c => c == 'J');
        
        // deal with JJJJJ case
        if(jokerCount < 5)
        {
            // find best group and replace J's with the group key
            var bestNonJoker = cardGroups.First(g => g.Key != 'J').Key;
            var newCards = hand.Cards.Replace('J', bestNonJoker);
        
            // regenerate cardgroups
            cardGroups = newCards.GroupBy(c => c, c=> c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key);
        }
        
        HandRank rank = HandRank.None;
        var groupCounts = cardGroups.Select(g => g.Count());
        
        if (groupCounts.Any(c => c == 5))
        {
            rank = HandRank.FiveOfAKind;
        } 
        else if (groupCounts.Any(c => c == 4))
        {
            rank = HandRank.FourOfAKind;
        } 
        else if (groupCounts.Any(c => c == 3))
        {
            if (groupCounts.Any(c => c == 2))
            {
                rank = HandRank.FullHouse;
            }
            else
            {
                rank = HandRank.ThreeOfAKind;                
            }
        } 
        else if (groupCounts.Count(c => c == 2) == 2)
        {
            rank = HandRank.TwoPair;
        }
        else if (groupCounts.Any(c => c == 2))
        {
            rank = HandRank.OnePair;
        }
    
        return rank;
    }
}