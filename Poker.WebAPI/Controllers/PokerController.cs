using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poker.WebAPI.Services;
using Poker.WebAPI.Utility;

namespace Poker.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokerController : ControllerBase
    {
        private readonly ILogger<PokerController> _logger;
        private IPokerRepository _repository;

        public PokerController(ILogger<PokerController> logger, IPokerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string player1, [FromQuery] string player2, [FromQuery] string hand1, [FromQuery] string hand2)     
        {
            // http://localhost:51000/api/Poker?player1=Angel&player2=Bodhi&hand1=2H,3D,5S,9C,KD&hand2=2C,3H,4S,8C,AH

            string gameResult = string.Empty;

            try
            {
                string[] players = new string[2] { player1, player2 };
                string[] hands = new string[2] { hand1, hand2 };

                if (string.IsNullOrEmpty(player1) ||
                  string.IsNullOrEmpty(player2) ||
                  string.IsNullOrEmpty(hand1) ||
                  string.IsNullOrEmpty(hand2) ||
                  Helper.isHandsValid(hands) == false
              )
                    return BadRequest("Bad Request");

                var playercards = Helper.PreparePlayersCards(players, hands);

                gameResult = _repository.PlayPokerGame(playercards);
            }
            catch (Exception ex)
            {
                // _logger.LogError( ex, ex.Message); --Log this to db or file
                return StatusCode(500);
            }

            return Ok(gameResult);
        }
    }
}
