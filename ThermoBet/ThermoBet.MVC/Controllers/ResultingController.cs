using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThermoBet.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System;
using System.Text.Json;
using System.ComponentModel;
using ThermoBet.MVC.Helper;

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users(int? id)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("Resulting/Users/LoadData/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UserOnTournament(int? id)
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                // getting all Customer data  
                var tournamentData = _tournamentService.GetAllTournamentAsync().Where(x => x.Id == id.Value);

                var customerData = tournamentData
                    .SelectMany(x => x.OptinUsers)
                    .Select(x => new
                    {
                        x.User.Id,
                        x.User.Email,
                        x.User.BetclicUserName,
                        NbGoodReponse = x.Tournament.Bets
                        .Where(y => y.User.Id == x.User.Id)
                        .Count(u => u.Selection.Id == x.Tournament.Markets.Single(t => t.Id == u.MarketId).WinningSelectionId),
                        AmountOfWinnings = x.Tournament.Winnables.FirstOrDefault(z => z.NbGoodAnswer ==
                                                                                      x.Tournament.Bets
                                                                                          .Where(y => y.User.Id == x.User.Id)
                                                                                          .Count(u => u.Selection.Id == x.Tournament.Markets.Single(t => t.Id == u.MarketId).WinningSelectionId)).AmountOfWinnings

                    });



                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var discountFilterExpression = customerData.GetExpression(sortColumn);

                    customerData = sortColumnDirection == "desc" 
                        ? customerData.OrderByDescending(discountFilterExpression) 
                        : customerData.OrderBy(discountFilterExpression);
                }

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.BetclicUserName.ToLower().Contains(searchValue.ToLower()) || m.Email.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data },
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
