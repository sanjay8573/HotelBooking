using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
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
            return _context.Rooms.Where(r => r.BranchId == branchId && r.RoomTypeId==RoomTypeId).ToArray();
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
