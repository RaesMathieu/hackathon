﻿using System.Collections.Generic;
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
                var userId = GetUserIdFromToken();
                var tournaments = await _tournamentService.GetCurrentsAsync(userId);
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
                var userId = GetUserIdFromToken();
                var tournaments = await _tournamentService.GetAlreadyStartedAsync(userId, number);
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

                foreach (var winnable in tournament.Winnables)
                    winnable.TypeOfReward = "freebets";

                foreach (var market in tournament.Markets)
                    market.ChosenSelectionId = bets.FirstOrDefault(s => s.Market.Id == market.Id)?.Selection?.Id;
            }
        }

        /// <summary>
        /// Optin on a tournament
        /// </summary>
        /// <param name="tournamentCode">identifier of the tournament</param>
        /// <returns></returns>
        /// <response code="200">Bet success</response>
        /// <response code="400">no logged</response>
        /// <response code="408">No optin can be take on this tournament</response>
        /// <response code="404">Tournament was not found</response>
        /// <response code="500">Failed with internal server error</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(void))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpPost("api/tournament/{tournamentCode}/Optin")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> OptinAsync([FromRoute] string tournamentCode)
        {
            //return Ok();
            try
            {
                var userId = GetUserIdFromToken();
                await _tournamentService.OptinAsync(userId, tournamentCode);

                return Ok();
            }
            catch (FinishedTournamentCoreException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No optin can be take on this tournament.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
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
                int userId = GetUserIdFromToken();
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

        private int GetUserIdFromToken()
        {
            return int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
        }
    }
}
