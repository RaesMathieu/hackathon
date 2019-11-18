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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly ITournamentService _tournamentService;
        private readonly IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            ITournamentService tournamentService,
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _tournamentService = tournamentService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetByAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<UserViewModel>(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel movie, string button)
        {
            if (button == "Cancel")
                return RedirectToAction(nameof(Index));

            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<Core.Models.UserModel>(movie);

                await _userService.Update(user);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        [HttpGet("User/{userId}/SeeResult/{tournamentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SeeResult(int? userId, int? tournamentId)
        {
            if (userId == null || tournamentId == null)
            {
                return NotFound();
            }

            var tournament = await _tournamentService.GetAsync(tournamentId.Value);
            if (tournament == null)
            {
                return NotFound();
            }
            var resultingTournamentViewModel = _mapper.Map<ResultingTournamentViewModel>(tournament);
            var bets = await _tournamentService.GetBetAsync(userId.Value, tournament.Id);

            foreach (var market in resultingTournamentViewModel.Markets)
            {
                market.ChosenSelectionId = bets.FirstOrDefault(s => s.Market.Id == market.Id)?.Selection?.Id;
            }

            return View(_mapper.Map<ResultingTournamentViewModel>(resultingTournamentViewModel));
        }

        public IActionResult LoadData()
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
                var customerData = _userService.GetAllUsersAsync();

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {

                    var discountFilterExpression = GetExpression<Core.Models.UserModel>(sortColumn);

                    if (sortColumnDirection == "desc")
                        customerData.OrderByDescending(discountFilterExpression);
                    else
                        customerData = customerData.OrderBy(discountFilterExpression);
                }

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Pseudo.ToLower().Contains(searchValue.ToLower()) || m.Login.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data },
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new DateTimeConverter() }
                });

            }
            catch (Exception)
            {
                throw;
            }

        }

        public class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Debug.Assert(typeToConvert == typeof(DateTime));
                return DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToUniversalTime().ToString("g"));
            }
        }
        //makes expression for specific prop
        public static Expression<Func<TSource, object>> GetExpression<TSource>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "x");
            Expression conversion = Expression.Convert(Expression.Property
            (param, propertyName), typeof(object));   //important to use the Expression.Convert
            return Expression.Lambda<Func<TSource, object>>(conversion, param);
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
