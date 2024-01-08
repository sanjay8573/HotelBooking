using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IFloor
    {
        IEnumerable<Floor> GetAllFloors(int BranchId);
        bool AddFloor(Floor FloorEntity);

        int UpdateFloor(Floor FloorEntity);
        void DeleteFloor(int Floorid);
    }
}
