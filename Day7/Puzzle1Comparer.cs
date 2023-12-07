using Utility;

namespace Day7;

public class Puzzle1Comparer : IHandComparer
{
    public int Compare(char x, char y)
    {
        const string rank = "23456789TQJKA";
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
            .ThenBy(g => g.Key)
            .Select(g => g.Count());

        HandRank rank = HandRank.None;
    
        if (cardGroups.Any(c => c == 5))
        {
            rank = HandRank.FiveOfAKind;
        } 
        else if (cardGroups.Any(c => c == 4))
        {
            rank = HandRank.FourOfAKind;
        } 
        else if (cardGroups.Any(c => c == 3))
        {
            if (cardGroups.Any(c => c == 2))
            {
                rank = HandRank.FullHouse;
            }
            else
            {
                rank = HandRank.ThreeOfAKind;                
            }
        } 
        else if (cardGroups.Count(c => c == 2) == 2)
        {
            rank = HandRank.TwoPair;
        }
        else if (cardGroups.Any(c => c == 2))
        {
            rank = HandRank.OnePair;
        }
    
        return rank;
    }
}