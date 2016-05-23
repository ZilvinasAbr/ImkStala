using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImkStala.DataAccess;
using ImkStala.DataAccess.Entities;
using ImkStala.Services;
using ImkStala.ServicesContracts;
using ImkStala.Web.Services;
using ImkStala.Web.ViewModels.Restaurant;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImkStala.Web.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class RestaurantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IApplicationService _applicationService;
        IHostingEnvironment _hostEnv;
        //private readonly ApplicationDbContext _context;

        public RestaurantController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory, IApplicationService applicationService,
        IHostingEnvironment hostEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _applicationService = applicationService;
            _hostEnv = hostEnv;
        }

        // GET: /<controller>/
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            IndexViewModel model = null;
            //if (user.AccountType == "Restaurant")
            //{
                model = new IndexViewModel()
                {

                };
            //}
            return View(model);
        }

        public async Task<IActionResult> ViewTables()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            //Restaurant restaurantData = await _context.Restaurants.FirstOrDefaultAsync(w => w.ApplicationUser.Id == user.Id);
            //List<RestaurantTable> tables = await _context.RestaurantTables.Where(w => w.Restaurant.Id == restaurantData.Id).ToListAsync();
            IList<RestaurantTable> tables =
                _applicationService.GetRestaurantTablesByUserId(user.Id);
            //IEnumerable<Table> tables = GetTablesEnumeration();
            ViewTablesViewModel viewTablesViewModel = null;
            //if (user.AccountType == "Restaurant")
            //{
                viewTablesViewModel = new ViewTablesViewModel()
                {
                    Tables = tables
                };
            //}
            return View(viewTablesViewModel);
        }

        

        

        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            Restaurant restaurant = _applicationService.GetRestaurantByUserId(user.Id);
            ViewBag.Id = restaurant.Id;
            return View();
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
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            //ApplicationUser applicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(w => w.Id == user.Id);
            //Restaurant restaurantData = await _context.Restaurants.FirstOrDefaultAsync(w => w.ApplicationUser.Id == user.Id);
            //if (user.AccountType == "Restaurant")
            //{
                Restaurant restaurant = _applicationService.GetRestaurantByUserId(user.Id);
                if (ModelState.IsValid)
                {
                //_context.RestaurantTables.Add(table);
                    for (int i = 0; i < tableViewModel.TableCount; i++)
                    {
                        RestaurantTable table = new RestaurantTable()
                        {
                            RestaurantTableSeats = tableViewModel.TableSeats
                        };
                        _applicationService.AddTableByRestaurantId(table, restaurant.Id);
                    }
                    TempData["Success"] = "Sėkmingai idejote " + tableViewModel.TableCount.ToString() + " staliukus!";
                //restaurant.RestaurantTables.Add(table);
                //_context.SaveChanges();
                return RedirectToAction("ViewTables");
                }
            //}
            
            return View(tableViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewMenu()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
            //Restaurant restaurantData = await _context.Restaurants.FirstOrDefaultAsync(w => w.ApplicationUser.Id == user.Id);
            //List<RestaurantTable> tables = await _context.RestaurantTables.Where(w => w.Restaurant.Id == restaurantData.Id).ToListAsync();
            IList<MenuItem> meals =
                _applicationService.GetRestaurantMenuByUserId(user.Id);

            ViewMenuViewModel viewMenuViewModel = null;
            //IEnumerable<Table> tables = GetTablesEnumeration();
            //if (user.AccountType == "Restaurant")
            //{
            viewMenuViewModel = new ViewMenuViewModel()
            {
                Meals = meals,
                MenuItemTypes = _applicationService.GetMenuItemTypesByUserId(user.Id)
            };
            //}
            return View(viewMenuViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMenu(AddMenuViewModel menuViewModel)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());

            Restaurant restaurant = _applicationService.GetRestaurantByUserId(user.Id);
            if (ModelState.IsValid)
            {
                MenuItem item = new MenuItem()
                {
                    Name = menuViewModel.Name,
                    Price = menuViewModel.Price
                };

                MenuItemType menuItemType = new MenuItemType();

                if (menuViewModel.SelectedMenuItemType == "-1" && !string.IsNullOrEmpty(menuViewModel.NewTypeName))
                {
                    //Pridedame nauja MenuItemType
                    menuItemType.Restaurant = restaurant;
                    menuItemType.TypeName = menuViewModel.NewTypeName;
                    bool success = _applicationService.AddMenuItemType(menuItemType);

                    if (!success)
                    {
                        //redirectina i pradzia, nepavyko prideti
                        //TODO: kazkaip isvesti nesekmes teksta
                        return RedirectToAction("ViewMenu");
                    }
                    TempData["Success"] = "Sėkmingai įdėjote" + menuViewModel.Name + " patiekalą!";

                }
                else
                {
                    menuItemType = _applicationService.GetMenuItemTypeByRestaurantIdTypeName(
                        restaurant.Id, menuViewModel.SelectedMenuItemType);

                    if (menuItemType == null)
                    {
                        //redirectina i pradzia, nepavyko prideti
                        //TODO: kazkaip isvesti nesekmes teksta
                        return RedirectToAction("ViewMenu");
                    }
                }

                item.Type = menuItemType;
                _applicationService.AddMenuItemByRestaurantId(item, restaurant.Id);
                TempData["Success"] = "Sėkmingai įdėjote" + menuViewModel.Name + " patiekalą!";
            }

            return RedirectToAction("ViewMenu");
        }

        [HttpGet]
        public IActionResult EditRestaurantProfile()
        {
            string userId = HttpContext.User.GetUserId();
            Restaurant restaurant = _applicationService.GetRestaurantByUserId(userId);
            EditRestaurantProfileViewModel model = new EditRestaurantProfileViewModel()
            {
                Address = restaurant.Address,
                Description = restaurant.Description,
                PhoneNumber = restaurant.PhoneNumber,
                RestaurantName = restaurant.RestaurantName,
                Website = restaurant.Website,
                LogoPath = restaurant.LogoPath,
                Interiors = restaurant.Interiors
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRestaurantProfile(EditRestaurantProfileViewModel model, ICollection<IFormFile> logo, ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_hostEnv.WebRootPath, "images\\logos\\");
                string logoPath = null;
                var user = await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
                Restaurant restaurant = _applicationService.GetRestaurantByUserId(user.Id);
                foreach (var file in logo)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fnm = restaurant.Email + ".png";
                    if (fileName.ToLower().EndsWith(".png") || fileName.ToLower().EndsWith(".jpg") || fileName.ToLower().EndsWith(".gif"))
                    {
                        var filePath = Path.Combine(uploads, fnm);
                        var directory = new DirectoryInfo(uploads);
                        if (directory.Exists == false)
                        {
                            directory.Create();
                        }
                        await file.SaveAsAsync(filePath);
                        logoPath = "images/logos/" + fnm;
                    }

                }

                uploads = Path.Combine(_hostEnv.WebRootPath, "images\\interior\\");
                List<Interior> interiors = new List<Interior>();
                int x = 1;
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fnm = restaurant.Email + x + ".png";
                    if (fileName.ToLower().EndsWith(".png") || fileName.ToLower().EndsWith(".jpg") || fileName.ToLower().EndsWith(".gif"))
                    {
                        var filePath = Path.Combine(uploads, fnm);
                        var directory = new DirectoryInfo(uploads);
                        if (directory.Exists == false)
                        {
                            directory.Create();
                        }
                        await file.SaveAsAsync(filePath);
                        Interior interior = new Interior()
                        {
                            InteriorPath = "images/interior/" + fnm
                        };
                        interiors.Add(interior);
                        x++;
                    }

                }

                _applicationService.EditRestaurantProfileByUserId(user.Id, model.RestaurantName,
                    model.Address, model.PhoneNumber, model.Website, model.Description, logoPath, interiors);

                TempData["Success"] = "Atnaujinta sėkmingai!";

                return RedirectToAction("EditRestaurantProfile");
            }

            return View();
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
    }


}