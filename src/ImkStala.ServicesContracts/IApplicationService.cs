using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.ServicesContracts
{
    public interface IApplicationService
    {
        Restaurant GetRestaurantByUserId(string userId);
        void AddRestaurant(Restaurant restaurant);
        void AddVisitor(Visitor visitor);
        void AddTableByRestaurantId(RestaurantTable restaurantTable, int id);
        IList<RestaurantTable> GetRestaurantTablesByRestaurantId(int resetaurantId);
        IList<RestaurantTable> GetRestaurantTablesByUserId(string userId);
        IList<Restaurant> GetAllRestaurants();
        IList<Restaurant> GetRestaurantsPage(int page);
        Restaurant GetRestaurantByRestaurantId(int id);
    }
}
