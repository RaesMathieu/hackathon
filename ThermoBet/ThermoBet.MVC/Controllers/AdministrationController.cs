using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThermoBet.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using ThermoBet.Core.Interfaces;
using System.Threading.Tasks;
using System;

namespace ThermoBet.MVC.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataAdministrationService _dataAdministrationService;
        private readonly IConfigurationService _configurationService;

        public AdministrationController(
            ILogger<HomeController> logger,
            IDataAdministrationService dataAdministrationService,
            IConfigurationService configurationService)
        {
            _logger = logger;
            _dataAdministrationService = dataAdministrationService;
            this._configurationService = configurationService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DataTest()
        {
            DateTime dateTime = await _configurationService.GetDateTimeUtcNow();
            return View(new AdministrationViewModel
            {
                DateTimeUtcNow = new DateTime(
                    dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerMinute),
                    dateTime.Kind)
            });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DataTest(AdministrationViewModel model)
        {
            await _configurationService.SetDateTimeUtcNow(model.DateTimeUtcNow);

            return RedirectToAction(nameof(DataTest));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ClearData()
        {
            _dataAdministrationService.ClearData();
            return RedirectToAction(nameof(DataTest));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult InsertTestData()
        {
            _dataAdministrationService.InsertTestData();
            return RedirectToAction(nameof(DataTest));
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
