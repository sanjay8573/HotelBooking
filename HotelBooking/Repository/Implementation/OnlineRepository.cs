using HotelBooking.Model;
using HotelBooking.Model.onlineAPI;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class OnlineRepository : IOnline
    {
        private readonly IBranch _br;
        private readonly IRoomType _roomType;
        private readonly IImage _roomTypeImages;
        private readonly IPriceManager _priceManager;
        private readonly ICurrency _curr;
        private readonly IRoom _room;
        private readonly IFloor _floor;
        private readonly IBooking _booking;
        private readonly IAmenities _amenities;

        public string ErrorCode { get; set; } = "000";
        public string ErrorDescription { get; set; } = "No Error";

        public OnlineRepository() {
            _br = new BranchRepository();
            _roomType = new RoomTypeRepository();
            _roomTypeImages = new ImageRepository();
            _priceManager = new PriceManageRepository();
            _curr= new CurrencyRepository();
            _room = new RoomRepository();
            _floor= new FloorRepository();
            _booking= new BookingRepository();
            _amenities = new AmenitiesRepository();

        }
        public RoomResponse getAvailableRooms(RoomRequest roomRequest)
        {
            RoomResponse roomResponse = new RoomResponse();
            Branch br= _br.GetBranchById(int.Parse(roomRequest.BranchID));
            roomResponse.HotelID = br.HotelId.ToString();
            roomResponse.HotelKey = br.HotelKey;
            roomResponse.HotelName = br.BranchName;
            roomResponse.HotelAddress = br.Address;
            roomResponse.HotelCity = br.City;
            roomResponse.HotelState=br.State;
            roomResponse.HotelCountry = br.Country;
            roomResponse.HotelCurrency = getHotelCurrency(int.Parse(roomRequest.BranchID));
            roomResponse.HotelNoOfRooms = getRoomCount(int.Parse(roomRequest.BranchID)).ToString();
            roomResponse.HotelNoOfFloors = getFloorCount(int.Parse(roomRequest.BranchID)).ToString(); 
            roomResponse.HotelCheckInTime = br.CheckInTime;
            roomResponse.HotelCheckOutTime =br.CheckOutTime;
            roomResponse.HotelPropInformation = br.PropInformation;
            roomResponse.HotelPropDescription =br.PropDescription;
            roomResponse.HotelDrivingDirections = br.DrivingDirections;
            roomResponse.BestTransportation = br.Transportation;
            roomResponse.FireSafetyCompliant = br.FireSafetyCompliant;
            roomResponse.CheckInInStructions = br.CheckInInStructions;
            roomResponse.SpecialCheckInInStructions =br.SpecialCheckInInStructions;
            roomResponse.HotelAmenities = new List<HotelAmenities>()
            {
                 new HotelAmenities
                 {
                      AmenitiesId=1,
                      Name="Secured parking"
                 },
                 new HotelAmenities
                 {
                     AmenitiesId=2,
                      Name="Uncovered parking"
                 }
            };

            roomResponse.RoomInfoAry = getRooms(br.Id,roomRequest.CheckInDate,roomRequest.CheckOutDate);
            roomResponse.Errors = new Error { Code = ErrorCode, Description = ErrorDescription };
            return roomResponse;
        }

        public bool CreateBooking(BookingRequest bookingEntity)
        {
            return _booking.AddBooking(bookingEntity);
        }
        public string CreateOnlineBooking(BookingRequest bookingEntity)
        {
            return _booking.AddOnlineBooking(bookingEntity);
        }
        private string getHotelCurrency(int BranchId)
        {
            string rtnVal = string.Empty;
            if (BranchId > 0)
            {
                try
                {
                    rtnVal = _curr.GetAvailableCurrency(BranchId).Where(c => c.isBusinessCurrency == true).SingleOrDefault().CurrencyName;
                }
                catch (Exception)
                {

                    ErrorCode = "001";
                    ErrorDescription = "Error in getHotelCurrency ";
                }
               
            }
            return rtnVal;
        }
        private int getRoomCount(int BranchId)
        {
            int rtnVal = 0;
            if (BranchId > 0)
            {
                try
                {
                    rtnVal = _room.GetRooms(BranchId).Where(r => r.isActive == true && r.isDeleted == false).Count();
                }
                catch (Exception)
                {

                    ErrorCode = "002";
                    ErrorDescription = "Error in getRoomCount ";
                }
                
            }
            return rtnVal;
        }
        private int getFloorCount(int BranchId)
        {
            int rtnVal = 0;
            if (BranchId > 0)
            {
                try
                {
                    rtnVal = _floor.GetAllFloors(BranchId).Where(r => r.isActive == true && r.isDeleted == false).Count();
                }
                catch (Exception)
                {

                    ErrorCode = "003";
                    ErrorDescription = "Error in getFloorCount ";
                }
                
            }
            return rtnVal;
        }

        private List<RoomInfo> getRooms(int branchId,string checkInDate,string checkOutDate)
        {
            List<RoomType> rt = new List<RoomType>();
            List<RoomInfo> ri = new List<RoomInfo>();
            rt =_roomType.GetRoomTypes(branchId).ToList();
            foreach(RoomType r in rt)
            {
                if(isRoomTypeAvailable(r.RoomTypeId,branchId,checkInDate,checkOutDate))
                {
                    List<PriceResponse> _priceRes= getRoomPrice(r.RoomTypeId, branchId, checkInDate, checkOutDate).ToList();
                    decimal rtAmount = _priceRes.Sum(r1 => r1.OfferPrice);
                    decimal baseAmount = _priceRes.Sum(r1 => r1.Amount);
                    decimal taxAmount = _priceRes.Sum(r1 => r1.TaxAmount);
                    decimal StrikethroughBaseAmount = baseAmount + (baseAmount*10/100);
                    string _rateCode = _priceRes.Select(c => c.CostId).First().ToString();
                    List<RoomImages> rtImg= getRoomTypeImages(r.RoomTypeId,branchId).ToList();
                    List<Amenities> _amenities = getRoomAmenities(branchId, r.Amenities).ToList();
                    RoomInfo rinfo = new RoomInfo
                    {
                        RoomType = r.RoomTypeId.ToString(),
                        RoomTypeDescription = r.Description,
                        RoomShortDescription = r.ShortDescription,
                        RoomLongDescription = r.LongDescription,
                        RoomRateAmount = double.Parse(rtAmount.ToString()),
                        RoomBaseAmount = double.Parse(baseAmount.ToString()),
                        RoomTaxAmount = double.Parse(taxAmount.ToString()),
                        RateDetails = _priceRes,
                        RoomStrikethroughBaseAmount = double.Parse(StrikethroughBaseAmount.ToString()),
                        RateCode= _rateCode,
                        RateDescripton= "1 Queen Bed",
                        SmokingPreference=null,
                        MaxOccupancy=r.HighOccupancy,
                        NonRefundable=true,
                        MealIncluded=false,
                        inclusion=r.inclusion,
                        CancellationPolicy=r.CancellationPolicy,
                        RoomImages= rtImg,
                        RoomAmenities= _amenities

                    };
                    ri.Add(rinfo);
                }
            }

           
            return ri;
        }
        private bool isRoomTypeAvailable(int roomTypeId,int branchId,string checkInDate, string checkOutDate)
        {
            Logger lr = new Logger();
            string inputData = "RoomTypeId" + roomTypeId.ToString() + "BranchId" + branchId.ToString() + "checkInDate" + checkInDate + "checkOutDate" + checkOutDate;
            lr.Log("isRoomTypeAvailable", "inputData" + inputData, DateTime.Now);
            bool rtnVal = false;
            int yy = int.Parse(checkInDate.Substring(6, 4));
            int dd = int.Parse(checkInDate.Substring(0, 2));
            int mm = int.Parse(checkInDate.Substring(3, 2));

            int yy1 = int.Parse(checkOutDate.Substring(6, 4));
            int dd1 = int.Parse(checkOutDate.Substring(0, 2));
            int mm1 = int.Parse(checkOutDate.Substring(3, 2));

            DateTime startDate = new DateTime(yy, mm, dd);
            DateTime endDate = new DateTime(yy1, mm1, dd1);
            
            //List<Booking> extBk= _booking.GetAllBooking(branchId).Where(b => b.RoomTypeId.Contains(roomTypeId.ToString()) && DateTime.Parse(b.CheckIn) >= startDate && DateTime.Parse(b.Checkout) <= endDate && b.BookingStatus != "COMPLETED" ).ToList();
            List<Booking> extBk = _booking.GetAllBooking(branchId).Where(b => b.RoomTypeId.Contains(roomTypeId.ToString()) && b.BookingStatus != "COMPLETED").ToList();
            var extBk1 = (from a in _booking.GetAllBooking(branchId).Where(b => b.RoomTypeId.Contains(roomTypeId.ToString()) && b.BookingStatus != "COMPLETED")
                          .AsEnumerable()
                         select (
                            DateTime.ParseExact(a.CheckIn, "dd-MM-yyyy",CultureInfo.InvariantCulture),
                            DateTime.ParseExact(a.Checkout, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                         )).Where(x => x.Item1 >= startDate && x.Item2 <= endDate); ;
                          
                                   

            int noOfRooms=_room.GetRooms(branchId).Where(r=>r.RoomTypeId==roomTypeId && r.isActive==true && r.isDeleted==false).Count();
            if(noOfRooms > extBk1.Count() )
            {
                rtnVal = true;
            }
            return rtnVal;
        }
        private IEnumerable<PriceResponse> getRoomPrice(int roomTypeId, int branchId, string checkInDate, string checkOutDate)
        {
            Logger lr = new Logger();
            string inputData = "RoomTypeId" + roomTypeId.ToString() + "BranchId" + branchId.ToString() + "checkInDate" + checkInDate + "checkOutDate" + checkOutDate;
            lr.Log("getRoomPrice", "inputData" + inputData, DateTime.Now);

            PriceRequest priceRequest = new PriceRequest();
            priceRequest.BranchId = branchId;
            priceRequest.CheckInDate = checkInDate;
            priceRequest.CheckOutDate = checkOutDate;
            priceRequest.roomTypeId = roomTypeId;
            IEnumerable<PriceResponse> pr = _booking.GetPricesForNight(priceRequest);

            return pr;
        }
        private IEnumerable<RoomImages> getRoomTypeImages(int roomTypeId,int branchId)
        {
            IEnumerable<ImageMaster> images=_roomTypeImages.GetImages(1,branchId,roomTypeId).ToArray();
            List<RoomImages> _rmImg = new List<RoomImages>();

            foreach (ImageMaster image in images)
            {
                _rmImg.Add(new RoomImages()
                {
                    imageId=image.ImageId,
                    ImageData=image.ImageData
                });
            }
            return _rmImg;
        }
        private IEnumerable<Amenities> getRoomAmenities(int branchid,string roomTypeIds)
        {
            return _amenities.GetAmenities(branchid).Where(a => roomTypeIds.Contains(a.AmenitiesId.ToString()));
        }
    }
}