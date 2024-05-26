using HotelBooking.Model;
using HotelBooking.Model.onlineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public  interface IOnline
    {
        RoomResponse getAvailableRooms(RoomRequest roomRequest);
        bool CreateBooking(BookingRequest bookingEntity);
        string CreateOnlineBooking(BookingRequest bookingEntity);


    }
}
