using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThermoBet.API.Controllers
{
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StatsController> _logger;
        private readonly IStatsService _statsService;

        public StatsController(
            ILogger<StatsController> logger,
            IMapper mapper,
            IStatsService statsService)
        {
            _mapper = mapper;
            _logger = logger;
            _statsService = statsService;
            _logger.LogInformation("TestController");
        }

        [HttpGet("api/stats")]
        [Authorize(Roles = "User")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Stats))]
        public async Task<ActionResult<Stats>> GetUserStats()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                var stats = await _statsService.GetByUserIdAsync(userId);
                var result = _mapper.Map<Stats>(stats);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + " " + ex.StackTrace);
            }
        }
    }
}
