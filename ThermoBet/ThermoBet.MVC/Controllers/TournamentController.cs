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
    public class TournamentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITournamentService _tournamentService;
        private readonly IMapper _mapper;

        public TournamentController(
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
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var tournament = new Core.Models.TournamentModel
            {
                Markets = new List<Core.Models.MarketModel>{
                    new Core.Models.MarketModel {
                        Selections = new List<Core.Models.SelectionModel> {
                            new Core.Models.SelectionModel()
                            {
                                IsYes = true
                            },
                            new Core.Models.SelectionModel()
                            {
                                IsYes = false
                            }
                        }
                    }
                }
            };

            return View("Edit", _mapper.Map<TournamentViewModel>(tournament));
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
        public IActionResult RemoveMarket(TournamentViewModel movie, string button)
        {
            ModelState.Clear();
            movie.Markets.RemoveAt(int.Parse(button));

            return View("Edit", movie);
        }

        public IActionResult AddMarket(TournamentViewModel movie)
        {
            ModelState.Clear();
            movie.Markets.Add(new MarketViewModel
            {
                Selections = new List<SelectionViewModel>{
                    new SelectionViewModel()
                    {
                        IsYes = true
                    },
                    new SelectionViewModel()
                    {
                        IsYes = false
                    }
                }
            });

            return View("Edit", movie);
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
                var customerData = _tournamentService.GetAll();
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {

                    var discountFilterExpression = GetExpression<Core.Models.TournamentModel>(sortColumn);

                    if (sortColumnDirection == "desc")
                        customerData.OrderByDescending(discountFilterExpression);
                    else
                        customerData = customerData.OrderBy(discountFilterExpression);
                }

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name.ToLower().Contains(searchValue.ToLower()));
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
