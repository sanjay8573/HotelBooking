using HotelBooking.Model;
using HotelBooking.Model.Tour;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IBooking
    {
        IEnumerable<Booking> GetAllBooking(int BranchId);
        Booking GetBooking(int BookingId);
        bool AddBooking(BookingRequest bookingEntity);
        string AddOnlineBooking(BookingRequest bookingRequestEntity);


        void DeleteBooking(int bookingid);
        bool AddAdditionalNight(BookingCost bookingCostEntity);
        bool AddAllNights(BookingCost[] bookingCostEntity);
        void DeleteNights(int bookingid);
        void DeleteNight(int bookingCostId);
        IEnumerable<BookingCost> GetAllBookingsCost(int bookingid);
        IEnumerable<PriceResponse> GetPricesForNight(PriceRequest req);
        IEnumerable<Room> GetAllRooms(int roomTypeId);

        bool AddBookedRoom(BookedRoom BookedRoomEntity);

        ///For Booking Payments
        ///
        bool AddBookingPayment(BookingPayments bkpEntity);
        IEnumerable<BookingPayments> GetAllBookingPayments(int BranchId,int BookingId);
        BookingPayments GetBookingPayment(int BookingPaymentId);
        void DeleteBookingPayment(int BookingPaymentId);
        VM_BookingDetails GetBookingDetails(int BranchId, int BookingId);
        bool UpdateBookingStatus(int BranchId, int Bookingid, string bookingStatus);
        bool UpdatePaymentStatus(int BranchId, int Bookingid, string paymentStatus);
        DashBoardData DashboardData(int BranchId);
        IEnumerable<DashBoardData> CalendarData(int BranchId);
        IEnumerable<Tour> GetAllTourBooking(int BranchId);
    }
}
