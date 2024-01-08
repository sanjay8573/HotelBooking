using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeImageController : ControllerBase
    {
        private readonly IRoomTypeImages _rti;
        public RoomTypeImageController(IRoomTypeImages rti)
        {
            _rti = rti;
        }
        [HttpPost("SaveRoomTypeImage/{BranchId}")]
        public ReturnData SaveRoomTypeImage([FromForm] FileViewModel fileviewmodel)
        {
            RoomeTypeImages rti = new RoomeTypeImages();
            ReturnData returndata = new ReturnData();
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileviewmodel.roomTypeImage.CopyTo(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        //create a AppFile mdoel and save the image into database.

                        rti.ImageName = fileviewmodel.ImageName;
                        rti.RoomTypeId = fileviewmodel.RoomTyepId;
                        rti.ImageData = memoryStream.ToArray();
                        rti.ImageContentType = fileviewmodel.roomTypeImage.ContentType;
                        rti.BranchId = fileviewmodel.BranchId;


                        _rti.SaveRoomTypeImageFile(rti);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }


                if (ModelState.IsValid)
                {
                    returndata.Name = rti.ImageName;
                    returndata.RoomTyepId = rti.RoomTypeId;
                    returndata.ImageBase64 = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(rti.ImageData));


                }




            }

            return returndata;
        }

        [HttpPost("GetRoomTypeImages/{BranchId}/{roomTypeId}")]
        public IEnumerable<RoomeTypeImages> GetRoomTypeImages(int roomTypeId)
        {
            return _rti.GetRoomTypeImages(roomTypeId);
        }

        [HttpPost("DeleteRoomTypeImage/{BranchId}/{roomTypeImageId}")]
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
