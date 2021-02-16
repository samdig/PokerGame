using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poker.WebAPI.Entities
{
    public class Player
    {
        public Player()
        {
            Cards = new List<Card>();
        }


        public string Name { get; set; }
        public int? CardRank { get; set; }
        public int Category { get; set; }
        public List<Card> Cards { get; set; }
      
    }
}
