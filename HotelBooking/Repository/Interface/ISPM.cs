using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface ISPM
    {
        IEnumerable<SPM> GetSpecialPrices(int BranchId);
        bool AddSpecialPrice(SPM SPMEntity);

        void DeleteSpecialPrice(int spmid);
    }
}
