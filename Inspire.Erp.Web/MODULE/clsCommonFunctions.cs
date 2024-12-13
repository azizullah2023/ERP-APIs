using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.MODULE
{
    public static class clsCommonFunctions
    {

 public static  StockRegister AddNewEntry_To_StockRegister(
                int StockRegisterStoreID,
                string StockRegisterRefVoucherNo,
       string StockRegisterTransType,
        DateTime StockRegisterVoucherDate,
       string StockRegisterRemarks,
       int? StockRegisterSno,
         int? StockRegisterMaterialId,
       int? StockRegisterUnitID,
       decimal? StockRegisterQuantity,
       decimal? StockRegisterSin,
       decimal? StockRegisterSout,
       string StockRegisterStatus,
        string StockRegisterBatchCode,
       decimal? StockRegisterRate,
       decimal? StockRegisterFcRate,
     decimal? StockRegisterAmount,
     decimal? StockRegisterFCAmount,
       decimal? StockRegisterLandingCost,
  int? StockRegisterLocationID,
        int? StockRegisterFsno,
       int? StockRegisterDepID,
         int? StockRegisterJobId,
        DateTime? StockRegisterExpDate,
       decimal? StockRegisterNetStkBal,
        bool? StockRegisterDelStatus
            )
        {

            StockRegister StockRegister = new StockRegister
            {

                StockRegisterStoreID = StockRegisterStoreID,
              
                StockRegisterVoucherDate = StockRegisterVoucherDate,
                StockRegisterRemarks = StockRegisterRemarks,
                StockRegisterSno = 0,
                StockRegisterQuantity = 0,
                StockRegisterSIN = StockRegisterSin,
                StockRegisterSout = StockRegisterSout,
                StockRegisterTransType = StockRegisterTransType,
                StockRegisterRefVoucherNo = StockRegisterRefVoucherNo,
               
                StockRegisterDepID = StockRegisterDepID,
                StockRegisterStatus = StockRegisterStatus,
                StockRegisterFSNO = StockRegisterFsno,
                StockRegisterBatchCode = StockRegisterBatchCode,
                StockRegisterJobID = StockRegisterJobId,
                StockRegisterRate = StockRegisterRate,
                StockRegisterFCAmount = StockRegisterFCAmount,
                
                StockRegisterLandingCost = StockRegisterLandingCost,
                
                StockRegisterLocationID = StockRegisterLocationID,
                
                StockRegisterAmount = StockRegisterAmount,
                StockRegisterUnitID = StockRegisterUnitID,
                
                StockRegisterExpDate = StockRegisterExpDate,
                
                StockRegisterNetStkBal = StockRegisterNetStkBal,
                StockRegisterMaterialID = StockRegisterMaterialId,
                
                StockRegisterFcRate = StockRegisterFcRate,
                StockRegisterDelStatus = StockRegisterDelStatus

            };
            return StockRegister;

        }




        public static AccountsTransactions AddNewEntry_To_AccountsTransactions(
                long AccountsTransactionsTransSno,
         string? AccountsTransactionsAccNo,
        DateTime AccountsTransactionsTransDate,
       string AccountsTransactionsParticulars,
       decimal AccountsTransactionsDebit,
       decimal AccountsTransactionsCredit,
       decimal? AccountsTransactionsFcDebit,
       decimal? AccountsTransactionsFcCredit,
       string AccountsTransactionsVoucherType,
       string AccountsTransactionsVoucherNo,
       string AccountsTransactionsDescription,
       long AccountsTransactionsUserId,
       string AccountsTransactionsStatus,
       DateTime AccountsTransactionsTstamp,
       string RefNo,
       decimal AccountsTransactionsFsno,
       decimal? AccountsTransactionsAllocDebit,
       decimal? AccountsTransactionsAllocCredit,
       decimal? AccountsTransactionsAllocBalance,
       decimal? AccountsTransactionsFcAllocDebit,
       decimal? AccountsTransactionsFcAllocCredit,
       decimal? AccountsTransactionsFcAllocBalance,
       int? AccountsTransactionsLocation,
       long? AccountsTransactionsJobNo,
       long? AccountsTransactionsCostCenterId,
       DateTime? AccountsTransactionsApprovalDt,
       int? AccountsTransactionsDepartment,
       decimal? AccountsTransactionsFcRate,
       int? AccountsTransactionsCompanyId,
       int? AccountsTransactionsCurrencyId,
       double? AccountsTransactionsDrGram,
       double? AccountsTransactionsCrGram,
       string AccountsTransactionsCheqNo,
       string AccountsTransactionsLpoNo,
       DateTime? AccountsTransactionsCheqDate,
       string AccountsTransactionsOpposEntryDesc,
         double? AccountsTransactionsAllocUpdateBal,
        long? AccountsTransactionsDeptId,
         string AccountsTransactionsVatno,
         decimal? AccountsTransactionsVatableAmount,
         bool? AccountstransactionsDelStatus
            )
        {

            AccountsTransactions accountsTransactions = new AccountsTransactions
            {

                AccountsTransactionsTransSno = AccountsTransactionsTransSno,
                AccountsTransactionsAccNo = AccountsTransactionsAccNo,
                AccountsTransactionsTransDate = AccountsTransactionsTransDate,
                AccountsTransactionsParticulars = AccountsTransactionsParticulars,
                AccountsTransactionsDebit = AccountsTransactionsDebit,
                AccountsTransactionsCredit = AccountsTransactionsCredit,
                AccountsTransactionsFcDebit = AccountsTransactionsFcDebit,
                AccountsTransactionsFcCredit = AccountsTransactionsFcCredit,
                AccountsTransactionsVoucherType = AccountsTransactionsVoucherType,
                AccountsTransactionsVoucherNo = AccountsTransactionsVoucherNo,
                AccountsTransactionsDescription = AccountsTransactionsDescription,
                AccountsTransactionsUserId = AccountsTransactionsUserId,
                AccountsTransactionsStatus = AccountsTransactionsStatus,
                AccountsTransactionsTstamp = AccountsTransactionsTstamp,
                RefNo = RefNo,
                AccountsTransactionsFsno = AccountsTransactionsFsno,
                AccountsTransactionsAllocDebit = AccountsTransactionsAllocDebit,
                AccountsTransactionsAllocCredit = AccountsTransactionsAllocCredit,
                AccountsTransactionsAllocBalance = AccountsTransactionsAllocBalance,
                AccountsTransactionsFcAllocDebit = AccountsTransactionsFcAllocDebit,
                AccountsTransactionsFcAllocCredit = AccountsTransactionsFcAllocCredit,
                AccountsTransactionsFcAllocBalance = AccountsTransactionsFcAllocBalance,
                AccountsTransactionsLocation = AccountsTransactionsLocation,
                AccountsTransactionsJobNo = AccountsTransactionsJobNo,
                AccountsTransactionsCostCenterId = AccountsTransactionsCostCenterId,
                AccountsTransactionsApprovalDt = AccountsTransactionsApprovalDt,
                AccountsTransactionsDepartment = AccountsTransactionsDepartment,
                AccountsTransactionsFcRate = AccountsTransactionsFcRate,
                AccountsTransactionsCompanyId = AccountsTransactionsCompanyId,
                AccountsTransactionsCurrencyId = AccountsTransactionsCurrencyId,
                AccountsTransactionsDrGram = AccountsTransactionsDrGram,
                AccountsTransactionsCrGram = AccountsTransactionsCrGram,
                AccountsTransactionsCheqNo = AccountsTransactionsCheqNo,
                AccountsTransactionsLpoNo = AccountsTransactionsLpoNo,
                AccountsTransactionsCheqDate = AccountsTransactionsCheqDate,
                AccountsTransactionsOpposEntryDesc = AccountsTransactionsOpposEntryDesc,
                AccountsTransactionsAllocUpdateBal = AccountsTransactionsAllocUpdateBal,
                AccountsTransactionsDeptId = AccountsTransactionsDeptId,
                AccountsTransactionsVatno = AccountsTransactionsVatno,
                AccountsTransactionsVatableAmount = AccountsTransactionsVatableAmount,
                AccountstransactionsDelStatus = false

            };
            return accountsTransactions;

        }









    }
}
