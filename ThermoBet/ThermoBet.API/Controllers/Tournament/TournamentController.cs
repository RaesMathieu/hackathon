using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ThermoBet.Core.Exception;
using System;
using System.Net;

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

        /// <summary>
        /// Get actives tournaments
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not logged</response>
        /// <response code="500">Failed with internal server error</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TournamentReponse>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpGet("api/tournaments")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<TournamentReponse>>> GetAsync()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                var tournaments = await _tournamentService.GetCurrentsAsync();
                var result = _mapper.Map<IEnumerable<TournamentReponse>>(tournaments);

                await MapUserBetAndWinningSelection(userId, result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Get the last x trounament activate or activated
        /// </summary>
        /// <param name="number">number of tournament to get back</param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not logged</response>
        /// <response code="500">Failed with internal server error</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TournamentReponse>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpGet("api/tournaments/last/{number}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<TournamentReponse>>> GetRankingAsync(int number)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                var tournaments = await _tournamentService.GetAlreadyStartedAsync(number);
                var result = _mapper.Map<IEnumerable<TournamentReponse>>(tournaments);

                await MapUserBetAndWinningSelection(userId, result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
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

        /// <summary>
        /// Bet on a tournament
        /// </summary>
        /// <param name="marketId">identifier of the market</param>
        /// <param name="selectionId">identifier of the Selection</param>
        /// <param name="tournamentId">identifier of the tournament</param>
        /// <returns></returns>
        /// <response code="200">Bet success</response>
        /// <response code="401">no logged</response>
        /// <response code="408">No bet can be take on this tournament</response>
        /// <response code="500">Failed with internal server error</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpPost("api/tournament/{tournamentId}/market/{marketId}/selection/{selectionId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> BetAsync([FromRoute] int tournamentId, [FromRoute] int marketId, [FromRoute] int selectionId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                await _tournamentService.BetAsync(userId, tournamentId, marketId, selectionId);

                return Ok();
            }
            catch (FinishedTournamentCoreException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No bet can be take on this tournament.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
