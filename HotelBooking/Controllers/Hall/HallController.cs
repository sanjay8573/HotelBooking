using HotelBooking.Model.Hall;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.Hall
{
    public class HallController : ApiController
    {
        private readonly IHall _hall;
        public HallController()
        {
            _hall = new HallRepository();
            
        }

        [Route("api/Hall/GetAllHall/{BranchId}")]
        [HttpGet]
        public IEnumerable<HALL_PARTY_MASTER> GetAllHall(int BranchId)
        {
            return _hall.GetAllHall(BranchId);
        }
        [Route("api/Hall/GetHall/{HallId}")]
        [HttpGet]
        public HALL_PARTY_MASTER GetHall(int HallId)
        {
            return _hall.GetHall(HallId);
        }

        [Route("api/Hall/SaveHall")]
        [HttpPost]
        public bool SaveHall(HALL_PARTY_MASTER hallEntity)
        {
            return _hall.SaveHall(hallEntity);
        }

        [Route("api/Hall/GetHallTimings/{BranchId}")]
        [HttpGet]
        public IEnumerable<Hall_Party_Time_Slot> GetHallTimings(int BranchId)
        {
            return _hall.GetHallTimings(BranchId);
        }
        [Route("api/Hall/SaveHallBooking")]
        [HttpPost]
        public bool SaveHallBooking(HallBooking hallBookingEntity)
        {
            return _hall.SaveHallBooking(hallBookingEntity);
        }

        [Route("api/Hall/GetHallBookings/{BranchId}")]
        [HttpGet]
        public IEnumerable<HallBooking> GetHallBookings(int BranchId)
        {
            return _hall.GetHallBookings(BranchId);
        }
    }
}
