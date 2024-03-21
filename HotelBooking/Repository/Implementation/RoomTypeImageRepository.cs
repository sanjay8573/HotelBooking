using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class RoomTypeImageRepository : IRoomTypeImages
    {
        private readonly CompanyContext _context;

        //public RoomTypeImageRepository(CompanyContext context)
        //{
        //    _context = context;

        //}
        public RoomTypeImageRepository()
        {
            _context = new CompanyContext();

        }
        public RoomeTypeImages GetRoomTypeImageFiles(int imageId)
        {
            var tmpEntity = _context.RoomeTYpeImages.Find(imageId);
            return tmpEntity;

        }
        public RoomeTypeImages[] GetRoomTypeImages(int roomTypeId)
        {
            return _context.RoomeTYpeImages.Where(rt => rt.RoomTypeId == roomTypeId).ToArray();
        }

        public bool SaveRoomTypeImageFile(RoomeTypeImages entityRoomeTypeImages)
        {
            bool rtnVal = false;
            if (entityRoomeTypeImages.RoomTypeImageId > 0)
            {
                var tmpAmnt = _context.RoomeTYpeImages.Find(entityRoomeTypeImages.RoomTypeImageId);
                if (tmpAmnt != null)
                {
                    tmpAmnt.ImageName = entityRoomeTypeImages.ImageName;
                    tmpAmnt.ImageContentType = entityRoomeTypeImages.ImageContentType;
                    tmpAmnt.ImageData = entityRoomeTypeImages.ImageData;
                    _context.SaveChanges();
                    rtnVal = true;
                }

            }
            else
            {
                try
                {
                    _context.RoomeTYpeImages.Add(entityRoomeTypeImages);
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

        public bool DeleteRoomTypeImage(int imageId)
        {
            bool rtnal = false;
            try
            {
                _context.RoomeTYpeImages.Remove(GetRoomTypeImageFiles(imageId));
                _context.SaveChanges();
                rtnal = true;
            }
            catch (Exception)
            {

                rtnal = false;
            }
            return rtnal;
        }
    }
}
