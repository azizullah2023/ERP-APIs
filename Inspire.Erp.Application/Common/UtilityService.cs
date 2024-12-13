using DinkToPdf;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Common
{
    public class UtilityService : IUtilityService
    {
        private readonly IRepository<UserTracking> _activityLog;
        private readonly IRepository<VouchersNumbers> _voucherNumber;
        private readonly IRepository<FinancialPeriods> _financialPeriods;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<MasterAccountsTable> _masterAccountTable;
        private readonly IRepository<ItemMaster> _itemMaster;
        private readonly ILogger<UtilityService> _logger;
        private readonly IRepository<UnitDetails> _unitDetail;
        private readonly EmailSettingViewModel _mailSettings;
        private readonly IRepository<StockRegister> _stockRegister;
        public UtilityService(IRepository<UserTracking> activityLog, IRepository<FinancialPeriods> financialPeriods, ILogger<UtilityService> logger,
             IRepository<AccountsTransactions> accountTransaction, IRepository<MasterAccountsTable> masterAccountTable, IRepository<UnitDetails> unitDetail,
            IRepository<VouchersNumbers> voucherNumber, IRepository<StockRegister> stockRegister, IRepository<ItemMaster> itemMaster,
            IOptions<EmailSettingViewModel> mailSettings)
        {
            _activityLog = activityLog;
            _itemMaster = itemMaster;
            _voucherNumber = voucherNumber;
            _accountTransaction = accountTransaction;
            _financialPeriods = financialPeriods;
            _masterAccountTable = masterAccountTable;
            _stockRegister=stockRegister;
            _unitDetail = unitDetail;
            _logger = logger;
            _mailSettings = mailSettings.Value;
        }
        public string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        public async Task<Response<bool>> AddUserTrackingLog(AddActivityLogViewModel model)
        {
            try
            {
                List<string> ignoreProperties = new List<string>()
                {
                        "Id" ,
                        "CreatedAt",
                        "CreatedBy",
                        "ModifiedAt",
                        "ModifiedBy",
                };
                if (!model.IsActive)
                {
                    ignoreProperties.Add("IsActive");
                }
                if (!model.IsDeleted)
                {
                    ignoreProperties.Add("IsDeleted");
                }
                List<UserTracking> logs = new List<UserTracking>();
                var user = await GetCurrentUser();
                if (model.OtherType != "" && model.OtherType != null)
                {
                    logs.Add(new UserTracking()
                    {
                        UserTrackingField = model.OtherType,
                        UserTrackingUserChangeDt = DateTime.Now,
                        UserTrackingUserChangeTime = DateTime.Now,
                        // UserName = user.ReturnObject.FullName,
                        UserTrackingPage = model.Page,
                        UserTrackingUserVpAction = model.OtherType,
                        UserTrackingEntity = model.Entity,
                        UserTrackingSection = model.Section,
                        UserTrackingUserVpNo=model.VpNo,
                        UserTrackingUserVpType = model.VpType,
                        UserTrackingUserUserId=model.VpUserId,
                        UserTrackingOldValue=model.Oldvalue,
                        UserTrackingNewValue=model.Newvalue,
                        

                    });
                    _activityLog.InsertList(logs);
                    return Response<bool>.Success(true, "Data found");
                }
                var changes = await _activityLog.TrackChangesAsync();
                var entries = changes.Where(x => !x.State.ToString().ToLower().Contains("Unchanged".ToLower())).ToList();

                foreach (var entry in entries)
                {

                    foreach (var prop in entry.Properties)
                    {

                        if (!ignoreProperties.Contains(prop.Metadata.Name))
                        {
                            switch (entry.State)
                            {

                                case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                                    if (prop.CurrentValue != prop.OriginalValue)
                                    {
                                        logs.Add(new UserTracking()
                                        {
                                            UserTrackingField = prop.Metadata.Name,
                                            UserTrackingUserChangeDt = DateTime.Now,
                                            UserTrackingUserChangeTime = DateTime.Now,
                                            UserTrackingNewValue = prop.CurrentValue != null ? prop.CurrentValue.ToString() : "",
                                            UserTrackingOldValue = prop.OriginalValue != null ? prop.OriginalValue.ToString() : "",
                                            // UserName = user.ReturnObject.FullName,
                                            UserTrackingPage = model.Page,
                                            UserTrackingUserVpAction = entry.State.ToString(),
                                            UserTrackingEntity = entry.Entity.GetType().Name,
                                            UserTrackingSection = model.Section
                                        });
                                    }
                                    break;
                                case Microsoft.EntityFrameworkCore.EntityState.Added:
                                    logs.Add(new UserTracking()
                                    {
                                        UserTrackingField = prop.Metadata.Name,
                                        UserTrackingUserChangeDt = DateTime.Now,
                                        UserTrackingUserChangeTime = DateTime.Now,
                                        UserTrackingNewValue = prop.CurrentValue != null ? prop.CurrentValue.ToString() : "",
                                        UserTrackingOldValue = "",
                                        // UserName = user.ReturnObject.FullName,
                                        UserTrackingPage = model.Page,
                                        UserTrackingUserVpAction = entry.State.ToString(),
                                        UserTrackingEntity = entry.Entity.GetType().Name,
                                        UserTrackingSection = model.Section
                                    });
                                    break;
                            }

                        }

                    }
                }
                _activityLog.InsertList(logs);

                return Response<bool>.Success(true, "Data found");
            }
            catch (Exception ex)
            {
                return Response<bool>.Success(false, ex.Message);
            }

        }
        public async Task<Response<GetUserViewModel>> GetCurrentUser()
        {
            try
            {
                var AdminUserViewModel = new GetUserViewModel();
                //var user = await _userManager.FindByIdAsync(CurrentUserId().ToString());
                //AdminUserViewModel.Email = user.Email;
                //AdminUserViewModel.DOB = user.DOB;
                //AdminUserViewModel.Firstname = user.Firstname;
                //AdminUserViewModel.LastName = user.LastName;
                //AdminUserViewModel.FullName = user.Firstname + " " + user.LastName;
                //AdminUserViewModel.Id = user.Id;
                //AdminUserViewModel.PhoneNumber = user.PhoneNumber;

                return Response<GetUserViewModel>.Success(await Task.FromResult(AdminUserViewModel), "Data found");
            }
            catch (Exception ex)
            {
                return Response<GetUserViewModel>.Success(null, ex.Message);
            }

        }
        public async Task<Response<DropdownResponse>> AddVoucherNumber(string type, string prefix)
        {
            try
            {
                DropdownResponse returnValue = new DropdownResponse();
                var maxid = await _voucherNumber.GetAsQueryable().Where(x => x.VouchersNumbersVType == type).ToListAsync();
                var financialPeriods = await _financialPeriods.GetAsQueryable().FirstOrDefaultAsync();
                var Id = maxid.Count > 0 ? Convert.ToInt32(maxid.Max(x => Math.Round(Convert.ToDecimal(x.VouchersNumbersVNoNu))) + 1) : 1;

                var prefixId = prefix + Id;
                VouchersNumbers model = new VouchersNumbers()
                {
                    VouchersNumbersVNoNu = Id,
                    VouchersNumbersFsno = financialPeriods.FinancialPeriodsFsno,
                    VouchersNumbersVNo = prefixId,
                    VouchersNumbersVType = type,
                    VouchersNumbersVoucherDate = DateTime.Now

                };
                _voucherNumber.Insert(model);
                _voucherNumber.SaveChangesAsync();
                returnValue.Id = Id;
                returnValue.Value = prefixId;
                return Response<DropdownResponse>.Success(returnValue, "Successfully Added.");
            }
            catch (Exception ex)
            {

                return Response<DropdownResponse>.Fail(new DropdownResponse(), ex.Message);
            }
        }
        public async Task<Response<FinancialPeriods>> GetFinancialPeriods()
        {
            try
            {
                return Response<FinancialPeriods>.Success(await _financialPeriods.GetAsQueryable().FirstOrDefaultAsync(), "Data Found");
            }
            catch (Exception ex)
            {

                return Response<FinancialPeriods>.Fail(new FinancialPeriods(), ex.Message);
            }
        }
        public async Task<Response<bool>> SaveAccountTransaction(AccountsTransactions model)
        {
            try
            {
                _accountTransaction.Insert(model);
                return Response<bool>.Success(true, "Data Saved");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(false, ex.Message);
            }
        }
        public async Task<Response<List<ItemMaster>>> GetItemMaster()
        {
            try
            {
                var result = await _itemMaster.GetAsQueryable().ToListAsync();
                return Response<List<ItemMaster>>.Success(result, "FOund");
            }
            catch (Exception ex)
            {

                return Response<List<ItemMaster>>.Fail(new List<ItemMaster>(), ex.Message);
            }
        }
        public async Task<Response<bool>> SaveMasterAccountTable(MasterAccountsTable model)
        {
            try
            {
                _masterAccountTable.Insert(model);
                return Response<bool>.Success(true, "Data Saved");
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(false, ex.Message);
            }
        }


        public async Task<decimal> GetBasicUnitConversion(int? itemId, int? unitId)
        {
            try
            {
                
               
                var unitDetails = await _unitDetail.GetAsQueryable().Where(x => itemId==null || x.UnitDetailsItemId == (Int64)itemId && x.UnitDetailsUnitId== unitId).FirstOrDefaultAsync();
                
                return Convert.ToDecimal(unitDetails.UnitDetailsConversionType);
            }
            catch (Exception ex)
            {

                return 1;
            }
        }


        public async Task<decimal> GetStockQuantity(long? itemId, int? locationId)
        {
            try
            {


                var result = await _stockRegister.GetAsQueryable().Where(x => x.StockRegisterMaterialID == (Int64)itemId && locationId==null || x.StockRegisterLocationID==locationId).SumAsync(y=>(y.StockRegisterSIN-y.StockRegisterSout));

                return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {

                return 1;
            }
        }

        public async Task<bool> SendEmailAsync(EmailRequestViewModel mailRequest)
        {
            try
            {
                _logger.LogInformation(@$"{nameof(SendEmailAsync)} has been started.");

                var client = new SendGridClient(_mailSettings.Key);
                var from = new EmailAddress(_mailSettings.Mail, _mailSettings.DisplayName);
                var subject = mailRequest.Subject;
                var emails = new List<EmailAddress>();
                foreach (var email in mailRequest.Emails)
                {
                    emails.Add(new EmailAddress(email));
                }
                emails.AddRange(_mailSettings.CCEmails.Select(x => new EmailAddress(x)).ToList());
                var plainTextContent = "";
                var htmlContent = mailRequest.Body;
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emails, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                _logger.LogInformation(@$"Message send successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }

        }



    }
}
