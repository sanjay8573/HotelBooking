using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IRoomType
    {
        IEnumerable<RoomType> GetRoomTypes(int BranchId);
        bool AddRoomType(RoomType RoomTypeEntity);

        int UpdateRoomType(RoomType RoomTypeEntity);
        void DeleteRoomType(int RoomTypeid);
    }
}