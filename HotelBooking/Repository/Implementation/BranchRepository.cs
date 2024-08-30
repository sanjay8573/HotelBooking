using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.TimeZone;
using HotelBooking.Model.EditHotel;
using HotelBooking.Model.onlineAPI;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System;
using TimeZone = HotelBooking.Model.TimeZone.TimeZone;

namespace HotelBooking.Repository.Implementation
{
    public class BranchRepository : IBranch
    {
        private readonly CompanyContext _context;
        //public BranchRepository(CompanyContext context)
        //{
        //    _context = context;
        //}
        public BranchRepository()
        {
            _context = new CompanyContext();
        }
        public int AddBranch(Branch BranchEntity)
        {
            int result = -1;

            if (BranchEntity != null)
            {
                _context.Branch.Add(BranchEntity);
                _context.SaveChanges();
                result = BranchEntity.Id;
            }
            return result;
        }

        public void DeleteBranch(int BrnachId)
        {
            var BranchEntity = _context.Branch.Find(BrnachId);
            if (BranchEntity != null)
            {
                _context.Branch.Remove(BranchEntity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Branch> GetBranch()
        {
            return _context.Branch.ToArray();
        }

        public IEnumerable<Branch> GetBranch(int CompanyId)
        {
             return _context.Branch.Where(b=>b.Id==CompanyId).ToArray();
        }

        public IEnumerable<Branch> GetBranchByCompanyId(int CompanyId)
        {
            var b1= _context.Branch.Where(b => b.CompanyId == CompanyId).ToArray();
            return b1.ToArray<Branch>();
        }

        public Branch GetBranchById(int BrnachId)
        {
            return _context.Branch.Find(BrnachId);
        }

        public int UpdateBranch(Branch BranchEntity)
        {
            int result = -1;

            if (BranchEntity != null)
            {
                _context.Branch.Find(BranchEntity.Id);
                _context.Entry(BranchEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = BranchEntity.Id;
            }
            return result;
        }

        public int AddBranchContactDetails(HotelContacts entityHotelContacts)
        {
            int result = -1;
            if (entityHotelContacts != null)
            {
                _context.HotelContacts.Add(entityHotelContacts);
                _context.SaveChanges();
                result = entityHotelContacts.HotelContactID;
            }
            return result;
        }
        public IEnumerable<HotelContacts> GetBranchConatctDetails(int BranchId)
        {
            return _context.HotelContacts.Where(c=>c.BranchId== BranchId).ToArray();
        }
        public HotelContacts getBranchContactById(int BranchConatctId)
        {
            return _context.HotelContacts.Where(c => c.HotelContactID == BranchConatctId).SingleOrDefault();
        }

        public VM_EditHotel GetHotelDetailById(int BranchId)
        {
            VM_EditHotel VM_H= new VM_EditHotel();
            Branch br = _context.Branch.Find(BranchId);
            IEnumerable<HotelContacts> hc = GetBranchConatctDetails(BranchId);
            var cd= (from a in hc 
                        select  new VM_ContactDetails
                        {
                            BranchId = a.BranchId,
                            ContactText = a.ContactText,    
                            ContactType = a.ContactType,
                            DateCreated = a.DateCreated,
                            DateModified = a.DateModified,
                            Description = a.Description,
                            ContactDetails=a.ContactDetails,
                            HotelContactID = a.HotelContactID,
                            HotelID = a.HotelID,
                            isActive=a.isActive,
                            isManager=a.isManager,
                            isDeleted=a.isDeleted
                        }
                    ).ToList<VM_ContactDetails>();
            IEnumerable<Amenities> HA= _context.Amenities.Where(c => c.BranchId==BranchId && c.isHotel==true).ToArray();
            var ham = (from a in HA
                       select new VM_Amenities
                       {
                        AmenitiesId = a.AmenitiesId,
                        BranchId = a.BranchId,
                        Code = a.Code,
                        DateCreated = a.DateCreated,
                        Description = a.Description,
                        imageData = a.imageData,
                        IsActive=a.IsActive,
                        isDeleted = a.isDeleted,
                        isHotel=a.isHotel,
                        Title= a.Title
                       }

                     ).ToList<VM_Amenities>();

            IEnumerable<ImageMaster> IM=_context.ImageMaster.Where(i=>i.ImageTypeId==0 && i.BranchId==BranchId).ToArray();
            var Hotelimages = (from a in IM
                               select  new VM_HotelImages
                               {
                                   BranchId = a.BranchId,
                                   ImageTypeId= a.ImageTypeId,
                                   ImageContentType= a.ImageContentType,
                                   ImageData= a.ImageData,
                                   ImageId= a.ImageId,
                                   ImageName= a.ImageName,
                                   isActive=a.isActive,
                                   isDeleted=a.isDeleted,
                                   RefId=a.RefId,
                                   isMainImage=a.isMainImage
                               }

                               ).ToList<VM_HotelImages>();


            VM_GeneralInfo genInfo = new VM_GeneralInfo();
            VM_HotelDetails _hd = new VM_HotelDetails();
            VM_WebConfiguration _wc = new VM_WebConfiguration();
            genInfo.Id = br.Id;
            genInfo.BranchName = br.BranchName;            
            genInfo.Address = br.Address;
            genInfo.City = br.City;
            genInfo.State = br.State;
            genInfo.Country = br.Country;
            genInfo.Postcode = br.Postcode;
            genInfo.Phone1 = br.Phone1;
            genInfo.Phone2 = br.Phone2;
            genInfo.Fax = br.Fax;
            genInfo.EmailID = br.EmailID;
            genInfo.Latitude = br.Latitude;
            genInfo.Longitude = br.Longitude;
            genInfo.ChainName = br.ChainName;
            genInfo.Buildyear = br.Buildyear;
            genInfo.NearestAirport = br.NearestAirport;
            
            genInfo.DecimalPlaces = br.DecimalPlaces;
            genInfo.PanCard = br.PanCard;
            genInfo.TaxNo1 = br.TaxNo1;
            genInfo.TaxNo2 = br.TaxNo2;
            genInfo.WebsiteURL = br.WebsiteURL;
            genInfo.StarRating = br.StarRating;
            genInfo.CheckInTime = br.CheckInTime;
            genInfo.CheckOutTime = br.CheckOutTime;
            genInfo.NoofFloors = br.NoofFloors;
            genInfo.TimeZone = br.TimeZone;





            _hd.ShortDescription = br.ShortDescription;
            _hd.LongDescription = br.LongDescription;
            _hd.CheckinPolicy = br.CheckinPolicy;
            _hd.CheckInInStructions = br.CheckInInStructions;
            _hd.SpecialCheckInInStructions = br.SpecialCheckInInStructions;
            _hd.PetsAllowed = br.PetsAllowed;
            _hd.CoupleFriendly = br.CoupleFriendly;
            _hd.CheckinWithLocalIds = br.CheckinWithLocalIds;



            _wc.WebsiteFooterFile = br.WebsiteFooterFile;
            _wc.WebsiteHeaderFile = br.WebsiteHeaderFile;
            _wc.WebsiteURL = br.WebsiteURL;
            _wc.LogoImage = br.LogoImage;




            VM_H.ContactInformation = cd;
            VM_H.HotelAmenities = ham;
            VM_H.HotelImages = Hotelimages;
            VM_H.GeneralInformation = genInfo;
            VM_H.HotelDetails=_hd;
            VM_H.WebSiteConfiguration = _wc;

            return VM_H;
        }
        public IEnumerable<TimeZone> GetTimeZones()
        {
            return _context.BranchTimeZone.ToArray();
        }

        public bool UpdateHotelGenInfo(VM_GeneralInfo entityGenInfo)
        {
            bool rtnVal = false;
            if (entityGenInfo != null)
            {
                Branch br = _context.Branch.Find(entityGenInfo.Id);
                if (br != null)
                {
                    br.Id = entityGenInfo.Id;
                    br.BranchName = entityGenInfo.BranchName;
                    br.Address = entityGenInfo.Address;
                    br.Latitude = entityGenInfo.Latitude;
                    br.Longitude = entityGenInfo.Longitude;
                    br.ChainName = entityGenInfo.ChainName;
                    br.Buildyear = entityGenInfo.Buildyear;
                    br.NearestAirport = entityGenInfo.NearestAirport;
                    br.PetsAllowed = entityGenInfo.PetsAllowed;
                    br.CheckinPolicy = entityGenInfo.CheckinPolicy;
                    br.CoupleFriendly = entityGenInfo.CoupleFriendly;
                    br.CheckinWithLocalIds = entityGenInfo.CheckinWithLocalIds;
                    br.DecimalPlaces = entityGenInfo.DecimalPlaces;
                    br.PanCard = entityGenInfo.PanCard;
                    br.TaxNo1 = entityGenInfo.TaxNo1;
                    br.TaxNo2 = entityGenInfo.TaxNo2;
                    br.WebsiteURL = entityGenInfo.WebsiteURL;
                    br.StarRating = entityGenInfo.StarRating;
                    br.CheckInTime = entityGenInfo.CheckInTime;
                    br.CheckOutTime = entityGenInfo.CheckOutTime;
                    br.NoofFloors = entityGenInfo.NoofFloors;
                    br.TimeZone = entityGenInfo.TimeZone;
                    _context.SaveChanges();
                    rtnVal = true;
                }
            }
            return rtnVal;
        }

        public bool UpdateHotelDetails(VM_HotelDetails entityHD)
        {
            bool rtnVal = false;
            if (entityHD != null)
            {
                Branch br = _context.Branch.Find(entityHD.Id);
                if (br != null)
                {
                    br.PetsAllowed = entityHD.PetsAllowed;
                    br.CheckinPolicy = entityHD.CheckinPolicy;
                    br.CoupleFriendly = entityHD.CoupleFriendly;
                    br.CheckinWithLocalIds = entityHD.CheckinWithLocalIds;
                    br.LongDescription = entityHD.LongDescription ;
                    br.ShortDescription = entityHD.ShortDescription;
                    br.CheckInInStructions = entityHD.CheckInInStructions;
                    br.SpecialCheckInInStructions = entityHD.SpecialCheckInInStructions;
                    _context.SaveChanges();
                    rtnVal = true;
                }
            }
            return rtnVal;
        }

        public bool UpdateWebConfiguration(VM_WebConfiguration entityWC)
        {
            bool rtnVal = false;
            if (entityWC != null)
            {
                Branch br = _context.Branch.Find(entityWC.Id);
                if (br != null)
                {
                    br.WebsiteURL = entityWC.WebsiteURL;
                    br.WebsiteHeaderFile = entityWC.WebsiteHeaderFile;
                    br.WebsiteFooterFile = entityWC.WebsiteFooterFile;
                    if (entityWC.LogoImage != null)
                    {
                        br.LogoImage = entityWC.LogoImage;
                    }
                   
                    _context.SaveChanges();
                    rtnVal = true;
                }
            }
            return rtnVal;
        }

        public bool UpdateHotelAmenities(IEnumerable<VM_Amenities> entityAmenities)
        {
            bool rtnVal = false;
            if (entityAmenities != null)

            {
                int brId = entityAmenities.Select(r => r.BranchId).FirstOrDefault();
                IEnumerable<Amenities> temEntiry = _context.Amenities.Where(a => a.BranchId == brId && a.isHotel == true).ToArray();
                if (temEntiry.Count()>0)
                {
                    _context.Amenities.RemoveRange(temEntiry);

                }
            }
            foreach(Amenities ami in entityAmenities) {
              
                if (ami != null)
                {
                    Amenities _am = new Amenities()
                    {
                        BranchId = ami.BranchId,
                        Title = ami.Title,
                        Description = ami.Description,
                        IsActive=ami.IsActive,
                        isDeleted=ami.isDeleted,
                        isHotel=ami.isHotel

                    
                    };
                    _context.Amenities.Add(_am);
                }
                
            }   
            _context.SaveChanges();
            return rtnVal;
        }
        public bool UpdateHotelContacts(IEnumerable<VM_ContactDetails> entityCD)
        {
            bool rtnVal = false;
            if (entityCD != null)

            {
                int brId = entityCD.Select(r => r.BranchId).FirstOrDefault();
                IEnumerable<HotelContacts> tempEntiry = _context.HotelContacts.Where(a => a.BranchId == brId).ToArray();
                if (tempEntiry.Count() > 0)
                {
                    _context.HotelContacts.RemoveRange(tempEntiry);

                }
            }
            foreach (HotelContacts ami in entityCD)
            {

                if (ami != null)
                {
                    DateTime dt = DateTime.Now;
                    HotelContacts _am = new HotelContacts()
                    {
                        BranchId = ami.BranchId,
                        ContactText = ami.ContactText,
                        Description = ami.Description,
                        ContactType = ami.ContactType,
                        ContactDetails = ami.ContactDetails,
                        isDeleted = ami.isDeleted,
                        isManager = ami.isManager,
                        isActive = ami.isActive,
                        DateCreated = dt,
                        DateModified=dt
                        


                    };
                    _context.HotelContacts.Add(_am);
                }

            }
            _context.SaveChanges();
            return rtnVal;
        }

        public bool UpdateHotelImages(IEnumerable<VM_HotelImages> entityHI)
        {
            bool rtnVal = false;
            if (entityHI != null)

            {
                int brId = entityHI.Select(r => r.BranchId).FirstOrDefault();
                IEnumerable<ImageMaster> tempEntiry = _context.ImageMaster.Where(a => a.BranchId == brId && a.ImageTypeId==0).ToArray();
                if (tempEntiry.Count() > 0)
                {
                    _context.ImageMaster.RemoveRange(tempEntiry);

                }
            }
            foreach (ImageMaster ami in entityHI)
            {

                if (ami != null)
                {
                    DateTime dt = DateTime.Now;
                    ImageMaster _am = new ImageMaster()
                    {
                        BranchId = ami.BranchId,
                        RefId=ami.RefId,
                        ImageName = ami.ImageName,
                        ImageData = ami.ImageData,
                        ImageContentType = ami.ImageContentType,
                        ImageTypeId = 0,
                        isDeleted = ami.isDeleted,                        
                        isActive = ami.isActive
                        


                    };
                    _context.ImageMaster.Add(_am);
                }

            }
            _context.SaveChanges();
            return rtnVal;
        }

        public bool RemoveHotelImages(int ImageId,int BranchId)
        {
            bool rtnVal = false;
            if (ImageId >0)

            {
               
                ImageMaster tempEntiry = _context.ImageMaster.Where(a => a.BranchId == BranchId && a.ImageId==ImageId && a.ImageTypeId == 0).SingleOrDefault();
                if (tempEntiry!=null)
                {
                    _context.ImageMaster.Remove(tempEntiry);

                }
            }
            _context.SaveChanges();
            return rtnVal;
        }
    }
}
