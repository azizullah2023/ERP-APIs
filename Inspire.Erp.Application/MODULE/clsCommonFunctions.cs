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
using SendGrid.Helpers.Mail;

namespace Inspire.Erp.Application.MODULE
{
    public static class clsCommonFunctions
    {

        //private static IRepository<StockRegister> _stockRegisterRepository;
        //private static  IRepository<AccountsTransactions> _accountTransactionRepository;
        //public static  void Delete_OldEntry_AccountsTransactions(string AccountsTransactionsVoucherNo, string AccountsTransactionsVoucherType)
        public static void Delete_OldEntry_AccountsTransactions(string AccountsTransactionsVoucherNo, string AccountsTransactionsVoucherType, IRepository<AccountsTransactions> _accountTransactionRepository)

        {




            List<AccountsTransactions> accttrans = _accountTransactionRepository.GetAsQueryable()
                 .Where(k => k.AccountsTransactionsVoucherNo == AccountsTransactionsVoucherNo && k.AccountsTransactionsVoucherType == AccountsTransactionsVoucherType && k.AccountstransactionsDelStatus != true).Select((l) => new AccountsTransactions
                 {
                     AccountsTransactionsTransSno = l.AccountsTransactionsTransSno,
                     AccountstransactionsDelStatus = true,

                     AccountsTransactionsAccNo = l.AccountsTransactionsAccNo,
                     AccountsTransactionsTransDate = l.AccountsTransactionsTransDate,
                     AccountsTransactionsParticulars = l.AccountsTransactionsParticulars,

                     AccountsTransactionsDebit = l.AccountsTransactionsDebit,


                     AccountsTransactionsCredit = l.AccountsTransactionsCredit,
                     AccountsTransactionsFcDebit = l.AccountsTransactionsFcDebit,
                     AccountsTransactionsFcCredit = l.AccountsTransactionsFcCredit,

                     AccountsTransactionsVoucherType = l.AccountsTransactionsVoucherType,







                     AccountsTransactionsVoucherNo = l.AccountsTransactionsVoucherNo,
                     AccountsTransactionsDescription = l.AccountsTransactionsDescription,
                     AccountsTransactionsUserId = l.AccountsTransactionsUserId,

                     AccountsTransactionsStatus = l.AccountsTransactionsStatus,







                     AccountsTransactionsTstamp = l.AccountsTransactionsTstamp,
                     RefNo = l.RefNo,
                     AccountsTransactionsFsno = l.AccountsTransactionsFsno,

                     AccountsTransactionsAllocDebit = l.AccountsTransactionsAllocDebit,



                     AccountsTransactionsAllocCredit = l.AccountsTransactionsAllocCredit,
                     AccountsTransactionsAllocBalance = l.AccountsTransactionsAllocBalance,
                     AccountsTransactionsFcAllocDebit = l.AccountsTransactionsFcAllocDebit,

                     AccountsTransactionsFcAllocCredit = l.AccountsTransactionsFcAllocCredit,




                     AccountsTransactionsFcAllocBalance = l.AccountsTransactionsFcAllocBalance,
                     AccountsTransactionsLocation = l.AccountsTransactionsLocation,
                     AccountsTransactionsJobNo = l.AccountsTransactionsJobNo,

                     AccountsTransactionsCostCenterId = l.AccountsTransactionsCostCenterId,






                     AccountsTransactionsApprovalDt = l.AccountsTransactionsApprovalDt,
                     AccountsTransactionsDepartment = l.AccountsTransactionsDepartment,
                     AccountsTransactionsFcRate = l.AccountsTransactionsFcRate,

                     AccountsTransactionsCompanyId = l.AccountsTransactionsCompanyId,

                     AccountsTransactionsCurrencyId = l.AccountsTransactionsCurrencyId,
                     AccountsTransactionsDrGram = l.AccountsTransactionsDrGram,
                     AccountsTransactionsCrGram = l.AccountsTransactionsCrGram,

                     AccountsTransactionsCheqNo = l.AccountsTransactionsCheqNo,


                     AccountsTransactionsLpoNo = l.AccountsTransactionsLpoNo,
                     AccountsTransactionsCheqDate = l.AccountsTransactionsCheqDate,
                     AccountsTransactionsOpposEntryDesc = l.AccountsTransactionsOpposEntryDesc,

                     AccountsTransactionsAllocUpdateBal = l.AccountsTransactionsAllocUpdateBal,






                     AccountsTransactionsDeptId = l.AccountsTransactionsDeptId,
                     AccountsTransactionsVatno = l.AccountsTransactionsVatno,
                     AccountsTransactionsVatableAmount = l.AccountsTransactionsVatableAmount,








                 }).ToList();
            _accountTransactionRepository.DeleteList(accttrans);
            //_accountTransactionRepository.UpdateList(accttrans);
        }

        public static void Delete_OldEntry_StockRegister(string StockRegisterVoucherNo, string StockRegisterTransType, IRepository<StockRegister> _stockRegisterRepository)
        {
            List<StockRegister> stockTrans = _stockRegisterRepository.GetAsQueryable()
                 .Where(k => k.StockRegisterRefVoucherNo == StockRegisterVoucherNo && k.StockRegisterTransType == StockRegisterTransType && k.StockRegisterDelStatus != true).Select((l) => new StockRegister
                 {
                     StockRegisterStoreID = l.StockRegisterStoreID,

                     //StockRegisterVoucherDate = l.StockRegisterVoucherDate,
                     StockRegisterRemarks = l.StockRegisterRemarks,
                     StockRegisterSno = l.StockRegisterSno,
                     StockRegisterQuantity = l.StockRegisterQuantity,
                     StockRegisterSIN = l.StockRegisterSIN,
                     StockRegisterSout = l.StockRegisterSout,
                     StockRegisterTransType = l.StockRegisterTransType,
                     StockRegisterRefVoucherNo = l.StockRegisterRefVoucherNo,

                     //StockRegisterDepID =  l.StockRegisterDepID,
                     StockRegisterStatus = l.StockRegisterStatus,
                     StockRegisterFSNO = l.StockRegisterFSNO,
                     StockRegisterBatchCode = l.StockRegisterBatchCode,
                     StockRegisterJobID = l.StockRegisterJobID,
                     StockRegisterRate = l.StockRegisterRate,
                     StockRegisterFCAmount = l.StockRegisterFCAmount,

                     //StockRegisterLandingCost =  l.StockRegisterLandingCost,

                     StockRegisterLocationID = l.StockRegisterLocationID,

                     StockRegisterAmount = l.StockRegisterAmount,
                     //StockRegisterUnitID =  l.StockRegisterUnitID,

                     StockRegisterExpDate = l.StockRegisterExpDate,

                     StockRegisterNetStkBal = l.StockRegisterNetStkBal,
                     StockRegisterMaterialID = l.StockRegisterMaterialID,

                     //StockRegisterFcRate =  l.StockRegisterFcRate,
                     StockRegisterDelStatus = false




                 }).ToList();
            _stockRegisterRepository.DeleteList(stockTrans);
            //_stockRegisterRepository.UpdateList(stockTrans);

        }


        public static void Delete_OldEntryOf_StockRegister(string Vno, string StockRegisterTransType, IRepository<StockRegister> _stockRegisterRepository)
        {
            var data = _stockRegisterRepository.GetAsQueryable().Where(a => a.StockRegisterPurchaseID == Vno && a.StockRegisterTransType.Trim() == StockRegisterTransType).ToList();
            if (data != null)
            {
                _stockRegisterRepository.DeleteList(data);
            }
        }

        public static VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate, string voucherType, string voucherPrefix,
                IRepository<ProgramSettings> _programsettingsRepository,
         IRepository<VouchersNumbers> _voucherNumbersRepository)
        {
            try
            {

                var prefix = _programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == voucherPrefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == voucherType)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = voucherType,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = VoucherGenDate

                };
                _voucherNumbersRepository.Insert(vouchersNumbers);
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //public static void Delete_OldEntry_StockRegister(string StockRegisterVoucherNo, string StockRegisterTransType, IRepository<StockRegister> _stockRegisterRepository)
        //{
        //    List<StockRegister> stockTrans = _stockRegisterRepository.GetAsQueryable()
        //         .Where(k => k.StockRegisterRefVoucherNo == StockRegisterVoucherNo && k.StockRegisterTransType == StockRegisterTransType && k.StockRegisterDelStatus != true).Select((l) => new StockRegister
        //         {
        //             StockRegisterStoreID = l.StockRegisterStoreID,

        //             //StockRegisterVoucherDate = l.StockRegisterVoucherDate,
        //             StockRegisterRemarks = l.StockRegisterRemarks,
        //             StockRegisterSno = l.StockRegisterSno,
        //             StockRegisterQuantity = l.StockRegisterQuantity,
        //             StockRegisterSIN = l.StockRegisterSIN,
        //             StockRegisterSout = l.StockRegisterSout,
        //             StockRegisterTransType = l.StockRegisterTransType,
        //             StockRegisterRefVoucherNo = l.StockRegisterRefVoucherNo,

        //             //StockRegisterDepID =  l.StockRegisterDepID,
        //             StockRegisterStatus = l.StockRegisterStatus,
        //             StockRegisterFSNO = l.StockRegisterFSNO,
        //             StockRegisterBatchCode = l.StockRegisterBatchCode,
        //             StockRegisterJobID = l.StockRegisterJobID,
        //             StockRegisterRate = l.StockRegisterRate,
        //             StockRegisterFCAmount = l.StockRegisterFCAmount,

        //             //StockRegisterLandingCost =  l.StockRegisterLandingCost,

        //             StockRegisterLocationID = l.StockRegisterLocationID,

        //             StockRegisterAmount = l.StockRegisterAmount,
        //             //StockRegisterUnitID =  l.StockRegisterUnitID,

        //             StockRegisterExpDate = l.StockRegisterExpDate,

        //             StockRegisterNetStkBal = l.StockRegisterNetStkBal,
        //             StockRegisterMaterialID = l.StockRegisterMaterialID,

        //             //StockRegisterFcRate =  l.StockRegisterFcRate,
        //             StockRegisterDelStatus = false




        //         }).ToList();
        //    _stockRegisterRepository.DeleteList(stockTrans);
        //    //_stockRegisterRepository.UpdateList(stockTrans);

        //}

        public static double getConverionCurrencyRate(int? itemid, IRepository<CurrencyMaster> _currencyMasterRepository)
        {
            try
            {
                return (double)_currencyMasterRepository.GetAsQueryable().Where(a => a.CurrencyMasterCurrencyId == itemid).Select(c => c.CurrencyMasterCurrencyRate ?? 0).FirstOrDefault();
            }
            catch
            {
                return 1;
            }
        }




    }
}
