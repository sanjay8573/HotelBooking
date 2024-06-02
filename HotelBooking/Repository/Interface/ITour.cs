using HotelBooking.Model.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface ITour
    {
        IEnumerable<Tour> getAllTourBooking(int branchId);
        TourBookingResponse CreatingTourBooking(TourBookingRequest request);
        TourBookingRequest getTourBooking(int branchId, int TourId);
        bool CancellTourBooking(int tourId);
        bool MarkAsFullPayment(int tourId);
    }
}
