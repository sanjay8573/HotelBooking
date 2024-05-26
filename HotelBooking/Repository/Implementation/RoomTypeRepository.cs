
using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class RoomTypeRepository : IRoomType
    {
        private readonly CompanyContext _context;
        //public RoomTypeRepository(CompanyContext context)
        //{
        //    _context = context;

        //}
        public RoomTypeRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddRoomType(RoomType RoomTypeEntity)
        {
            bool rtnVal = false;
            if (RoomTypeEntity.RoomTypeId > 0)
            {
                int rtnVal1 = UpdateRoomType(RoomTypeEntity);
                if (rtnVal1 > 0)
                {
                    rtnVal = true;
                }

            }
            else
            {
                try
                {
                    _context.RoomType.Add(RoomTypeEntity);
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

        public void DeleteRoomType(Int32 RoomTypeid)
        {
            try
            {
                var rtEntity = _context.RoomType.Find(RoomTypeid);
                if (rtEntity != null)
                {
                    rtEntity.isDeleted = true;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<RoomType> GetRoomTypes(Int32 BranchId)
        {
            return _context.RoomType.Where(i => i.BranchId == BranchId && i.isDeleted==false).ToArray();
        }

        public int UpdateRoomType(RoomType RoomTypeEntity)
        {
            int rtnVal = 0;
            var tmpAmnt = _context.RoomType.Find(RoomTypeEntity.RoomTypeId);
            if (tmpAmnt != null)
            {
                tmpAmnt.Title = RoomTypeEntity.Title;
                tmpAmnt.Slug = RoomTypeEntity.Slug;
                tmpAmnt.ShortCode = RoomTypeEntity.ShortCode;
                tmpAmnt.Description = RoomTypeEntity.Description;
                tmpAmnt.ShortDescription = RoomTypeEntity.ShortDescription;
                tmpAmnt.LongDescription = RoomTypeEntity.LongDescription;
                tmpAmnt.CancellationPolicy = RoomTypeEntity.CancellationPolicy;
                tmpAmnt.inclusion = RoomTypeEntity.inclusion;
                tmpAmnt.BaseOccupancy = RoomTypeEntity.BaseOccupancy;
                tmpAmnt.HighOccupancy = RoomTypeEntity.HighOccupancy;
                tmpAmnt.ExtraBed = RoomTypeEntity.ExtraBed;
                tmpAmnt.KidOccupancy = RoomTypeEntity.KidOccupancy;
                tmpAmnt.Amenities = RoomTypeEntity.Amenities;
                tmpAmnt.BasePrice = RoomTypeEntity.BasePrice;
                tmpAmnt.AdditionalPersonPrice = RoomTypeEntity.AdditionalPersonPrice;
                tmpAmnt.BasePrice = RoomTypeEntity.BasePrice;
                tmpAmnt.ExtraBedPrice = RoomTypeEntity.ExtraBedPrice;
                tmpAmnt.BranchId = RoomTypeEntity.BranchId;
                tmpAmnt.IsExtraBed = RoomTypeEntity.IsExtraBed;
                tmpAmnt.isActive = RoomTypeEntity.isActive;
                tmpAmnt.isDeleted = RoomTypeEntity.isDeleted;

                _context.SaveChanges();
                rtnVal = tmpAmnt.RoomTypeId;
            }


            return rtnVal;
        }
    }
}
