using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface ITnC
    {
        IEnumerable<TNC> GetTnC();
        TNC GetTnCByCompanyId(int CompanyId);
        TNC GetTnCById(int TnCId);
        int AddTnC(TNC TnCEntity);
        int UpdateTnC(TNC TnCEntity);
        void DeleteTnC(int TnCId);
    }
}
