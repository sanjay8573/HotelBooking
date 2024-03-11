using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IBookedRoom
    {
        IEnumerable<BookedRoom> GetAllBookedRoom(int BranchId);
        IEnumerable<BookedRoom> GetAllBookedRoomByBookingId(int BranchId, int BookingId);
        bool AddBookedRoom(BookedRoom BookedRoomEntity);
        bool CheckOutRoom(CheckOutRequest coReq);

    }
}
