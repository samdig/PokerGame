using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poker.WebAPI.Utility
{
    public class Enums
    {
        //Represent cards values
        public enum Rank
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14


        }

        //Represent cards suits
        public enum Suit
        {
            D = 1,
            C = 2,
            H = 3,
            S = 4,
        }
    }
}
