using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class PaidServicesRepository : IPaidServices
    {
        private readonly CompanyContext _context;

        public PaidServicesRepository(CompanyContext context)
        {
            _context = context;

        }
        public PaidServicesRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddPaidServices(PaidServices PaidServicesEntity)
        {
            bool rtnVal = false;
            if (PaidServicesEntity.PaidServiceId > 0)
            {
                var tmpEntity = _context.PaidServices.Find(PaidServicesEntity.PaidServiceId);
                if (tmpEntity != null)
                {
                    tmpEntity.RoomTypeId = PaidServicesEntity.RoomTypeId;
                    tmpEntity.PriceType = PaidServicesEntity.PriceType;
                    tmpEntity.PriceTypeId = PaidServicesEntity.PriceTypeId;
                    tmpEntity.Price = PaidServicesEntity.Price;
                    tmpEntity.Description = PaidServicesEntity.Description;
                    tmpEntity.isActive = PaidServicesEntity.isActive;
                    _context.SaveChanges();

                }

            }
            else
            {
                try
                {
                    _context.PaidServices.Add(PaidServicesEntity);
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

        public void DeletePaidService(int paidserviceid)
        {
            try
            {
                var tmpEntity = _context.PaidServices.Find(paidserviceid);
                if (tmpEntity != null)
                {
                    tmpEntity.isDeleted = true;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<PaidServices> GetPaidServices(int BranchId)
        {
            return _context.PaidServices.Where(b => b.BranchId == BranchId).ToArray();
        }

        public IEnumerable<PaidServices> GetPaidServicesByRoomType(int roomTypeId)
        {
            return _context.PaidServices.Where(b => b.RoomTypeId.Contains(roomTypeId.ToString())).ToArray();
        }
    }
}
