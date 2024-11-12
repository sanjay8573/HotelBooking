
using HotelBooking.Model;
using HotelBooking.Model.DynamicPrice;
using HotelBooking.Model.Hall;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Model.Review;
using HotelBooking.Model.SocialMedia;
using HotelBooking.Model.Tour;
using System;
using System.Data.Entity;

namespace HotelBooking.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("Name=HBConstring")
        {
           

        }
        
        public DbSet<Company> Company
        {
            get;
            set;
        }
        public DbSet<RegionalSettings> RegionalSettings
        {
            get;
            set;
        }

        public DbSet<TNC> TNC
        {
            get;
            set;
        }
        public DbSet<Branch> Branch
        {
            get;
            set;
        }
        public DbSet<HotelContacts> HotelContacts
        {
            get;
            set;
        }
        public DbSet<Amenities> Amenities
        {
            get;
            set;
        }
        public DbSet<RoomType> RoomType
        {
            get;
            set;
        }
        public DbSet<Floor> Floors
        {
            get;
            set;
        }
        public DbSet<PriceManager> PriceManager
        {
            get;
            set;
        }
        
        public DbSet<RoomeTypeImages> RoomeTYpeImages
        {
            get;
            set;
        }
        public DbSet<Room> Rooms
        {
            get;
            set;
        }
        public DbSet<PriceType> PriceType
        {
            get;
            set;
        }
        public DbSet<PaidServices> PaidServices
        {
            get;
            set;
        }

        public DbSet<Coupon> Coupon
        {
            get;
            set;
        }

        public DbSet<Booking> Booking
        {
            get;
            set;
        }
        public DbSet<BookingCost> BookingCost
        {
            get;
            set;
        }
        public DbSet<SPM> SpecialPrice
        {
            get;
            set;
        }

        public DbSet<BookedRoom> BookedRoom
        {
            get;
            set;
        }

        public DbSet<HouseKeepingData> TBLHouseKeeping
        {
            get;
            set;
        }

        public DbSet<BookingPayments> BookingPayments
        {
            get;
            set;
        }
        public DbSet<Guests> Guests
        {
            get;
            set;
        }
        public DbSet<BookingDocuments> BookingDocuments
        {
            get;
            set;
        }
        public DbSet<DocumentType> DocumentType
        {
            get;
            set;
        }
        public DbSet<DocType> DocType
        {
            get;
            set;
        }
        public DbSet<AvailableCurrency> AvailableCurrency
        {
            get;
            set;
        }
        public DbSet<CurrencyExchange> CurrencyExchange
        {
            get;
            set;
        }
        public DbSet<ExchangeTransaction> ExchangeTransaction
        {
            get;
            set;
        }

        public DbSet<RestaurantModel> Restaurant
        {
            get;
            set;
        }
        public DbSet<RestaurantTables> RestaurantTables
        {
            get;
            set;
        }
        public DbSet<ImageMaster> ImageMaster
        {
            get;
            set;
        }
        public DbSet<RestaurantMenu> RestaurantMenu
        {
            get;
            set;
        }
        public DbSet<RestaurantMenuHeading> RestaurantMenuHeading
        {
            get;
            set;
        }
        public DbSet<RestaurantMenuItem> RestaurantMenuItem
        {
            get;
            set;
        }
        public DbSet<RestaurantRoomService> RestaurantRoomService
        {
            get;
            set;
        }

        public DbSet<BillingMaster> BillingMaster
        {
            get;
            set;
        }
        public DbSet<BillingDetails> BillingDetails
        {
            get;
            set;
        }
        public DbSet<TaxMaster> TaxMaster
        {
            get;
            set;
        }
        public DbSet<TaxableItems> TaxableItems
        {
            get;
            set;
        }
        public DbSet<BookingSource> BookingSource
        {
            get;
            set;
        }
        public DbSet<Tour> Tours
        {
            get;
            set;
        }

        public DbSet<HotelBooking.Model.TimeZone.TimeZone> BranchTimeZone
        {
            get;
            set;
        }
        public DbSet<ReviewMaster> ReviewMaster
        {
            get;
            set;
        }
        public DbSet<ReviewText> ReviewText
        {
            get;
            set;
        }
        public DbSet<ReviewImages> ReviewImages
        {
            get;
            set;
        }
        public DbSet<DynamicPriceModel> DynamicPrice
        {
            get;
            set;
        }
        public DbSet<SocialMediaMaster> SocialMediaMaster
        {
            get;
            set;
        }
        public DbSet<SocialMediaConfiguration> SocialMediaConfiguration
        {
            get;
            set;
        }
        public DbSet<SocialMediaBroadCast> SocialMediaBroadCast
        {
            get;
            set;
        }
        public DbSet<BuffetMenuDetails> BuffetMenuDetails
        {
            get;
            set;
        }
        public DbSet<restaurantBuffetMenu> restaurantBuffetMenu
        {
            get;
            set;
        }
        public DbSet<BuffetMenuItem> BuffetMenuItem
        {
            get;
            set;
        }

        ////Hall
        ///
        public DbSet<HALL_PARTY_MASTER> Hall
        {
            get;
            set;
        }
        public DbSet<Hall_Party_Time_Slot> HallTimeSlot
        {
            get;
            set;
        }
        public DbSet<HallBooking> HallBooking
        {
            get;
            set;
        }
        public DbSet<HallBookingDetails> HallBookingDetails
        {
            get;
            set;
        }
        public DbSet<HallBookingCost> HallBookingCost
        {
            get;
            set;
        }
        public DbSet<HallBookingPayment> HallBookingPayment
        {
            get;
            set;
        }

    }
}
