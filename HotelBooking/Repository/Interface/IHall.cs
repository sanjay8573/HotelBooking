using HotelBooking.Model.Hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IHall
    {
        IEnumerable<HALL_PARTY_MASTER> GetAllHall(int BranchId);
        HALL_PARTY_MASTER GetHall(int HallId);
        bool SaveHall(HALL_PARTY_MASTER hallEntity);
        IEnumerable<Hall_Party_Time_Slot> GetHallTimings(int BranchId);
        bool SaveHallBooking(HallBooking hallEntity);
        IEnumerable<HallBooking> GetHallBookings(int BranchId);

        IEnumerable<HallBookingCost> GetHallBookingCost(int BranchId,int hallBookingId);
        IEnumerable<HallBookingCost> GetHallBookingCostByHall(int BranchId, int hallId);
        bool SaveHallBookingCost(HallBookingCost hallCostingEntity);
        IEnumerable<HallBookingPayment> GetHallBookingPayment(int BranchId, int hallBookingId);
        bool SaveHallBookingPayment(HallBookingPayment hallPaymentEntity);

        IEnumerable<HallBooking> CheckHallAvailability(int HallId,DateTime bookingDate,int slotId);

    }
}
