using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.File;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class AccountStatementMultiAccountService : IAccountStatementMultiAccountService
    {
        private readonly IRepository<MasterAccountsTable> _masterAccountsTable;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<GetAccountTransactionDBResponse> _accountResponse;
        private readonly IRepository<JobMaster> _jobMaster;
        private readonly IRepository<StationMaster> _locationMaster;
        private readonly IFileService _fileService;
        public AccountStatementMultiAccountService(IRepository<MasterAccountsTable> masterAccountsTable, IRepository<JobMaster> jobMaster,
            IRepository<AccountsTransactions> accountTransaction, IRepository<StationMaster> locationMaster,
            IRepository<GetAccountTransactionDBResponse> accountResponse,
            IFileService fileService)
        {
            _masterAccountsTable = masterAccountsTable;
            _accountTransaction = accountTransaction;
            _jobMaster = jobMaster;
            _locationMaster = locationMaster;
            _accountResponse = accountResponse;
            _fileService = fileService;
        }
        public async Task<Response<List<DropdownResponse>>> GetAccountMaster(string filter)
        {
            try
            {
                var response = await _masterAccountsTable.GetAsQueryable().Where(filter).Where(x => x.MaAccType == "A")
                    .Select(x => new DropdownResponse
                    {
                        Value = x.MaAccNo,
                        Name = x.MaAccName
                    }).ToListAsync();
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetAccountMasterGroup()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value = " 1 == 1",
                    Name = " All "
                }); ;
                response.AddRange(await _masterAccountsTable.ListSelectAsync(x => x.MaAccType == "S" && x.MaStatus == "R", x => new DropdownResponse
                {
                    Value = @$" MaRelativeNo == ""{x.MaAccNo}"" ",
                    Name = x.MaAccName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }

        private async Task<List<GetAccountTransactionMutilAccountResponse>> GetAccountTransactionsMultiAccount(GenericGridViewModel model)
        {
            try
            {
                var result = await _accountResponse.GetBySPWithParameters(@$" exec GetAccountTransactionsMultiAccount {model.Skip},{model.Take},{model.Search},{model.Field},{model.Dir},{model.Filter},{model.Total}", x => new GetAccountTransactionMutilAccountResponse
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsAccName = x.AccountsTransactionsAccName,
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
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit
                    //AccountsTransactions_Alloc_Balance=x.AccountsTransactionsAllocBalance,
                    //AccountsTransactions_AllocUpdateBal=x.AccountsTransactionsAllocUpdateBal,
                    //AccountsTransactions_Alloc_Credit=x.AccountsTransactionsAllocCredit,
                    //AccountsTransactions_Alloc_Debit=x.AccountsTransactionsAllocDebit,
                    //AccountsTransactions_ApprovalDt=x.AccountsTransactionsApprovalDt,
                    //AccountsTransactions_CheqDate=x.AccountsTransactionsCheqDate,
                    //AccountsTransactions_CheqNo=x.AccountsTransactionsCheqNo,
                    //AccountsTransactions_CompanyId=x.AccountsTransactionsCompanyId,
                    //AccountsTransactions_CostCenter_Id=x.AccountsTransactionsCostCenterId,
                    //AccountsTransactions_Credit=Convert.ToDecimal(x.AccountsTransactionsCredit),
                    //AccountsTransactions_CrGram=x.AccountsTransactionsCrGram,
                    //AccountsTransactions_CurrencyId=x.AccountsTransactionsCurrencyId,
                    //AccountsTransactions_Debit=Convert.ToDecimal(x.AccountsTransactionsDebit),
                    //AccountsTransactions_DelStatus=x.AccountstransactionsDelStatus,
                    //AccountsTransactions_Department=x.AccountsTransactionsDepartment,
                    //AccountsTransactions_DeptId=x.AccountsTransactionsDeptId,
                    //AccountsTransactions_Description=x.AccountsTransactionsDescription,
                    //AccountsTransactions_DrGram=x.AccountsTransactionsDrGram,
                    //AccountsTransactions_FcAlloc_Balance=x.AccountsTransactionsFcAllocBalance,
                    //AccountsTransactions_FcAlloc_Credit=x.AccountsTransactionsFcAllocCredit,
                    //AccountsTransactions_FcAlloc_Debit=x.AccountsTransactionsFcAllocDebit,
                    //AccountsTransactions_Fc_Credit=x.AccountsTransactionsFcCredit,
                    //AccountsTransactions_Fc_Debit=x.AccountsTransactionsFcDebit,
                    //AccountsTransactions_Fc_Rate=x.AccountsTransactionsFcRate
                });
                var gridReponse = new GridWrapperResponse<List<GetAccountTransactionMutilAccountResponse>>();
                gridReponse.Data = result;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return result;
            }
            catch (Exception ex)
            {
                return new List<GetAccountTransactionMutilAccountResponse>();
            }
        }

        public async Task<Response<GridWrapperResponse<List<GetMultiAccountSummaryResponse>>>> GetAccountTransactionsMultiAccountSummary(GenericGridViewModel model)
        {
            try
            {
                var result = await GetAccountTransactionsMultiAccount(model);
                var gridmodel = new List<GetMultiAccountSummaryResponse>();
                foreach (var accounts in result.GroupBy(x => x.AccountsTransactionsAccNo))
                {
                    decimal totalDebit = 0;
                    decimal totalCredit = 0;
                    string accountName = "";
                    foreach (var item in accounts)
                    {
                        accountName = accountName != null && accountName != "" ? accountName : item.AccountsTransactionsAccName;
                        totalCredit += item.AccountsTransactionsCredit;
                        totalDebit += item.AccountsTransactionsDebit;
                    }
                    gridmodel.Add(new GetMultiAccountSummaryResponse()
                    {
                        AccountName = accountName,
                        TotalCredit = totalCredit.ToString(),
                        TotalDebit = totalDebit.ToString(),
                        AccountNo = accounts.Key,
                        CreditBalance = ((totalCredit - totalDebit) > 0 ? totalCredit - totalDebit : 0).ToString(),
                        DebitBalance = ((totalDebit - totalCredit) > 0 ? totalDebit - totalCredit : 0).ToString()
                    });
                }
                var gridReponse = new GridWrapperResponse<List<GetMultiAccountSummaryResponse>>();
                gridReponse.Data = gridmodel;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response<GridWrapperResponse<List<GetMultiAccountSummaryResponse>>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetMultiAccountSummaryResponse>>>.Fail(new GridWrapperResponse<List<GetMultiAccountSummaryResponse>>(), ex.Message);
            }
        }

        public async Task<Response<MultiAccountDetailResponse>> GetAccountTransactionsMultiAccountDetail(GenericGridViewModel model)
        {
            try
            {
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                var result = await GetAccountTransactionsMultiAccount(model);
                var modelWrapper = new MultiAccountDetailResponse();
                var gridmodel = new List<GetMultiAccountDetailResponse>();
                decimal grandTotal = 0;
                foreach (var accounts in result.GroupBy(x => x.AccountsTransactionsAccNo))
                {
                    decimal runningBalance = 0;
                    decimal totalDebit = 0;
                    decimal totalCredit = 0;
                    string accountName = string.Empty;
                    //gridmodel.Add(new GetMultiAccountDetailResponse()
                    //{
                    //    AccountName = accounts.Select(x=>x.AccountsTransactionsAccName).FirstOrDefault(),
                    //    AccountNo = accounts.Key,
                    //    Credit = totalCredit.ToString(),
                    //    Debit = totalDebit.ToString(),
                    //    Date = string.Empty,
                    //    Description = string.Empty,
                    //    RefNo = string.Empty,
                    //    VoucherNo = string.Empty,
                    //    VoucherType = string.Empty,
                    //    RunningBalance = runningBalance.ToString()
                    //});
                    foreach (var item in accounts)
                    {
                        totalCredit += item.AccountsTransactionsCredit;
                        accountName = item.AccountsTransactionsAccName;
                        totalDebit += item.AccountsTransactionsDebit;
                        runningBalance += (item.AccountsTransactionsDebit - item.AccountsTransactionsCredit) + runningBalance;
                        gridmodel.Add(new GetMultiAccountDetailResponse()
                        {
                            AccountName = item.AccountsTransactionsAccName,
                            AccountNo = item.AccountsTransactionsAccNo,
                            Credit = item.AccountsTransactionsCredit.ToString(),
                            Debit = item.AccountsTransactionsDebit.ToString(),
                            Date = item.AccountsTransactionsTransDateString,
                            Description = item.AccountsTransactionsDescription,
                            RefNo = item.RefNo,
                            VoucherNo = item.AccountsTransactionsVoucherNo,
                            VoucherType = item.AccountsTransactionsVoucherType,
                            RunningBalance = runningBalance.ToString()
                        });
                    }
                    grandTotal += totalDebit;
                    gridmodel.Add(new GetMultiAccountDetailResponse()
                    {
                        AccountName = accountName + " Total ",
                        AccountNo = string.Empty,
                        Credit = totalCredit.ToString(),
                        Debit = totalDebit.ToString(),
                        Date = string.Empty,
                        Description = string.Empty,
                        RefNo = string.Empty,
                        VoucherNo = string.Empty,
                        VoucherType = string.Empty,
                        RunningBalance = runningBalance.ToString()
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
                return Response<MultiAccountDetailResponse>.Success(modelWrapper, "Data found");
            }
            catch (Exception ex)
            {
                return Response<MultiAccountDetailResponse>.Fail(new MultiAccountDetailResponse(), ex.Message);
            }
        }


        public async Task<Response<List<StatementOfAccountFCDetailResponse>>> GetAccountTransactionsForeignCurrencyDetail(GenericGridViewModel model)
        {
            try
            {
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();

                var AccountTrans= _accountTransaction.GetAsQueryable()
                                                             .Where(x => (model.FromDate==null || x.AccountsTransactionsTransDate >= model.FromDate)
                                               && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate)
                                                && (model.Filter == null || x.AccountsTransactionsAccNo.Contains(model.Filter)))
                                             .Select(o =>
                                               new StatementOfAccountFCDetailResponse
                                               {
                                                   AccNo = o.AccountsTransactionsAccNo,
                                                   TransDate = (DateTime)o.AccountsTransactionsTransDate,
                                                   Particulars = o.AccountsTransactionsParticulars,
                                                   Debit = Convert.ToDouble(o.AccountsTransactionsFcDebit??0),
                                                   Credit = Convert.ToDouble(o.AccountsTransactionsFcCredit??0),
                                                   VoucherType = o.AccountsTransactionsVoucherType,
                                                   VoucherNo = o.AccountsTransactionsVoucherNo,
                                                   Description =o.AccountsTransactionsDescription,
                                                   RefNo = o.RefNo
                                               }).ToList();

                foreach(var account in AccountTrans)
                {
                   if(account.Debit>account.Credit)
                    {
                        account.RunningBalance = account.RunningBalance + account.Debit;
                    }

                    if (account.Credit > account.Debit)
                    {
                        account.RunningBalance = account.RunningBalance - account.Credit;
                    }
                }

                //If DrBala > CrBala Then
                //          RunningBala = RunningBala + DrBala
                //        ElseIf CrBala > DrBala Then
                //            RunningBala = RunningBala - CrBala
                //        End If
                 return Response<List<StatementOfAccountFCDetailResponse>>.Success(AccountTrans, "Data found");
            }
            catch (Exception ex)
            {
                 return Response<List<StatementOfAccountFCDetailResponse>>.Fail(new List<StatementOfAccountFCDetailResponse>(), ex.Message);
            }
        }


        public async Task<Response<StatementOfAccountFCSummResponse>> GetAccountTransactionsForeignCurrencySummary(GenericGridViewModel model)
        {
            try
            {
                //var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();

                var AccountTrans = _accountTransaction.GetAsQueryable()
                                                             .Where(x => (model.FromDate == null || x.AccountsTransactionsTransDate >= model.FromDate)
                                               && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate)
                                                //&&  model.accountNos.Contains(x.AccountsTransactionsAccNo)
                                                && x.AccountsTransactionsAccNo.ToLower() == model.Filter.ToLower()
                                               )
                                                            .GroupBy(r => r.AccountsTransactionsAccNo).Select(o =>

                                               new StatementOfAccountFCSummResponse
                                               {
                                                   AccNo = o.Key,
                                                   TotalDebit = Convert.ToDouble(o.Sum(pc => pc.AccountsTransactionsFcDebit ?? 0)),
                                                   TotalCredit = Convert.ToDouble(o.Sum(pc => pc.AccountsTransactionsFcCredit ?? 0)),

                                               }).ToList();

                //var AccountTrans = _accountTransaction.GetAsQueryable()
                //                                       .Where(x => (model.FromDate == null || x.AccountsTransactionsTransDate >= model.FromDate)
                //                         && (model.ToDate == null || x.AccountsTransactionsTransDate <= model.ToDate)
                //                          //&&  model.accountNos.Contains(x.AccountsTransactionsAccNo)
                //                          && x.AccountsTransactionsAccNo.ToLower() == model.Filter.ToLower()
                //                         )
                //                                          //.GroupBy(r => r.AccountsTransactionsAccNo).Select(o =>
                //                                          .GroupBy(c => new
                //                                          {
                //                                              c.AccountsTransactionsAccNo
                //                                              //c.A

                //                                          }).Select(o =>
                //                         new StatementOfAccountFCSummResponse
                //                         {
                //                             AccNo = o.Key.AccountsTransactionsAccNo,
                //                             TotalDebit = Convert.ToDouble(o.Sum(pc => pc.AccountsTransactionsFcDebit ?? 0)),
                //                             TotalCredit = Convert.ToDouble(o.Sum(pc => pc.AccountsTransactionsFcCredit ?? 0)),

                //                         }).ToList();

                if (AccountTrans.Count > 0)
                { 
                    double Total_Dr = AccountTrans.Sum(x => Convert.ToDouble(x.TotalDebit));
                double Total_Cr = AccountTrans.Sum(x => Convert.ToDouble(x.TotalCredit));
                var acctrans = new StatementOfAccountFCSummResponse
                {
                    AccNo = AccountTrans.FirstOrDefault().AccNo,
                    TotalDebit = Total_Dr,
                    TotalCredit = Total_Cr
                };
                if (acctrans.TotalDebit > 0)
                {
                    acctrans.DebitBalance = acctrans.TotalDebit;
                }
                else
                {
                    acctrans.DebitBalance = 0;
                }
                if (acctrans.TotalCredit > 0)
                {
                    acctrans.CreditBalance = acctrans.TotalCredit;
                }
                else
                {
                    acctrans.CreditBalance = 0;
                }

                if (acctrans.DebitBalance >= acctrans.CreditBalance)
                {
                    acctrans.DebitBalance = acctrans.DebitBalance - acctrans.CreditBalance;
                    acctrans.CreditBalance = 0;
                }
                if (acctrans.CreditBalance > acctrans.DebitBalance)
                {
                    acctrans.DebitBalance = 0;
                    acctrans.CreditBalance = acctrans.CreditBalance - acctrans.DebitBalance;
                }
                return Response<StatementOfAccountFCSummResponse>.Success(acctrans, "Data found");
            }
                else
                {
                    return Response<StatementOfAccountFCSummResponse>.Success(new StatementOfAccountFCSummResponse(), "No Data found");
                }
               
            }
            catch (Exception ex)
            {
                return Response<StatementOfAccountFCSummResponse>.Fail(new StatementOfAccountFCSummResponse(), ex.Message);
            }
        }  

        public async Task<Response<ReturnPrintResponse>> VoucherPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "MultiAccount";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetAccountTransactionsMultiAccount(model);
                if (dbResult.Count < 1)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "Account", "multiaccountstyles.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = GetHTMLString(dbResult),
                    CssPath = cssPath,
                    OutputPath = outPutPath
                };
                var pdf = await _fileService.CreatePDFFromHtml(pDFResponse);
                if (pdf.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No PDF Generated");
                }
                if (model.Extension == "PDF")
                {
                    var downloadPDF = await _fileService.DownloadFile(pdf.Result);
                    var pdfContentType = await _fileService.GetContentType(pdf.Result);
                    response.stream = downloadPDF.Result;
                    response.ContentType = pdfContentType.Result;
                    response.Path = pdf.Result;
                    return Response<ReturnPrintResponse>.Success(response, "File Created");
                }
                var file = await _fileService.CreateFileFromExtension(new GetFileFromExtensionResponse()
                {
                    Extension = model.Extension,
                    Path = pdf.Result,
                    FileName = fileName
                });
                if (file.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No File Generated");
                }
                var download = await _fileService.DownloadFile(file.Result);
                var ContentType = await _fileService.GetContentType(file.Result);
                response.stream = download.Result;
                response.ContentType = ContentType.Result;
                response.Path = file.Result;
                return Response<ReturnPrintResponse>.Success(response, "File Created");
            }
            catch (Exception ex)
            {
                return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), ex.Message);
            }
        }
        private string GetHTMLString(List<GetAccountTransactionMutilAccountResponse> records)
        {
            var results = records;
            var sb = new StringBuilder();
            sb.Append(@"
                        <!DOCTYPE html>
<html>

<head>
    <meta charset='UTF - 8' />
    <meta http - equiv = 'X-UA-Compatible' content = 'IE=edge' />
    <meta name='viewport'content='width=device-width,initial-scale=1.0'/>
    <link rel ='stylesheet'href='MAS.css'/>
    <title> Multi Account Statement </title>
</head>
<body>
    <div class='abc'>
        <h1>GulfWay Delivery Services LLC</h1>
        <h3>Address: Building No. 317-0, Street 613-936, Ras Al Khor Indus</h3>
        <h3>Tel:34555-566</h3>
        <h3>Fax:34555-566</h3>
        <h3>Email:abcd13 @gmail.com</h3>
        <h4>ACCOUNT STATEMENT</h4>
    </div>
    <table class='def'>
        <tr>
            <th>VNo.</th>
            <th>VDate</th>
            <th>Voucher type</th>
            <th>Description</th>
            <th>Debit</th>
            <th>Credit</th>
            <th>Balance</th>
        </tr>
        <tbody>
<tr class='AE'>
                <td colspan='7'>
                    <strong> Absolute Royal Technologies LLC </strong>
                </td>
            </tr>
");
            foreach (var item in records)
            {
                sb.Append(@$"<tr>
                    <td>{item?.AccountsTransactionsVoucherNo}</td>
                    <td>{item?.AccountsTransactionsTransDate}</td>
                    <td>{item?.AccountsTransactionsVoucherType}</td>
                    <td>{item?.AccountsTransactionsDescription}</td>
                    <td>{item?.AccountsTransactionsDebit}</td>
                    <td>{item?.AccountsTransactionsCredit}</td>
                    <td>{item?.AccountsTransactionsAllocBalance}</td>
                    </tr>
                ");
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
