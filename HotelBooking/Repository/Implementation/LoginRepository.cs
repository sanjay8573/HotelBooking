﻿using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;

namespace HotelBooking.Repository.Implementation
{
    public class LoginRepository : ILogin
    {
        private readonly IStaff _istaff;
        private readonly IBranch _ib;
        private readonly ICompany _cmp;
        private readonly IDesignation _desig;
        private readonly IRoles _irole;
        private readonly ITaxMaster _tax;
        private readonly ICurrency _currency;

        public LoginRepository()
        {
            _istaff= new StaffRepository();
            _ib= new BranchRepository();
            _cmp= new CompanyRepository();
            _desig= new DesignationRepository();
            _irole= new RoleRepository();
            _tax = new TaxMasterRespository();
            _currency= new CurrencyRepository();
        }
        public LoginResponse ValidateLogin(LoginRequestModel lognReq)
        {
            LoginResponse lr = new LoginResponse();
            lr.UserId=_istaff.ValidateLogin(lognReq);
            lr.validToen=this.GetHashCode().ToString();
            return lr;

        }
        public UserLoginResponse Login(int userId)
        {
            UserLoginResponse loginResponse = new UserLoginResponse();
            try
            {
               
                if (userId > 0)
                {
                    Staff s = _istaff.GetStaff(userId);
                    loginResponse.UserId = s.Id;
                    loginResponse.UserName = s.StaffName;
                    loginResponse.Email = s.Email;
                    loginResponse.BranchId = s.BranchId;
                    try
                    {
                        string BranchName = string.Empty;
                        
                        loginResponse.BranchName = (_ib.GetBranchById(s.BranchId) != null) ? _ib.GetBranchById(s.BranchId).BranchName : "";
                        //loginResponse.BranchTaxPercentage = _tax.GetTaxForBranch(s.BranchId).Value;
                        loginResponse.TaxDetails=_tax.GetAllTax(s.BranchId).Where(t=>t.isActive).ToList();
                        AvailableCurrency avlCurr = _currency.GetAvailableCurrency(s.BranchId).Where(b=>b.isBusinessCurrency==true).SingleOrDefault();
                        if(avlCurr != null)
                        {
                            loginResponse.BranchCurrencyName = avlCurr.CurrencyName;
                            loginResponse.BranchCurrencyCode = avlCurr.CurrencyCode;
                            loginResponse.BranchCurrencySymbol = avlCurr.CurrencySymbol;
                        }
                        else
                        {
                            loginResponse.BranchCurrencyName = "Rupees";
                            loginResponse.BranchCurrencyCode = "INR";
                            loginResponse.BranchCurrencySymbol = "R";
                        }
                         
                    }
                    catch (Exception)
                    {

                        loginResponse.BranchName = "";
                    }
                   
                    loginResponse.CompanyId = s.CompanyId;
                    try
                    {
                        Company cmp = _cmp.GetCompanyById(s.CompanyId);
                        loginResponse.CompanyName = (!string.IsNullOrEmpty(cmp.Name)) ? cmp.Name : string.Empty;
                        loginResponse.CompanyLog = (!string.IsNullOrEmpty(cmp.CompanyLog)) ? cmp.CompanyLog : string.Empty;
                    }
                    catch (Exception)
                    {

                        loginResponse.CompanyName = "";
                        loginResponse.CompanyLog = "No logo";
                    }
                   
                    loginResponse.DesignationId = s.DesignationId;
                    try
                    {
                        loginResponse.DesignationName =(_desig.getDesignationById(s.DesignationId)!=null) ? _desig.getDesignationById(s.DesignationId).DesignationName:"";
                    }
                    catch (Exception)
                    {

                        loginResponse.DesignationName = "";
                    }
                    
                    loginResponse.PrimaryRoleID = s.PrimaryRoleID;
                    loginResponse.SecondryRoleID = s.SecondryRoleID;
                    try
                    {
                        loginResponse.PrimaryRoles = (_irole.GetRoleById(s.PrimaryRoleID)!=null)?_irole.GetRoleById(s.PrimaryRoleID).Roles : "";
                        //loginResponse.SecondryRoles = (_irole.GetRoleById(s.SecondryRoleID) != null) ? _irole.GetRoleById(s.SecondryRoleID).Roles : "";
                        loginResponse.Modules = getModulesObject(loginResponse.PrimaryRoles);
                    }
                    catch (Exception)
                    {

                        loginResponse.PrimaryRoles = "NA";
                        loginResponse.SecondryRoles = "NA";
                    }
                    
                   
                    loginResponse.ResponseMessage = "Login Success";
                }
            }
            catch (Exception ex)
            {

                loginResponse.ResponseMessage = "Login Failed "+ex.Message.ToString();
            }
            

            return loginResponse;

        }
        private List<VM_Module> getModulesObject(string json)
        {
            return JsonConvert.DeserializeObject<List<VM_Module>>(json);
        }
    }
}