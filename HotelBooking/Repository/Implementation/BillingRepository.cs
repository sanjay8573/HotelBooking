using Antlr.Runtime;
using HotelBooking.Context;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class BillingRepository : IBilling
    {
        private CompanyContext _context;
        public BillingRepository() { 
            _context = new CompanyContext();
        }
        public BillingMaster getBilling(int restaurantId, int TableId)
        {
            BillingMaster BM= _context.BillingMaster.Where(b => b.RestaurantId == restaurantId && b.TableNo_RoomNumber == TableId).SingleOrDefault();
            BM.BillingDetails = getBillingDetails(BM.BillingId);
            return BM;
         }

        public bool SaveBilling(BillingMaster billingMasterEntity)
        {
            bool rtnVal = false;
            try
            {
                BillingMaster BM = _context.BillingMaster.Find(billingMasterEntity.BillingId);
                billingMasterEntity.ParkingTime = DateTime.Now;
                billingMasterEntity.ClosingTime = DateTime.Now;
                int MasterId=0;
                if (BM != null)
                {
                    MasterId = BM.BillingId;
                    BM.TotalAmount = billingMasterEntity.TotalAmount;
                    BM.TaxAmount = billingMasterEntity.TaxAmount;
                    BM.GrantTotal = billingMasterEntity.GrantTotal;
                     rtnVal = true;
                }
                else
                {
                    _context.BillingMaster.Add(billingMasterEntity);
                   
                     
                }
                if(billingMasterEntity.isRoomService==true)
                {
                    RestaurantRoomService rrs = _context.RestaurantRoomService.Where(r=>r.RoomNumber.ToString()==billingMasterEntity.TableNo_RoomNumber.ToString() &&  r.RestaurantId==billingMasterEntity.RestaurantId).SingleOrDefault();
                    rrs.isOrdered = true;
                    _context.SaveChanges();

                }
                else
                {
                    RestaurantTables rt = _context.RestaurantTables.Find(billingMasterEntity.Tableid);
                    rt.isOccupied = true;
                    _context.SaveChanges();
                }

                
                MasterId = billingMasterEntity.BillingId;

                foreach (var item in billingMasterEntity.BillingDetails) {
                    item.BillingMasterId = MasterId;
                    rtnVal = insertUpdateBillingDetails(item);
                }


            }
            catch (Exception)
            {

                throw;
            }
            return rtnVal;
        }
        private IEnumerable<BillingDetails> getBillingDetails(int billingId)
        {
            return _context.BillingDetails.Where(b => b.BillingMasterId == billingId).ToArray();
        }
        private bool insertUpdateBillingDetails(BillingDetails billingDetailsEntity)
        {
            bool rtnVal = false;
            try
            {
                BillingDetails bd = _context.BillingDetails.Find(billingDetailsEntity.BillingDetailsId);
                if(bd != null)
                {
                    bd.Quantity= billingDetailsEntity.Quantity;
                    bd.Tax= billingDetailsEntity.Tax;
                    bd.Amount= billingDetailsEntity.Amount;
                    bd.ItemId= billingDetailsEntity.ItemId;
                    bd.ItemName= billingDetailsEntity.ItemName;
                    

                    rtnVal = true;
                }
                else
                {
                    _context.BillingDetails.Add(billingDetailsEntity);
                    rtnVal = true;

                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return rtnVal;
        }
    }
}