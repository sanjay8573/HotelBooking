using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class BookingSourceRepository : IBookingSource
    {
        private readonly CompanyContext _context;
        public BookingSourceRepository() { 
        _context = new CompanyContext();
        }

        public bool AddBookingSource(BookingSource bookingSource)
        {
            bool rtnVal = false;
            if (bookingSource != null)
            {
                BookingSource tmpBookingSource = _context.BookingSource.Find(bookingSource.BookingSourceId);
                if (tmpBookingSource != null)
                {
                    tmpBookingSource.Description = bookingSource.Description;
                    tmpBookingSource.Name= bookingSource.Name;
                    tmpBookingSource.Commission = bookingSource.Commission;
                    tmpBookingSource.CommissionType= bookingSource.CommissionType;
                    tmpBookingSource.isActive = bookingSource.isActive;
                    rtnVal = true;
                }
                else
                {
                    _context.BookingSource.Add(bookingSource);
                    rtnVal = true;
                }
                _context.SaveChanges();
            }
            return rtnVal;
        }

        public bool DeleteBookingSource(int branchId, int bookingSourceId)
        {
            bool rtnVal = false;
            BookingSource tmpBookingSource = _context.BookingSource.Find(bookingSourceId);
            if (tmpBookingSource != null)
            {
                tmpBookingSource.isDeleted = true;
                rtnVal=true; 
                _context.SaveChanges();
            }
                return rtnVal;
        }

        public IEnumerable<BookingSource> GetAllBookingSource(int branchId)
        {
           
           return _context.BookingSource.Where(b=>b.BranchId==branchId).ToArray();
        }
    }
}