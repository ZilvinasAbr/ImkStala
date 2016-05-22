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
    public class VisitorsController : Controller
    {
        private readonly IApplicationService _applicationService;

        public VisitorsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("reservations/{visitorId}")]
        public IEnumerable<Reservation> GetReservations(int visitorId)
        {
            IEnumerable<Reservation> reservations = _applicationService.GetVisitorReservationsById(visitorId);

            return reservations;
        }

        [HttpGet("favorites/{visitorId}")]
        public IEnumerable<Restaurant> GetFavorites(int visitorId)
        {
            List<Restaurant> favorites = _applicationService.GetFavorites(visitorId).ToList();

            return favorites;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            
        }
    }
}
