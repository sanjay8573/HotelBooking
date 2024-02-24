using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class GuestRepository : IGuests
    {
        private readonly CompanyContext _context;
        public GuestRepository()
        {
            _context = new CompanyContext();

        }
        
        public bool AddGuest(Guests guestEntity)
        {
            bool rtnVal = false;
            if (guestEntity.GuestId > 0)
            {
                var tmpAmnt = _context.Guests.Find(guestEntity.GuestId);
                if (tmpAmnt != null)
                {
                    try
                    {
                        tmpAmnt.Name = guestEntity.Name;
                        tmpAmnt.Address = guestEntity.Address;
                        tmpAmnt.city = guestEntity.city;
                        tmpAmnt.pincode = guestEntity.pincode;
                        tmpAmnt.country = guestEntity.country;
                        tmpAmnt.Phone = guestEntity.Phone;
                        tmpAmnt.email = guestEntity.email;
                        tmpAmnt.isActive = guestEntity.isActive;
                        _context.SaveChanges();
                        rtnVal = true;
                    }
                    catch (Exception)
                    {

                        rtnVal = false;
                    }
                   
                }
                


               
            }
            else
            {
                try
                {
                    _context.Guests.Add(guestEntity);
                    _context.SaveChanges();
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    rtnVal = false;
                }

            }
            return rtnVal;
        }

        public Guests GetGuest(int guestId)
        {
            return _context.Guests.Find(guestId);
        }
        public void DeleteGuest(int guestId)
        {
            try
            {
                var tmpGuest = _context.Guests.Find(guestId);
                if (tmpGuest != null)
                {
                    tmpGuest.isDeleted = true;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Guests> GetAllGuest(int BranchId)
        {
            return _context.Guests.Where(i => i.BranchId == BranchId && i.isDeleted == false).ToArray();
        }

        public bool SetAsVip(int guestId)
        {
            bool rtnVal = false;
            try
            {
                var tmpGuest = _context.Guests.Find(guestId);
                if (tmpGuest != null)
                {
                    tmpGuest.isVIP = true;
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
    }
}