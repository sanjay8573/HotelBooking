using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    public class AllocateRoomResponse
    {
        public IEnumerable<BookedRoom> BookedRooms { get; set; }
        public IEnumerable<Room> AvailableRooms { get; set; }
    }
}