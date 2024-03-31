using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                    foreach (var crr in exchanges)
                    {
                        rtnVal=InsertUpdate(crr);
                    }
                     
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }

            return rtnVal;
        }
        private bool InsertUpdate(CurrencyExchange currEx)
        {
            bool rtnVal = false;

            try
            {
                CurrencyExchange tmpCurrEx = _context.CurrencyExchange.Find(currEx.CurrExchangeId);
                if (tmpCurrEx!=null)
                {
                    tmpCurrEx.ExchangeValue = currEx.ExchangeValue;
                    tmpCurrEx.isActive = currEx.isActive;

                }
                else
                {
                    _context.CurrencyExchange.Add(currEx);
                }

                //

                _context.SaveChanges();
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }

            return rtnVal;
        }
        public bool SaveExchangeCurrency(ExchangeTransaction Excurrency)
        {
            bool rtnVal = false;
            try
            {
               
              _context.ExchangeTransaction.Add(Excurrency);
              _context.SaveChanges();
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }
        public IEnumerable<ExchangeTransaction> GetExchangeTransList(int branchId)
        {
            return _context.ExchangeTransaction.Where(b=>b.BranchId== branchId).ToArray();
        }
    }
}