using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.onlineAPI
{
    public class RoomResponse
    {
        public Error Errors { get; set; }
        public string HotelID { get; set; }
        public string HotelKey { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelCity { get; set; }

        public string HotelState { get; set; }
        public string HotelCountry { get; set; }
        public string HotelCurrency { get; set; }
        public string HotelNoOfRooms { get; set; }
        public string HotelNoOfFloors { get; set; }
        public string HotelCheckInTime { get; set; }

        public string HotelCheckOutTime { get; set; }
        public string HotelPropInformation { get; set; }
        public string HotelPropDescription { get; set; }
        public string HotelDrivingDirections { get; set; }

        public string BestTransportation { get; set; }
        public string FireSafetyCompliant { get; set; }
        public string CheckInInStructions { get; set; }
        public string SpecialCheckInInStructions { get; set; }
        public List<HotelImage> HotelImages { get; set; }
        public List<HotelAmenities> HotelAmenities { get; set; }
        public List<RoomInfo> RoomInfoAry { get; set; }






    }
    public class Error
    {
       
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class HotelImage
    {
        public string ImageId { get; set; }
        public string Image { get; set; }
    }
    public class HotelAmenities
    {
        public int AmenitiesId { get; set; }
        public string Name { get; set; }
    }
    public class RoomImages
    {
        public int imageId { get; set; }
        public byte[]  ImageData { get; set; }
    }
    public class BedType
    {
        public int BedTypeId { get; set; }
        public string BedTypeDescription { get; set; }
    }
    public class RoomSurcharges
    {
        public int SurchargeType { get; set; }
        public string SurchargeDescription { get; set; }
        public double SurchargeAmount { get; set; }

    }


    public class RoomInfo
    {
        public string RoomType { get; set; }
        public string RoomTypeDescription { get; set; }
        public string RoomShortDescription { get; set; }
        public string RoomLongDescription { get; set; }
        public double RoomRateAmount { get; set; }
        public double RoomBaseAmount { get; set; }
        public double RoomTaxAmount { get; set; }
        public double RoomStrikethroughBaseAmount { get; set; }
        public string RateCode { get; set; }
        public string RateDescripton { get; set; }
        public string SmokingPreference { get; set; }
        public List<PriceResponse> RateDetails { get; set; }
        public int MaxOccupancy { get; set; }
        public bool NonRefundable { get; set; }
        public string CancellationPolicy { get; set; }
        public bool MealIncluded { get; set; }

        public RoomSurcharges RoomSurchargesAry { get; set; }
        public List<RoomImages> RoomImages { get; set; }
        public List<Amenities> RoomAmenities { get; set; }
        public BedType BedTypesAry { get; set; }
    }
}