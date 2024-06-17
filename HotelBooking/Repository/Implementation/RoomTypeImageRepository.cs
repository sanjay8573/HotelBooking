using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
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
            List<ImageMaster> rtimages =_context.ImageMaster.Where(rt => rt.RefId == roomTypeId && rt.ImageTypeId == 1 && rt.isDeleted==false).ToList();
            List<RoomeTypeImages> rtImages = new List<RoomeTypeImages>();    
            
            foreach (var image in rtimages)
            {
                rtImages.Add(new RoomeTypeImages()
                {
                    RoomTypeImageId = image.ImageId,
                    ImageName=image.ImageName,
                    RoomTypeId = image.RefId,
                    ImageContentType = image.ImageContentType,
                    ImageData=image.ImageData,
                    isActive=image.isActive
                }
                );
            }
            return rtImages.ToArray<RoomeTypeImages>();

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
                    tmpimg.isActive = true;
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
                ImageMaster t= _context.ImageMaster.Where(i=>i.ImageId==imageId && i.ImageTypeId==1).FirstOrDefault();
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
        public bool inActiveRoomTypeImage(int imageId,string act)
        {
            bool rtnal = false;
            try
            {
                ImageMaster t = _context.ImageMaster.Where(i => i.ImageId == imageId && i.ImageTypeId == 1).FirstOrDefault();
                if (t != null)
                {
                    if(act=="A")
                    {
                        t.isActive = true;
                    }
                    else
                    {
                        t.isActive = false;
                    }
                    
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
