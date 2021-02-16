using Poker.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Poker.WebAPI.Utility.Enums;

namespace Poker.WebAPI.Utility
{
    public static class Helper
    {
        #region Public methods
        public static List<Player> PreparePlayersCards(string[] players, string[] cards)
        {
            List<Player> PlayerList = new List<Player>();

            for (int i = 0; i < players.Length; i++)
            {
                var player = new Player();
                player.Name = players[i];
                player.Cards = GetCard(cards[i]);
                PlayerList.Add(player);
            }
            return PlayerList;
        }

        public static bool isHandsValid(string[] hands)
        {
            if (hands == null) return false;        //Cards cannot be empty

            foreach (var hand in hands)
            {
                var cards = hand.Split(',');
                if (cards.Length > 5) return false;  //checks if there are more than 5 cards per players

                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i].Length < 2 || cards[i].Length > 3) return false; // Checks to make sure our card value and suit between 2 - 3 length
                }
            }
            if (!IsCardRankValid(hands)) return false;  // Validate the card value
            return true;
        }

        #endregion

        #region private methods
        private static int? GetCardRank(string card)
        {

            var res = card.Length == 2 ? card.Substring(0, 1).ToUpper() : card.Substring(0, 2).ToUpper();

            switch (res)
            {
                case "A":
                    return (int)Rank.A;
                case "K":
                    return (int)Rank.K;
                case "Q":
                    return (int)Rank.Q;
                case "J":
                    return (int)Rank.J;
                case "10":
                    return (int)Rank.Ten;
                case "9":
                    return (int)Rank.Nine;
                case "8":
                    return (int)Rank.Eight;
                case "7":
                    return (int)Rank.Seven;
                case "6":
                    return (int)Rank.Six;
                case "5":
                    return (int)Rank.Five;
                case "4":
                    return (int)Rank.Four;
                case "3":
                    return (int)Rank.Three;
                case "2":
                    return (int)Rank.Two;
                default:
                    return null;

            }
        }
        private static int? GetCardSuit(string card)
        {
            var res = card.Substring(card.Length - 1, 1);

            switch (res.ToUpper())
            {
                case "D":
                    return (int)Suit.D;      //Diamonds          
                case "C":
                    return (int)Suit.C;      //Clubs
                case "H":
                    return (int)Suit.H;      //Hearts
                case "S":
                    return (int)Suit.S;      //Spades
                default:
                    return null;             // invalid Suit
            }
        }
        private static bool IsCardRankValid(string[] cards)
        {
            foreach (var card in cards)
            {
                var result = GetCard(card);
                if (result.Any(card => card.Rank == null || card.Suit == null)) return false;
            }
            return true;
        }
        private static List<Card> GetCard(string cards)
        {
            var list = cards.Split(',');
            List<Card> allCards = new List<Card>();
            for (int i = 0; i < list.Length; i++)
            {
                Card card = new Card();
                card.Rank = GetCardRank(list[i]);
                card.Suit = GetCardSuit(list[i]);

                allCards.Add(card);
            }
            return allCards;
        }

        #endregion
    }
}
