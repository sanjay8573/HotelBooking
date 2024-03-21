using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class CurrencyRepository : ICurrency
    {
        private readonly CompanyContext _context;
        public CurrencyRepository()
        {
            _context = new CompanyContext();

        }

        public IEnumerable<AvailableCurrency> GetAvailableCurrency(int branchId)
        {
            return _context.AvailableCurrency.Where(c=>c.BranchId == branchId).ToArray();
        }

        public IEnumerable<CurrencyExchange> GetExchangeList(int branchId)
        {
            return _context.CurrencyExchange.Where(c => c.BranchId == branchId).ToArray();
        }

        public bool SaveNewCurrency(AvailableCurrency currency)
        {
            bool rtnVal = false;
            try
            {
                _context.AvailableCurrency.Add(currency);
                _context.SaveChanges();
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }

           return rtnVal;
        }

        public bool SetExchange(IEnumerable<CurrencyExchange> exchanges)
        {
            bool rtnVal = false;
            try
            {
                _context.CurrencyExchange.AddRange(exchanges);
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