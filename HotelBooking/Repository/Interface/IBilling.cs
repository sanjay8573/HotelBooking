using HotelBooking.Model.Reatraurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IBilling
    {
        BillingMaster getBilling(int restaurantId, int TableId);
        bool SaveBilling(BillingMaster billingMasterEntity);
        
    }
}
