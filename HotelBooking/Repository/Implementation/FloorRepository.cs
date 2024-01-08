using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class FloorRepository : IFloor
    {
        private readonly CompanyContext _context;
        public FloorRepository(CompanyContext context)
        {
            _context = context;

        }
        public FloorRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddFloor(Floor FloorEntity)
        {
            bool rtnVal = false;
            if (FloorEntity.FloorId > 0)
            {
                int rtnVal1 = UpdateFloor(FloorEntity);
                if (rtnVal1 > 0)
                {
                    rtnVal = true;
                }

            }
            else
            {
                try
                {
                    _context.Floors.Add(FloorEntity);
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

        public void DeleteFloor(int Floorid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Floor> GetAllFloors(int BranchId)
        {
            return _context.Floors.Where(i => i.BranchId == BranchId).ToArray();
        }

        public int UpdateFloor(Floor FloorEntity)
        {
            int rtnVal = 0;
            var tmpflr = _context.Floors.Find(FloorEntity.FloorId);
            if (tmpflr != null)
            {
                tmpflr.FloorNumber = FloorEntity.FloorNumber;
                tmpflr.Description = FloorEntity.Description;
                tmpflr.isActive = FloorEntity.isActive;
                _context.SaveChanges();
                rtnVal = tmpflr.FloorId;
            }


            return rtnVal;
        }
    }
}
