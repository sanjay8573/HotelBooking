using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IBookingDocument
    {
        IEnumerable<BookingDocuments> GetDocuments(int BookingId);
        bool AddDocument(BookingDocuments documentsEntity);
        BookingDocuments GetDocumentById(int DocumentId);

        void DeleteDocument(int BookingId,int DocumentId);
        IEnumerable<DocumentType> GetDocumentTypes(int BranchId);
    }
}
