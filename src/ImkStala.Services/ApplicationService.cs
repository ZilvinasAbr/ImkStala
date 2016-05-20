using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ImkStala.DataAccess;
using ImkStala.DataAccess.Entities;
using ImkStala.ServicesContracts;
using Microsoft.AspNet.Identity;
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
            _dbContext.SaveChanges();

            return true;
        }

        public bool AddMenuItemByRestaurantId(MenuItem item, int id)
        {
            Restaurant restaurant = _dbContext
                .Restaurants
                //.Include(x => x.RestaurantTables)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return false;
            }

            restaurant.Meals.Add(item);
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
                .Include(r => r.RestaurantTables)
                .ThenInclude(t => t.Reservations)
                .Include(x => x.Meals)
                .ToList();

            return restaurants;
        }

        public IList<Restaurant> GetRestaurantsPage(int page, string searchKey)
        {
            int skip = page * 4;
            int pageLength = 4;

            if (searchKey != "all")
            {
                var restaurantsSearch = _dbContext.Restaurants
                    .Include(r => r.RestaurantTables)
                    .ThenInclude(t => t.Reservations)
                    .ThenInclude(re => re.Visitor)
                    .Where(x => x.RestaurantName.Contains(searchKey))
                    .OrderByDescending(x => x.RegistrationDate)
                    .Skip(skip)
                    .Take(pageLength)
                    .ToList();
                return restaurantsSearch;
            }

            var restaurants = _dbContext.Restaurants
                .Include(r => r.RestaurantTables)
                .ThenInclude(t => t.Reservations)
                .ThenInclude(re => re.Visitor)
                .OrderByDescending(x => x.RegistrationDate)
                .Skip(skip)
                .Take(pageLength)
                .ToList();

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
                .ThenInclude(t => t.Reservations)
                .ThenInclude(re => re.Visitor)
                .Include(x => x.Meals)
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

        public IList<MenuItem> GetRestaurantMenuByRestaurantId(int restaurantId)
        {
            IList<MenuItem> meals = _dbContext.Meals
                .Where(r => r.Restaurant.Id == restaurantId)
                .ToList();

            return meals;
        }

        public IList<RestaurantTable> GetRestaurantTablesByUserId(string userId)
        {
            Restaurant restaurant = GetRestaurantByUserId(userId);

            if (restaurant == null)
            {
                return null;
            }

            IList<RestaurantTable> restaurantTables = GetRestaurantTablesByRestaurantId(restaurant.Id);//TODO

            return restaurantTables;
        }

        public IList<MenuItem> GetRestaurantMenuByUserId(string userId)
        {
            Restaurant restaurant = GetRestaurantByUserId(userId);

            if (restaurant == null)
            {
                return null;
            }

            IList<MenuItem> meals = GetRestaurantMenuByRestaurantId(restaurant.Id);//TODO

            return meals;
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

        public bool AddRating(int ratingValue, string userId, int restaurantId)
        {
            Visitor visitor = this.GetVisitorByUserId(userId);
            Restaurant restaurant = _dbContext.Restaurants.Where(r => r.Id == restaurantId).SingleOrDefault();
            if (visitor == null || restaurant == null)
                return false;
            Rating find = _dbContext.Ratings.Where(r => r.Restaurant.Id == restaurantId && r.Visitor.Id == visitor.Id).SingleOrDefault();          
            if(find != null)
                find.RatingValue = ratingValue;
            else
            {
                Rating rating = new Rating();
                rating.Visitor = visitor;
                rating.Restaurant = restaurant;
                rating.RatingValue = ratingValue;
                visitor.Ratings.Add(rating);
                restaurant.Ratings.Add(rating);
                restaurant.RateAmount++;
                _dbContext.SaveChanges();
            }
            List<Rating> findAll = _dbContext.Ratings.Where(r => r.Restaurant.Id == restaurantId).ToList();
            double sum = 0;
            foreach (var ratings in findAll)
                sum += ratings.RatingValue;
            restaurant.Rating = Math.Round((sum / restaurant.RateAmount), 1, MidpointRounding.AwayFromZero);
            _dbContext.SaveChanges();
            return true;
        }

        public bool EditRestaurantProfileByUserId(string userId, string restaurantName,
            string address, string phoneNumber, string website, string description, string logoPath)
        {
            Restaurant restaurant = this.GetRestaurantByUserId(userId);
            if (restaurant == null)
            {
                return false;
            }

            if(restaurantName != null)
                restaurant.RestaurantName = restaurantName;
            if(address != null)
                restaurant.Address = address;
            if(phoneNumber != null)
                restaurant.PhoneNumber = phoneNumber;
            if(website != null && website != "http://")
                restaurant.Website = website;
            if(description != null)
                restaurant.Description = description;
            if (logoPath != null)
                restaurant.LogoPath = logoPath;

            _dbContext.SaveChanges();

            return true;
        }

        public bool EditVisitorProfileByUserId(string userId, string firstName,
            string lastName, string phoneNumber)
        {
            Visitor visitor = this.GetVisitorByUserId(userId);
            if (visitor == null)
            {
                return false;
            }

            if (firstName != null)
                visitor.FirstName = firstName;
            if (lastName != null)
                visitor.LastName = lastName;
            if (phoneNumber != null)
                visitor.PhoneNumber = phoneNumber;

            _dbContext.SaveChanges();

            return true;
        }
        public IEnumerable<Reservation> GetVisitorReservationsById(int visitorId)
        {
            var reservations = _dbContext.Reservations
                .Include(r => r.Restaurant)
                .Include(r => r.RestaurantTable)
                .Where(r => r.Visitor.Id == visitorId)
                .ToList();

            return reservations;
        }
    }
}
