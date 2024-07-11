using HotelBooking.Model.onlineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.EditHotel
{
    public class VM_EditHotel
    {
        public VM_EditHotel() {
            GeneralInformation= new VM_GeneralInfo();
            HotelDetails= new VM_HotelDetails();
            HotelAmenities = new List<VM_Amenities>();
            ContactInformation= new List<VM_ContactDetails>();
            WebSiteConfiguration= new VM_WebConfiguration();
            HotelImages= new List<VM_HotelImages>();

        }
        public VM_GeneralInfo GeneralInformation { get; set; }
        public VM_HotelDetails HotelDetails { get; set; }
        public List<VM_Amenities> HotelAmenities { get; set; }
        public List<VM_ContactDetails> ContactInformation { get; set; }
        public VM_WebConfiguration WebSiteConfiguration { get; set; }
        public List<VM_HotelImages> HotelImages { get; set; }
    }
}