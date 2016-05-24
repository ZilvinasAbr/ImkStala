using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;
using ImkStala.Web.Services;
using ImkStala.Web.ViewModels.Home;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json;

namespace ImkStala.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IApplicationService _applicationService;

        public HomeController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory, IApplicationService applicationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _applicationService = applicationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Book(int id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookRestaurantTableViewModel model)
        {
            DateTime reservationStartDateTime = new DateTime(model.Date.Year,
                model.Date.Month, model.Date.Day, model.Time.Hour, model.Time.Minute, 0);

            DateTime reservationEndDateTime = new DateTime(model.Date.Year,
                model.Date.Month, model.Date.Day, model.Time.Hour + 2, model.Time.Minute, 0);

            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            string joinedMeals=null;
            if (model.Selected != null)
            {
                var meals = model.Selected;
                joinedMeals = string.Join(", ", meals);
            }
            Reservation reservation = new Reservation()
            {
                ReservationStartDateTime = reservationStartDateTime,
                ReservationEndDateTime = reservationEndDateTime,
                VisitorMessage = model.VisitorMessage,
                Meals = joinedMeals
            };

            bool succeeded = _applicationService.AddReservation(reservation, user.Id, model.RestaurantId, model.RestaurantTableSeats);
            Restaurant rest = _applicationService.GetRestaurantByRestaurantId(model.RestaurantId);
            if (succeeded)
            {
                TempData["Success"] = "Sėkmingai užsirezervavote staliuką " + rest.RestaurantName + " restorane! Jūs esate laukiamas " + reservationStartDateTime.ToString("yyyy-MM-dd H:mm");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> ToFavourites(int restaurantId)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Visitor")]
        public IActionResult Rate(int id)
        {
            ViewData["Id"] = id;
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Rate(/*[FromBody]int restaurantId2*/)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            int ratingValue = int.Parse(Request.Form["rating"]);
            int restaurantId = int.Parse(Request.Form["id"]);

            bool succeeded = _applicationService.AddRating(ratingValue, user.Id, restaurantId);

            if (succeeded)
            {
                TempData["Success"] = "Sėkmingai įvertintas! Jūsų įvertinimas: " + ratingValue.ToString();
                return Redirect(Request.Headers["Referer"]);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> Favorite()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            int restaurantId = int.Parse(Request.Form["id"]);
            bool succeeded =  _applicationService.AddFavorite(user.Id, restaurantId);
            Restaurant restaurant = _applicationService.GetRestaurantByRestaurantId(restaurantId);
            if (succeeded)
            {
                TempData["Success"] = "Restoranas " + restaurant.RestaurantName +" sėkmingai įdėtas i megstamų sąrašą!";
                return Redirect(Request.Headers["Referer"]);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Restaurant(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<IActionResult> History()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            Visitor visitor = _applicationService.GetVisitorByUserId(user.Id);
            ViewBag.Id = visitor.Id;
            return View();
        }
    }
}
