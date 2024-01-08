using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IBookedRoom
    {
        IEnumerable<BookedRoom> GetAllBookedRoom(int BranchId);
        bool AddBookedRoom(BookedRoom BookedRoomEntity);
        bool CheckOutRoom(CheckOutRequest coReq);

    }
}
