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
    public class VoucherPrintService : IVoucherPrintService
    {
        private readonly IRepository<MasterAccountsTable> _masterAccountsTable;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<VouchersNumbers> _voucherNumber;
        private readonly IFileService _fileService;
        private readonly IRepository<StationMaster> _locationMaster;
        public VoucherPrintService(IRepository<AccountsTransactions> accountTransaction, IFileService fileService,
            IRepository<MasterAccountsTable> masterAccountsTable, IRepository<StationMaster> locationMaster,
            IRepository<VouchersNumbers> voucherNumber)
        {
            _accountTransaction = accountTransaction;
            _voucherNumber = voucherNumber;
            _masterAccountsTable = masterAccountsTable;
            _fileService = fileService;
            _locationMaster = locationMaster;
        }
        public async Task<Response<List<DropdownResponse>>> GetVoucherTypeDropdown()
        {
            try
            {
                var response =new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value="",
                    Name="All"
                });
                 response.AddRange( await _voucherNumber.GetAsQueryable().Select(x => x.VouchersNumbersVType)
                    .Distinct().Select(x => new DropdownResponse
                    {
                        Value = x,
                        Name = x
                    }).ToListAsync());
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetVoucherNoDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value = "",
                    Name = "All"
                });
               response.AddRange( await _voucherNumber.GetAsQueryable().Select(x => new DropdownResponse
                {
                    Value = x.VouchersNumbersVNo,
                    Name = x.VouchersNumbersVType + @$" ({x.VouchersNumbersVNo})"
                }).ToListAsync());
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<GetVoucherPrintingResponse>> GetAccountTransactions(GenericGridViewModel model)
        {
            try
            {
                GetVoucherPrintingResponse gridModel = new GetVoucherPrintingResponse();
                gridModel.Details = new List<GetAccountTransactionResponse>();
                string query = @$" 1==1 {model.Filter}";
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                //var a = await _accountTransaction.GetAsQueryable().Where(x =>( x.AccountsTransactionsTransDate >= Convert.ToDateTime("")) &&  )
                //    .ToListAsync();
                var result = await _accountTransaction.GetAsQueryable().Where(query).Select(x => new GetAccountTransactionResponse
                {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsAccName = _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo) != null ?_masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo).MaAccName:"",
                    AccountsTransactionsDebit = Convert.ToDecimal(x.AccountsTransactionsDebit),
                    AccountsTransactionsCredit = Convert.ToDecimal(x.AccountsTransactionsCredit),
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
                foreach (var x in result)
                {
                    gridModel.Details.Add(new GetAccountTransactionResponse()
                    {
                        AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
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
                    });
                }
                gridModel.StationMasterStationName = location != null ? location.StationMasterStationName : "";
                gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                return Response<GetVoucherPrintingResponse>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GetVoucherPrintingResponse>.Fail(new GetVoucherPrintingResponse(), ex.Message);
            }
        }
        public async Task<Response<ReturnPrintResponse>> DownloadMultiAccountPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "VoucherPrint";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetAccountTransactions(model);
                if (dbResult.Result.Details.Count < 1)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(),  "Assets","Styles","Account", "voucherStyles.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = GetHTMLString(dbResult.Result.Details),
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
                    var pdfContentType=await _fileService.GetContentType(pdf.Result);
                    response.stream = downloadPDF.Result;
                    response.ContentType = pdfContentType.Result;
                    response.Path = pdf.Result;
                    return Response<ReturnPrintResponse>.Success(response, "File Created");
                }
                var file = await _fileService.CreateFileFromExtension(new GetFileFromExtensionResponse()
                {
                    Extension = model.Extension,
                    Path = pdf.Result,
                    FileName=fileName
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
        private string GetHTMLString(List<GetAccountTransactionResponse> records)
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
