using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class HouseKeepingRepository : IHouseKeeping
    {
        private readonly CompanyContext _context;

        public HouseKeepingRepository(CompanyContext cmp)
        {
            _context = cmp;
        }
        public HouseKeepingRepository()
        {
            _context = new CompanyContext();
        }
        public bool addHouseKeeping(HouseKeepingData houseKeepingEntity)
        {
            bool rtnVal = false;
            if (houseKeepingEntity.HouseKeepingId > 0)
            {
                rtnVal = UpdateAmenities(houseKeepingEntity);
            }
            else
            {
                try
                {
                    _context.TBLHouseKeeping.Add(houseKeepingEntity);
                    _context.SaveChanges();
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    return false;
                }

            }


            return rtnVal;
        }

        public bool UpdateAmenities(HouseKeepingData houseKeepingEntity)
        {
            bool rtnVal = false;

            try
            {
                var tmpHK = _context.TBLHouseKeeping.Find(houseKeepingEntity.HouseKeepingId);

                if (tmpHK != null)
                {
                    tmpHK.HouseKeepingStatus = houseKeepingEntity.HouseKeepingStatus;

                }
                rtnVal = true;
            }
            catch (Exception ex)
            {

                rtnVal = false;
            }
           


            return rtnVal;
        }

        public bool DeleteHouseKeeping(int houseKeepingid)
        {
            bool rtnVal=false;
            try
            {
                HouseKeepingData houseKeeping = _context.TBLHouseKeeping.Find(houseKeepingid);
                if (houseKeeping != null)
                {
                    _context.TBLHouseKeeping.Remove(houseKeeping);
                    _context.SaveChanges();
                    rtnVal = true;

                }
            }

            catch (Exception)
            {

                rtnVal = false;
            }

            return rtnVal;


;        }

        public IEnumerable<HouseKeepingData> GetAll(int BramchId, int roomId)
        {
            return _context.TBLHouseKeeping.Where(h => h.BramchId == BramchId && h.RoomId == roomId).ToArray();
        }
    }
}