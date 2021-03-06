﻿using System;
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

        bool AddMenuItemByRestaurantId(MenuItem item, int id);

        bool RemoveMenuItemByRestaurantId(int menuId, int id);

        bool AddFavorite(string userId, int restaurantId);

        IList<RestaurantTable> GetRestaurantTablesByRestaurantId(int resetaurantId);

        IList<RestaurantTable> GetRestaurantTablesByUserId(string userId);

        IList<MenuItem> GetRestaurantMenuByUserId(string userId);

        IList<MenuItem> GetRestaurantMenuByRestaurantId(int restaurantId);

        IList<Restaurant> GetFavorites(int visitorId);

        IList<Restaurant> GetAllRestaurants();

        IList<Restaurant> GetTopRestaurants();

        IList<Restaurant> GetRestaurantsPage(int page, string searchKey);

        Restaurant GetRestaurantByRestaurantId(int id);

        Restaurant GetRestaurantByUserId(string userId);

        Visitor GetVisitorByUserId(string userId);

        bool AddReservation(Reservation reservation, string userId, int restaurantId, int reservationTableSeats);

        bool AddRating(int ratingValue, string userId, int restaurentId);

        bool EditRestaurantProfileByUserId(string userId, string restaurantName,
            string address, string phoneNumber, string website, string description, string logoPath, List<Interior> interiors);

        bool EditVisitorProfileByUserId(string userId, string firstName,
            string lastName, string phoneNumber);

        IEnumerable<Reservation> GetVisitorReservationsById(int visitorId);

        IEnumerable<MenuItemType> GetMenuItemTypesByUserId(string id);

        MenuItemType GetMenuItemTypeByRestaurantIdTypeName(int restaurantId, string selectedMenuItemType);

        bool AddMenuItemType(MenuItemType menuItemType);

        Dictionary<int, int> GetRestaurantTablesByUserIdCounted(string userId);

        bool AddTablesByUserId(int tableSeats, int tableCount, string userId);
    }
}
