using HotelBooking.Model;
using HotelBooking.Model.onlineAPI;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.onlineAPI
{
    public class LFApiController : ApiController
    {
        private readonly IOnline _online;
        public LFApiController() { 
        _online= new OnlineRepository();
        }

        [Route("api/online/GetAvailableRooms")]
        [HttpPost]
        public RoomResponse GetAvailableRooms(RoomRequest roomRequest)
        {
            RoomResponse rr= _online.getAvailableRooms(roomRequest);
            return rr;
        }

        [HttpPost]
        [Route("api/online/CreateBooking")]
        public bool CreateBooking(BookingRequest bookingEntity)
        {
            return _online.CreateBooking(bookingEntity);
        }

    }
}
