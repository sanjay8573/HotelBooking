using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{

    public class AmenitiesRepository : IAmenities
    {
        private readonly CompanyContext _context;

        public AmenitiesRepository() {
            _context = new CompanyContext();

        }

        public IEnumerable<Amenities> GetAmenities(int BranchId)
        {
            return _context.Amenities.Where(i=>i.BranchId==BranchId && i.isDeleted==false).ToArray();
        }
        public bool AddAmenities(Amenities AmenitiesEntity)
        {
            bool rtnVal = false;
            if(AmenitiesEntity.AmenitiesId>0)
            {
                int  rtnVal1=UpdateAmenities(AmenitiesEntity);
                if(rtnVal1>0)
                {
                    rtnVal = true;
                }

            }
            else
            {
                try
                {
                    _context.Amenities.Add(AmenitiesEntity);
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
        public int UpdateAmenities(Amenities AmenitiesEntity)
        {
            int rtnVal = 0;
            var tmpAmnt = _context.Amenities.Find(AmenitiesEntity.AmenitiesId);
            if(tmpAmnt!= null) {
                tmpAmnt.Title = AmenitiesEntity.Title;
                tmpAmnt.Description = AmenitiesEntity.Description;
                tmpAmnt.Code = AmenitiesEntity.Code;
                tmpAmnt.IsActive = AmenitiesEntity.IsActive;
                if (AmenitiesEntity.imageData.Length > 0)
                {
                    tmpAmnt.imageData = AmenitiesEntity.imageData;
                }
                _context.SaveChanges();
                rtnVal = tmpAmnt.AmenitiesId;
            }


            return rtnVal;
        }
        public void DeleteAmenities(int Amenitiesid)
        {
            try
            {
                var AmenitiesEntity = _context.Amenities.Find(Amenitiesid);
                if (AmenitiesEntity != null)
                {
                    AmenitiesEntity.isDeleted= true;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
