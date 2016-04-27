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

            Reservation reservation = new Reservation()
            {
                ReservationStartDateTime = reservationStartDateTime,
                ReservationEndDateTime = reservationEndDateTime,
                VisitorMessage = model.VisitorMessage,
            };

            bool succeeded = _applicationService.AddReservation(reservation, user.Id, model.RestaurantId, model.RestaurantTableSeats);

            if (succeeded)
            {
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
        public async Task<IActionResult> Rate()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            var radioValue = Request.Form["rating"];
            int restaurantId = int.Parse(Request.Form["id"]);
            int ratingValue = int.Parse(radioValue);

            bool succeeded = _applicationService.AddRating(ratingValue, user.Id, restaurantId);

            if (succeeded)
            {
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
    }
}
