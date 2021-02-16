using Poker.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poker.WebAPI.Utility
{
    public static class PokerManager
    {
        #region public methods
        public static bool IsPair(Card[] cards)
        {
            var sortedCards = SortCardsByRank(cards);

            var result = sortedCards.GroupBy(c => c.Rank).Count(group => group.Count() == 2) == 1;
            return result;
        }

        public static bool IsTwoPair(Card[] cards)
        {
            var sortedCards = SortCardsByRank(cards);

            var result = sortedCards.GroupBy(c => c.Rank).Count(group => group.Count() == 2) == 2;
            return result;
        }

        public static bool IsThreeOfAKind(Card[] cards)
        {
            var sortedCards = SortCardsByRank(cards);

            var result = sortedCards.GroupBy(x => x.Rank).Any(group => group.Count() == 3);
            return result;
        }

        public static bool IsStraight(Card[] cards)
        {
            var sortedCards = SortCardsByRank(cards);

            if (sortedCards[4].Rank == 14)
            {
                //test straight for Ace
                bool c1 = sortedCards[0].Rank == 2 && sortedCards[1].Rank == 3 && sortedCards[2].Rank == 4 && sortedCards[3].Rank == 5;
                bool c2 = sortedCards[0].Rank == 10 && sortedCards[1].Rank == 11 && sortedCards[2].Rank == 12 && sortedCards[3].Rank == 13;
                return (c1 || c2);
            }
            else
            {
                //checking for increasing values
                var res = sortedCards[0].Rank + 0;
                for (int i = 1; i < 5; i++)
                {
                    if (res != sortedCards[i].Rank - 1) return false; //failed straight
                    res = res + 1;
                }
                return true;
            }
        }

        public static bool IsFlush(Card[] cards)
        {
            var sortedCards = SortCardsBySuit(cards);
            return (sortedCards[0].Suit == sortedCards[4].Suit);
        }

        public static bool IsFullHouse(Card[] cards)
        {
            //sort cards
            var sortedCards = SortCardsByRank(cards);
            var grp1 = sortedCards[0].Rank == sortedCards[1].Rank &
                        sortedCards[1].Rank == sortedCards[2].Rank &
                        sortedCards[3].Rank == sortedCards[4].Rank;

            var grp2 = sortedCards[0].Rank == sortedCards[1].Rank &
                        sortedCards[2].Rank == sortedCards[3].Rank &
                        sortedCards[3].Rank == sortedCards[4].Rank;

            return (grp1 || grp2);
        }

        public static bool IsFourOfAKind(Card[] cards)
        {
            var sortedCards = SortCardsByRank(cards);

            var result = sortedCards.GroupBy(x => x.Rank).Any(group => group.Count() == 4);
            return result;
        }

        public static bool IsStraightFlush(Card[] cards)
        {
            return IsStraight(cards) & IsFlush(cards);
        }
        #endregion

        #region  private methods
        private static Card[] SortCardsBySuit(Card[] cards)
        {
            cards = cards.OrderBy(card => card.Suit).ToArray();
            return cards;
        }

        private static Card[] SortCardsByRank(Card[] cards)
        {
            cards = cards.OrderBy(card => card.Rank).ToArray();
            return cards;
        }
        #endregion
    }
}
