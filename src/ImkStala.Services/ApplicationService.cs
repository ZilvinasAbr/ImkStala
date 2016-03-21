using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;

namespace ImkStala.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
        }

        public void AddTableByRestaurantId(RestaurantTable restaurantTable, int id)
        {
            Restaurant restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if(restaurant.RestaurantTables == null)
                restaurant.RestaurantTables = new List<RestaurantTable>();
            restaurant.RestaurantTables.Add(restaurantTable);
            _dbContext.RestaurantTables.Add(restaurantTable);
            _dbContext.SaveChanges();

        }

        public void AddVisitor(Visitor visitor)
        {
            _dbContext.Visitors.Add(visitor);
            _dbContext.SaveChanges();
        }

        public IList<Restaurant> GetAllRestaurants()
        {
            IList<Restaurant> restaurants = _dbContext.Restaurants.ToList();
            foreach (var restaurant in restaurants)
            {
                restaurant.RestaurantTables =
                    _dbContext.RestaurantTables.Where(r => r.Restaurant.Id == restaurant.Id).ToList();
            }
            return restaurants;
        }

        public Restaurant GetRestaurantByRestaurantId(int id)
        {
            Restaurant restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            return restaurant;
        }

        public Restaurant GetRestaurantByUserId(string userId)
        {
            return _dbContext.Restaurants.FirstOrDefault(r => r.ApplicationUser.Id == userId);
        }

        public IList<RestaurantTable> GetRestaurantTablesByRestaurantId(int restaurantId)
        {
            //Restaurant restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            IList<RestaurantTable> restaurantTables = _dbContext.RestaurantTables.Where(r => r.Restaurant.Id == restaurantId).ToList();
            return restaurantTables;
        }

        public IList<RestaurantTable> GetRestaurantTablesByUserId(string userId)
        {
            Restaurant restaurant = GetRestaurantByUserId(userId);
            if (restaurant == null)
                return null;
            IList<RestaurantTable> restaurantTables = GetRestaurantTablesByRestaurantId(restaurant.Id);
            return restaurantTables;
        }
    }
}
