using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Room_Type_images")]
    public class RoomeTypeImages
    {
        [Key]
       public int RoomTypeImageId { get; set; }
        public int RoomTypeId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ImageContentType { get; set; }
        public byte [] ImageData { get; set; }
        public  int BranchId { get; set; }
    }
    public class FileViewModel
    {
        public string ImageName { get; set; }
        public int RoomTyepId { get; set; }
        public string ImagePath { get; set; }
        public int BranchId { get; set; }
        //public IFormFile roomTypeImage { get; set; }
        //public List<IFormFile> Files { get; set; }
    }
    public class ReturnData
    {
        public string Name { get; set; }
        public int RoomTyepId { get; set; }
        public string ImageBase64 { get; set; }
    }
}
