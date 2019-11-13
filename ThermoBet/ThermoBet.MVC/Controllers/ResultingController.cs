using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThermoBet.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;

namespace ThermoBet.MVC.Controllers
{
    public class ResultingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITournamentService _tournamentService;
        private readonly IMapper _mapper;

        public ResultingController(
            ILogger<HomeController> logger,
            ITournamentService tournamentService,
            IMapper mapper)
        {
            _logger = logger;
            _tournamentService = tournamentService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(TournamentController.Index), nameof(TournamentController).Replace("Controller", ""));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _tournamentService.GetAsync(id.Value);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<TournamentViewModel>(tournament));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TournamentViewModel movie, string button)
        {
            if (button == "Cancel")
                return RedirectToAction(nameof(Index));

            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tournament = _mapper.Map<Core.Models.TournamentModel>(movie);
                await _tournamentService.Update(tournament);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
