using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Model.Reatraurant
{
    [Table("Restaurant")]
    public class RestaurantModel
    {
        [Key]
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public string Area { get; set; }
        public bool isBarAvailable { get; set; }
        public bool isBreakfastServe { get; set; }
        public string lastOrderAcceptedTill { get; set; }
        public string OpeningTime { get;set; }
        public string ClosingTime { get; set; }
        public int NoOfTable { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        [NotMapped]
        public List<RestaurantTables> AvailableTables { get; set; }
        public RestaurantModel()
        {
            AvailableTables = new List<RestaurantTables>();
            ImageData = new List<ImageMaster>();
        }
        [NotMapped]
        public List<ImageMaster> ImageData { get; set; }

    }

    [Table("RestaurantTables")]
    public class RestaurantTables
    {
        [Key]
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public int NoOfSeat { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public bool isOccupied { get; set; }
        [NotMapped]
        public bool Status { get; set; }

    }
    [Table("RestaurantRoomService")]
    public class RestaurantRoomService
    {
        [Key]
        public int RoomServiceId { get; set; }
        public int RestaurantId { get; set; }
        public int RoomId { get; set; }
        public string  RoomNumber { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public bool isOrdered { get; set; }


    }

    /// <summary>
    /// For RoomType image =1, for Restaurant image=2,
    /// Restaurant Menu items Image=3
    /// Store item Image=4
    /// 
    /// </summary>
    [Table("ImageMaster")]
    public class ImageMaster
    {
        [Key]
        public int ImageId { get; set; }
        public int ImageTypeId { get; set; }
        public int RefId { get; set; }
        public string ImageName { get; set; }
        public string ImageContentType { get; set; }
        public byte[] ImageData { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

        
    }


}