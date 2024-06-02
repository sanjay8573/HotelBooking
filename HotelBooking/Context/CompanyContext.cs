﻿
using HotelBooking.Model;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Model.Tour;
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

        public DbSet<HouseKeeping> HouseKeeping
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



    }
}
