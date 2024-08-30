using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class RoomRepository : IRoom
    {
        private readonly CompanyContext _context;

        public RoomRepository(CompanyContext context)
        {
            _context = context;

        }
        public RoomRepository()
        {
            _context = new CompanyContext();

        }
        public bool DeleteRoom(int roomId)
        {
            bool rtnVal = false;
            try
            {
                var RoomEntity = _context.Rooms.Find(roomId);
                if (RoomEntity != null)
                {
                    RoomEntity.isDeleted = true;
                    _context.SaveChanges();
                     rtnVal = true;
                   
                }
            }
            catch (Exception ex)
            {

                rtnVal = false;
            }
            return rtnVal;
        }

        public Room GetRoom(int roomId)
        {
           
            return _context.Rooms.Where(r => r.RoomId == roomId).Single(); ;
             
        }

        public IEnumerable<Room> GetRooms(int branchId)
        {
            return _context.Rooms.Where(r=>r.BranchId== branchId).ToArray();  
        }

        public IEnumerable<Room> GetRoomsByRoomTypeId(int branchId,int RoomTypeId)
        {
            IEnumerable<BookedRoom>  lstBookedRoom = _context.BookedRoom.Where(r => r.RoomTypeId == RoomTypeId && r.isCheckout == false && r.BranchId== branchId).ToArray();
            IEnumerable<Room> rms= _context.Rooms.Where(r => r.BranchId == branchId && r.RoomTypeId == RoomTypeId).ToArray();
            IEnumerable<Room> rms1 = from r in rms
                                     where !(from bked in lstBookedRoom
                                             select bked.RoomId)
                                             .Contains(r.RoomId)
                                     select r;
            return rms1;
        }
        //public IEnumerable<Room> GetRoomsByRoomTypeIds(int branchId, string RoomTypeId,int BookingId)
        //{
        //    IEnumerable<BookedRoom> lstBookedRoom = _context.BookedRoom.Where(r => RoomTypeId.Contains( r.RoomTypeId.ToString()) && r.isCheckout == false && r.BranchId == branchId).ToArray();
        //    IEnumerable<Room> rms = _context.Rooms.Where(r => r.BranchId == branchId && RoomTypeId.Contains(r.RoomTypeId.ToString())).ToArray();
        //    IEnumerable<Room> rms1 = from r in rms
        //                             where !(from bked in lstBookedRoom
        //                                     select bked.RoomId)
        //                                     .Contains(r.RoomId)
        //                             select r;
        //    return rms1;
        //}

        public AllocateRoomResponse GetRoomsByRoomTypeIds(int branchId, string RoomTypeId, int BookingId)
        {
            if(!RoomTypeId.Contains(","))
            {
                RoomTypeId = RoomTypeId + ",";
            }
            List<int> intIds = new List<int>();
            string[] split = RoomTypeId.Split(',');
            foreach (string item in split)
            {
               
                if (!string.IsNullOrEmpty(item))
                {
                    string tmpVal = item.Replace("'","");
                    int val = 0;
                    if (int.TryParse(tmpVal, out val) == true)
                    {
                        intIds.Add(val);
                    }


                }
                
            }

            AllocateRoomResponse avl = new AllocateRoomResponse();
            IEnumerable<BookedRoom> lstBookedRoom = _context.BookedRoom.Where(r=>r.BranchId == branchId && intIds.Contains(r.RoomTypeId) && r.BookingId== BookingId).ToArray();
            IEnumerable<BookedRoom> lstAllocatedRoom = lstBookedRoom.Where(b => b.BookingId == BookingId).ToArray();
            IEnumerable<Room> rms = _context.Rooms.Where(r => r.BranchId == branchId && intIds.Contains(r.RoomTypeId)).ToArray();
            IEnumerable<Room> rms1 = from r in rms
                                     where !(from bked in lstBookedRoom
                                             select bked.RoomId)
                                             .Equals(r.RoomId)
                                     select r;
            avl.AvailableRooms = rms1;
            avl.BookedRooms = lstAllocatedRoom;
            return avl;
        }

        public bool SaveRoom(Room entityRoom)
        {
            bool rtnVal = false;
            if (entityRoom.RoomId > 0)
            {
                var tmpEntity = _context.Rooms.Find(entityRoom.RoomId);
                if (tmpEntity != null)
                {
                    tmpEntity.RoomTypeId = entityRoom.RoomTypeId;
                    tmpEntity.RoomTypeName = entityRoom.RoomTypeName;
                    tmpEntity.floor = entityRoom.floor;
                    tmpEntity.RoomNumber=entityRoom.RoomNumber;
                    tmpEntity.FloorName = entityRoom.FloorName;
                    tmpEntity.isActive = entityRoom.isActive;
                    _context.SaveChanges();
                    
                }

            }
            else
            {
                try
                {
                    _context.Rooms.Add(entityRoom);
                    _context.SaveChanges();
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    return false;
                }

            }


            return rtnVal;
        }
    }
}
