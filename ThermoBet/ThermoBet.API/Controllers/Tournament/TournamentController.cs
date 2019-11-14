using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ThermoBet.API.Controllers
{
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TournamentController> _logger;
        private ITournamentService _tournamentService;

        public TournamentController(
            ILogger<TournamentController> logger,
            ITournamentService tournamentService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _tournamentService = tournamentService;
            _logger.LogInformation("TestController");
        }

        [HttpGet("api/tournaments")]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<TournamentReponse>> GetAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
            var tournaments = await _tournamentService.GetCurrentsAsync();
            var result = _mapper.Map<IEnumerable<TournamentReponse>>(tournaments);

            await MapUserBetAndWinningSelection(userId, result);

            return result;
        }

        [HttpGet("api/tournaments/last/{number}")]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<TournamentReponse>> GetRankingAsync(int number)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
            var tournaments = await _tournamentService.GetAlreadyStartedAsync(number);
            var result = _mapper.Map<IEnumerable<TournamentReponse>>(tournaments);

            await MapUserBetAndWinningSelection(userId, result);

            return result;
        }

        private async Task MapUserBetAndWinningSelection(int userId, IEnumerable<TournamentReponse> result)
        {
            // set user choice.
            foreach (var tournament in result)
            {
                var bets = await _tournamentService.GetBetAsync(userId, tournament.Id);

                foreach (var market in tournament.Markets)
                {
                    market.WinningSelectionId = market.WinningSelectionId;
                    market.ChosenSelectionId = bets.FirstOrDefault(s => s.Market.Id == market.Id)?.Selection?.Id;
                }
            }
        }

        [HttpPost("api/tournament/{tournamentId}/market/{marketId}/selection/{selectionId}")]
        [Authorize(Roles = "User")]
        public async Task BetAsync([FromRoute] int tournamentId, [FromRoute] int marketId, [FromRoute] int selectionId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
            await _tournamentService.BetAsync(userId, tournamentId, marketId, selectionId);
        }
    }
}
