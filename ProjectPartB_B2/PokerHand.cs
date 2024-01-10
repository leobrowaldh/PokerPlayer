using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPartB_B2
{
    class PokerHand : HandOfCards
    {
        #region Clear
        public override void Clear()
        {
            base.Clear();
            ClearRank();
        }
        private void ClearRank()
        {
            _rankHigh = null;
            _rankHighPair1 = null;
            _rankHighPair2 = null;
            _rank = PokerRank.Unknown;
        }
        #endregion

        #region Remove and Add related
        public override void Add(PlayingCard card)
        {
            base.Add(card);
        }
        #endregion

        #region Poker Rank related

        private PokerRank _rank = PokerRank.Unknown;
        private PlayingCard _rankHigh = null;
        private PlayingCard _rankHighPair1 = null;
        private PlayingCard _rankHighPair2 = null;

        public PokerRank Rank => _rank;
        public PlayingCard RankHiCard => _rankHigh;
        public PlayingCard RankHiCardPair1 => _rankHighPair1;
        public PlayingCard RankHiCardPair2 => _rankHighPair2;

        private bool IsSameColor(out PlayingCard HighCard)
        {
            if (Count <= 0)
            {
                HighCard = null;
                return false;
            }

            //every item must have same as Highest cards
            HighCard = this.Highest;
            foreach (var item in cards)
            {
                if (item.Color != HighCard.Color)
                {
                    HighCard = null;
                    return false;
                }
            }

            return true;
        }
        private bool IsConsecutive(out PlayingCard HighCard)
        {
            if (Count <= 0)
            {
                HighCard = null;
                return false;
            }

            PlayingCardValue prevValue = cards[0].Value;
            for (int i = 1; i < Count; i++)
            {
                if (cards[i].Value == prevValue + 1)
                    prevValue = cards[i].Value;
                else
                {
                    HighCard = null;
                    return false;
                }
            }

            HighCard = Highest;
            return true;
        }

        
        private bool IsRoyalFlush => IsSameColor(out _rankHigh) && IsConsecutive(out _rankHigh) && _rankHigh.Value == PlayingCardValue.Ace;
        private bool IsStraightFlush => IsSameColor(out _rankHigh) && IsConsecutive(out _rankHigh);
        private bool IsFourOfAKind => CheckDuplicates(out _rankHigh, out _) == 4;

        //howMany is an array that counts how many of each card there is in the hand. (see CheckDuplicates())
        //If there is three of a kind, but there is also a pair in the hand, then it's a full house:
        private bool IsFullHouse => CheckDuplicates(out _rankHigh, out int[] howMany) == 3 && howMany.Contains(2);
        private bool IsFlush => IsSameColor(out _rankHigh);
        private bool IsStraight => IsConsecutive(out _rankHigh);
        private bool IsThreeOfAKind => CheckDuplicates(out _rankHigh, out int[] howMany) == 3;

        //a total of four cards in the hand will have a count of 2 in howMany if there are two pairs:
        private bool IsTwoPair => CheckDuplicates(out _, out int[] howMany) == 2 && howMany.Count(x => x == 2) == 4;
        private bool IsPair => CheckDuplicates(out _rankHigh, out int[] howMany) == 2;

        /// <summary>
        /// Sets the highest poker rank of the pokerhand.
        /// </summary>
        public void DetermineRank()
        {
            if (IsRoyalFlush)
                _rank = PokerRank.RoyalFlush;

            else if (IsStraightFlush)
                _rank = PokerRank.StraightFlush;

            else if (IsFourOfAKind)
                _rank = PokerRank.FourOfAKind;

            else if (IsFullHouse)
                _rank = PokerRank.FullHouse;

            else if (IsFlush)
                _rank = PokerRank.Flush;

            else if (IsStraight)
                _rank = PokerRank.Straight;

            else if (IsThreeOfAKind)
                _rank = PokerRank.ThreeOfAKind;
            
            else if (IsTwoPair)
            {
                GetTwoPairsRanking(out _rankHigh, out _rankHighPair1, out _rankHighPair2);
                _rank = PokerRank.TwoPair;
            }

            else if (IsPair)
                _rank = PokerRank.Pair;
            
            else
            {
                _rankHigh = Highest;
                _rank = PokerRank.HighCard;
            }
        }

        /// <summary>
        /// Determines wich is the high pair and wich is the low pair.
        /// </summary>
        /// <param name="HighCard"></param>
        /// <param name="HighestValuePair"></param>
        /// <param name="LowestValuePair"></param>
        private void GetTwoPairsRanking(out PlayingCard HighCard, out PlayingCard HighestValuePair, out PlayingCard LowestValuePair )
        {
            HighestValuePair = null;
            LowestValuePair = null;
            HighCard = null;
            CheckDuplicates(out _, out int[] howMany);
            List<PlayingCard> pairedCards = new List<PlayingCard>();

            for (int i = 0; i < howMany.Length; i++)
            {
                if (howMany[i] == 2)
                {
                    pairedCards.Add(cards[i]);
                }
            }
            int maxValue = 0;
            int minValue = 20;

            foreach (PlayingCard card in pairedCards)
            {
                if ((int)card.Value > maxValue)
                {
                    HighestValuePair = card;
                    maxValue = (int)card.Value;
                }
                if ((int)card.Value < minValue)
                {
                    LowestValuePair = card;
                    minValue = (int)card.Value;
                }
                    
            }
            HighCard = HighestValuePair;
        }

        /// <summary>
        /// Returns how many repetitions there is of the most repeated card in the hand. Out comes the highest ranking card
        /// and an array that counts how many repetitions of every card in the hand there are.
        /// </summary>
        /// <param name="HighCard"></param>
        /// <param name="howManyOfEach"></param>
        /// <returns>most repeated card in hand</returns>
        private int CheckDuplicates(out PlayingCard HighCard, out int[] howManyOfEach)
        {
            HighCard = null;
            howManyOfEach = new int[] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < cards.Count; i++)
            {
                foreach (PlayingCard card in cards)
                {
                    if (card.Value == cards[i].Value)
                    {
                        howManyOfEach[i]++;
                    }
                }
            }
            int bestCount = howManyOfEach.Max();
            HighCard = cards[Array.IndexOf(howManyOfEach, bestCount)];
            return bestCount;
        }

        #endregion

        
    }
}