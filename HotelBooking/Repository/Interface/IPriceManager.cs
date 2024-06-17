using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IPriceManager
    {
        IEnumerable<PriceManager> GetPrices(int BranchId);
        string AddPrice(PriceManager PMEntity);

        int UpdatePrice(PriceManager PMEntity);
        void Deleteprice(int pmid);
      
       
    }
}
