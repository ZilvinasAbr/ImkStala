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
                ReservationCalendar = new ReservationCalendar(),
                RestaurantTableSeats = entity.RestaurantTableSeats
            };
            model.ReservationCalendar.Reservations = new List<Reservation>();
            if (entity.ReservationCalendar == null || entity.ReservationCalendar.Reservations == null)
            {
                return model;
            }
            foreach (var reservation in entity.ReservationCalendar.Reservations)
            {
                ReservationModel reservationModel = new ReservationModel()
                {
                    ReservationStartDateTime = reservation.ReservationStartDateTime,
                    ReservationEndDateTime = reservation.ReservationEndDateTime
                };
            }
            return model;
        }
    }
}
