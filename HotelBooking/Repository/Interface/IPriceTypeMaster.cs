using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IPriceTypeMaster
    {
        IEnumerable<PriceType> GetPriceTypes(int BranchId);
    }
}
