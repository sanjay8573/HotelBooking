using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace HotelBooking.Model
{
    public class VM_BookingDetails
    {

        public VM_BookingDetails() 
        {

         BookedNiths= new List<BookingCost>().AsEnumerable<BookingCost>();
         BookingPayments= new List<BookingPayments>().AsEnumerable<BookingPayments>();
        }

        public int BookingId { get; set; }
        public int BranchId { get; set; }
        public string BookingRef { get; set; }
        public string BookingDate { get; set; }
        public int BookingTypeid { get; set; }
        public string BookingTypeName { get; set; }
        public string MealPlan { get; set; }


        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Room { get; set;}
        public string RoomTypeId { get; set; }
        public int NoOfRooms { get; set; }
        public string BookingStatus { get; set; }
        public string PaymnentStatus { get; set; }
        public string  Audlts { get; set; }
        public string Child { get; set; }
        public string  Nights { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        
        public string CompanyName { get; set;}
        public string CompanyAddress { get; set;}
        public string CompanyCity { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyPhone { get; set;}
        public string CompanyEmail { get; set;}
        
        public IEnumerable<BookingCost> BookedNiths { get; set; }

        public IEnumerable<BookingPayments> BookingPayments { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Amountpaid { get; set; }
        public decimal AmountPending { get; set; }

        ///Guest Details
        ///
        public string  GuestAddress { get; set; }
        public string  GuestCity { get; set; }
        public string Guestpincode { get; set; }
        public string Guestcountry { get; set; }
        public string GuestPhone { get; set; }
        public string Guestemail { get; set; }


    }
}