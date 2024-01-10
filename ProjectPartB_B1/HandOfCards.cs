using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B1
{
    class HandOfCards : DeckOfCards, IHandOfCards
    {
        public int Wins { get; set; } = 0;

        #region Pick and Add related
        public void Add(PlayingCard card)
        { 
            cards.Add(card);
            Sort();
        }
        #endregion

        #region Highest Card related
        public PlayingCard Highest
        {
            get
            {
               return cards[^1];
            }
         }
        public PlayingCard Lowest
        {
            get
            {
               return cards[0];
            }
        }
        #endregion
    }
}
