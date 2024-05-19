using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    public class RoomTypeImageController : ApiController
    {
        private readonly IRoomTypeImages _rti;
        public RoomTypeImageController(IRoomTypeImages rti)
        {
            _rti = rti;
        }
        public RoomTypeImageController()
        {
            _rti = new RoomTypeImageRepository();
        }
        [HttpPost]
        [Route("api/RoomTypeImage/SaveRoomTypeImage/{BranchId}")]
        public ReturnData SaveRoomTypeImage(FileViewModel fileviewmodel)
        {
            RoomeTypeImages rti = new RoomeTypeImages();
            ReturnData returndata = new ReturnData();
            
               
                        //create a AppFile mdoel and save the image into database.

                        rti.ImageName = fileviewmodel.ImageName;
                        rti.RoomTypeId = fileviewmodel.RoomTyepId;
                        rti.ImageData = fileviewmodel.ImageData;
                        //rti.ImageContentType = fileviewmodel.roomTypeImage.ContentType;
                        rti.BranchId = fileviewmodel.BranchId;
                        rti.isActive = true;


                        _rti.SaveRoomTypeImageFile(rti);

            returndata.RoomTyepId= fileviewmodel.RoomTyepId;
            returndata.ImageBase64= fileviewmodel.ImageData.ToString();

            return returndata;
        }
        [HttpPost]
        [Route("api/RoomTypeImages/GetRoomTypeImages/{BranchId}/{roomTypeId}")]
        public IEnumerable<RoomeTypeImages> GetRoomTypeImages(int roomTypeId)
        {
            return _rti.GetRoomTypeImages(roomTypeId);
        }
        [HttpPost]
        [Route("api/RoomTypeImage/DeleteRoomTypeImage/{BranchId}/{roomTypeImageId}")]
        public HttpResponseMessage DeleteRoomTypeImage(int roomTypeImageId)
        {
            HttpResponseMessage rtn= new HttpResponseMessage(System.Net.HttpStatusCode.NotFound); ;
            bool k= _rti.DeleteRoomTypeImage(roomTypeImageId);
            if (k) {
                rtn= new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            return rtn;
        }
    }
}
