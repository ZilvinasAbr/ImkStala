using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;
using ImkStala.Web.Helpers;
using ImkStala.Web.Models;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImkStala.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class RestaurantsController : Controller
    {
        private readonly IApplicationService _applicationService;

        public RestaurantsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = _applicationService.GetAllRestaurants().ToList();

            return restaurants;
        }
        [HttpGet("pages/{page}/{searchKey}")]
        public IEnumerable<Restaurant> GetRestaurantsPage(int page, string searchKey)
        {
           IList<Restaurant> restaurants = _applicationService.GetRestaurantsPage(page, searchKey);
            return restaurants;
        }

        [HttpGet("favorites/{visitorId}")]
        public IEnumerable<Restaurant> GetFavorites(int visitorId)
        {
            List<Restaurant> favorites = _applicationService.GetFavorites(visitorId).ToList();

            return favorites;
        }

        [HttpGet("{id}")]
        public Restaurant GetRestaurantById(int id)
        {
            Restaurant restaurant = _applicationService.GetRestaurantByRestaurantId(id);
            //RestaurantModel model = RestaurantEntityToModelMapper.EntityToModel(restaurant);
            return restaurant;
           // return model;
        }

        //somehow this http put method doesn't work, can't get data from body to value
        [HttpPut("{id}")]
        public void EditRestaurantProfile(int id, [FromBody]RestaurantModel value)
        {
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
