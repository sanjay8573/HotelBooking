using HotelBooking.Model;
namespace HotelBooking.Repository.Interface
{
    public interface IRoomTypeImages
    {
        bool SaveRoomTypeImageFile(RoomeTypeImages entityRoomeTypeImages);
        RoomeTypeImages GetRoomTypeImageFiles(int imageId);
        RoomeTypeImages[] GetRoomTypeImages(int roomTypeId);

        bool DeleteRoomTypeImage(int imageId);
    }
}
