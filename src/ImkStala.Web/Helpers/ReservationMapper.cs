using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using ImkStala.Web.Models;

namespace ImkStala.Web.Helpers
{
    public class ReservationMapper
    {
        public static ReservationModel EntityToModel(Reservation entity)
        {
            ReservationModel reservationModel = new ReservationModel()
            {
                ReservationStartDateTime = entity.ReservationStartDateTime,
                ReservationEndDateTime = entity.ReservationEndDateTime
            };

            return reservationModel;
        }
    }
}
