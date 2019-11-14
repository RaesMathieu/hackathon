//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using ThermoBet.Core.Models;
//using System.Security.Claims;
//using System.Linq;

//namespace ThermoBet.API.Controllers
//{
//    [ApiController]
//    public class StatsController : ControllerBase
//    {
//        private readonly IMapper _mapper;
//        private readonly ILogger<StatsController> _logger;

//        public StatsController(
//            ILogger<StatsController> logger,
//            IMapper mapper)
//        {
//            _mapper = mapper;
//            _logger = logger;
//            _tournamentService = tournamentService;
//            _logger.LogInformation("TestController");
//        }

//        [HttpGet("api/tournaments")]
//        [Authorize(Roles = "User")]
//        public async Task<IEnumerable<TournamentReponse>> GetAsync()
//        {
//            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
//            var tournaments = await _tournamentService.GetCurrentsAsync();
//            var result = _mapper.Map<IEnumerable<TournamentReponse>>(tournaments);

//            // set user choice.
//            foreach (var tournament in result)
//            {
//                var bets = await _tournamentService.GetBetAsync(userId, tournament.Id);

//                foreach (var market in tournament.Markets)
//                {
//                    market.WinningSelectionId = market.Selections.FirstOrDefault(s => s.Result == true)?.Id;
//                    market.ChosenSelectionId = bets.FirstOrDefault(s => s.Market.Id == market.Id)?.Selection?.Id;
//                }

//            }

//            return result;
//        }


//        [HttpPost("api/tournament/{tournamentId}/market/{marketId}/selection/{selectionId}")]
//        [Authorize(Roles = "User")]
//        public async Task BetAsync([FromRoute] int tournamentId, [FromRoute] int marketId, [FromRoute] int selectionId)
//        {
//            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
//            await _tournamentService.BetAsync(userId, tournamentId, marketId, selectionId);
//        }
//    }
//}
