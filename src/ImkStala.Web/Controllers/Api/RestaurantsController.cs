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
        public IEnumerable<RestaurantModel> GetAllRestaurants()
        {
            List<Restaurant> restaurants = _applicationService.GetAllRestaurants().ToList();
            //magical by Resharper generated line, just maps all the Resetaurant objects to RestaurantModel
            IList<RestaurantModel> models = restaurants.Select(RestaurantEntityToModelMapper.EntityToModel).ToList(); 
            return models;
        }
        [HttpGet("pages/{page}/{searchKey}")]
        public IEnumerable<RestaurantModel> GetRestaurantsPage(int page, string searchKey)
        {
            List<Restaurant> restaurants = _applicationService.GetRestaurantsPage(page, searchKey).ToList();
            IList<RestaurantModel> models = restaurants.Select(RestaurantEntityToModelMapper.EntityToModel).ToList();
            return models;
        }

        [HttpGet("favorites/{visitorId}")]
        public IEnumerable<RestaurantModel> GetFavorites(int visitorId)
        {
            List<Restaurant> favorites = _applicationService.GetFavorites(visitorId).ToList();
            IList<RestaurantModel> models = favorites.Select(RestaurantEntityToModelMapper.EntityToModel).ToList();
            return models;
        }

        [HttpGet("{id}")]
        public RestaurantModel GetRestaurantById(int id)
        {
            Restaurant restaurant = _applicationService.GetRestaurantByRestaurantId(id);
            RestaurantModel model = RestaurantEntityToModelMapper.EntityToModel(restaurant);

            return model;
        }

        //somehow this http put method doesn't work, can't get data from body to value
        [HttpPut("editrestaurantprofile/{id}")]
        public void EditRestaurantProfile(int id, [FromBody]string value)
        {
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
