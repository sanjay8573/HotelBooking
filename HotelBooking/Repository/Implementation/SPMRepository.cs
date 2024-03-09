using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace HotelBooking.Repository.Implementation
{
    public class SPMRepository : ISPM
    {
        private readonly CompanyContext _context;
        public SPMRepository(CompanyContext context)
        {
            _context = context;

        }
        public SPMRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddSpecialPrice(SPM SPMEntity)
        {
            bool rtnVal = false;
            if (SPMEntity.SpecialPriceManageId > 0)
            {
                int rtnVal1 = UpdateSpecialPrice(SPMEntity);
                if (rtnVal1 > 0)
                {
                    rtnVal = true;
                }

            }
            else
            {
                SPM tmpPM = _context.SpecialPrice.Where(r => r.RoomTypeId1 == SPMEntity.RoomTypeId1 && r.isActive1 == true).SingleOrDefault<SPM>();
                if (tmpPM != null && tmpPM.SpecialPriceManageId > 0)
                {
                    
                        int rtnVal1 = UpdateSpecialPrice(SPMEntity);
                        if (rtnVal1 > 0)
                        {
                            rtnVal = true;
                        }
                    
                }
                else
                {
                    try
                    {
                        _context.SpecialPrice.Add(SPMEntity);
                        _context.SaveChanges();
                        rtnVal = true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }

            }


            return rtnVal;
        }

        public void DeleteSpecialPrice(int spmid)
        {
            try
            {
                var spmEntity = _context.SpecialPrice.Find(spmid);
                if (spmEntity != null)
                {
                    _context.SpecialPrice.Remove(spmEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<SPM> GetSpecialPrices(int BranchId)
        {
            return _context.SpecialPrice.Where(i => i.BranchId1 == BranchId).ToArray();
        }
        private int UpdateSpecialPrice(SPM SPMEntity)
        {
            int rtnVal = 0;
            var tmpPM = _context.SpecialPrice.Find(SPMEntity.SpecialPriceManageId);
            if (tmpPM != null)
            {
                tmpPM.Title1 = SPMEntity.Title1;
                tmpPM.DateRangeFrom = SPMEntity.DateRangeFrom;
                tmpPM.DateRangeTo = SPMEntity.DateRangeTo;
                tmpPM.SUN1 = SPMEntity.SUN1;
                tmpPM.MON1 = SPMEntity.MON1;
                tmpPM.TUE1 = SPMEntity.TUE1;
                tmpPM.WED1 = SPMEntity.WED1;
                tmpPM.THUR1 = SPMEntity.THUR1;
                tmpPM.FRI1 = SPMEntity.FRI1;
                tmpPM.SAT1 =     SPMEntity.SAT1;
                tmpPM.RoomTypeId1 =  SPMEntity.RoomTypeId1;
                tmpPM.isActive1 =   SPMEntity.isActive1;

                _context.SaveChanges();
                rtnVal = tmpPM.SpecialPriceManageId;
            }


            return rtnVal;
        }
    }
}
