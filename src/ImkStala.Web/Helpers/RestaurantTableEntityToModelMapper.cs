using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using ImkStala.Web.Models;
using Microsoft.Data.Entity.Metadata.Internal;

namespace ImkStala.Web.Helpers
{
    public class RestaurantTableEntityToModelMapper
    {
        public static RestaurantTableModel EntityToModel(RestaurantTable entity)
        {
            RestaurantTableModel model = new RestaurantTableModel
            {
                Id = entity.RestaurantTableId,
                Reservations = new List<ReservationModel>(),
                RestaurantTableSeats = entity.RestaurantTableSeats
            };

            if (entity.Reservations == null)
            {
                return model;
            }
            foreach (var reservation in entity.Reservations)
            {
                ReservationModel reservationModel = new ReservationModel()
                {
                    ReservationStartDateTime = reservation.ReservationStartDateTime,
                    ReservationEndDateTime = reservation.ReservationEndDateTime
                };
                model.Reservations.Add(reservationModel);
            }
            return model;
        }
    }
}
