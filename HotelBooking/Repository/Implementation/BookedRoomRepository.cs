using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class BookedRoomRepository : IBookedRoom
    {
        private readonly CompanyContext _context;

        public BookedRoomRepository(CompanyContext context)
        {
            _context = context;

        }
        public BookedRoomRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddBookedRoom(BookedRoom BookedRoomEntity)
        {
            bool rtnVal = false;
            try
            {
                _context.BookedRoom.Add(BookedRoomEntity);
                _context.SaveChanges();
                rtnVal = true;
            }
            catch (Exception ex)
            {

                rtnVal = false;
            }
            return rtnVal;
        }

        public IEnumerable<BookedRoom> GetAllBookedRoom(int BranchId)
        {
           return _context.BookedRoom.Where(b=>b.BranchId == BranchId && b.isCheckout==false).ToArray();
        }
        public bool CheckOutRoom(CheckOutRequest coReq)
        {
            bool rtnVal;
            try
            {
                //_context.BookedRoom.Where(b => b.BookingId == coReq.BookingId && b.BookedRoomId == coReq.BookedRoomId && b.BranchId==coReq.BranchId)
                                                  //  .ExecuteUpdate(e => e.SetProperty(u => u.isCheckout , true));
                _context.SaveChanges();

                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;


        }
    }
}
