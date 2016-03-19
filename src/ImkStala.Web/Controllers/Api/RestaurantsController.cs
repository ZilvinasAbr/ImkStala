using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;
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

        // GET api/values/5
        /*[HttpGet("{id}")]
        public Restaurant Get(int id)
        {
            return "value";
        }*/

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
