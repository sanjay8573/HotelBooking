using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public  interface IDashboard
    {
        int getAvailableRoomCount();
        int getBookingTodaysCheckin();
        int numberOfGuestComingToday();
        
    }
}
