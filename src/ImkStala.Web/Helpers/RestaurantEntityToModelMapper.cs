using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using ImkStala.Web.Models;

namespace ImkStala.Web.Helpers
{
    public class RestaurantEntityToModelMapper
    {
        public static RestaurantModel EntityToModel(Restaurant entity)
        {
            if (entity == null)
                return null;
            RestaurantModel model = new RestaurantModel
            {
                Id = entity.Id,
                Adress = entity.Address,
                Email = entity.Email,
                Manager = entity.Manager,
                PhoneNumber = entity.PhoneNumber,
                Rating = entity.Rating,
                RegistrationDate = entity.RegistrationDate,
                RestaurantName = entity.RestaurantName,
                VatCode = entity.VatCode,
                Website = entity.Website,
                Workhours = entity.Workhours,
                Description = entity.Description,
                RestaurantTables = new List<RestaurantTableModel>()
            };
            if (entity.RestaurantTables != null)
            {
                foreach (var restaurantTable in entity.RestaurantTables)
                {
                    model.RestaurantTables.Add(RestaurantTableEntityToModelMapper.EntityToModel(restaurantTable));
                }
            }
            
            return model;
        }
    }
}
