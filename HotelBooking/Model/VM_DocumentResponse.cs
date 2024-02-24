using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    public class VM_DocumentResponse
    {
        public int BookingId { get; set; }
        public int Pax { get; set; }
        public int Rooms { get; set; }
       public IPagedList<BookingDocuments> Documents { get; set; }
    }
}