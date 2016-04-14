using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ImkStala.DataAccess;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;
using Microsoft.Data.Entity;

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

        public bool AddTableByRestaurantId(RestaurantTable restaurantTable, int id)
        {
            Restaurant restaurant = _dbContext
                .Restaurants
                //.Include(x => x.RestaurantTables)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return false;
            }
                
            restaurant.RestaurantTables.Add(restaurantTable);
            //_dbContext.RestaurantTables.Add(restaurantTable);
            _dbContext.SaveChanges();

            return true;
        }

        public void AddVisitor(Visitor visitor)
        {
            _dbContext.Visitors.Add(visitor);
            _dbContext.SaveChanges();
        }

        public bool AddFavorite(string userId, int restaurantId)
        {
            Visitor visitor = this.GetVisitorByUserId(userId);
            Restaurant restaurant = this.GetRestaurantByRestaurantId(restaurantId);
            visitor.Favorites.Add(restaurant);
            _dbContext.SaveChanges();
            return true;
        }

        public IList<Restaurant> GetAllRestaurants()
        {
            IList<Restaurant> restaurants = _dbContext.Restaurants
                .Include(x => x.RestaurantTables)
                .ToList();

            return restaurants;
        }

        public IList<Restaurant> GetRestaurantsPage(int page, string searchKey)
        {
            int skip = page * 5;
            int pageLength = 4;
            if (searchKey != "all")
            {
                IList < Restaurant > restaurantsSearch = _dbContext.Restaurants
                    .Where(x => x.RestaurantName.StartsWith(searchKey))
                    .OrderByDescending(x => x.RegistrationDate)
                    .Skip(skip)
                    .Take(pageLength)
                    .ToList();
                return restaurantsSearch;
            }
            IList<Restaurant> restaurants = _dbContext.Restaurants
                .OrderByDescending(x => x.RegistrationDate)
                .Skip(skip)
                .Take(pageLength)
                .ToList();

            foreach (var restaurant in restaurants)
            {
                restaurant.RestaurantTables =
                    _dbContext.RestaurantTables.Where(r => r.Restaurant.Id == restaurant.Id).ToList();
            }
            return restaurants;
        }

        public IList<Restaurant> GetFavorites(int visitorId)
        {
            Visitor visitor = _dbContext.Visitors
                .Include(x => x.Favorites)
                .SingleOrDefault(v => v.Id == visitorId);

            List<Restaurant> favourites = (List<Restaurant>) visitor?.Favorites;

            return favourites;
        }

        public Restaurant GetRestaurantByRestaurantId(int id)
        {
            Restaurant restaurant = _dbContext.Restaurants
                .Include(r => r.RestaurantTables)
                .FirstOrDefault(x => x.Id == id);
            return restaurant;
        }

        public Restaurant GetRestaurantByUserId(string userId)
        {
            return _dbContext.Restaurants
                .Include(x => x.RestaurantTables)
                .FirstOrDefault(r => r.ApplicationUser.Id == userId);
        }

        public Visitor GetVisitorByUserId(string userId)
        {
            return _dbContext.Visitors.FirstOrDefault(r => r.ApplicationUser.Id == userId);
        }

        public IList<RestaurantTable> GetRestaurantTablesByRestaurantId(int restaurantId)
        {
            IList<RestaurantTable> restaurantTables = _dbContext.RestaurantTables
                .Where(r => r.Restaurant.Id == restaurantId)
                .ToList();

            return restaurantTables;
        }

        public IList<RestaurantTable> GetRestaurantTablesByUserId(string userId)
        {
            /*IList<RestaurantTable> restaurantTables2 = _dbContext.RestaurantTables
                .Where(r => r.Restaurant.ApplicationUser.Id == userId).ToList();*/
            Restaurant restaurant = GetRestaurantByUserId(userId);

            if (restaurant == null)
            {
                return null;
            }

            IList<RestaurantTable> restaurantTables = GetRestaurantTablesByRestaurantId(restaurant.Id);//TODO

            return restaurantTables;
        }

        public bool AddReservation(Reservation reservation, string userId, int restaurantId, int reservationTableSeats)
        {
            Visitor visitor = this.GetVisitorByUserId(userId);
            Restaurant restaurant = this.GetRestaurantByRestaurantId(restaurantId);
            reservation.Visitor = visitor;
            reservation.Restaurant = restaurant;
            RestaurantTable restaurantTable = _dbContext.RestaurantTables
                .Include(r => r.Reservations)
                .Where(r => r.RestaurantTableSeats == reservationTableSeats && r.Restaurant.Id == restaurantId)
                .FirstOrDefault(r => r.CanAddReservation(reservation));

            /*RestaurantTable restaurantTable = restaurant.RestaurantTables
                .Where(r => r.RestaurantTableSeats == reservationTableSeats)
                .FirstOrDefault(t => t.CanAddReservation(reservation));*/

            if (restaurantTable == null)
            {
                return false;
            }

            reservation.RestaurantTable = restaurantTable;
            reservation.Visitor = visitor;
            visitor.VisitorReservations.Add(reservation);
            restaurantTable.Reservations.Add(reservation);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
