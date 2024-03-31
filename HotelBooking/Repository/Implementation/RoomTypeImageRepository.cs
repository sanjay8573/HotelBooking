﻿using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Reatraurant;
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
            var tmpEntity = _context.ImageMaster.Find(imageId);
            RoomeTypeImages rtImg = new RoomeTypeImages();
            
            rtImg.BranchId = tmpEntity.BranchId;
            rtImg.RoomTypeImageId = tmpEntity.ImageId;
            rtImg.ImageName= tmpEntity.ImageName;
            rtImg.ImageData = tmpEntity.ImageData;
            rtImg.RoomTypeId = tmpEntity.RefId;
            rtImg.ImageContentType = tmpEntity.ImageContentType;
            rtImg.isActive = tmpEntity.isActive;
            return rtImg;

        }
        public RoomeTypeImages[] GetRoomTypeImages(int roomTypeId)
        {
            var rtimages = from a in _context.ImageMaster.Where(rt => rt.RefId == roomTypeId && rt.ImageTypeId==1)
                           select new RoomeTypeImages
                           {
                               RoomTypeImageId = a.ImageId,
                               RoomTypeId=a.RefId,
                               ImageName = a.ImageName,
                               ImageData = a.ImageData,
                               ImageContentType = a.ImageContentType,
                               BranchId = a.BranchId,
                               isActive = a.isActive

                           };
            return rtimages.ToArray<RoomeTypeImages>();

        }

        public bool SaveRoomTypeImageFile(RoomeTypeImages entityRoomeTypeImages)
        {
            bool rtnVal = false;
            if (entityRoomeTypeImages.RoomTypeImageId > 0)
            {
                ImageMaster tmpimg = _context.ImageMaster.Where(i => i.RefId == entityRoomeTypeImages.RoomTypeImageId && i.ImageTypeId == 1).SingleOrDefault();
                //var tmpAmnt = _context.RoomeTYpeImages.Find(entityRoomeTypeImages.RoomTypeImageId);
                if (tmpimg != null)
                {
                    //var tmpAmnt = _context.RoomeTYpeImages.Find(tmpimg.RefId);
                    tmpimg.ImageName = entityRoomeTypeImages.ImageName;
                    tmpimg.ImageContentType = entityRoomeTypeImages.ImageContentType;
                    tmpimg.ImageData = entityRoomeTypeImages.ImageData;
                    _context.SaveChanges();
                    rtnVal = true;
                }

            }
            else
            {
                try
                {
                    ImageMaster tmpimg = new ImageMaster();
                    tmpimg.ImageName = entityRoomeTypeImages.ImageName;
                    tmpimg.ImageContentType = entityRoomeTypeImages.ImageContentType;
                    tmpimg.ImageData = entityRoomeTypeImages.ImageData;
                    tmpimg.ImageTypeId = 1;
                    tmpimg.RefId= entityRoomeTypeImages.RoomTypeId;
                    tmpimg.BranchId= entityRoomeTypeImages.BranchId;
                    _context.ImageMaster.Add(tmpimg);
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
                ImageMaster t= _context.ImageMaster.Where(i=>i.RefId==imageId && i.ImageTypeId==1).FirstOrDefault();
                if (t!=null)
                {
                    t.isDeleted = true;
                    _context.SaveChanges();
                }
                
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
