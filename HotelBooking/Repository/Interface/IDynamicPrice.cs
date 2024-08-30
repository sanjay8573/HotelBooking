using HotelBooking.Model.DynamicPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IDynamicPrice
    {
        IEnumerable<DynamicPriceModel> GetAllDynamicPrice(int banchId);
        bool UpdateDynamicPrice(IEnumerable<DynamicPriceModel> dynamicPriceEntity);
    }
}
