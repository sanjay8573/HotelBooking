using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IBooking
    {
        IEnumerable<Booking> GetAllBooking(int BranchId);
        Booking GetBooking(int BookingId);
        bool AddBooking(BookingRequest bookingEntity);

      
        void DeleteBooking(int bookingid);
        bool AddAdditionalNight(BookingCost bookingCostEntity);
        bool AddAllNights(BookingCost[] bookingCostEntity);
        void DeleteNights(int bookingid);
        void DeleteNight(int bookingCostId);
        IEnumerable<BookingCost> GetAllBookingsCost(int bookingid);
        IEnumerable<PriceResponse> GetPricesForNight(PriceRequest req);
        IEnumerable<Room> GetAllRooms(int roomTypeId);

        bool AddBookedRoom(BookedRoom BookedRoomEntity);
    }
}
