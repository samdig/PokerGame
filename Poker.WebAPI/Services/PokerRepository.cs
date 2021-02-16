using Poker.WebAPI.Entities;
using Poker.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poker.WebAPI.Services
{
    public class PokerRepository : IPokerRepository
    {
        #region public method
        public string PlayPokerGame(List<Player> players)
        {
            var player1 = StartGame(players[0]);
            var player2 = StartGame(players[1]);
            var winner = CheckGameWinner(player1, player2);
            return winner;
        }
        #endregion

        #region private methods
        private Player StartGame(Player player)
        {
            Card[] cards = player.Cards.ToArray();
            if (PokerManager.IsStraightFlush(cards))
            {
                player.Category = 9;
                player.CardRank = cards.Max(x => x.Rank);
                return player;
            }
            else if (PokerManager.IsFourOfAKind(cards))
            {
                player.Category = 8;
                player.CardRank = cards[2].Rank;
                return player;

            }
            else if (PokerManager.IsFullHouse(cards))
            {
                player.Category = 7;
                player.CardRank = cards[2].Rank;
                return player;
            }
            else if (PokerManager.IsFlush(cards))
            {
                player.Category = 6;
                return player;
            }
            else if (PokerManager.IsStraight(cards))
            {
                player.Category = 5;
                player.CardRank = cards.Max(x => x.Rank);
                return player;
            }
            else if (PokerManager.IsThreeOfAKind(cards))
            {
                player.Category = 4;
                player.CardRank = cards[2].Rank;
                return player;
            }
            else if (PokerManager.IsTwoPair(cards))
            {
                player.Category = 3;
                return player;
            }
            else if (PokerManager.IsPair(cards))
            {
                player.Category = 2;
                return player;
            }
            else
            {
                player.Category = 1;
                player.CardRank = cards[2].Rank;
                return player;
            };
        }
        private string CheckGameWinner(Player player1, Player player2)
        {
            //Players without category ties
            if (player1.Category > player2.Category)
            {
                return $" Player {player1.Name} is the winner";
            }
            else if (player2.Category > player1.Category)
            {
                return $" Player {player2.Name} is the winner";
            }

            // Players with ties
            else if (player1.Category == 9 && player2.Category == 9) //Straight flush
            {
                return CheckStraightFlushWinner(player1, player2);
            }
            else if (player1.Category == 8 && player2.Category == 8)  //Four of a kind
            {
                return CheckFourOfAKindWinner(player1, player2);
            }
            else if (player1.Category == 7 && player2.Category == 7)  //Full house
            {
                return CheckFullHouseWinner(player1, player2);
            }
            else if (player1.Category == 6 && player2.Category == 6)  //Flush
            {
                return CheckHighCardWinner(player1, player2);
            }
            else if (player1.Category == 5 && player2.Category == 5)  //Straight
            {
                return CheckStraightWinner(player1, player2);
            }
            else if (player1.Category == 4 && player2.Category == 4)  //Three of a kind
            {
                return CheckThreeOfAKindWinner(player1, player2);
            }
            else if (player1.Category == 3 && player2.Category == 3) //Two Pair
            {
                return CheckTwoPairWinner(player1, player2);
            }
            else if (player1.Category == 2 && player2.Category == 2) //Pair
            {
                return CheckPairWinner(player1, player2);
            }
            else                                                       //High Card
            {
                return CheckHighCardWinner(player1, player2);
            };
        }
        private string CheckHighCardWinner(Player player1, Player player2)
        {
            //sort both cards in descending order.
            player1.Cards = player1.Cards.OrderByDescending(card => card.Rank).ToList();
            player2.Cards = player2.Cards.OrderByDescending(card => card.Rank).ToList();

            for (int i = 0; i < 5; i++)
            {
                if (player1.Cards[i].Rank > player2.Cards[i].Rank)
                {
                    return $" Player {player1.Name} is the winner";
                }
                else if (player2.Cards[i].Rank > player1.Cards[i].Rank)
                {
                    return $" Player {player2.Name} is the winner";
                }
            }

            return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
        }
        private string CheckTwoPairWinner(Player player1, Player player2)
        {
            var hand1 = player1.Cards.GroupBy(card => card.Rank)
            .Select(n => new { key = n.Key, cards = n.Count() }).
             OrderByDescending(c => c.cards).ThenByDescending(x => x.key).ToArray();

            var hand2 = player2.Cards.GroupBy(card => card.Rank)
            .Select(n => new { key = n.Key, cards = n.Count() }).
            OrderByDescending(c => c.cards).ThenByDescending(x => x.key).ToArray();

            for (int i = 0; i < 3; i++)
            {
                if (hand1[i].key > hand2[i].key)
                {
                    return $" Player {player1.Name} is the winner";
                }
                else if (hand2[i].key > hand1[i].key)
                {
                    return $" Player {player2.Name} is the winner";
                }
            }
            return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
        }
        private string CheckPairWinner(Player player1, Player player2)
        {
            //Get the two pair          
            var hand1 = player1.Cards.GroupBy(card => card.Rank)
            .Select(n => new { key = n.Key, cards = n.Count() }).
             OrderByDescending(n => n.cards).ThenByDescending(n => n.key).ToArray();

            var hand2 = player2.Cards.GroupBy(card => card.Rank)
            .Select(n => new { key = n.Key, cards = n.Count() }).
             OrderByDescending(n => n.cards).ThenByDescending(n => n.key).ToArray();

            for (int i = 0; i < 4; i++)
            {
                if (hand1[i].key > hand2[i].key)
                {
                    return $" Player {player1.Name} is the winner";
                }
                else if (hand2[i].key > hand1[i].key)
                {
                    return $" Player {player2.Name} is the winner";
                }
            }
            return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
        }
        private string CheckFourOfAKindWinner(Player player1, Player player2)
        {
            if (player1.Cards[3].Rank > player2.Cards[3].Rank)
            {
                return $" Player {player1.Name} is the winner";
            }
            else if (player2.Cards[3].Rank > player1.Cards[3].Rank)
            {
                return $" Player {player2.Name} is the winner";
            }
            else
            {
                return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
            }
        }
        private string CheckStraightFlushWinner(Player player1, Player player2)
        {
            if (player1.CardRank > player2.CardRank)
            {
                return $" Player {player1.Name} is the winner";
            }
            else if (player2.CardRank > player1.CardRank)
            {
                return $" Player {player2.Name} is the winner";
            }
            else
            {
                return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
            }
        }
        private string CheckStraightWinner(Player player1, Player player2)
        {
            if (player1.CardRank > player2.CardRank)
            {
                return $" Player {player1.Name} is the winner";
            }
            else if (player2.CardRank > player1.CardRank)
            {
                return $" Player {player2.Name} is the winner";
            }
            else
            {
                return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
            }
        }
        private string CheckFullHouseWinner(Player player1, Player player2)
        {
            var hand1 = player1.Cards.GroupBy(card => card.Rank)
            .Select(n => new { key = n.Key, cards = n.Count() }).
            OrderByDescending(n => n.cards).ToArray();

            var hand2 = player2.Cards.GroupBy(card => card.Rank)
            .Select(n => new { key = n.Key, cards = n.Count() }).
             OrderByDescending(n => n.cards).ToArray();

            if (hand1[0].key > hand2[0].key)
            {
                return $" Player {player1.Name} is the winner";
            }
            else if (hand2[0].key > hand1[0].key)
            {
                return $" Player {player2.Name} is the winner";
            }
            else
            {
                return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
            }
        }
        private string CheckThreeOfAKindWinner(Player player1, Player player2)
        {
            if (player1.CardRank > player2.CardRank)
            {
                return $" Player {player1.Name} is the winner";
            }
            else if (player2.CardRank > player1.CardRank)
            {
                return $" Player {player2.Name} is the winner";
            }
            else
            {
                return $" Is a tie. Player {player1.Name} and Player {player2.Name} has same cards value. ";
            }
        }
        #endregion
    }
}
