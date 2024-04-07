using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    public class AvailabilityCalendar
    {
        public int Sr { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string rendering { get; set; }
        public string backgroundColor { get; set; }
        public string textColor { get; set; }
        public string className { get; set; }
        
        public DateTime AvalDate { get; set; }
        public int AvailableRooms { get; set; }
        public int BookedRooms { get; set; }
    }
}