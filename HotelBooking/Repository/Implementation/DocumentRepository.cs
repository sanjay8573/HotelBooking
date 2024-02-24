using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class DocumentRepository : IBookingDocument
    {
        private readonly CompanyContext _context;

        public DocumentRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddDocument(BookingDocuments documentsEntity)
        {
            bool rtnVal=false;
            if (documentsEntity != null)
            {
                _context.BookingDocuments.Add(documentsEntity);
                _context.SaveChanges();
                rtnVal = true;
            }
            return rtnVal;
        }

        public void DeleteDocument(int bookigId,int DocumentId)
        {
            var tmpDoc = _context.BookingDocuments.Find(DocumentId);
            if (tmpDoc != null)
            {
                tmpDoc.isDeleted = true;
                _context.SaveChanges();
            }
        }

        public BookingDocuments GetDocumentById(int DocumentId)
        {
            return _context.BookingDocuments.Where(b => b.DocumentId == DocumentId).SingleOrDefault();
        }

        public IEnumerable<BookingDocuments> GetDocuments(int BookingId)
        {
           return  _context.BookingDocuments.Where(b => b.BookingId == BookingId).ToArray();
        }
        public IEnumerable<DocumentType> GetDocumentTypes(int BranchId)
        {
           return  _context.DocumentType.Where(d => d.BranchId == BranchId).ToArray();
        }
    }
}