using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IPaidServices
    {
        IEnumerable<PaidServices> GetPaidServices(int BranchId);
        bool AddPaidServices(PaidServices PaidServicesEntity);

        void DeletePaidService(int paidserviceid);
        IEnumerable<PaidServices> GetPaidServicesByRoomType(int roomTypeId);
    }
}
