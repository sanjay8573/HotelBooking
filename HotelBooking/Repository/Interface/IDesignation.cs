using HotelBooking.Model;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IDesignation
    {


        IEnumerable<Designation> getAllDesignation();
        Designation getDesignationById(int DesigId);
        
        int AddDesignation(Designation DesignationEntity);
        int UpdateDesignation(Designation DesignationEntity);
        void DeleteDesignation(int DesigId);
    }
}
