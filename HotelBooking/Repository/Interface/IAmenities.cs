using HotelBooking.Model;
using System;
using System.Collections.Generic;

namespace HotelBooking.Repository.Interface
{
    public interface IAmenities
    {
        IEnumerable<Amenities> GetAmenities(int BranchId);
        bool AddAmenities(Amenities AmenitiesEntity);
        
        int UpdateAmenities(Amenities AmenitiesEntity);
        void DeleteAmenities(int Amenitiesid);
    }
}
