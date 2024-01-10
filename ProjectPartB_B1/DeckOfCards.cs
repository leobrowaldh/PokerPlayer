using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace ProjectPartB_B1
{
    class DeckOfCards : IDeckOfCards
    {
        #region cards List related
        protected const int MaxNrOfCards = 52;
        protected List<PlayingCard> cards = new List<PlayingCard>(MaxNrOfCards);

        public PlayingCard this[int idx] => cards[idx];
        public int Count => cards.Count;
        #endregion

        #region ToString() related
        public override string ToString()
        {
            string returnString = "";
            for (int i = 0; i < cards.Count; i++)
            {
                returnString += $"{cards[i], -8} ";
                if ((i + 1) % 13 == 0 && i != 0) { returnString += "\n"; }
            }
            return returnString;
        }
        #endregion

        #region Shuffle and Sorting
        public void Shuffle()
        {
            csSeedGenerator rnd = new csSeedGenerator();
            int cardsInStack = cards.Count;
            List<PlayingCard> shuffeledCards = new List<PlayingCard>(cardsInStack);

            for (int i = 0; i < cardsInStack; i++)
            {
                PlayingCard randomPlayingCard = rnd.FromList<PlayingCard>(cards);
                shuffeledCards.Add(randomPlayingCard);
                cards.Remove(randomPlayingCard);
            }
            cards = shuffeledCards;
        }
        public void Sort() => cards.Sort();
        #endregion

        #region Creating a fresh Deck
        public void Clear() => cards.Clear();
        public void CreateFreshDeck()
        {
            cards.Clear();
            for (PlayingCardColor color = PlayingCardColor.Clubs; color <= PlayingCardColor.Spades; color++)
            {
                for (PlayingCardValue value = PlayingCardValue.Two; value <= PlayingCardValue.Ace;  value++)
                {
                    cards.Add(new PlayingCard() { Color = color, Value = value});
                }
            }
        }
        #endregion

        #region Dealing
        public PlayingCard RemoveTopCard()
        {
            PlayingCard topCard = cards[0];
            cards.RemoveAt(0);
            return topCard;
        }
        #endregion
    }
}
