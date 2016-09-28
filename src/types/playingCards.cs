using System;

namespace LinqFaroShuffleTypes
{
    public class PlayingCards
    {
        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public enum Rank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public Suit CardSuit { get; }
        public Rank CardRank { get; }

        public PlayingCards(Suit s, Rank r)
        {
            CardSuit = s;
            CardRank = r;
        }

        public override string ToString()
        {
            return $"{CardRank} of {CardSuit}";
        }
    }
}
