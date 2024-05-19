using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IBookingSource
    {
        IEnumerable<BookingSource> GetAllBookingSource(int branchId);
        bool AddBookingSource(BookingSource bookingSource);
        bool DeleteBookingSource(int branchId,int bookingSourceId);
    }
}
