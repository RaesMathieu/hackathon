using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThermoBet.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ThermoBet.MVC.Controllers
{
    public class ResultingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITournamentService _tournamentService;
        private readonly IStatsService _statsService;
        private readonly IMapper _mapper;

        public ResultingController(
            ILogger<HomeController> logger,
            ITournamentService tournamentService,
            IStatsService statsService,
            IMapper mapper)
        {
            _logger = logger;
            _tournamentService = tournamentService;
            _mapper = mapper;
            _statsService = statsService;
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
            return View(_mapper.Map<ResultingTournamentViewModel>(tournament));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResultingTournamentViewModel tournamentToEdit, string button)
        {
            if (button == "Cancel")
                return RedirectToAction(nameof(TournamentController.Index), nameof(TournamentController).Replace("Controller", ""));

            if (id != tournamentToEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tournament = _mapper.Map<Core.Models.TournamentModel>(tournamentToEdit);
                var tournamentEdit = await _tournamentService.GetAsync(tournamentToEdit.Id);

                var result = tournament.Markets.ToDictionary(x => x.Id, x => x.WinningSelectionId);

                foreach (var market in tournamentEdit.Markets)
                    market.WinningSelectionId = result[market.Id];

                await _tournamentService.Update(tournamentEdit);
                await _statsService.Compute(tournamentToEdit.Id);

                return RedirectToAction(nameof(Index));
            }
            return View(tournamentToEdit);
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
