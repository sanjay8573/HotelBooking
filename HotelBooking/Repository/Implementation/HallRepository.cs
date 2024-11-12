using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Hall;
using HotelBooking.Repository.Interface;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class HallRepository : IHall
    {
        private readonly CompanyContext _context;

        public HallRepository()
        {
            _context = new CompanyContext();

        }
       
        public IEnumerable<HALL_PARTY_MASTER> GetAllHall(int BranchId)
        {
            return _context.Hall.Where(b => b.BranchId == BranchId).ToArray();
        }

        public HALL_PARTY_MASTER GetHall(int HallId)
        {
            return _context.Hall.Where(b => b.HALLID == HallId).SingleOrDefault();
        }

        public IEnumerable<Hall_Party_Time_Slot> GetHallTimings(int BranchId)
        {
            return _context.HallTimeSlot.Where(b=>b.BranchId== BranchId).ToArray();
        }

        public bool SaveHall(HALL_PARTY_MASTER hallEntity)
        {
            bool rtnVal = false;
            HALL_PARTY_MASTER tmpHall = _context.Hall.Find(hallEntity.HALLID);
            if (tmpHall != null)
            {
                tmpHall.HallName = hallEntity.HallName;
                tmpHall.HallShortName= hallEntity.HallShortName;
                tmpHall.Capasity= hallEntity.Capasity;
                tmpHall.FloorId= hallEntity.FloorId;
                tmpHall.FloorName= hallEntity.FloorName;
                tmpHall.isActive = hallEntity.isActive;
                rtnVal = true;
            }
            else
            {
                _context.Hall.Add(hallEntity);
                rtnVal = true;
            }
            _context.SaveChanges();
            return rtnVal;
        }
        public bool SaveHallBooking(HallBooking hallEntity)
        {
            bool rtnVal = false;
            HallBooking tmp = _context.HallBooking.Find(hallEntity.HallBookingId);
            if (tmp != null)
            {
                tmp.CustomerName= hallEntity.CustomerName;
                tmp.CustomerEmail= hallEntity.CustomerEmail;
                tmp.CustomerPhone= hallEntity.CustomerPhone;
                tmp.Number_Of_Person = hallEntity.Number_Of_Person;
                tmp.MenuId = hallEntity.MenuId;
                tmp.MenuName = hallEntity.MenuName;
                tmp.SlotId = hallEntity.SlotId;
                tmp.SlotName = hallEntity.SlotName;
                tmp.HallId = hallEntity.HallId;
                tmp.HallName = hallEntity.HallName;
                tmp.HallBookingDetails = hallEntity.HallBookingDetails;
                tmp.TotalAmount = hallEntity.TotalAmount;
                tmp.TotalTax = hallEntity.TotalTax;
                tmp.PayableAmount = hallEntity.PayableAmount;
                rtnVal = true;

            }
            else
            {
                _context.HallBooking.Add(hallEntity);
                rtnVal = true;
            }
            _context.SaveChanges();

            foreach(HallBookingCost hc in hallEntity.HallBookingCosting)
            {
                hc.HallBookingId = hallEntity.HallBookingId;
                rtnVal=SaveHallBookingCost(hc);
            }
            foreach(HallBookingPayment hp in hallEntity.HallBookingPayment)
            {
                hp.HallBookingId=hallEntity.HallBookingId;
                rtnVal = SaveHallBookingPayment(hp);
            }
            return rtnVal;
        }
        
        private bool DeleteAddMenuItems(HallBookingDetails hmdEntity)
        {
           bool  rtnVal = false;
            var tmp = _context.HallBookingDetails.Find(hmdEntity.HallBookingDetailId);
            if (tmp != null) {
            _context.HallBookingDetails.Remove(tmp);
            }
            _context.HallBookingDetails.Add(hmdEntity);
            _context.SaveChanges();
            return rtnVal;
        }
        
        public IEnumerable<HallBooking> GetHallBookings(int BranchId)
        {
            IEnumerable<HallBooking> hlb= _context.HallBooking.Where(b => b.BranchId == BranchId).ToArray();
           
            return hlb;
        }
        
        public IEnumerable<HallBookingCost> GetHallBookingCost(int BranchId, int hallBookingId)
        {
            return _context.HallBookingCost.Where(b=>b.HallBookingId == hallBookingId && b.BranchId== BranchId).ToArray();
        }

        public IEnumerable<HallBookingCost> GetHallBookingCostByHall(int BranchId, int hallId)
        {
            return _context.HallBookingCost.Where(b => b.HallId == hallId && b.BranchId == BranchId).ToArray();
        }
        
        public bool SaveHallBookingCost(HallBookingCost hallCostingEntity)
        {
            bool rtnVal = false;
            var tmp = _context.HallBookingCost.Find(hallCostingEntity.HallBookingCostId);
            if (tmp != null)
            {
                tmp.COST= hallCostingEntity.COST;
                tmp.DATE= hallCostingEntity.DATE;
                tmp.TAX= hallCostingEntity.TAX;
                tmp.TaxAmount = hallCostingEntity.TaxAmount;
                rtnVal = true;
            }
            else
            {
                _context.HallBookingCost.Add(hallCostingEntity);
                rtnVal = true;
            }
            
            _context.SaveChanges();
            return rtnVal;
        }
        
        public IEnumerable<HallBookingPayment> GetHallBookingPayment(int BranchId, int hallBookingId)
        {
            return _context.HallBookingPayment.Where(b => b.BranchId == BranchId && b.HallBookingId == hallBookingId).ToArray();
        }
        
        public bool SaveHallBookingPayment(HallBookingPayment hallPaymentEntity)
        {
            bool rtnVal = false;
            var tmp = _context.HallBookingPayment.Find(hallPaymentEntity.HallBookingPaymentId);
            if (tmp != null)
            {
                tmp.Amount = hallPaymentEntity.Amount;
                tmp.paymentDate= hallPaymentEntity.paymentDate;
              
                rtnVal = true;
            }
            else
            {
                _context.HallBookingPayment.Add(hallPaymentEntity);
                rtnVal = true;
            }

            _context.SaveChanges();
            return rtnVal;
        }

        public IEnumerable<HallBooking> CheckHallAvailability(int HallId, DateTime bookingDate, int slotId)
        {
            return _context.HallBooking.Where(h => h.HallId == HallId && h.BookingDate == bookingDate && h.SlotId == slotId).ToArray();
        }
    }
}