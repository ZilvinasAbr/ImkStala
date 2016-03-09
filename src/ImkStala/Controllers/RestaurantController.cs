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
using Microsoft.Data.Entity;
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
        private readonly ApplicationDbContext _context;

        public RestaurantController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _context = context;
        }

        // GET: /<controller>/
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            IndexViewModel model = null;
            if (user.UserAccountType == "Restaurant")
            {
                model = new IndexViewModel()
                {

                };
            }
            return View(model);
        }

        public async Task<IActionResult> ViewTables()
        {
            var user = await GetCurrentUserAsync();
            Restaurant restaurantData = await _context.Restaurants.FirstOrDefaultAsync(w => w.ApplicationUser.Id == user.Id);
            List<Table> tables = await _context.Tables.Where(w => w.Restaurant.Id == restaurantData.Id).ToListAsync();
            //IEnumerable<Table> tables = GetTablesEnumeration();
            ViewTablesViewModel viewTablesViewModel = null;
            if (user.UserAccountType == "Restaurant")
            {
                viewTablesViewModel = new ViewTablesViewModel()
                {
                    Tables = restaurantData.Tables
                };
            }
            return View(viewTablesViewModel);
        }

        [HttpGet]
        public IActionResult AddTable()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTable(AddTableViewModel tableViewModel)
        {
            var user = await GetCurrentUserAsync();
            //ApplicationUser applicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(w => w.Id == user.Id);
            Restaurant restaurantData = await _context.Restaurants.FirstOrDefaultAsync(w => w.ApplicationUser.Id == user.Id);
            
            if (user.UserAccountType == "Restaurant")
            {
                if (ModelState.IsValid)
                {
                    Table table = new Table()
                    {
                        TableSeats = tableViewModel.TableSeats
                    };
                    _context.Tables.Add(table);
                    if (restaurantData == null)
                        user.RestaurantData = new Restaurant();
                    if(restaurantData.Tables == null)
                        user.RestaurantData.Tables = new List<Table>();
                    restaurantData.Tables.Add(table);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            return View(tableViewModel);
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