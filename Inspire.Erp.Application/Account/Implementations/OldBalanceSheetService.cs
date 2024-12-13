using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
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
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class OldBalanceSheetService : IOldBalanceSheetService
    {
        private readonly IRepository<VouchersNumbers> _voucherNumber;
        private readonly IRepository<SalesVoucher> _salesVoucher;
        private readonly IRepository<OldBsMaster> _oldBsMaster;
        private readonly IRepository<JournalVoucher> _journalVoucher;
        private readonly IRepository<JournalVoucherDetails> _journalVoucherDetail;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<OldBsMasterDetails> _oldBsMasterDetail;
        private readonly IRepository<MasterAccountsTable> _masterAccount;
        private readonly IFileService _fileService;
        private readonly IUtilityService _utilityService;
        private readonly IRepository<FinancialPeriods> _financialPeriods;
        private readonly string Jv_Type = "JOURNAL";
        public OldBalanceSheetService(IRepository<VouchersNumbers> voucherNumber, IRepository<JournalVoucher> journalVoucher, IFileService fileService
            , IRepository<OldBsMaster> oldBsMaster, IRepository<JournalVoucherDetails> journalVoucherDetail, IRepository<FinancialPeriods> financialPeriods,
            IRepository<OldBsMasterDetails> oldBsMasterDetail, IRepository<MasterAccountsTable> masterAccount, IUtilityService utilityService,
            IRepository<SalesVoucher> salesVoucher, IRepository<AccountsTransactions> accountTransaction)
        {
            _voucherNumber = voucherNumber;
            _salesVoucher = salesVoucher;
            _oldBsMaster = oldBsMaster;
            _oldBsMasterDetail = oldBsMasterDetail;
            _accountTransaction = accountTransaction;
            _journalVoucher = journalVoucher;
            _journalVoucherDetail = journalVoucherDetail;
            _financialPeriods = financialPeriods;
            _fileService = fileService;
            _masterAccount = masterAccount;
        }
        private decimal GetNewVoucherNo()
        {
            try
            {
                var voucherNumber = _voucherNumber.GetAsQueryable();
                var sales = _salesVoucher.GetAsQueryable();
                decimal? maxValue = 0;
                if (true)
                {
                    maxValue = voucherNumber.Where(x => x.VouchersNumbersVType == Jv_Type)
                         .Max(x => x.VouchersNumbersVsno);
                }
                else
                {

                    maxValue = voucherNumber    // your starting point - table in the "from" statement
                       .Join(sales, // the source table of the inner join
                          voucher => voucher.VouchersNumbersVNo,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                          sales => sales.SalesVoucherShortNo.ToString(),   // Select the foreign key (the second part of the "on" clause)
                          (voucher, sales) => new
                          {
                              Vno = (voucher.VouchersNumbersVNoNu != null ? Convert.ToDecimal(voucher.VouchersNumbersVNoNu) : 0),
                              VoucherType = voucher.VouchersNumbersVType,
                              SalesVoucherType = sales.SalesVoucherType
                          }) // selection
                       .Where(salesAndVoucher => salesAndVoucher.VoucherType == "SALES" && salesAndVoucher.SalesVoucherType.ToString() == "").Max(x => x.Vno);
                }

                return maxValue != null ? Convert.ToDecimal(maxValue) : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async Task<string> InsertVoucherNumber(string oldJvNO = "")
        {
            try
            {
                string tempJvNo = oldJvNO;
                if (oldJvNO == "")
                {
                    decimal vShortNumber = GetNewVoucherNo() + 1;
                    tempJvNo = vShortNumber.ToString();
                    _voucherNumber.Insert(new VouchersNumbers()
                    {
                        VouchersNumbersVNo = tempJvNo,
                        VouchersNumbersVNoNu = vShortNumber,
                        VouchersNumbersVType = Jv_Type,
                        VouchersNumbersFsno = null,
                        VouchersNumbersStatus = "P",
                        VouchersNumbersLocationId = 0
                    });
                    #region ADD ACTIVITY LOGS
                    AddActivityLogViewModel log = new AddActivityLogViewModel()
                    {
                        Page = "Old Balance Sheet",
                        Section = "Add Old Balance Sheet",
                        Entity = "Old Balance Sheet",

                    };
                    await _utilityService.AddUserTrackingLog(log);
                    #endregion
                    _voucherNumber.SaveChangesAsync();
                }

                return tempJvNo;
            }
            catch (Exception ex)
            {
                return oldJvNO;
            }
        }
        private async Task<int?> InsertOldBsMaster(int? oldbsFiD, string tempJvNO, int? financialId = null, bool isBsExisting = true)
        {
            try
            {
                if (isBsExisting)
                {
                    var result = _oldBsMaster.GetAsQueryable().FirstOrDefault(x => x.OldBsMasterId == oldbsFiD);
                    result.OldBsMasterPost = "P";
                    result.OldBsMasterFsno = financialId;
                    result.OldBsMasterRefJv = tempJvNO;
                    _oldBsMaster.Update(result);
                    #region ADD ACTIVITY LOGS
                    AddActivityLogViewModel log = new AddActivityLogViewModel()
                    {
                        Page = "Old Balance Sheet",
                        Section = "Update Old Balance Sheet",
                        Entity = "Old Balance Sheet",

                    };
                    await _utilityService.AddUserTrackingLog(log);
                    #endregion
                    _oldBsMaster.SaveChangesAsync();

                    var deleteDetails = _oldBsMasterDetail.GetAsQueryable().Where(x => x.OldBsMasterDetailsSno == oldbsFiD).ToList();
                    _oldBsMasterDetail.DeleteList(deleteDetails);
                    #region ADD ACTIVITY LOGS
                    AddActivityLogViewModel Deletelog = new AddActivityLogViewModel()
                    {
                        Page = "Old Balance Sheet",
                        Section = "Delete Old Balance Sheet",
                        Entity = "Old Balance Sheet",

                    };
                    await _utilityService.AddUserTrackingLog(Deletelog);
                    #endregion
                    _oldBsMasterDetail.SaveChangesAsync();
                }
                else
                {
                    Random rnd = new Random();
                    int bs = rnd.Next();
                    _oldBsMaster.Insert(new OldBsMaster()
                    {
                        OldBsMasterId = bs,
                        OldBsMasterDescription = "Old Balance Sheet Posting",
                        OldBsMasterPost = "NP",
                        OldBsMasterRefJv = tempJvNO,
                        OldBsMasterFsno = financialId
                    });
                    #region ADD ACTIVITY LOGS
                    AddActivityLogViewModel log = new AddActivityLogViewModel()
                    {
                        Page = "Old Balance Sheet",
                        Section = "Post Old Balance Sheet",
                        Entity = "Old Balance Sheet",

                    };
                    await _utilityService.AddUserTrackingLog(log);
                    #endregion
                    _oldBsMaster.SaveChangesAsync();
                    oldbsFiD = bs;
                }

                return oldbsFiD;
            }
            catch (Exception ex)
            {
                return oldbsFiD;
            }
        }
        private void CheckJVNoAndDeleteRecords(int? olDJvNO = null, int? financialId = null)
        {
            try
            {
                if (olDJvNO != null)
                {
                    var journalVoucher = _journalVoucher.GetAsQueryable().Where(x => x.JournalVoucherId == olDJvNO).ToList();
                    _journalVoucher.DeleteList(journalVoucher);
                    _journalVoucher.SaveChangesAsync();

                    var journalVoucherDetail = _journalVoucherDetail.GetAsQueryable().Where(x => x.JournalVoucherDetailsId== olDJvNO).ToList();
                    _journalVoucherDetail.DeleteList(journalVoucherDetail);
                    _journalVoucherDetail.SaveChangesAsync();

                    var accountTransaction = _accountTransaction.GetAsQueryable()
                        .Where(x => x.AccountsTransactionsVoucherNo == olDJvNO.ToString() && x.AccountsTransactionsFsno == financialId && x.AccountsTransactionsVoucherType == Jv_Type)
                        .ToList();
                    _accountTransaction.DeleteList(accountTransaction);
                    _accountTransaction.SaveChangesAsync();

                    var voucherNumber = _voucherNumber.GetAsQueryable()
                        .FirstOrDefault(x => x.VouchersNumbersVNo == olDJvNO.ToString().Trim()
                        && x.VouchersNumbersVType == Jv_Type);
                    voucherNumber.VouchersNumbersStatus = "D";
                    _voucherNumber.Delete(voucherNumber);
                    _voucherNumber.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
            }
        }
        private bool AddRecordsInBalanceSheet(AddOldBalanceSheetResponse model, int? tempJvNO, int? oldbsId)
        {
            try
            {
                Random rnd = new Random();
                int id = rnd.Next();
                int journalVoucher = rnd.Next();
                _journalVoucher.Insert(new JournalVoucher()
                {
                    JournalVoucherId = (int)tempJvNO,
                    // JournalVoucher_ID = Convert.ToInt32(tempJvNO),
                    JournalVoucherDate= model.FinancialPeriodsStartDate,
                    JournalVoucherAmount = (double)model.TotalAmount,
                    JournalVoucherNarration= "Opening Balance",
                    // JournalVoucherFsno = model.OldBsMasterFsno,
                    JournalVoucherRefNo = model.OldBsMasterId.ToString(),
                    // JournalVoucher_DelStatus = "PB",
                    JournalVoucherUserId = null,
                    JournalVoucherType= Jv_Type
                });
                _journalVoucher.SaveChangesAsync();
                foreach (var details in model.OldBsMasterDetails)
                {
                    var accountNo = details.OldBsMasterDetailsAccNo;
                    var amount = details.Debit != null ? details.Debit : details.Credit;
                    var narration = details.OldBsMasterDetailsNarration;
                    _oldBsMasterDetail.Insert(new OldBsMasterDetails()
                    {
                        OldBsMasterDetailsId = id,
                        OldBsMasterDetailsSno = oldbsId,
                        OldBsMasterDetailsAccNo = accountNo,
                        OldBsMasterDetailsFsno = model.OldBsMasterFsno,
                        OldBsMasterDetailsDrCrType = details.Debit != null ? "D" : "C",
                        OldBsMasterDetailsNarration = details.OldBsMasterDetailsNarration,
                        OldBsMasterDetailsGroupName = "Equity",
                        OldBsMasterDetailsFcAmount = amount,
                        OldBsMasterDetailsBcAmount = amount
                    });
                    _oldBsMasterDetail.SaveChangesAsync();

                    _journalVoucherDetail.Insert(new JournalVoucherDetails()
                    {
                        JournalVoucherDetailsId = (int)tempJvNO,
                        JournalVoucherDetailsReferenceNo = journalVoucher.ToString(),
                        JournalVoucherDetailsAccNo = accountNo,
                        // JournalVoucherDetails_Narration = details.Debit != null ? "D" : "C",
                        // JournalVoucherDetailsFcAmount = amount,
                        // JournalVoucherDetailsAmount = amount,
                        // JournalVoucherDetails_Reference_No = model.OldBsMasterFsno,
                        JournalVoucherDetailsNarration = "Opening Balance"
                    });
                    _journalVoucherDetail.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Response<bool>> AddEditRecordsInBalanceSheet(AddOldBalanceSheetResponse model)
        {
            try
            {

                string tempJVno = await InsertVoucherNumber(model.OldBsMasterId.ToString());
                int? returnId =await InsertOldBsMaster(model.OldBsMasterId, tempJVno, model.OldBsMasterFsno, model.IsBsExisting);
                CheckJVNoAndDeleteRecords(model.OldBsMasterId, model.OldBsMasterFsno);
                AddRecordsInBalanceSheet(model, Convert.ToInt32(tempJVno), returnId);
                return Response<bool>.Success(true, "Changes Saved Successfully.");
            }
            catch (Exception ex)
            {

                return Response<bool>.Fail(false, ex.Message);
            }
        }


        public async Task<Response<AddOldBalanceSheetResponse>> GetBalanceSheetList(GenericGridViewModel model)
        {
            try
            {
                AddOldBalanceSheetResponse response = new AddOldBalanceSheetResponse();
                var financialPeriod = _financialPeriods.GetAsQueryable().FirstOrDefault(x => x.FinancialPeriodsStatus == "R");
                var bs = await _oldBsMaster.GetAsQueryable().Where(x => x.OldBsMasterFsno == financialPeriod.FinancialPeriodsFsno).FirstOrDefaultAsync();
                response.OldBsMasterId = bs != null ? bs.OldBsMasterId : null;
                response.OldBsMasterPost = bs != null ? bs.OldBsMasterPost : null;
                response.OldBsMasterRefJv = bs != null ? bs.OldBsMasterRefJv : null;
                response.JvNO = bs != null ? bs.OldBsMasterRefJv : null;
                response.OldBsMasterFsno = bs != null ? bs.OldBsMasterFsno : financialPeriod.FinancialPeriodsFsno;
                response.FinancialPeriodsStartDate = bs != null ? financialPeriod.FinancialPeriodsStartDate : financialPeriod.FinancialPeriodsStartDate;
                response.IsBsExisting = bs != null ? true : false;
                response.OldBsMasterDetails = new List<AddOldBalanceSheetDetailResponse>();
                if (bs != null)
                {
                    var detailbs = await _oldBsMasterDetail.GetAsQueryable().Where(x => x.OldBsMasterDetailsSno == bs.OldBsMasterId).ToListAsync();
                    foreach (var dbs in detailbs)
                    {
                        response.OldBsMasterDetails.Add(new AddOldBalanceSheetDetailResponse()
                        {
                            Credit = dbs.OldBsMasterDetailsDrCrType == "C" ? dbs.OldBsMasterDetailsFcAmount : null,
                            Debit = dbs.OldBsMasterDetailsDrCrType == "D" ? dbs.OldBsMasterDetailsFcAmount : null,
                            OldBsMasterDetailsDrCrType = dbs.OldBsMasterDetailsDrCrType,
                            OldBsMasterDetailsFsno = dbs.OldBsMasterDetailsFsno,
                            OldBsMasterDetailsId = dbs.OldBsMasterDetailsId,
                            OldBsMasterDetailsNarration = dbs.OldBsMasterDetailsNarration,
                            OldBsMasterDetailsGroupName = dbs.OldBsMasterDetailsGroupName,
                            OldBsMasterDetailsAccNo = dbs.OldBsMasterDetailsAccNo,
                            OldBsMasterDetailsAccName = _masterAccount.GetAsQueryable().FirstOrDefault(x => x.MaAccNo == dbs.OldBsMasterDetailsAccNo).MaAccName
                        });
                    }
                }
                return Response<AddOldBalanceSheetResponse>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<AddOldBalanceSheetResponse>.Fail(new AddOldBalanceSheetResponse(), ex.Message);
            }
        }
        public async Task<Response<ReturnPrintResponse>> OldBalanceSheetPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "OldBalanceSheet";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetBalanceSheetList(model);
                // if (dbResult.ReturnObject == null)
                // {
                //     return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                // }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "Account", "oldBalanceSheet.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    // Html = GetHTMLString(dbResult.ReturnObject),
                    CssPath = cssPath,
                    OutputPath = outPutPath
                };
                var pdf = await _fileService.CreatePDFFromHtml(pDFResponse);
                // if (pdf.ReturnObject.Trim(' ') == "")
                // {
                //     return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No PDF Generated");
                // }
                if (model.Extension == "PDF")
                {
                    // var downloadPDF = await _fileService.DownloadFile(pdf.ReturnObject);
                    // var pdfContentType = await _fileService.GetContentType(pdf.ReturnObject);
                    // response.stream = downloadPDF.ReturnObject;
                    // response.ContentType = pdfContentType.ReturnObject;
                    // response.Path = pdf.ReturnObject;
                    return Response<ReturnPrintResponse>.Success(response, "File Created");
                }
                var file = await _fileService.CreateFileFromExtension(new GetFileFromExtensionResponse()
                {
                    Extension = model.Extension,
                    // Path = pdf.ReturnObject,
                    FileName = fileName
                });
                // if (file.ReturnObject.Trim(' ') == "")
                // {
                //     return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No File Generated");
                // }
                // var download = await _fileService.DownloadFile(file.ReturnObject);
                // var ContentType = await _fileService.GetContentType(file.ReturnObject);
                // response.stream = download.ReturnObject;
                // response.ContentType = ContentType.ReturnObject;
                // response.Path = file.ReturnObject;
                return Response<ReturnPrintResponse>.Success(response, "File Created");
            }
            catch (Exception ex)
            {
                return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), ex.Message);
            }
        }
        private string GetHTMLString(AddOldBalanceSheetResponse model)
        {
            var results = model;
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
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
