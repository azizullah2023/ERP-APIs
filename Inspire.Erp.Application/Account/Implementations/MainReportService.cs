using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spire.Pdf.Exporting.XPS.Schema;
using DinkToPdf;
using SendGrid.Helpers.Mail;
using Inspire.Erp.Domain.Modals;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class MainReportService : IMainReportService
    {




        private readonly ILogger<PaymentVoucherService> _logger;

        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<UnitDetails> _unitDetailsRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<ReportStockBaseUnit> _reportStockBaseUnitRepository;
        private IRepository<ReportStockBaseUnit> _reportStockBaseUnitLocationRepository;

        private IRepository<ReportSalesVoucher> _reportSalesVoucherRepository;

        private IRepository<ReportPurchaseVoucher> _reportPurchaseVoucherRepository;

        private IRepository<TempReportAccStatement> _tempReportAccStatementRepository;
        private IRepository<ReportStockRegister> _reportStockRegisterRepository;
        private IRepository<ViewAccountsTransactionsVoucherType> _viewAccountsTransactionsVoucherTypeRepository;
        private IRepository<ReportAccountsTransactions> _reportAccountsTransactionsRepository;
        private IRepository<ReportPurchaseRequisition> _reportPurchaseRequisitionRepository;
        private IRepository<ReportPurchaseOrder> _reportPurchaseOrderRepository;
        public MainReportService(
              IRepository<ItemMaster> itemMasterRepository,
              IRepository<ReportStockBaseUnit> reportStockBaseUnitRepository,
            IRepository<ReportStockBaseUnit> reportStockBaseUnitLocationRepository,




              IRepository<ReportSalesVoucher> reportSalesVoucherRepository,
               IRepository<ReportPurchaseVoucher> reportPurchaseVoucherRepository,

        IRepository<ReportAccountsTransactions> reportAccountsTransactionsRepository,
                   IRepository<ReportStockRegister> reportStockRegisterRepository,
                   IRepository<ViewAccountsTransactionsVoucherType> viewAccountsTransactionsVoucherTypeRepository,
                   IRepository<TempReportAccStatement> tempReportAccStatementRepository,
                   IRepository<ReportPurchaseRequisition> reportPurchaseRequisitionRepository,
                   IRepository<ReportPurchaseOrder> reportPurchaseOderRepository,
                 IRepository<StockRegister> stockRegisterRepository,
                IRepository<UnitDetails> unitDetailsRepository,
                IRepository<UnitMaster> unitMasterRepository

                  )
        {

            _itemMasterRepository = itemMasterRepository;
            _reportStockBaseUnitRepository = reportStockBaseUnitRepository;
            _reportStockBaseUnitLocationRepository = reportStockBaseUnitLocationRepository;


            _reportSalesVoucherRepository = reportSalesVoucherRepository;
            _reportPurchaseVoucherRepository = reportPurchaseVoucherRepository;


            _reportAccountsTransactionsRepository = reportAccountsTransactionsRepository;
            _reportStockRegisterRepository = reportStockRegisterRepository;
            _tempReportAccStatementRepository = tempReportAccStatementRepository;
            _viewAccountsTransactionsVoucherTypeRepository = viewAccountsTransactionsVoucherTypeRepository;
            _reportPurchaseRequisitionRepository = reportPurchaseRequisitionRepository;
            _reportPurchaseOrderRepository = reportPurchaseOderRepository;
            _stockRegisterRepository = stockRegisterRepository;
            _unitDetailsRepository = unitDetailsRepository;
            _unitMasterRepository = unitMasterRepository;
        }

        public IEnumerable<ReportStockBaseUnit> MainReport_GetReportStockLedger_PARAM_CLASS(StockLedgerParameters stockLedgerParameters, List<ViewStockTransferType> viewStockTransferType)
        {

            var data = (from im in _itemMasterRepository.GetAll()
            .Where(x => x.ItemMasterItemType == "A" && x.ItemMasterDelStatus != true)

                        join sr in _stockRegisterRepository.GetAll()
                        on im.ItemMasterItemId equals (long?)sr.StockRegisterMaterialID into ims

                        from imsr in ims.DefaultIfEmpty()

                        join ud in (from unitD in _unitDetailsRepository.GetAll()
                                    .Where(x => x.UnitDetailsConversionType == (double)1)
                                    select new { unitD.UnitDetailsUnitId, unitD.UnitDetailsItemId })
                        on (long?)im.ItemMasterItemId equals ud.UnitDetailsItemId into uds

                        from srud in uds.DefaultIfEmpty()

                        join um in _unitMasterRepository.GetAll()
                        on (srud == null ? -1 : srud.UnitDetailsUnitId) equals um.UnitMasterUnitId into umu

                        from umud in umu.DefaultIfEmpty()

                        select new
                        {
                            StockRegisterMaterialId = im.ItemMasterItemId,
                            StockRegisterAmount = imsr?.StockRegisterAmount ?? 0,
                            ItemMasterItemName = im.ItemMasterItemName,
                            ItemMasterPartNo = im.ItemMasterPartNo,
                            RelativeNo = im.ItemMasterAccountNo,
                            StockRegisterRate = imsr?.StockRegisterRate ?? 0,
                            UnitMasterUnitShortName = umud?.UnitMasterUnitShortName ?? "",
                            StockRegisterLocationID = imsr?.StockRegisterLocationID ?? 0,
                            StockRegisterUnitId = umud?.UnitMasterUnitId ?? 0,
                            StockRegisterSIN = imsr?.StockRegisterSIN ?? 0,
                            StockRegisterSout = imsr?.StockRegisterSout ?? 0
                        }).Where(s =>
                (stockLedgerParameters.rptLocation == -1 || s.StockRegisterLocationID == stockLedgerParameters.rptLocation) &&
                 (stockLedgerParameters.rptItemGroup == -1 || (s.RelativeNo == stockLedgerParameters.rptItemGroup)) &&
                (stockLedgerParameters.rptItem == -1 || s.StockRegisterMaterialId == stockLedgerParameters.rptItem) &&
                (string.IsNullOrEmpty(stockLedgerParameters.rptPartNo) || s.ItemMasterPartNo == stockLedgerParameters.rptPartNo.Trim())
            )
            .GroupBy(m => new
            {
                m.StockRegisterMaterialId,
                m.StockRegisterAmount,
                m.ItemMasterItemName,
                m.ItemMasterPartNo,
                m.RelativeNo,
                m.StockRegisterRate,
                m.StockRegisterUnitId,
                m.UnitMasterUnitShortName,
                m.StockRegisterLocationID
            })
            .Select(grp => new ReportStockBaseUnit
            {
                StockRegisterMaterialId = grp.Key.StockRegisterMaterialId,
                ItemMasterPartNo = grp.Key.ItemMasterPartNo,
                ItemMasterItemName = grp.Key.ItemMasterItemName,
                StockRegisterUnitId = grp.Key.StockRegisterUnitId,
                UnitMasterUnitShortName = grp.Key.UnitMasterUnitShortName,
                Stock = grp.Sum(x => (x.StockRegisterSIN) - (x.StockRegisterSout)),
                StockRegisterRate = grp.Key.StockRegisterRate,
                StockRegisterAmount = grp.Key.StockRegisterAmount,
                RelativeNo = grp.Key.RelativeNo,
                StockRegisterLocationID = grp.Key.StockRegisterLocationID
            })
            .ToList();


            return data;
        }
        public IEnumerable<ReportAccountsTransactions> MainReport_GetReportAccountsTransactions(string AccNo, int Location, int Job, int CostCenter)
        {
            //return _reportAccountsTransactionsRepository.GetAll();
            int intLocation;
            int intJob;
            int intCostCenter;

            double dblOpeningBal;
            if (Location == -1)
            {
                intLocation = 0;
            }
            else
            {
                intLocation = Location;
            }

            if (Job == -1)
            {
                intJob = 0;
            }
            else
            {
                intJob = Location;
            }

            if (CostCenter == -1)
            {
                intCostCenter = 0;
            }
            else
            {
                intCostCenter = CostCenter;
            }





            IEnumerable<ReportAccountsTransactions> reportAccountsTransactions_ALL = _reportAccountsTransactionsRepository.GetAll().Where(k => k.AccountsTransactionsAccNo == AccNo
            //&& (k.AccountsTransactionsCostCenterId ?? 0) == intCostCenter
            // && (k.AccountsTransactionsJobNo ?? 0) == intJob
            //  && (k.AccountsTransactionsLocation ?? 0) == intLocation 
            && (k.AccountstransactionsDelStatus == false || k.AccountstransactionsDelStatus == null));




            ReportAccountsTransactions OB_ReportAccountsTransactions = new ReportAccountsTransactions
            {
                AccName = null,
                AccGroupNo = null,
                AccGroup = null,
                LocationMasterLocationName = null,
                JobMasterJobName = null,
                CostCenterMasterCostCenterName = null,
                CurrencyMasterCurrencyName = null,
                AccountsTransactionsTransSno = 0,
                AccountsTransactionsAccNo = null,
                AccountsTransactionsTransDate = DateTime.Now,
                AccountsTransactionsParticulars = null,
                AccountsTransactionsDebit = 100,
                AccountsTransactionsCredit = 200,
                AccountsTransactionsFcDebit = null,
                AccountsTransactionsFcCredit = null,
                AccountsTransactionsVoucherType = "OPENING BALANCE",
                AccountsTransactionsVoucherNo = null,
                AccountsTransactionsDescription = null,
                AccountsTransactionsUserId = 0,
                AccountsTransactionsStatus = null,
                AccountsTransactionsTstamp = DateTime.Now,
                RefNo = null,
                AccountsTransactionsFsno = 0,
                AccountsTransactionsAllocDebit = null,
                AccountsTransactionsAllocCredit = null,
                AccountsTransactionsAllocBalance = null,
                AccountsTransactionsFcAllocDebit = null,
                AccountsTransactionsFcAllocCredit = null,
                AccountsTransactionsFcAllocBalance = null,
                AccountsTransactionsLocation = null,
                AccountsTransactionsJobNo = null,
                AccountsTransactionsCostCenterId = null,
                AccountsTransactionsApprovalDt = null,
                AccountsTransactionsDepartment = null,
                AccountsTransactionsFcRate = null,
                AccountsTransactionsCompanyId = null,
                AccountsTransactionsCurrencyId = null,
                AccountsTransactionsDrGram = null,
                AccountsTransactionsCrGram = null,
                AccountsTransactionsCheqNo = null,
                AccountsTransactionsLpoNo = null,
                AccountsTransactionsCheqDate = null,
                AccountsTransactionsOpposEntryDesc = null,
                AccountsTransactionsAllocUpdateBal = null,
                AccountsTransactionsDeptId = null,
                AccountsTransactionsVatno = null,
                AccountsTransactionsVatableAmount = null,
                AccountstransactionsDelStatus = null,
                DepartmentMasterDepartmentName = null,
            };

            //reportAccountsTransactions_ALL.Add(  OB_ReportAccountsTransactions  );

            return reportAccountsTransactions_ALL;

        }
        decimal RunningTot;
        public decimal RunningCalc(decimal Diff)
        {
            RunningTot = RunningTot + Diff;
            return RunningTot;
        }
        public void Delete_OldEntry_TempReportAccStatement()
        {
            List<TempReportAccStatement> tempReport = _tempReportAccStatementRepository.GetAsQueryable().Select((l) => new TempReportAccStatement
            {
                AccSlNo = l.AccSlNo,
                AccAccNo = l.AccAccNo,
                AccName = l.AccName,
                AccVchNo = l.AccVchNo,
                AccVchDate = l.AccVchDate,
                AccVchType = l.AccVchType,
                AccDesc = l.AccDesc,
                AccDebit = l.AccDebit,
                AccCredit = l.AccCredit,
                AccTotDebit = l.AccTotDebit,
                AccTotCredit = l.AccTotCredit,
                AccRunning = l.AccRunning,
                AccDateDiff = l.AccDateDiff,
                AccRefNo = l.AccRefNo,
                AccVchAgainst = l.AccVchAgainst,
                AccCostcenter = l.AccCostcenter,
                AccLocation = l.AccLocation,
                AccJob = l.AccJob,
                AccDepartment = l.AccDepartment,
                AccLpoNo = l.AccLpoNo,
                AccChqNo = l.AccChqNo,
                AccChqDate = l.AccChqDate,
                AccVatNo = l.AccVatNo,
                AccVatAmt = l.AccVatAmt,
            }).ToList();
            _tempReportAccStatementRepository.DeleteList(tempReport);
            //_accountTransactionRepository.UpdateList(accttrans);
        }
        public void PrintReport_TempTableEntry(AccountStatementParameters accountStatementParameters, List<ReportAccountsTransactions> DTLS)

        {
            Delete_OldEntry_TempReportAccStatement();
            List<TempReportAccStatement> Temp = new List<TempReportAccStatement>();
            RunningTot = 0;
            for (int i = 0; i < DTLS.Count; i++)
            {
                Temp.Add(AddNewEntry_To_TempReportAccStatement(
 i + 1,    //int ? AccSlNo,
         accountStatementParameters.rptAccount, //string AccAccNo,
                                      DTLS[i].AccName,         //string AccName,
                                      DTLS[i].AccountsTransactionsVoucherNo,    //string AccVchNo,
                                           DTLS[i].AccountsTransactionsTransDate,     //DateTime ? AccVchDate ,
                                            DTLS[i].AccountsTransactionsVoucherType,     //string AccVchType,
                                           DTLS[i].AccountsTransactionsParticulars,  //string AccDesc,
                                          DTLS[i].AccountsTransactionsDebit, //decimal ? AccDebit ,
                                          DTLS[i].AccountsTransactionsCredit,  //decimal ? AccCredit ,
                                           (DTLS[i].AccountsTransactionsDebit - DTLS[i].AccountsTransactionsCredit) < 0 ? 0 : (DTLS[i].AccountsTransactionsDebit - DTLS[i].AccountsTransactionsCredit),  //decimal ? AccTotDebit ,
                                              (DTLS[i].AccountsTransactionsDebit - DTLS[i].AccountsTransactionsCredit) < 0 ? (DTLS[i].AccountsTransactionsDebit - DTLS[i].AccountsTransactionsCredit) * -1 : 0, //decimal ? AccTotCredit ,  

 RunningCalc(DTLS[i].AccountsTransactionsDebit - DTLS[i].AccountsTransactionsCredit), //decimal ? AccRunning ,
   DTLS[i].AccountsTransactionsCredit, //decimal ? AccDateDiff ,
  DTLS[i].RefNo,  //string AccRefNo,
DTLS[i].CostCenterMasterCostCenterName,//string AccCostcenter,
DTLS[i].AccountsTransactionsOpposEntryDesc,//string AccVchAgainst,
DTLS[i].LocationMasterLocationName,//string AccLocation,
DTLS[i].JobMasterJobName, //string AccJob,
DTLS[i].DepartmentMasterDepartmentName, //string AccDepartment,
DTLS[i].AccountsTransactionsLpoNo,//string AccLpoNo,
DTLS[i].AccountsTransactionsCheqNo, //string AccChqNo,
DTLS[i].AccountsTransactionsCheqDate, //DateTime ? AccChqDate ,
DTLS[i].AccountsTransactionsVatno,//string AccVatNo,
DTLS[i].AccountsTransactionsVatableAmount,//decimal ? AccVatAmt
 accountStatementParameters.rptLocation, //long ? AccLocationId,
   accountStatementParameters.rptJob, //            long ? AccJobId,
   accountStatementParameters.rptCostCenter, //            long ? AccCostcenterId,
   0 //            long ? AccDepartmentId
));
            }
            _tempReportAccStatementRepository.InsertList(Temp);
        }
        public static TempReportAccStatement AddNewEntry_To_TempReportAccStatement(
                             int AccSlNo,
                string AccAccNo,
                string AccName,
                string AccVchNo,
                DateTime? AccVchDate,
                string AccVchType,
                string AccDesc,
                decimal? AccDebit,
                decimal? AccCredit,
                decimal? AccTotDebit,
                decimal? AccTotCredit,
                decimal? AccRunning,
                decimal? AccDateDiff,
                string AccRefNo,
                string AccCostcenter,
                string AccVchAgainst,
                string AccLocation,
                string AccJob,
                string AccDepartment,
                string AccLpoNo,
                string AccChqNo,
                DateTime? AccChqDate,
                string AccVatNo,
                decimal? AccVatAmt,
        long? AccLocationId,
                 long? AccJobId,
                 long? AccCostcenterId,
                 long? AccDepartmentId
         )
        {
            TempReportAccStatement tempReportAccStatement = new TempReportAccStatement
            {
                AccSlNo = AccSlNo,
                AccAccNo = AccAccNo,
                AccName = AccName,
                AccVchNo = AccVchNo,
                AccVchDate = AccVchDate,
                AccVchType = AccVchType,
                AccDesc = AccDesc,
                AccDebit = AccDebit,
                AccCredit = AccCredit,
                AccTotDebit = AccTotDebit,
                AccTotCredit = AccTotCredit,
                AccRunning = AccRunning,
                AccDateDiff = AccDateDiff,
                AccRefNo = AccRefNo,
                AccCostcenter = AccCostcenter,
                AccVchAgainst = AccVchAgainst,
                AccLocation = AccLocation,
                AccJob = AccJob,
                AccDepartment = AccDepartment,
                AccLpoNo = AccLpoNo,
                AccChqNo = AccChqNo,
                AccChqDate = AccChqDate,
                AccVatNo = AccVatNo,
                AccVatAmt = AccVatAmt,
                AccLocationId = AccLocationId,
                AccJobId = AccJobId,
                AccCostcenterId = AccCostcenterId,
                AccDepartmentId = AccDepartmentId
            };
            return tempReportAccStatement;
        }
        public List<ReportAccountsTransactions> MainReport_GetReportAccountsTransactions_PARAM_CLASS(AccountStatementParameters accountStatementParameters)
        {
            var voucherTypes = accountStatementParameters.ViewAccountsTransactionsVoucherType.Select((k) =>
            {
                return k;
            }).ToList();


            if (accountStatementParameters.rptDetails == true)
            {
                List<ReportAccountsTransactions> reportAccountsTransactions_ALL = _reportAccountsTransactionsRepository.GetAsQueryable().Where(k =>
                     ((k.AccountsTransactionsAccNo == accountStatementParameters.rptAccount)
                    && (accountStatementParameters.rptLocation == -1 ? true : (k.AccountsTransactionsLocation == accountStatementParameters.rptLocation))
                    && (accountStatementParameters.rptJob == -1 ? true : (k.AccountsTransactionsJobNo == accountStatementParameters.rptJob))
                    && (accountStatementParameters.rptCostCenter == -1 ? true : (k.AccountsTransactionsCostCenterId == accountStatementParameters.rptCostCenter))
                    && (accountStatementParameters.rptAsOnDate == false ? true : k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpAson.Value.Date)
                   && (accountStatementParameters.rptDateRange == false ? true : (k.AccountsTransactionsTransDate.Date >= accountStatementParameters.rptDtpFrom.Value.Date)
                   && (k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpTo.Value.Date))
                   && (accountStatementParameters.rptNarration.Trim() == "" ? true : (k.AccountsTransactionsOpposEntryDesc.Trim() == accountStatementParameters.rptNarration.Trim()))
                   && (accountStatementParameters.rptDescription.Trim() == "" ? true : (k.AccountsTransactionsDescription.Trim() == accountStatementParameters.rptDescription.Trim()))
                   && (k.AccountstransactionsDelStatus == false || k.AccountstransactionsDelStatus == null)
                    && (voucherTypes.Count > 0 ? voucherTypes.Contains(k.AccountsTransactionsVoucherType.Trim()) : true)
                    )).OrderBy(x => x.AccountsTransactionsTransDate).ToList();

                if (accountStatementParameters.rptDateRange == true)
                {
                    if (accountStatementParameters.rptHideOpeningBalance == false)
                    {
                        var DiffCreditDebit = (_reportAccountsTransactionsRepository.GetAsQueryable().Where(k =>
                              ((k.AccountsTransactionsAccNo == accountStatementParameters.rptAccount)
                              && (accountStatementParameters.rptLocation == -1 ? true : (k.AccountsTransactionsLocation == accountStatementParameters.rptLocation))
                              && (accountStatementParameters.rptJob == -1 ? true : (k.AccountsTransactionsJobNo == accountStatementParameters.rptJob))
                              && (accountStatementParameters.rptCostCenter == -1 ? true : (k.AccountsTransactionsCostCenterId == accountStatementParameters.rptCostCenter))
                              //&& (accountStatementParameters.rptAsOnDate == false ? true : k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpAson.Value.Date)
                              && (accountStatementParameters.rptDateRange == false ? true : (k.AccountsTransactionsTransDate.Date < accountStatementParameters.rptDtpFrom.Value.Date))
                              && (accountStatementParameters.rptNarration.Trim() == "" ? true : (k.AccountsTransactionsOpposEntryDesc.Trim() == accountStatementParameters.rptNarration.Trim()))
                              && (accountStatementParameters.rptDescription.Trim() == "" ? true : (k.AccountsTransactionsDescription.Trim() == accountStatementParameters.rptDescription.Trim()))
                              && (k.AccountstransactionsDelStatus == false || k.AccountstransactionsDelStatus == null)
                              && (voucherTypes.Count > 0 ? voucherTypes.Contains(k.AccountsTransactionsVoucherType) : true)
                              )).Select(c =>
                              new { difference = c.AccountsTransactionsDebit - c.AccountsTransactionsCredit }))
                                    .Sum(b => b.difference);
                        decimal dblOpeningBalCredit = 0;
                        decimal dblOpeningBalDebit = 0;
                        if (DiffCreditDebit < 0)
                        {
                            dblOpeningBalCredit = DiffCreditDebit;
                        }
                        else
                        {
                            dblOpeningBalDebit = DiffCreditDebit;
                        }
                        ReportAccountsTransactions OB_ReportAccountsTransactions = new ReportAccountsTransactions
                        {
                            AccName = null,
                            AccGroupNo = null,
                            AccGroup = null,
                            LocationMasterLocationName = null,
                            JobMasterJobName = null,
                            CostCenterMasterCostCenterName = null,
                            CurrencyMasterCurrencyName = null,
                            AccountsTransactionsTransSno = 0,
                            AccountsTransactionsAccNo = null,
                            AccountsTransactionsTransDate = accountStatementParameters.rptDtpFrom.Value.Date,
                            AccountsTransactionsParticulars = null,
                            AccountsTransactionsDebit = dblOpeningBalDebit,
                            AccountsTransactionsCredit = dblOpeningBalCredit,
                            AccountsTransactionsFcDebit = null,
                            AccountsTransactionsFcCredit = null,
                            AccountsTransactionsVoucherType = "OPENING BALANCE",
                            AccountsTransactionsVoucherNo = null,
                            AccountsTransactionsDescription = null,
                            AccountsTransactionsUserId = 0,
                            AccountsTransactionsStatus = null,
                            AccountsTransactionsTstamp = DateTime.Now,
                            RefNo = null,
                            AccountsTransactionsFsno = 0,
                            AccountsTransactionsAllocDebit = null,
                            AccountsTransactionsAllocCredit = null,
                            AccountsTransactionsAllocBalance = null,
                            AccountsTransactionsFcAllocDebit = null,
                            AccountsTransactionsFcAllocCredit = null,
                            AccountsTransactionsFcAllocBalance = null,
                            AccountsTransactionsLocation = null,
                            AccountsTransactionsJobNo = null,
                            AccountsTransactionsCostCenterId = null,
                            AccountsTransactionsApprovalDt = null,
                            AccountsTransactionsDepartment = null,
                            AccountsTransactionsFcRate = null,
                            AccountsTransactionsCompanyId = null,
                            AccountsTransactionsCurrencyId = null,
                            AccountsTransactionsDrGram = null,
                            AccountsTransactionsCrGram = null,
                            AccountsTransactionsCheqNo = null,
                            AccountsTransactionsLpoNo = null,
                            AccountsTransactionsCheqDate = null,
                            AccountsTransactionsOpposEntryDesc = null,
                            AccountsTransactionsAllocUpdateBal = null,
                            AccountsTransactionsDeptId = null,
                            AccountsTransactionsVatno = null,
                            AccountsTransactionsVatableAmount = null,
                            AccountstransactionsDelStatus = null,
                            DepartmentMasterDepartmentName = null,
                        };
                        reportAccountsTransactions_ALL.Insert(0, OB_ReportAccountsTransactions);
                    }
                }
                PrintReport_TempTableEntry(accountStatementParameters, reportAccountsTransactions_ALL);
                return reportAccountsTransactions_ALL;
            }
            else
            {
                List<ReportAccountsTransactions> reportAccountsTransactions_ALL = new List<ReportAccountsTransactions>();
                DateTime StartDate;
                string date_Range;
                if (accountStatementParameters.rptAsOnDate == true)
                {
                    StartDate = accountStatementParameters.rptDtpAson.Value.Date;
                    date_Range = "As On" + StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                {
                    StartDate = accountStatementParameters.rptDtpFrom.Value.Date;
                    date_Range = " From " + StartDate.ToString("dd/MMM/yyyy") + " To " + accountStatementParameters.rptDtpTo.Value.Date.ToString("dd/MMM/yyyy");
                }
                var Debit = (_reportAccountsTransactionsRepository.GetAsQueryable().Where(k =>
                           ((k.AccountsTransactionsAccNo == accountStatementParameters.rptAccount)
                       && (accountStatementParameters.rptLocation == -1 ? true : (k.AccountsTransactionsLocation == accountStatementParameters.rptLocation))
                       && (accountStatementParameters.rptJob == -1 ? true : (k.AccountsTransactionsJobNo == accountStatementParameters.rptJob))
                       && (accountStatementParameters.rptCostCenter == -1 ? true : (k.AccountsTransactionsCostCenterId == accountStatementParameters.rptCostCenter))
                       && (accountStatementParameters.rptAsOnDate == false ? true : k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpAson.Value.Date)
                       && (accountStatementParameters.rptDateRange == false ? true : (k.AccountsTransactionsTransDate.Date >= accountStatementParameters.rptDtpFrom.Value.Date)
                            && (k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpTo.Value.Date))
                       && (accountStatementParameters.rptNarration.Trim() == "" ? true : (k.AccountsTransactionsOpposEntryDesc.Trim() == accountStatementParameters.rptNarration.Trim()))
                       && (accountStatementParameters.rptDescription.Trim() == "" ? true : (k.AccountsTransactionsDescription.Trim() == accountStatementParameters.rptDescription.Trim()))
                            && (k.AccountstransactionsDelStatus == false || k.AccountstransactionsDelStatus == null)
                            && (voucherTypes.Count > 0 ? voucherTypes.Contains(k.AccountsTransactionsVoucherType) : true)
                            )).Select(c =>
                            new { difference = c.AccountsTransactionsDebit }))
                                  .Sum(b => b.difference);

                var Credit = (_reportAccountsTransactionsRepository.GetAsQueryable().Where(k =>
                           ((k.AccountsTransactionsAccNo == accountStatementParameters.rptAccount)
                      && (accountStatementParameters.rptLocation == -1 ? true : (k.AccountsTransactionsLocation == accountStatementParameters.rptLocation))
                      && (accountStatementParameters.rptJob == -1 ? true : (k.AccountsTransactionsJobNo == accountStatementParameters.rptJob))
                      && (accountStatementParameters.rptCostCenter == -1 ? true : (k.AccountsTransactionsCostCenterId == accountStatementParameters.rptCostCenter))
                      && (accountStatementParameters.rptAsOnDate == false ? true : k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpAson.Value.Date)
                      && (accountStatementParameters.rptDateRange == false ? true : (k.AccountsTransactionsTransDate.Date >= accountStatementParameters.rptDtpFrom.Value.Date)
                           && (k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpTo.Value.Date))
                      && (accountStatementParameters.rptNarration.Trim() == "" ? true : (k.AccountsTransactionsOpposEntryDesc.Trim() == accountStatementParameters.rptNarration.Trim()))
                      && (accountStatementParameters.rptDescription.Trim() == "" ? true : (k.AccountsTransactionsDescription.Trim() == accountStatementParameters.rptDescription.Trim()))
                           && (k.AccountstransactionsDelStatus == false || k.AccountstransactionsDelStatus == null)
                           && (voucherTypes.Count > 0 ? voucherTypes.Contains(k.AccountsTransactionsVoucherType) : true)
                           )).Select(c =>
                           new { difference = c.AccountsTransactionsCredit }))
                                 .Sum(b => b.difference);

                ReportAccountsTransactions Summ_ReportAccountsTransactions = new ReportAccountsTransactions
                {
                    AccName = null,
                    AccGroupNo = null,
                    AccGroup = null,
                    LocationMasterLocationName = null,
                    JobMasterJobName = null,
                    CostCenterMasterCostCenterName = null,
                    CurrencyMasterCurrencyName = null,
                    AccountsTransactionsTransSno = 0,
                    AccountsTransactionsAccNo = null,
                    AccountsTransactionsTransDate = StartDate.Date,
                    AccountsTransactionsParticulars = date_Range,
                    AccountsTransactionsDebit = Debit,
                    AccountsTransactionsCredit = Credit,
                    AccountsTransactionsFcDebit = null,
                    AccountsTransactionsFcCredit = null,
                    AccountsTransactionsVoucherType = null,
                    AccountsTransactionsVoucherNo = null,
                    AccountsTransactionsDescription = null,
                    AccountsTransactionsUserId = 0,
                    AccountsTransactionsStatus = null,
                    AccountsTransactionsTstamp = DateTime.Now,
                    RefNo = null,
                    AccountsTransactionsFsno = 0,
                    AccountsTransactionsAllocDebit = null,
                    AccountsTransactionsAllocCredit = null,
                    AccountsTransactionsAllocBalance = null,
                    AccountsTransactionsFcAllocDebit = null,
                    AccountsTransactionsFcAllocCredit = null,
                    AccountsTransactionsFcAllocBalance = null,
                    AccountsTransactionsLocation = null,
                    AccountsTransactionsJobNo = null,
                    AccountsTransactionsCostCenterId = null,
                    AccountsTransactionsApprovalDt = null,
                    AccountsTransactionsDepartment = null,
                    AccountsTransactionsFcRate = null,
                    AccountsTransactionsCompanyId = null,
                    AccountsTransactionsCurrencyId = null,
                    AccountsTransactionsDrGram = null,
                    AccountsTransactionsCrGram = null,
                    AccountsTransactionsCheqNo = null,
                    AccountsTransactionsLpoNo = null,
                    AccountsTransactionsCheqDate = null,
                    AccountsTransactionsOpposEntryDesc = null,
                    AccountsTransactionsAllocUpdateBal = null,
                    AccountsTransactionsDeptId = null,
                    AccountsTransactionsVatno = null,
                    AccountsTransactionsVatableAmount = null,
                    AccountstransactionsDelStatus = null,
                    DepartmentMasterDepartmentName = null,
                };
                var DiffCreditDebit = (_reportAccountsTransactionsRepository.GetAsQueryable().Where(k =>
                             ((k.AccountsTransactionsAccNo == accountStatementParameters.rptAccount)
                             && (accountStatementParameters.rptLocation == -1 ? true : (k.AccountsTransactionsLocation == accountStatementParameters.rptLocation))
                             && (accountStatementParameters.rptJob == -1 ? true : (k.AccountsTransactionsJobNo == accountStatementParameters.rptJob))
                             && (accountStatementParameters.rptCostCenter == -1 ? true : (k.AccountsTransactionsCostCenterId == accountStatementParameters.rptCostCenter))
                             //&& (accountStatementParameters.rptAsOnDate == false ? true : k.AccountsTransactionsTransDate.Date <= accountStatementParameters.rptDtpAson.Value.Date)
                             && (accountStatementParameters.rptDateRange == false ? true : (k.AccountsTransactionsTransDate.Date < accountStatementParameters.rptDtpFrom.Value.Date))
                             && (accountStatementParameters.rptNarration.Trim() == "" ? true : (k.AccountsTransactionsOpposEntryDesc.Trim() == accountStatementParameters.rptNarration.Trim()))
                             && (accountStatementParameters.rptDescription.Trim() == "" ? true : (k.AccountsTransactionsDescription.Trim() == accountStatementParameters.rptDescription.Trim()))
                             && (k.AccountstransactionsDelStatus == false || k.AccountstransactionsDelStatus == null)
                             && (voucherTypes.Count > 0 ? voucherTypes.Contains(k.AccountsTransactionsVoucherType) : true)

                             )).Select(c =>
                             new { difference = c.AccountsTransactionsDebit - c.AccountsTransactionsCredit }))
                                   .Sum(b => b.difference);
                if (accountStatementParameters.rptDateRange == true)
                {
                    if (accountStatementParameters.rptHideOpeningBalance == false)
                    {
                        decimal dblOpeningBalCredit = 0;
                        decimal dblOpeningBalDebit = 0;
                        if (DiffCreditDebit < 0)
                        {
                            dblOpeningBalCredit = DiffCreditDebit * -1;
                        }
                        else
                        {

                            dblOpeningBalDebit = DiffCreditDebit;
                        }
                        ReportAccountsTransactions OB_ReportAccountsTransactions = new ReportAccountsTransactions
                        {
                            AccName = null,
                            AccGroupNo = null,
                            AccGroup = null,
                            LocationMasterLocationName = null,
                            JobMasterJobName = null,
                            CostCenterMasterCostCenterName = null,
                            CurrencyMasterCurrencyName = null,
                            AccountsTransactionsTransSno = 0,
                            AccountsTransactionsAccNo = null,
                            AccountsTransactionsTransDate = accountStatementParameters.rptDtpFrom.Value.Date,
                            AccountsTransactionsParticulars = "OPENING BALANCE",
                            AccountsTransactionsDebit = dblOpeningBalDebit,
                            AccountsTransactionsCredit = dblOpeningBalCredit,
                            AccountsTransactionsFcDebit = null,
                            AccountsTransactionsFcCredit = null,
                            AccountsTransactionsVoucherType = null,
                            AccountsTransactionsVoucherNo = null,
                            AccountsTransactionsDescription = null,
                            AccountsTransactionsUserId = 0,
                            AccountsTransactionsStatus = null,
                            AccountsTransactionsTstamp = DateTime.Now,
                            RefNo = null,
                            AccountsTransactionsFsno = 0,
                            AccountsTransactionsAllocDebit = null,
                            AccountsTransactionsAllocCredit = null,
                            AccountsTransactionsAllocBalance = null,
                            AccountsTransactionsFcAllocDebit = null,
                            AccountsTransactionsFcAllocCredit = null,
                            AccountsTransactionsFcAllocBalance = null,
                            AccountsTransactionsLocation = null,
                            AccountsTransactionsJobNo = null,
                            AccountsTransactionsCostCenterId = null,
                            AccountsTransactionsApprovalDt = null,
                            AccountsTransactionsDepartment = null,
                            AccountsTransactionsFcRate = null,
                            AccountsTransactionsCompanyId = null,
                            AccountsTransactionsCurrencyId = null,
                            AccountsTransactionsDrGram = null,
                            AccountsTransactionsCrGram = null,
                            AccountsTransactionsCheqNo = null,
                            AccountsTransactionsLpoNo = null,
                            AccountsTransactionsCheqDate = null,
                            AccountsTransactionsOpposEntryDesc = null,
                            AccountsTransactionsAllocUpdateBal = null,
                            AccountsTransactionsDeptId = null,
                            AccountsTransactionsVatno = null,
                            AccountsTransactionsVatableAmount = null,
                            AccountstransactionsDelStatus = null,
                            DepartmentMasterDepartmentName = null,
                        };
                        reportAccountsTransactions_ALL.Insert(0, OB_ReportAccountsTransactions);
                        reportAccountsTransactions_ALL.Insert(1, Summ_ReportAccountsTransactions);
                        PrintReport_TempTableEntry(accountStatementParameters, reportAccountsTransactions_ALL);
                        return reportAccountsTransactions_ALL;
                    }
                }
                reportAccountsTransactions_ALL.Insert(0, Summ_ReportAccountsTransactions);
                PrintReport_TempTableEntry(accountStatementParameters, reportAccountsTransactions_ALL);
                return reportAccountsTransactions_ALL;
            }
        }
        public IEnumerable<ViewAccountsTransactionsVoucherType> MainReport_GetAccountsTransactions_VoucherType()
        {
            return _viewAccountsTransactionsVoucherTypeRepository.GetAll();
        }
        public IEnumerable<ReportStockRegister> MainReport_GetReportStockRegister()
        {
            return _reportStockRegisterRepository.GetAll();
        }



        public IEnumerable<ReportStockBaseUnit> MainReport_GetReportStockLedger_Location_PARAM_CLASS(StockLedgerParameters stockLedgerParameters, List<ViewStockTransferType> viewStockTransferType)
        {
            //return _reportStockBaseUnitRepository.GetAll();

            return _reportStockBaseUnitLocationRepository.GetAll();

        }


        public IEnumerable<ItemMaster> MainReport_GetAllItemMaster()
        {
            //return _itemMasterRepository.GetAll();

            List<ItemMaster> reportItemMaster = _itemMasterRepository.GetAsQueryable().Where(k => k.ItemMasterItemType.Trim() == "A").OrderBy(x => x.ItemMasterItemName).ToList();

            return reportItemMaster;

        }


        public IEnumerable<ItemMaster> MainReport_GetAllItemMaster_Group()
        {
            //return _itemMasterRepository.GetAll();


            List<ItemMaster> reportItemMaster = _itemMasterRepository.GetAsQueryable().Where(k => k.ItemMasterItemType.Trim() == "G").OrderBy(x => x.ItemMasterItemName).ToList();

            return reportItemMaster;
        }

        public IEnumerable<ReportSalesVoucher> MainReport_GetReportSalesVoucher_PARAM_CLASS(SalesvoucherreportParameters salesvoucherreportParameters, List<ViewStockTransferType> viewStockTransferType)
        {

            if (salesvoucherreportParameters.rptSummary == true)
            {

                List<ReportSalesVoucher> reportSalesVoucher_ALL = (from k in _reportSalesVoucherRepository.GetAsQueryable()
                                                                   where ((salesvoucherreportParameters.rptInvoiceType == "ALL" ? true : (k.SalesVoucherType == salesvoucherreportParameters.rptInvoiceType))
                && (salesvoucherreportParameters.rptSalesMan == -1 ? true : (k.SalesVoucherSalesManId == salesvoucherreportParameters.rptSalesMan))
                  && (salesvoucherreportParameters.rptBrand == -1 ? true : (k.ItemBrandId == salesvoucherreportParameters.rptBrand))
                    && (salesvoucherreportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == salesvoucherreportParameters.rptItemGroup))
                   && (salesvoucherreportParameters.rptItem == -1 ? true : (k.SalesVoucherDetailsMatId == salesvoucherreportParameters.rptItem))
                         && (salesvoucherreportParameters.rptCustomer == -1 ? true : (k.SalesVoucherPartyId == salesvoucherreportParameters.rptCustomer))
                          && (salesvoucherreportParameters.rptDpt == -1 ? true : ((k.SalesVoucherDptId == salesvoucherreportParameters.rptDpt)
                         && (salesvoucherreportParameters.rptLocation == -1 ? true : (k.SalesVoucherLocationId == salesvoucherreportParameters.rptLocation))
                         && (salesvoucherreportParameters.rptJob == -1 ? true : (k.SalesVoucherJobId == salesvoucherreportParameters.rptJob))
                    && (salesvoucherreportParameters.rptDateRange == false ? true : (k.SalesVoucherDate.Date >= salesvoucherreportParameters.rptDtpFrom.Value.Date)
                                && (k.SalesVoucherDate.Date <= salesvoucherreportParameters.rptDtpTo.Value.Date))
                  && (k.SalesVoucherDelStatus == false || k.SalesVoucherDelStatus == null))))
                                                                   group k by new
                                                                   {
                                                                       k.SalesVoucherNo,
                                                                       k.SalesVoucherDate,
                                                                       k.SalesVoucherType,
                                                                       k.CustomerMasterCustomerName,
                                                                       k.SalesVoucherTotalGrossAmount,
                                                                       k.SalesVoucherTotalItemDisAmount,
                                                                       k.SalesVoucherNetDisAmount,
                                                                       k.SalesVoucherTotalDisAmount,
                                                                       k.SalesVoucherVatAmt,
                                                                       k.SalesVoucherVatRountAmt,
                                                                       k.SalesVoucherNetAmount,
                                                                   } into grp
                                                                   select new ReportSalesVoucher
                                                                   {
                                                                       SalesVoucherNo = grp.Key.SalesVoucherNo,
                                                                       SalesVoucherDate = grp.Key.SalesVoucherDate,
                                                                       SalesVoucherType = grp.Key.SalesVoucherType,
                                                                       CustomerMasterCustomerName = grp.Key.CustomerMasterCustomerName,
                                                                       SalesVoucherTotalGrossAmount = grp.Key.SalesVoucherTotalGrossAmount,
                                                                       SalesVoucherTotalItemDisAmount = grp.Key.SalesVoucherTotalItemDisAmount,
                                                                       SalesVoucherNetDisAmount = grp.Key.SalesVoucherNetDisAmount,
                                                                       SalesVoucherTotalDisAmount = grp.Key.SalesVoucherTotalDisAmount,
                                                                       SalesVoucherVatAmt = grp.Key.SalesVoucherVatAmt,
                                                                       SalesVoucherVatRountAmt = grp.Key.SalesVoucherVatRountAmt,
                                                                       SalesVoucherNetAmount = grp.Key.SalesVoucherNetAmount
                                                                       //,
                                                                       //Stock = grp.Sum(x => x.Stock),
                                                                       //StockRegisterRate = grp.Average(x => x.StockRegisterRate),
                                                                       //StockRegisterAmount = grp.Sum(x => x.StockRegisterAmount),
                                                                       //LocationMasterLocationName = "",
                                                                       //StockRegisterLocationID = 0,
                                                                   }).ToList();

                return reportSalesVoucher_ALL;
            }
            else
            {

                List<ReportSalesVoucher> reportSalesVoucher_ALL = _reportSalesVoucherRepository.GetAsQueryable().Where(k =>
                         (salesvoucherreportParameters.rptInvoiceType == "ALL" ? true : (k.SalesVoucherType == salesvoucherreportParameters.rptInvoiceType))
                && (salesvoucherreportParameters.rptSalesMan == -1 ? true : (k.SalesVoucherSalesManId == salesvoucherreportParameters.rptSalesMan))
                  && (salesvoucherreportParameters.rptBrand == -1 ? true : (k.ItemBrandId == salesvoucherreportParameters.rptBrand))
                    && (salesvoucherreportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == salesvoucherreportParameters.rptItemGroup))
                   && (salesvoucherreportParameters.rptItem == -1 ? true : (k.SalesVoucherDetailsMatId == salesvoucherreportParameters.rptItem))
                         && (salesvoucherreportParameters.rptCustomer == -1 ? true : (k.SalesVoucherPartyId == salesvoucherreportParameters.rptCustomer))
                          && (salesvoucherreportParameters.rptDpt == -1 ? true : ((k.SalesVoucherDptId == salesvoucherreportParameters.rptDpt)
                         && (salesvoucherreportParameters.rptLocation == -1 ? true : (k.SalesVoucherLocationId == salesvoucherreportParameters.rptLocation))
                         && (salesvoucherreportParameters.rptJob == -1 ? true : (k.SalesVoucherJobId == salesvoucherreportParameters.rptJob))
                    && (salesvoucherreportParameters.rptDateRange == false ? true : (k.SalesVoucherDate.Date >= salesvoucherreportParameters.rptDtpFrom.Value.Date)
                                && (k.SalesVoucherDate.Date <= salesvoucherreportParameters.rptDtpTo.Value.Date))
                  && (k.SalesVoucherDelStatus == false || k.SalesVoucherDelStatus == null)))).OrderBy(x => x.SalesVoucherId).
                  Select(s => new ReportSalesVoucher
                  {

                      SalesVoucherNo = s.SalesVoucherNo,
                      SalesVoucherDate = s.SalesVoucherDate,
                      SalesVoucherType = s.SalesVoucherType,
                      CustomerMasterCustomerName = s.CustomerMasterCustomerName,
                      SalesVoucherDetailsGrossAmount = s.SalesVoucherDetailsGrossAmount,
                      SalesVoucherDetailsDiscAmount = s.SalesVoucherDetailsDiscAmount,
                      SalesVoucherDetailsNetAmt = s.SalesVoucherDetailsNetAmt,
                      SalesVoucherDetailsVatAmt = s.SalesVoucherDetailsVatAmt,
                      SalesVoucherVatRountAmt = s.SalesVoucherVatRountAmt,
                      SalesVoucherDetailsRate = s.SalesVoucherDetailsRate,
                      SalesVoucherDetailsQuantity = s.SalesVoucherDetailsQuantity,
                      //ItemBrandId  = s.ItemBrandId,
                      //ItemGroupId  = s.ItemGroupId,
                      //ItemBrandName = s.ItemBrandName,
                      //ItemGroupName = s.ItemGroupName,
                      //JobMasterJobName = s.JobMasterJobName,
                      //DepartmentMasterDepartmentName = s.DepartmentMasterDepartmentName,
                      //ItemMasterPartNo = s.ItemMasterPartNo,
                      LocationMasterLocationName = s.LocationMasterLocationName,







                  }).ToList();
                return reportSalesVoucher_ALL;
            }

            //return _reportSalesVoucherRepository.GetAll();
        }

        public IEnumerable<ReportPurchaseVoucher> MainReport_GetReportPurchaseVoucher_PARAM_CLASS(PurchasevoucherreportParameters purchasevoucherreportParameters, List<ViewStockTransferType> viewStockTransferType)
        {
            try
            {
                if (purchasevoucherreportParameters.rptSummary == true)
                {

                    List<ReportPurchaseVoucher> reportPurchaseVoucher_ALL = (from k in _reportPurchaseVoucherRepository.GetAsQueryable()
                                                                             where ((purchasevoucherreportParameters.rptSupplier == -1 ? true : (k.PurchaseVoucherPartyId == purchasevoucherreportParameters.rptSupplier))
                                    && (purchasevoucherreportParameters.rptCurrency == -1 ? true : ((k.PurchaseVoucherCurrencyId == purchasevoucherreportParameters.rptCurrency)
                                   && (purchasevoucherreportParameters.rptLocation == -1 ? true : (k.PurchaseVoucherLocationId == purchasevoucherreportParameters.rptLocation))
                                   && (purchasevoucherreportParameters.rptJob == -1 ? true : (k.PurchaseVoucherJobId == purchasevoucherreportParameters.rptJob))
                                     && (purchasevoucherreportParameters.rptSuppInvNo.Trim() == "" ? true : (k.PurchaseVoucherSupplyInvoiceNo.Trim() == purchasevoucherreportParameters.rptSuppInvNo.Trim()))
                                                                              && (purchasevoucherreportParameters.rptInvoiceType == "ALL" ? true : (k.PurchaseVoucherPurchaseType == purchasevoucherreportParameters.rptInvoiceType))
                        && (purchasevoucherreportParameters.rptBrand == -1 ? true : (k.ItemBrandId == (long)purchasevoucherreportParameters.rptBrand))
                              && (purchasevoucherreportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == purchasevoucherreportParameters.rptItemGroup))
                             && (purchasevoucherreportParameters.rptItem == -1 ? true : (k.PurchaseVoucherDetailsMaterialId == purchasevoucherreportParameters.rptItem))

                              && (purchasevoucherreportParameters.rptDateRange == false ? true : (k.PurchaseVoucherPurchaseDate.Value.Date >= purchasevoucherreportParameters.rptDtpFrom.Value.Date)
                                          && (k.PurchaseVoucherPurchaseDate.Value.Date <= purchasevoucherreportParameters.rptDtpTo.Value.Date))
                            && (k.PurchaseVoucherDelStatus == false || k.PurchaseVoucherDelStatus == null))))
                                                                             group k by new
                                                                             {
                                                                                 k.PurchaseVoucherVoucherNo,
                                                                                 k.PurchaseVoucherPurchaseDate,
                                                                                 k.PurchaseVoucherPurchaseType,
                                                                                 k.PurchaseVoucherSupplyInvoiceNo,
                                                                                 k.LocationMasterLocationName,
                                                                                 k.SuppliersMasterSupplierName,
                                                                                 k.PurchaseVoucherTotalGrossAmt,
                                                                                 k.PurchaseVoucherTotalItemDisAmount,
                                                                                 k.PurchaseVoucherDisAmount,
                                                                                 k.PurchaseVoucherTotalDiscountAmt,
                                                                                 k.PurchaseVoucherVatAmount,
                                                                                 k.PurchaseVoucherVatRoundAmount,
                                                                                 k.PurchaseVoucherNetAmount,
                                                                             } into grp
                                                                             select new ReportPurchaseVoucher
                                                                             {
                                                                                 PurchaseVoucherVoucherNo = grp.Key.PurchaseVoucherVoucherNo,
                                                                                 PurchaseVoucherPurchaseDate = grp.Key.PurchaseVoucherPurchaseDate,
                                                                                 PurchaseVoucherPurchaseType = grp.Key.PurchaseVoucherPurchaseType,
                                                                                 PurchaseVoucherSupplyInvoiceNo = grp.Key.PurchaseVoucherSupplyInvoiceNo,
                                                                                 LocationMasterLocationName = grp.Key.LocationMasterLocationName,
                                                                                 SuppliersMasterSupplierName = grp.Key.SuppliersMasterSupplierName,
                                                                                 PurchaseVoucherTotalGrossAmt = grp.Key.PurchaseVoucherTotalGrossAmt,
                                                                                 PurchaseVoucherTotalItemDisAmount = grp.Key.PurchaseVoucherTotalItemDisAmount,
                                                                                 PurchaseVoucherDisAmount = grp.Key.PurchaseVoucherDisAmount,
                                                                                 PurchaseVoucherTotalDiscountAmt = grp.Key.PurchaseVoucherTotalDiscountAmt,
                                                                                 PurchaseVoucherVatAmount = grp.Key.PurchaseVoucherVatAmount,
                                                                                 PurchaseVoucherVatRoundAmount = grp.Key.PurchaseVoucherVatRoundAmount,
                                                                                 PurchaseVoucherNetAmount = grp.Key.PurchaseVoucherNetAmount

                                                                             }).ToList();

                    return reportPurchaseVoucher_ALL;
                }
                else
                {

                    List<ReportPurchaseVoucher> reportPurchaseVoucher_ALL
                        = _reportPurchaseVoucherRepository.GetAsQueryable().Where(k =>
                           (purchasevoucherreportParameters.rptSupplier == -1 ? true : (k.PurchaseVoucherPartyId == purchasevoucherreportParameters.rptSupplier))
                        && (purchasevoucherreportParameters.rptCurrency == -1 ? true : ((k.PurchaseVoucherCurrencyId == purchasevoucherreportParameters.rptCurrency)
                        && (purchasevoucherreportParameters.rptLocation == -1 ? true : (k.PurchaseVoucherLocationId == purchasevoucherreportParameters.rptLocation))
                        && (purchasevoucherreportParameters.rptJob == -1 ? true : (k.PurchaseVoucherJobId == purchasevoucherreportParameters.rptJob))
                        && (purchasevoucherreportParameters.rptSuppInvNo.Trim() == "" ? true : (k.PurchaseVoucherSupplyInvoiceNo.Trim() == purchasevoucherreportParameters.rptSuppInvNo.Trim()))
                        && (purchasevoucherreportParameters.rptInvoiceType == "ALL" ? true : (k.PurchaseVoucherPurchaseType == purchasevoucherreportParameters.rptInvoiceType))
                        && (purchasevoucherreportParameters.rptBrand == -1 ? true : (k.ItemBrandId == purchasevoucherreportParameters.rptBrand))
                        && (purchasevoucherreportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == purchasevoucherreportParameters.rptItemGroup))
                        && (purchasevoucherreportParameters.rptItem == -1 ? true : (k.PurchaseVoucherDetailsMaterialId == purchasevoucherreportParameters.rptItem))
                        && (purchasevoucherreportParameters.rptDateRange == false ? true : (k.PurchaseVoucherPurchaseDate.Value.Date >= purchasevoucherreportParameters.rptDtpFrom.Value.Date)
                        && (k.PurchaseVoucherPurchaseDate.Value.Date <= purchasevoucherreportParameters.rptDtpTo.Value.Date))
                        && (k.PurchaseVoucherDelStatus == false || k.PurchaseVoucherDelStatus == null)))).OrderBy(x => x.PurchaseVoucherPurId).
                        Select(c => new ReportPurchaseVoucher
                        {

                            PurchaseVoucherVoucherNo = c.PurchaseVoucherVoucherNo,
                            PurchaseVoucherPurchaseDate = c.PurchaseVoucherPurchaseDate,
                            PurchaseVoucherNetAmount = c.PurchaseVoucherNetAmount,
                            ItemBrandId = c.ItemBrandId,
                            ItemGroupId = c.ItemGroupId,
                            ItemBrandName = c.ItemBrandName,
                            ItemGroupName = c.ItemGroupName,
                            PurchaseVoucherDetailsRate = c.PurchaseVoucherDetailsRate,
                            PurchaseVoucherDetailsQuantity = c.PurchaseVoucherDetailsQuantity,
                            PurchaseVoucherDetailsNetAmount = c.PurchaseVoucherDetailsNetAmount,
                            PurchaseVoucherDetailsDiscountAmoutPurchase = c.PurchaseVoucherDetailsDiscountAmoutPurchase,
                            PurchaseVoucherDetailsMaterialId = c.PurchaseVoucherDetailsMaterialId,



                        }).ToList();

                    return reportPurchaseVoucher_ALL;
                }

            }
            catch (Exception ex)
            {

                throw;
            }





            //     }


            //return _reportPurchaseVoucherRepository.GetAll();

        }


        ///==================================================================================================================================================
        ///
        public IEnumerable<ReportPurchaseOrder> MainReport_GetReportPurchaseOrder_PARAM_CLASS(PurchaseOrderReportParameters purchaseOrderreportParameters, List<ViewStockTransferType> viewStockTransferType)

        {

            if (purchaseOrderreportParameters.rptSummary == true)
            {

                List<ReportPurchaseOrder> reportPurchaseOrder_ALL = (from k in _reportPurchaseOrderRepository.GetAsQueryable()
                                                                     where ((purchaseOrderreportParameters.rptSupplier == -1 ? true : (k.PurchaseOrderPartyId == purchaseOrderreportParameters.rptSupplier))
                            && (purchaseOrderreportParameters.rptCurrency == -1 ? true : ((k.PurchaseOrderCurrencyId == purchaseOrderreportParameters.rptCurrency)
                           && (purchaseOrderreportParameters.rptLocation == -1 ? true : (k.PurchaseOrderLocationId == purchaseOrderreportParameters.rptLocation))
                           && (purchaseOrderreportParameters.rptJob == -1 ? true : (k.PurchaseOrderJobId == purchaseOrderreportParameters.rptJob))
                             && (purchaseOrderreportParameters.rptSuppInvNo.Trim() == "" ? true : (k.PurchaseOrderSupInvNo.Trim() == purchaseOrderreportParameters.rptSuppInvNo.Trim()))
                                                                      && (purchaseOrderreportParameters.rptInvoiceType == "ALL" ? true : (k.PurchaseOrderType == purchaseOrderreportParameters.rptInvoiceType))
                     ////&& (purchasevoucherreportParameters.rptBrand == -1 ? true : (k.ItemBrandId == purchasevoucherreportParameters.rptBrand))
                     ////      && (purchasevoucherreportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == purchasevoucherreportParameters.rptItemGroup))
                     && (purchaseOrderreportParameters.rptItem == -1 ? true : (k.ItemMasterItemId == purchaseOrderreportParameters.rptItem))

                      && (purchaseOrderreportParameters.rptDateRange == false ? true : (k.PurchaseOrderDate.Value.Date >= purchaseOrderreportParameters.rptDtpFrom.Value.Date)
                                  && (k.PurchaseOrderDate.Value.Date <= purchaseOrderreportParameters.rptDtpTo.Value.Date))
                    && (k.PurchaseOrderDelStatus == false || k.PurchaseOrderDelStatus == null))))
                                                                     group k by new
                                                                     {
                                                                         k.PurchaseOrderNo,
                                                                         k.PurchaseOrderDate,
                                                                         k.PurchaseOrderType,
                                                                         k.PurchaseOrderSupInvNo,
                                                                         //  k.LocationMasterLocationName,
                                                                         k.SuppliersMasterSupplierName,
                                                                         k.PurchaseOrderTotalGrossAmount,
                                                                         k.PurchaseOrderTotalItemDisAmount,
                                                                         k.PurchaseOrderTotalDisAmount,
                                                                         k.PurchaseOrderNetDisAmount,
                                                                         k.PurchaseOrderVatAmt,
                                                                         k.PurchaseOrderVatRountAmt,
                                                                         k.PurchaseOrderNetAmount,
                                                                     } into grp
                                                                     select new ReportPurchaseOrder
                                                                     {
                                                                         PurchaseOrderNo = grp.Key.PurchaseOrderNo,
                                                                         PurchaseOrderDate = grp.Key.PurchaseOrderDate,
                                                                         PurchaseOrderType = grp.Key.PurchaseOrderType,
                                                                         PurchaseOrderSupInvNo = grp.Key.PurchaseOrderSupInvNo,
                                                                         // LocationMasterLocationName = grp.Key.LocationMasterLocationName,
                                                                         SuppliersMasterSupplierName = grp.Key.SuppliersMasterSupplierName,
                                                                         PurchaseOrderTotalGrossAmount = grp.Key.PurchaseOrderTotalGrossAmount,
                                                                         PurchaseOrderTotalItemDisAmount = grp.Key.PurchaseOrderTotalItemDisAmount,
                                                                         PurchaseOrderTotalDisAmount = grp.Key.PurchaseOrderTotalDisAmount,
                                                                         PurchaseOrderNetDisAmount = grp.Key.PurchaseOrderNetDisAmount,
                                                                         PurchaseOrderVatAmt = grp.Key.PurchaseOrderVatAmt,
                                                                         PurchaseOrderVatRountAmt = grp.Key.PurchaseOrderVatRountAmt,
                                                                         PurchaseOrderNetAmount = grp.Key.PurchaseOrderNetAmount

                                                                     }).ToList();

                return reportPurchaseOrder_ALL;
            }
            else
            {
                List<ReportPurchaseOrder> reportPurchaseOrder_ALL = _reportPurchaseOrderRepository.GetAsQueryable().Where(k =>
                (purchaseOrderreportParameters.rptSupplier == -1 ? true : (k.PurchaseOrderPartyId == purchaseOrderreportParameters.rptSupplier))
                          && (purchaseOrderreportParameters.rptCurrency == -1 ? true : ((k.PurchaseOrderCurrencyId == purchaseOrderreportParameters.rptCurrency)
                         && (purchaseOrderreportParameters.rptLocation == -1 ? true : (k.PurchaseOrderLocationId == purchaseOrderreportParameters.rptLocation))
                         && (purchaseOrderreportParameters.rptJob == -1 ? true : (k.PurchaseOrderJobId == purchaseOrderreportParameters.rptJob))
                          && (purchaseOrderreportParameters.rptSuppInvNo.Trim() == "" ? true : (k.PurchaseOrderSupInvNo.Trim() == purchaseOrderreportParameters.rptSuppInvNo.Trim()))
                          && (purchaseOrderreportParameters.rptInvoiceType == "ALL" ? true : (k.PurchaseOrderType == purchaseOrderreportParameters.rptInvoiceType))
                   //&& (purchasevoucherreportParameters.rptBrand == -1 ? true : (k.ItemBrandId == purchasevoucherreportParameters.rptBrand))
                   //  && (purchasevoucherreportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == purchasevoucherreportParameters.rptItemGroup))
                   && (purchaseOrderreportParameters.rptItem == -1 ? true : (k.ItemMasterItemId == purchaseOrderreportParameters.rptItem))
                    && (purchaseOrderreportParameters.rptDateRange == false ? true : (k.PurchaseOrderDate.Value.Date >= purchaseOrderreportParameters.rptDtpFrom.Value.Date)
                                && (k.PurchaseOrderDate.Value.Date <= purchaseOrderreportParameters.rptDtpTo.Value.Date))
                  && (k.PurchaseOrderDelStatus == false || k.PurchaseOrderDelStatus == null)))).OrderBy(x => x.PurchaseOrderId).ToList();

                return reportPurchaseOrder_ALL;
            }


            //return _reportPurchaseVoucherRepository.GetAll();

        }

        ///======================================================================================================================================================================================



        public IEnumerable<ReportPurchaseRequisition> MainReport_GetReportPurchaseRequisition_PARAM_CLASS(PurchaserequisitionReportParameters purchaserequisitionReportParameters, List<ViewStockTransferType> viewStockTransferType)
        {




            if (purchaserequisitionReportParameters.rptSummary == true)
            {

                List<ReportPurchaseRequisition> reportPurchaseRequisition_ALL = (from k in _reportPurchaseRequisitionRepository.GetAsQueryable()
                                                                                 where ((purchaserequisitionReportParameters.rptSupplier == -1 ? true : (k.PurchaseRequisitionPartyId == purchaserequisitionReportParameters.rptSupplier))
                                        && (purchaserequisitionReportParameters.rptCurrency == -1 ? true : ((k.PurchaseRequisitionCurrencyId == purchaserequisitionReportParameters.rptCurrency)
                                       && (purchaserequisitionReportParameters.rptLocation == -1 ? true : (k.PurchaseRequisitionLocationId == purchaserequisitionReportParameters.rptLocation))
                                       && (purchaserequisitionReportParameters.rptJob == -1 ? true : (k.PurchaseRequisitionJobId == purchaserequisitionReportParameters.rptJob))
                                         && (purchaserequisitionReportParameters.rptSuppInvNo.Trim() == "" ? true : (k.PurchaseRequisitionSupInvNo.Trim() == purchaserequisitionReportParameters.rptSuppInvNo.Trim()))
                                                                                  && (purchaserequisitionReportParameters.rptInvoiceType == "ALL" ? true : (k.PurchaseRequisitionType == purchaserequisitionReportParameters.rptInvoiceType))
                            && (purchaserequisitionReportParameters.rptBrand == -1 ? true : (k.ItemBrandId == purchaserequisitionReportParameters.rptBrand))
                                  && (purchaserequisitionReportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == purchaserequisitionReportParameters.rptItemGroup))
                                 && (purchaserequisitionReportParameters.rptItem == -1 ? true : (k.PurchaseRequisitionDetailsMatId == purchaserequisitionReportParameters.rptItem))

                                  && (purchaserequisitionReportParameters.rptDateRange == false ? true : (k.PurchaseRequisitionDate.Date >= purchaserequisitionReportParameters.rptDtpFrom.Value.Date)
                                              && (k.PurchaseRequisitionDate.Date <= purchaserequisitionReportParameters.rptDtpTo.Value.Date))
                                && (k.PurchaseRequisitionDelStatus == false || k.PurchaseRequisitionDelStatus == null))))
                                                                                 group k by new
                                                                                 {
                                                                                     k.PurchaseRequisitionNo,
                                                                                     k.PurchaseRequisitionDate,
                                                                                     k.PurchaseRequisitionType,
                                                                                     k.PurchaseRequisitionSupInvNo,
                                                                                     k.LocationMasterLocationName,
                                                                                     k.SuppliersMasterSupplierName,
                                                                                     k.PurchaseRequisitionTotalGrossAmount,
                                                                                     k.PurchaseRequisitionTotalItemDisAmount,
                                                                                     k.PurchaseRequisitionNetDisAmount,
                                                                                     k.PurchaseRequisitionTotalDisAmount,
                                                                                     k.PurchaseRequisitionVatAmt,
                                                                                     k.PurchaseRequisitionVatRountAmt,
                                                                                     k.PurchaseRequisitionNetAmount,
                                                                                 } into grp
                                                                                 select new ReportPurchaseRequisition
                                                                                 {
                                                                                     PurchaseRequisitionNo = grp.Key.PurchaseRequisitionNo,
                                                                                     PurchaseRequisitionDate = grp.Key.PurchaseRequisitionDate,
                                                                                     PurchaseRequisitionType = grp.Key.PurchaseRequisitionType,
                                                                                     PurchaseRequisitionSupInvNo = grp.Key.PurchaseRequisitionSupInvNo,
                                                                                     LocationMasterLocationName = grp.Key.LocationMasterLocationName,
                                                                                     SuppliersMasterSupplierName = grp.Key.SuppliersMasterSupplierName,
                                                                                     PurchaseRequisitionTotalGrossAmount = grp.Key.PurchaseRequisitionTotalGrossAmount,
                                                                                     PurchaseRequisitionTotalItemDisAmount = grp.Key.PurchaseRequisitionTotalItemDisAmount,
                                                                                     PurchaseRequisitionNetDisAmount = grp.Key.PurchaseRequisitionNetDisAmount,
                                                                                     PurchaseRequisitionTotalDisAmount = grp.Key.PurchaseRequisitionTotalDisAmount,
                                                                                     PurchaseRequisitionVatAmt = grp.Key.PurchaseRequisitionVatAmt,
                                                                                     PurchaseRequisitionVatRountAmt = grp.Key.PurchaseRequisitionVatRountAmt,
                                                                                     PurchaseRequisitionNetAmount = grp.Key.PurchaseRequisitionNetAmount

                                                                                 }).ToList();

                return reportPurchaseRequisition_ALL;
            }
            else
            {
                List<ReportPurchaseRequisition> reportPurchaseRequisition_ALL = _reportPurchaseRequisitionRepository.GetAsQueryable().Where(k =>
                (purchaserequisitionReportParameters.rptSupplier == -1 ? true : (k.PurchaseRequisitionPartyId == purchaserequisitionReportParameters.rptSupplier))
                          && (purchaserequisitionReportParameters.rptCurrency == -1 ? true : ((k.PurchaseRequisitionCurrencyId == purchaserequisitionReportParameters.rptCurrency)
                         && (purchaserequisitionReportParameters.rptLocation == -1 ? true : (k.PurchaseRequisitionLocationId == purchaserequisitionReportParameters.rptLocation))
                         && (purchaserequisitionReportParameters.rptJob == -1 ? true : (k.PurchaseRequisitionJobId == purchaserequisitionReportParameters.rptJob))
                          && (purchaserequisitionReportParameters.rptSuppInvNo.Trim() == "" ? true : (k.PurchaseRequisitionSupInvNo.Trim() == purchaserequisitionReportParameters.rptSuppInvNo.Trim()))
                          && (purchaserequisitionReportParameters.rptInvoiceType == "ALL" ? true : (k.PurchaseRequisitionType == purchaserequisitionReportParameters.rptInvoiceType))
                  && (purchaserequisitionReportParameters.rptBrand == -1 ? true : (k.ItemBrandId == purchaserequisitionReportParameters.rptBrand))
                    && (purchaserequisitionReportParameters.rptItemGroup == -1 ? true : (k.ItemGroupId == purchaserequisitionReportParameters.rptItemGroup))
                   && (purchaserequisitionReportParameters.rptItem == -1 ? true : (k.PurchaseRequisitionDetailsMatId == purchaserequisitionReportParameters.rptItem))
                    && (purchaserequisitionReportParameters.rptDateRange == false ? true : (k.PurchaseRequisitionDate.Date >= purchaserequisitionReportParameters.rptDtpFrom.Value.Date)
                                && (k.PurchaseRequisitionDate.Date <= purchaserequisitionReportParameters.rptDtpTo.Value.Date))
                  && (k.PurchaseRequisitionDelStatus == false || k.PurchaseRequisitionDelStatus == null)))).OrderBy(x => x.PurchaseRequisitionId).ToList();


                return reportPurchaseRequisition_ALL;
            }

            //return _reportPurchaseVoucherRepository.GetAll();

        }

    }
}
