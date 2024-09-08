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

        //public BookedRoomRepository(CompanyContext context)
        //{
        //    _context = context;

        //}
        public BookedRoomRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddBookedRoom(BookedRoom BookedRoomEntity)
        {
            bool rtnVal = false;
            try
            {
                BookedRoom bkedRoom = _context.BookedRoom.Find(BookedRoomEntity.BookedRoomId);
                if (bkedRoom != null) {
                    bkedRoom.CheckIn = BookedRoomEntity.CheckIn;
                    bkedRoom.CheckOut = BookedRoomEntity.CheckOut;
                    bkedRoom.FloorId= BookedRoomEntity.FloorId;
                    bkedRoom.FloorName = BookedRoomEntity.FloorName;
                    bkedRoom.RoomNumber = BookedRoomEntity.RoomNumber;
                    bkedRoom.RoomTypeId = BookedRoomEntity.RoomTypeId;
                    bkedRoom.RoomTypeName  = BookedRoomEntity.RoomTypeName;

                }
                else
                {
                    _context.BookedRoom.Add(BookedRoomEntity);
                }
               
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

        public IEnumerable<BookedRoom> GetAllBookedRoomByBookingId(int BranchId,int BookingId)
        {
            return _context.BookedRoom.Where(b => b.BranchId == BranchId && b.BookingId== BookingId).ToArray();
        }
        public bool CheckOutRoom(CheckOutRequest coReq)
        {
            bool rtnVal;
            try
            {
                BookedRoom BKD = _context.BookedRoom.Where(b => b.BookingId == coReq.BookingId && b.BookedRoomId == coReq.BookedRoomId && b.BranchId == coReq.BranchId).SingleOrDefault();
                BKD.isCheckout = true;
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
