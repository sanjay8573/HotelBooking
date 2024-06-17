using HotelBooking.Model;
namespace HotelBooking.Repository.Interface
{
    public interface IRoomTypeImages
    {
        bool SaveRoomTypeImageFile(RoomeTypeImages entityRoomeTypeImages);
        RoomeTypeImages GetRoomTypeImageFiles(int imageId);
        RoomeTypeImages[] GetRoomTypeImages(int roomTypeId);
        bool inActiveRoomTypeImage(int imageId,string act);
        bool DeleteRoomTypeImage(int imageId);
    }
}
