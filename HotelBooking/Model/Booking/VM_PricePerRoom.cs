using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.VMBooking
{
    public class VM_PricePerRoom
    {
        public IEnumerable<BookedRoom> EsitingRooms { get; set; }
        public int ENOR { get; set; }
        public int NOR { get; set; }
        public int todalNight { get; set; }

    }
}