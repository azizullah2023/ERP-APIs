using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class VatStatementService : IVatStatementService
    {
        private readonly IRepository<StationMaster> _locationMaster;
        private readonly IRepository<MasterAccountsTable> _masterAccountsTable;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<GetAccountTransactionVatStatementResponse> _accountResponse;
        private IRepository<AccountSettings> _accountsSettingsRepo;
        public VatStatementService(IRepository<MasterAccountsTable> masterAccountsTable,
            IRepository<AccountsTransactions> accountTransaction, IRepository<StationMaster> locationMaster, IRepository<AccountSettings> accountsSettingsRepo,
            IRepository<GetAccountTransactionVatStatementResponse> accountResponse)
        {
            _masterAccountsTable = masterAccountsTable;
            _accountTransaction = accountTransaction;
            _accountResponse = accountResponse; _accountsSettingsRepo = accountsSettingsRepo;
            _locationMaster = locationMaster;
        }
        public async Task<Response<List<DropdownResponse>>> GetDistinctVoucherType()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = " 1 == 1",
                //    Name = " All "
                //});
                response.AddRange(_accountTransaction.GetAsQueryable().Select(x => x.AccountsTransactionsVoucherType).Distinct().ToList().Select(x => new DropdownResponse
                {
                    Value = x,
                    Name = x
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }

        public async Task<List<GetAccountTransactionVatStatementResponse>> GetVatStatementAccountTransaction(GenericGridViewModel model)
        {
            try
            {
                List<GetAccountTransactionVatStatementResponse> result = null;

                if (model.isDate)
                {
                    result = await _accountTransaction.GetAsQueryable().Where(a => a.AccountsTransactionsAccNo.Contains(model.Filter) && (a.AccountsTransactionsTransDate >= model.FromDate && a.AccountsTransactionsTransDate <= model.ToDate)).Select(x => new GetAccountTransactionVatStatementResponse
                    {
                        AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                        AccountsTransactionsAccName = _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo) != null ? _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo).MaAccName : "",
                        AccountsTransactionsDebit = Convert.ToDecimal(x.AccountsTransactionsDebit),
                        AccountsTransactionsCredit = Convert.ToDecimal(x.AccountsTransactionsCredit),
                        AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                        AccountsTransactionsVoucherNo = x.AccountsTransactionsVoucherNo,
                        AccountsTransactionsVoucherType = x.AccountsTransactionsVoucherType,
                        AccountsTransactionsDescription = x.AccountsTransactionsDescription,
                        AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                        AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                        RefNo = x.RefNo,
                        AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                        AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                        AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit,
                        AccountsTransactionsVatableAmount = x.AccountsTransactionsVatableAmount,
                        AccountsTransactionsVatno = x.AccountsTransactionsVatno
                    }).ToListAsync();
                }
                else
                {
                    result = await _accountTransaction.GetAsQueryable().Where(a => a.AccountsTransactionsTransDate <= model.FromDate && a.AccountsTransactionsAccNo.Contains(model.Filter)).Select(x => new GetAccountTransactionVatStatementResponse
                    {
                        AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                        AccountsTransactionsAccName = _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo) != null ? _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo).MaAccName : "",
                        AccountsTransactionsDebit = Convert.ToDecimal(x.AccountsTransactionsDebit),
                        AccountsTransactionsCredit = Convert.ToDecimal(x.AccountsTransactionsCredit),
                        AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                        AccountsTransactionsVoucherNo = x.AccountsTransactionsVoucherNo,
                        AccountsTransactionsVoucherType = x.AccountsTransactionsVoucherType,
                        AccountsTransactionsDescription = x.AccountsTransactionsDescription,
                        AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                        AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                        RefNo = x.RefNo,
                        AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                        AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                        AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit,
                        AccountsTransactionsVatableAmount = x.AccountsTransactionsVatableAmount,
                        AccountsTransactionsVatno = x.AccountsTransactionsVatno
                    }).ToListAsync();
                }
                var gridReponse = new GridWrapperResponse<List<GetAccountTransactionVatStatementResponse>>();
                gridReponse.Data = result;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return result;
            }
            catch (Exception ex)
            {
                return new List<GetAccountTransactionVatStatementResponse>();
            }
        }
        public async Task<Response<GetVatStatementSummaryResponse>> GetVatStatementSummary(GenericGridViewModel model)
        {
            try
            {
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                var result = await GetVatStatementAccountTransaction(model);
                var modelWrapper = new GetVatStatementSummaryResponse();
                var gridmodel = new List<GetAccountTransactionVatStatementSummaryResponse>();
                decimal grandTotal = 0;
                decimal runningBalance = 0;
                foreach (var accounts in result.GroupBy(x => x.AccountsTransactionsAccNo))
                {

                    decimal debit = 0;
                    decimal credit = 0;
                    string accountName = "";
                    foreach (var account in accounts)
                    {
                        accountName = account.AccountsTransactionsAccName;
                        debit += account.AccountsTransactionsDebit;
                        credit += account.AccountsTransactionsCredit;
                    }
                    runningBalance = (debit - credit) + (runningBalance);
                    gridmodel.Add(new GetAccountTransactionVatStatementSummaryResponse()
                    {
                        RunningBalance = runningBalance.ToString(),
                        AccountName = accountName,
                        AccountNo = accounts.Key,
                        Credit = credit.ToString(),
                        Debit = debit.ToString()
                    });
                }
                modelWrapper.Details = gridmodel;
                modelWrapper.Total = grandTotal.ToString();
                modelWrapper.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                modelWrapper.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                modelWrapper.StationMasterFax = location != null ? location.StationMasterFax : "";
                modelWrapper.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                modelWrapper.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                modelWrapper.StationMasterCity = location != null ? location.StationMasterCity : "";
                modelWrapper.StationMasterCode = location != null ? location.StationMasterCode : 0;
                modelWrapper.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                modelWrapper.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                modelWrapper.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                modelWrapper.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                modelWrapper.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                modelWrapper.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                return Response<GetVatStatementSummaryResponse>.Success(modelWrapper, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GetVatStatementSummaryResponse>.Fail(new GetVatStatementSummaryResponse(), ex.Message);
            }
        }
        public async Task<Response<GetVatStatementDetailResponse>> GetVatStatementDetail(GenericGridViewModel model)
        {
            try
            {
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                var result = await GetVatStatementAccountTransaction(model);
                var modelWrapper = new GetVatStatementDetailResponse();
                var gridmodel = new List<GetAccountTransactionVatStatementDetailResponse>();
                decimal grandTotal = 0;
                decimal runningBalance = 0;
                foreach (var accounts in result)
                {

                    runningBalance = (accounts.AccountsTransactionsDebit - accounts.AccountsTransactionsCredit) + (runningBalance);
                    gridmodel.Add(new GetAccountTransactionVatStatementDetailResponse()
                    {
                        RunningBalance = runningBalance.ToString(),
                        AccountName = accounts.AccountsTransactionsAccName,
                        AccountNo = accounts.AccountsTransactionsAccNo,
                        Credit = accounts.AccountsTransactionsCredit.ToString(),
                        Debit = accounts.AccountsTransactionsDebit.ToString(),
                        Date = accounts.AccountsTransactionsTransDateString,
                        Description = accounts.AccountsTransactionsDescription,
                        RefNo = accounts.RefNo,
                        VatAmount = accounts.AccountsTransactionsVatableAmount.ToString(),
                        VATNo = accounts.AccountsTransactionsVatno,
                        VoucherNo = accounts.AccountsTransactionsVoucherNo,
                        VoucherType = accounts.AccountsTransactionsVoucherType
                    });
                }

                modelWrapper.Details = gridmodel;
                modelWrapper.Total = grandTotal.ToString();
                modelWrapper.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                modelWrapper.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                modelWrapper.StationMasterFax = location != null ? location.StationMasterFax : "";
                modelWrapper.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                modelWrapper.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                modelWrapper.StationMasterCity = location != null ? location.StationMasterCity : "";
                modelWrapper.StationMasterCode = location != null ? location.StationMasterCode : 0;
                modelWrapper.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                modelWrapper.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                modelWrapper.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                modelWrapper.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                modelWrapper.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                modelWrapper.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                return Response<GetVatStatementDetailResponse>.Success(modelWrapper, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GetVatStatementDetailResponse>.Fail(new GetVatStatementDetailResponse(), ex.Message);
            }
        }
    }
}
