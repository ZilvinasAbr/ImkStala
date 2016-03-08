using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImkStala.Models;
using ImkStala.Services;
using ImkStala.ViewModels.Restaurant;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImkStala.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public RestaurantController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        // GET: /<controller>/
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            IndexViewModel model = null;
            {

            };
            if (user.UserAccountType == "Restaurant")
            {
                model = new IndexViewModel()
                {

                };
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewTables()
        {
            var user = await GetCurrentUserAsync();
            IEnumerable<Table> tables = GetTablesEnumeration();
            ViewTablesViewModel viewTablesViewModel = null;
            if (user.UserAccountType == "Restaurant")
            {
                viewTablesViewModel = new ViewTablesViewModel()
                {
                    Tables = tables
                };
            }
            return View(viewTablesViewModel);
        }

        /*[Authorize]
        public IActionResult ViewTables()
        {
            IEnumerable<Table> tables = GetTablesEnumeration();
            ViewTablesViewModel viewTablesViewModel = new ViewTablesViewModel()
            {
                Tables = tables
            };
            return View(viewTablesViewModel);
        }*/
        private IEnumerable<Table> GetTablesEnumeration()
        {
            List<Table> tables = new List<Table>();
            tables.Add(new Table() { Id = 1, ReservationCalendar = null, TableSeats = 4 });
            tables.Add(new Table() { Id = 2, ReservationCalendar = null, TableSeats = 2 });
            tables.Add(new Table() { Id = 3, ReservationCalendar = null, TableSeats = 1 });
            tables.Add(new Table() { Id = 4, ReservationCalendar = null, TableSeats = 3 });
            tables.Add(new Table() { Id = 5, ReservationCalendar = null, TableSeats = 5 });
            tables.Add(new Table() { Id = 6, ReservationCalendar = null, TableSeats = 8 });
            tables.Add(new Table() { Id = 7, ReservationCalendar = null, TableSeats = 2 });
            return tables;
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }
    }


}
