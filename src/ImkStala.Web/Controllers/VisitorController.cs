using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using ImkStala.DataAccess;
using ImkStala.Web.ViewModels.Account;
using ImkStala.Web.ViewModels.Visitor;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;
using ImkStala.Web.Services;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImkStala.Web.Controllers
{
    [Authorize(Roles = "Visitor")]
    public class VisitorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IApplicationService _applicationService;

        public VisitorController(UserManager<ApplicationUser> userManager,
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


        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            IndexViewModel model = null;
            //if (user.AccountType == "User")
            //{
                model = new IndexViewModel()
                {

                };
            //}
            return View(model);
        }

        [HttpGet]
        public IActionResult EditVisitorProfile()
        {
            string userId = HttpContext.User.GetUserId();
            Visitor visitor = _applicationService.GetVisitorByUserId(userId);
            EditVisitorProfileViewModel model = new EditVisitorProfileViewModel()
            {
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                PhoneNumber = visitor.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditVisitorProfile(EditVisitorProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());

            Visitor visitor = _applicationService.GetVisitorByUserId(user.Id);
            if (ModelState.IsValid)
            {
                _applicationService.EditVisitorProfileByUserId(user.Id, model.FirstName,
                    model.LastName, model.PhoneNumber);

                TempData["Success"] = "Atnaujinta sėkmingai!";

                return RedirectToAction("EditVisitorProfile");
            }

            return View();
        }
        public async Task<IActionResult> GetVisitorFavorites()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());

            Visitor visitor = _applicationService.GetVisitorByUserId(user.Id);

            ViewData["Id"] = visitor.Id;

            return View();
        }
    }
}
