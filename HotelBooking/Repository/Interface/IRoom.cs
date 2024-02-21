using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IRoom
    {
        bool SaveRoom(Room entityRoom);
        Room GetRoom(int roomId);
        IEnumerable<Room> GetRooms(int branchId);
        IEnumerable<Room> GetRoomsByRoomTypeId(int branchId, int RoomTypeId);
        bool DeleteRoom(int roomId);
    }
}
