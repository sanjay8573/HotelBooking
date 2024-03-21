using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface ICurrency
    {
        IEnumerable<AvailableCurrency> GetAvailableCurrency(int branchId);
        bool SaveNewCurrency(AvailableCurrency currency);
        IEnumerable<CurrencyExchange> GetExchangeList(int branchId);
        bool SetExchange(IEnumerable<CurrencyExchange> exchanges);

    }
}
