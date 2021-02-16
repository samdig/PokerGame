using Poker.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poker.WebAPI.Services
{
    public interface IPokerRepository
    {
        string PlayPokerGame(List<Player> players);
    }
}
