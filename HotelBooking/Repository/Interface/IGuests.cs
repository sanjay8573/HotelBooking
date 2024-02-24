using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public  interface IGuests
    {

        IEnumerable<Guests> GetAllGuest(int BranchId);

        Guests GetGuest(int guestId);
        bool AddGuest(Guests guestEntity);

       
        void DeleteGuest(int guestId);
        bool SetAsVip(int guestId);
    }
}
