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
    public class DaybookService : IDaybookService
    {
        private readonly IRepository<MasterAccountsTable> _masterAccountsTable;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<StationMaster> _locationMaster;
        private IRepository<AccountSettings> _accountsSettingsRepo;
        private readonly IFileService _fileService;
        public DaybookService(IRepository<MasterAccountsTable> masterAccountsTable, IFileService fileService, IRepository<AccountSettings> accountsSettingsRepo,
            IRepository<AccountsTransactions> accountTransaction, IRepository<StationMaster> locationMaster)
        {
            _masterAccountsTable = masterAccountsTable;
            _accountTransaction = accountTransaction;
            _fileService = fileService; _accountsSettingsRepo = accountsSettingsRepo;
            _locationMaster = locationMaster;
        }
        public async Task<Response<GridWrapperResponse<GetDaybookResponse>>> GetDaybookAccountTransactions(GenericGridViewModel model)
        {
            try
            {
                var gridModel = new GetDaybookResponse();
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                var total = await _accountTransaction.GetAsQueryable().AsNoTracking().CountAsync(x => x.AccountstransactionsDelStatus != true);

                var cashacc = _accountsSettingsRepo.GetAsQueryable().AsNoTracking().Where(a => a.AccountSettingsAccountDescription.Trim() == "Cash Account").Select(x => x.AccountSettingsAccountTextValue).FirstOrDefault();
                string[] parts = cashacc.Split(new string[] { "::" }, StringSplitOptions.None);

                string accountNumber = parts.Length > 1 ? parts[1].Trim() : string.Empty;
                GetDaybookAccountTransaction data = null;

                IQueryable<AccountsTransactions> queryData;
                if (model.isDate)
                {
                    queryData = _accountTransaction.GetAsQueryable().AsNoTracking().Where(a => a.AccountsTransactionsAccNo != accountNumber && a.AccountsTransactionsTransDate >= model.FromDate && a.AccountsTransactionsTransDate <= model.ToDate).OrderBy(row => row.AccountsTransactionsTransDate ?? DateTime.MaxValue);
                    var openingentry = _accountTransaction.GetAsQueryable().AsNoTracking().Where(a => a.AccountsTransactionsAccNo == accountNumber && a.AccountsTransactionsTransDate < model.FromDate).ToList();

                    decimal totalDebits = openingentry.Sum(a => a.AccountsTransactionsDebit);
                    decimal totalCredits = openingentry.Sum(a => a.AccountsTransactionsCredit);

                    decimal openingBalance = totalCredits - totalDebits;

                    data = new GetDaybookAccountTransaction
                    {
                        AccountsTransactionsTransDateString = "",
                        AccountsTransactionsVoucherNo = "",
                        RefNo = "",
                        AccountsTransactionsDescription = "",
                        AccountsTransactionsParticulars = "",
                        AccountsTransactionsDebit = openingBalance < 0 ? openingBalance : 0,
                        AccountsTransactionsCredit = openingBalance > 0 ? openingBalance : 0,
                        AccountsTransactionsVoucherType = "Opening Balance"
                    };

                }
                else
                {
                    queryData = _accountTransaction.GetAsQueryable().AsNoTracking().Where(a => a.AccountsTransactionsAccNo != accountNumber).OrderBy(row => row.AccountsTransactionsTransDate ?? DateTime.MaxValue);
                }
                var result = await queryData.Skip(model.Skip).Take(model.Take).Select(x => new GetDaybookAccountTransaction
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsAccName = _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo).MaAccName,
                    AccountsTransactionsDebit = x.AccountsTransactionsDebit,
                    AccountsTransactionsCredit = x.AccountsTransactionsCredit,
                    AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                    AccountsTransactionsVoucherNo = x.AccountsTransactionsVoucherNo,
                    AccountsTransactionsVoucherType = x.AccountsTransactionsVoucherType,
                    AccountsTransactionsDescription = x.AccountsTransactionsDescription,
                    AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                    AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                    AccountsTransactionsParticulars = x.AccountsTransactionsParticulars,
                    RefNo = x.RefNo,
                    AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit
                }).ToListAsync();
                var groupResponse = result.GroupBy(x => x.AccountsTransactionsTransDate);
                gridModel.Details = new List<GetDaybookAccountTransaction>();
                decimal totalDebit = 0;
                decimal totalCredit = 0;
                foreach (var item in groupResponse)
                {
                    decimal credit = 0;
                    decimal debit = 0;
                    foreach (var records in item)
                    {
                        if (model.isDate)
                        {
                            gridModel.Details.Insert(0, data);
                        }
                        gridModel.Details.Add(new GetDaybookAccountTransaction()
                        {
                            AccountsTransactionsTransDateString = records.AccountsTransactionsTransDateString,
                            AccountsTransactionsVoucherNo = records.AccountsTransactionsVoucherNo,
                            RefNo = records.RefNo,
                            AccountsTransactionsDescription = records.AccountsTransactionsDescription,
                            AccountsTransactionsParticulars = records.AccountsTransactionsParticulars,
                            AccountsTransactionsDebit = records.AccountsTransactionsDebit,
                            AccountsTransactionsCredit = records.AccountsTransactionsCredit,
                            AccountsTransactionsVoucherType = records.AccountsTransactionsVoucherType
                        });
                    }
                    //credit += records.AccountsTransactionsCredit;
                    //debit += records.AccountsTransactionsDebit;

                    //gridModel.Details.Add(new GetDaybookAccountTransaction()
                    //{
                    //    AccountsTransactionsParticulars = "Total",
                    //    AccountsTransactionsDebit = debit,
                    //    AccountsTransactionsCredit = credit,
                    //});
                    //totalCredit += credit;
                    //totalDebit += debit;
                }
                //gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                //gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                //gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                //gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                //gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                //gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                //gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                //gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                //gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                //gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                //gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                //gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                //gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                //gridModel.TotalCredit = totalCredit.ToString();
                //gridModel.TotalDebit = totalDebit.ToString();
                var gridReponse = new GridWrapperResponse<GetDaybookResponse>()
                {
                    Data = gridModel,
                    Total = total
                };
                return Response<GridWrapperResponse<GetDaybookResponse>>.Success(gridReponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<GetDaybookResponse>>.Fail(new GridWrapperResponse<GetDaybookResponse>(), ex.Message);
            }
        }
        public async Task<Response<ReturnPrintResponse>> DaybookPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "Daybook";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetDaybookAccountTransactions(model);
                if (dbResult.Result.Data.Details.Count < 1)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "Account", "daybookStyles.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = GetHTMLString(dbResult.Result.Data.Details),
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
        private string GetHTMLString(List<GetDaybookAccountTransaction> records)
        {
            var results = records;
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Voucher Type</th>
                                        <th>Voucher Number</th>
                                        <th>Account Number</th>
                                        <th>Accout Name</th>
                                            <th>Particulars</th>
                                         <th>Date</th>
                                         <th>Debit</th>
                                         <th>Credit</th>
                                    </tr>");
            foreach (var item in results)
            {
                sb.Append(@$"<tr>
                                    <td>{item.AccountsTransactionsVoucherType}</td>
                                    <td>{item.AccountsTransactionsVoucherNo}</td>
                                    <td>{item.AccountsTransactionsAccNo}</td>
                                    <td>{item.AccountsTransactionsAccName}</td>
                                    <td>{item.AccountsTransactionsParticulars}</td>
                                    <td>{item.AccountsTransactionsTransDateString}</td>
                                    <td>{item.AccountsTransactionsDebit}</td>
                                    <td>{item.AccountsTransactionsCredit}</td>
                                  </tr>");
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
