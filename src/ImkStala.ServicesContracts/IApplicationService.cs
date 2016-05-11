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

        bool AddFavorite(string userId, int restaurantId);

        IList<RestaurantTable> GetRestaurantTablesByRestaurantId(int resetaurantId);

        IList<RestaurantTable> GetRestaurantTablesByUserId(string userId);

        IList<Restaurant> GetFavorites(int visitorId);

        IList<Restaurant> GetAllRestaurants();

        IList<Restaurant> GetRestaurantsPage(int page, string searchKey);

        Restaurant GetRestaurantByRestaurantId(int id);

        Restaurant GetRestaurantByUserId(string userId);

        Visitor GetVisitorByUserId(string userId);

        bool AddReservation(Reservation reservation, string userId, int restaurantId, int reservationTableSeats);

        bool AddRating(int ratingValue, string userId, int restaurentId);

        bool EditRestaurantProfileByUserId(string userId, string restaurantName,
            string address, string phoneNumber, string website, string description);

        bool EditVisitorProfileByUserId(string userId, string firstName,
            string lastName, string phoneNumber);
    }
}
