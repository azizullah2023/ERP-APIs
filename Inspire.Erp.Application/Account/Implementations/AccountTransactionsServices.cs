using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class AccountTransactionsServices : IAccountTransactionsServices
    {
        private IRepository<AllocationVoucherDetails> allocationVDlts;
        private IRepository<AccountsTransactions> _accountTransactionRepository;

        public AccountTransactionsServices(IRepository<AllocationVoucherDetails> allocationVDlts, IRepository<AccountsTransactions> accountTransactionRepository)
        {
            this.allocationVDlts = allocationVDlts;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<ApiResponse<List<AllocationDetails>>> GetReceiptVoucherAllocations(string accNo, string refNo, string vType)
        {
            try
            {
                await Task.Delay(500);
                var result = (from AD in this.allocationVDlts.GetAll()
                              join AT in _accountTransactionRepository.GetAll() on AD.AllocationVoucherDetailsTransSno equals (int)AT.AccountsTransactionsTransSno
                              where AD.AllocationVoucherDetailsAccNo == accNo && AD.AllocationVoucherDetailsVno.ToUpper() == refNo.ToUpper() && AD.AllocationVoucherDetailsRefVtype.ToUpper() == vType.ToUpper()
                              select new Domain.Models.AllocationDetails()
                              {
                                  TransNo = AT.AccountsTransactionsTransSno,
                                  Status = "P",
                                  TransDate = AT.AccountsTransactionsTransDate,
                                  VoucherNo = AD.AllocationVoucherDetailsVno,
                                  Type = AD.AllocationVoucherDetailsRefVtype,
                                  Credit = (decimal)AT.AccountsTransactionsCredit > 0 ? AT.AccountsTransactionsCredit : 0,
                                  Debit = (decimal)AT.AccountsTransactionsDebit > 0 ? AT.AccountsTransactionsDebit : 0,
                                  Balance = AT.AccountsTransactionsAllocBalance,
                                  AllocAmount = AT.AccountsTransactionsAllocCredit > 0 ? AT.AccountsTransactionsAllocCredit : AT.AccountsTransactionsAllocDebit,
                                  NetAllocation = getNatAllocationAmount(AT.AccountsTransactionsTransSno),
                                  RefLocation = AD.AllocationVoucherDetailsRefLocationId.ToString()
                              }).ToList();


                var transValues = _accountTransactionRepository.GetAsQueryable()
                                      .Where(a => a.AccountsTransactionsAccNo.ToUpper() == accNo.ToUpper()
                                        && a.AccountsTransactionsAllocBalance > 0
                                        && ((vType == VoucherType.Receipt && a.AccountsTransactionsDebit != 0 && a.AccountsTransactionsVoucherType == VoucherType.SalesVoucher_TYPE)
                                      || (vType == VoucherType.Payment && a.AccountsTransactionsCredit != 0 && a.AccountsTransactionsVoucherType == VoucherType.PurchaseVoucher_TYPE)))
                                        .Select(g => new Domain.Models.AllocationDetails()
                                        {
                                            TransNo = g.AccountsTransactionsTransSno,
                                            Status = "P",
                                            TransDate = g.AccountsTransactionsTransDate,
                                            VoucherNo = g.AccountsTransactionsVoucherNo,
                                            Type = g.AccountsTransactionsVoucherType,
                                            Credit = (decimal)g.AccountsTransactionsCredit > 0 ? g.AccountsTransactionsCredit : 0,
                                            Debit = (decimal)g.AccountsTransactionsDebit > 0 ? g.AccountsTransactionsDebit : 0,
                                            Balance = g.AccountsTransactionsAllocBalance,
                                            AllocAmount = g.AccountsTransactionsAllocCredit > 0 ? g.AccountsTransactionsAllocCredit : g.AccountsTransactionsAllocDebit,
                                            RefLocation = g.AccountsTransactionsLocation.ToString()
                                        }).ToList();



                result.AddRange(transValues);

                return ApiResponse<List<AllocationDetails>>.Success(result, "Data Found");
            }
            catch (Exception ex)
            {

                return ApiResponse<List<AllocationDetails>>.Fail(ex.Message);
            }
        }




        private decimal? getNatAllocationAmount(long transNo)
        {
            try
            {
                var allocValue = allocationVDlts.GetAll()
                                        .Where(a => a.AllocationVoucherDetailsTransSno == transNo)
                                        .GroupBy(a => true)
                                        .Select(g => Math.Abs((g.Sum(a => a.AllocationVoucherDetailsAllocDebit ?? 0) - g.Sum(a => a.AllocationVoucherDetailsAllocCredit ?? 0))))
                                        .FirstOrDefault();
                var transValue = _accountTransactionRepository.GetAll()
                                        .Where(a => a.AccountsTransactionsTransSno == transNo)
                                        .GroupBy(a => true)
                                        .Select(g => Math.Abs((g.Sum(a => a.AccountsTransactionsDebit) - g.Sum(a => a.AccountsTransactionsCredit))))
                                        .FirstOrDefault();
                var diff = transValue - (decimal)allocValue;
                return diff;
            }
            catch (Exception) { return 0; }
        }

        //public async Task<ResponseInfo> getAllocationforPrint(string accNo, DateTime date, string vType)
        //{
        //    var objresponse = new ResponseInfo();
        //    var dt = date.Date;
        //    var details = await this.allocationVDlts.GetAsQueryable().Where(a => a.RefSPDate.Value.Date == dt && a.RefSPAccountNo == accNo && a.AllocationVoucherDetailsRefVtype == vType).Select(c => new
        //    {
        //        c.AllocationVoucherDetailsVno,
        //        c.AllocationVoucherDetailsRefVtype,
        //        c.RefSPAccountNo,
        //        c.AllocationVoucherDetailsAllocCredit,
        //        c.AllocationVoucherDetailsAllocDebit,
        //        c.RefSPDate
        //    }).ToListAsync();
        //    objresponse.ResultSet = new
        //    {
        //        details = details,
        //    };
        //    return objresponse;
        //}

        public async Task<ResponseInfo> getAllocationforPrint(int? Id)
        {
            var objresponse = new ResponseInfo();            
            var details = await this.allocationVDlts.GetAsQueryable().Where(a => a.AllocationVoucherDetailsSno== Id).Select(c => new
            {
                c.AllocationVoucherDetailsVno,
                c.AllocationVoucherDetailsRefVtype,
                //c.RefSPAccountNo,
                c.AllocationVoucherDetailsAllocCredit,
                c.AllocationVoucherDetailsAllocDebit,
                c.RefSPDate.Value.Date
            }).ToListAsync();
            objresponse.ResultSet = new
            {
                details = details,
            };
            return objresponse;
        }

    }
}
