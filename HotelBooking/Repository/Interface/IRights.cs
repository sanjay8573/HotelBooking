using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IRights
    {

        IEnumerable<Rights> GetAllRights();

        IEnumerable<Rights> GetAllRightsByModuleId(int mId);

        Rights GetRightsById(int rightId);
        int AddRights(Rights rightsEntity);
        int UpdateRights(Rights rightsEntity);
        void DeleteRights(int rightId);
    }
}
