using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.ServicesContracts
{
    public interface IApplicationService
    {
        void AddRestaurant(Restaurant restaurant);
        void AddVisitor(Visitor visitor);
        bool AddTableByRestaurantId(RestaurantTable restaurantTable, int id);
        IList<RestaurantTable> GetRestaurantTablesByRestaurantId(int resetaurantId);
        IList<RestaurantTable> GetRestaurantTablesByUserId(string userId);
        
        IList<Restaurant> GetAllRestaurants();
        IList<Restaurant> GetRestaurantsPage(int page);
        Restaurant GetRestaurantByRestaurantId(int id);
        Restaurant GetRestaurantByUserId(string userId);
        Visitor GetVisitorByUserId(string userId);
        bool AddReservation(Reservation reservation, string userId, int restaurantId, int reservationTableSeats);
    }
}
