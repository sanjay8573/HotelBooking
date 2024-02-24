using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    public class DocumentController : ApiController
    {
        IBookingDocument _BD;
        public DocumentController()
        {
            _BD = new DocumentRepository();
        }
        public DocumentController(IBookingDocument im)
        {
            _BD = im;
        }
        [Route("api/Document/GetDocuments/{BookingId}")]
        [HttpGet]
        public IEnumerable<BookingDocuments> GetDocuments(int BookingId)
        {
            return _BD.GetDocuments(BookingId);
        }
        //[HttpPost("AddAmenities/{BranchId}")]
        [Route("api/Document/AddDocuments/{BookingId}")]
        [HttpPost]
        public bool AddDocuments(BookingDocuments DocsEntity)
        {
            return _BD.AddDocument(DocsEntity);
        }



        [Route("api/Document/DeleteDocuments/{BookingId}/{DocumentId}")]
        [HttpDelete]
        public void DeleteDocuments(int BookingId, int DocumentId)
        {
            _BD.DeleteDocument(BookingId, DocumentId);
        }
        [Route("api/Document/GetDocumentById/{DocumentId}")]
        [HttpGet]
        public BookingDocuments GetDocumentById( int DocumentId)
        {
            return _BD.GetDocumentById(DocumentId);
        }

        [Route("api/Document/GetDocumentTypes/{BranchId}")]
        [HttpGet]
        public IEnumerable<DocumentType> GetDocumentTypes(int BranchId)
        {
            return _BD.GetDocumentTypes(BranchId);
        }
    }
}
