using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    internal interface IHouseKeeping
    {
        IEnumerable<HouseKeepingData> GetAll(int BramchId,int roomId);
        bool addHouseKeeping(HouseKeepingData houseKeepingEntity);
        bool DeleteHouseKeeping(int houseKeepingid);
    }
}
