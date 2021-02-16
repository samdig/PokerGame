using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poker.WebAPI.Entities
{
    public class Card
    {
        public int? Rank { get; set; }
        public int? Suit { get; set; }
    }
}
