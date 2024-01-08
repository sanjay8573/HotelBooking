using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class PriceManageRepository : IPriceManager
    {
        private readonly CompanyContext _context;
        public PriceManageRepository(CompanyContext context)
        {
            _context = context;

        }
        public PriceManageRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddPrice(PriceManager PMEntity)
        {
            bool rtnVal = false;
            if (PMEntity.PriceManageId > 0)
            {
                int rtnVal1 = UpdatePrice(PMEntity);
                if (rtnVal1 > 0)
                {
                    rtnVal = true;
                }

            }
            else
            {
                PriceManager tmpPM = _context.PriceManager.Where(r => r.RoomTypeId == PMEntity.RoomTypeId && r.isActive == true).SingleOrDefault<PriceManager>();
                if (tmpPM != null)
                {
                    int rtnVal1 = UpdatePrice(PMEntity);
                    if (rtnVal1 > 0)
                    {
                        rtnVal = true;
                    }
                }
                else
                {
                    try
                    {
                        _context.PriceManager.Add(PMEntity);
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

        public void Deleteprice(int pmid)
        {
            try
            {
                var pmEntity = _context.PriceManager.Find(pmid);
                if (pmEntity != null)
                {
                    _context.PriceManager.Remove(pmEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<PriceManager> GetPrices(int BranchId)
        {
            return _context.PriceManager.Where(i => i.BranchId == BranchId).ToArray();
        }

        public int UpdatePrice(PriceManager PMEntity)
        {
            int rtnVal = 0;
            var tmpPM = _context.PriceManager.Find(PMEntity.PriceManageId);
            if (tmpPM != null)
            {
                tmpPM.Title = PMEntity.Title;
                tmpPM.RoomTypeTitle = PMEntity.RoomTypeTitle;
                tmpPM.SUN = PMEntity.SUN;
                tmpPM.MON = PMEntity.MON;
                tmpPM.TUE = PMEntity.TUE;
                tmpPM.WED = PMEntity.WED;
                tmpPM.THUR = PMEntity.THUR;
                tmpPM.FRI = PMEntity.FRI;
                tmpPM.SAT = PMEntity.SAT;
                tmpPM.RoomTypeId = PMEntity.RoomTypeId;
                tmpPM.isActive = PMEntity.isActive;

                _context.SaveChanges();
                rtnVal = tmpPM.PriceManageId;
            }


            return rtnVal;
        }
    }
}
