using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThermoBet.Core.Models;

namespace ThermoBet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<TournamentReponse>> Get()
        {
            var tournament = await _tournamentService.GetAllAsync();
            return _mapper.Map<IEnumerable<TournamentReponse>>(tournament);
        }
    }
}
