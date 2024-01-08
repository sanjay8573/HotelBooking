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
                SPM tmpPM = _context.SpecialPrice.Where(r => r.RoomTypeId == SPMEntity.RoomTypeId && r.isActive == true).SingleOrDefault<SPM>();
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
            return _context.SpecialPrice.Where(i => i.BranchId == BranchId).ToArray();
        }
        private int UpdateSpecialPrice(SPM SPMEntity)
        {
            int rtnVal = 0;
            var tmpPM = _context.SpecialPrice.Find(SPMEntity.SpecialPriceManageId);
            if (tmpPM != null)
            {
                tmpPM.Title = SPMEntity.Title;
                tmpPM.DateRange = SPMEntity.DateRange;
                tmpPM.SUN = SPMEntity.SUN;
                tmpPM.MON = SPMEntity.MON;
                tmpPM.TUE = SPMEntity.TUE;
                tmpPM.WED = SPMEntity.WED;
                tmpPM.THUR = SPMEntity.THUR;
                tmpPM.FRI = SPMEntity.FRI;
                tmpPM.SAT =     SPMEntity.SAT;
                tmpPM.RoomTypeId =  SPMEntity.RoomTypeId;
                tmpPM.isActive =   SPMEntity.isActive;

                _context.SaveChanges();
                rtnVal = tmpPM.SpecialPriceManageId;
            }


            return rtnVal;
        }
    }
}
