using Org.BouncyCastle.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Branches")]
    public class Branch
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }

        public string State { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public string Fax  { get; set; }
        

        public string EmailID { get; set; }
       

        public string IATANo { get; set; }


        public string Bank1Name { get; set; }
        public string Bank1AcNumber { get; set; }

        public string Bank2Name { get; set; }
        public string Bank2AcNumber { get; set; }

        public string AltBankName { get; set; }
        public string AltBankAcNumber { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public Guid HotelId { get; set; }
        public string HotelKey  { get; set; }
        public string CheckInTime { get; set;}
        public string CheckOutTime { get; set;}
        public string PropInformation { get; set; }
        public string PropDescription { get; set; }
        public string DrivingDirections { get; set; }
        public string Transportation { get; set; }
        public string FireSafetyCompliant { get; set; }
        public string CheckInInStructions { get; set; }
        public string SpecialCheckInInStructions { get; set; }
        
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        
        public string ChainName { get; set; }
        public Int16 Buildyear { get; set; }
        public Byte StarRating { get; set; }
       
        public string Amenities { get; set; }
        public string NearestAirport { get; set; }
        public string WebsiteURL { get; set; }
        public bool PetsAllowed { get; set; }
        
        public string CheckinPolicy { get; set; }
        public bool CoupleFriendly { get; set; }
        public bool CheckinWithLocalIds { get; set; }
        public string WebsiteHeaderFile { get; set; }
        public string WebsiteFooterFile { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
       
        public int NoofFloors { get; set; }
        public byte [] LogoImage { get; set; }
        public Byte DecimalPlaces { get; set; }
        public string PanCard { get; set; }
        public string TaxNo1 { get; set; }
        public string TaxNo2 { get; set; }
        public int TimeZone { get; set; }
    }

    [Table("HotelContacts")]
    public class HotelContacts
    {
        [Key]
        public int HotelContactID { get; set; }
        public int HotelID { get; set; }
        public int BranchId { get; set; }
        public string ContactText { get; set; }
        public string ContactType { get; set; }
        public string Description { get; set; }
        public string ContactDetails { get; set; }
        public bool isManager { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }

    }
}
