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
using System.Xml;

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
        [Route("api/Hall/GetHallBookingCost/{BranchId}/{hallBookingId}")]
        [HttpGet]
        public IEnumerable<HallBookingCost> GetHallBookingCost(int BranchId, int hallBookingId)
        {
            return _hall.GetHallBookingCost(BranchId, hallBookingId);
        }
        [Route("api/Hall/SaveHallBookingCost")]
        [HttpPost]
        public bool SaveHallBookingCost(HallBookingCost hallBookingcostEntity)
        {
            return _hall.SaveHallBookingCost(hallBookingcostEntity);
        }

        [Route("api/Hall/GetHallBookingPayment/{BranchId}/{hallBookingId}")]
        [HttpGet]
        public IEnumerable<HallBookingPayment> GetHallBookingPayment(int BranchId, int hallBookingId)
        {
            return _hall.GetHallBookingPayment(BranchId, hallBookingId);
        }

        [Route("api/Hall/CheckHallAvailability")]
        [HttpGet]
        public HallBookingCheckResponse CheckHallAvailability(HallBookingCheckRequest _req)
        {
            HallBookingCheckResponse _resp = new HallBookingCheckResponse();
            _resp.HallBooked = _hall.CheckHallAvailability(_req.HallId, _req.BookingDate, _req.SlotId);
            int confirmedCount = _resp.HallBooked.Where(b => b.Status.ToUpper() == "CONFIRMED").Count();
            if (confirmedCount > 0)
            {
                _resp.isAvailable = false;
            }
            else
            {
                _resp.isAvailable = true;
            }
            return _resp;
        }
    }
}
