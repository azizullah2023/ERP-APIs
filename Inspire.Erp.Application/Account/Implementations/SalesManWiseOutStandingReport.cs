using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Account.Implementations
{
    class SalesManWiseOutStandingReport : ISalesManWiseOutStandingReport
    {

        private readonly IRepository<AccountsTransactions> _accountTrans;
        private readonly IRepository<SalesVoucher> _salesVoucher;
        private readonly IRepository<SalesManMaster> _salesManMaster;
        private readonly IRepository<CustomerMaster> _customerMaster;
        private readonly IRepository<JournalVoucher> _journalVoucher;

        //public SalesManWiseOutStandingReport(IRepository<AccountsTransactions> accountTrans, IRepository<SalesVoucher> salesVoucher, IRepository<SalesManMaster> salesManMaster, IRepository<CustomerMaster> customerMaster, IRepository<JournalVoucher> journalVoucher)
        //{
        //    _accountTrans = accountTrans;
        //    _salesVoucher = salesVoucher;
        //    _salesManMaster = salesManMaster;
        //    _customerMaster = customerMaster;
        //    _journalVoucher = journalVoucher;
        //}

        //public SalesmanWiseOutstandingRptResponse GetSalesmanWiseOutstandingRpt()
        //{
        //    SalesmanWiseOutstandingRptResponse res = new SalesmanWiseOutstandingRptResponse();
        //    try
        //    {
        //        var result = from at in _accountTrans.GetAll()
        //                     join sv in _salesVoucher.GetAll() on at.AccountsTransactionsVoucherNo equals sv.SalesVoucherNo into atsv
        //                     from sv in atsv.DefaultIfEmpty()
        //                     join sm in _salesManMaster.GetAll() on sv?.SalesVoucherSalesManId equals sm.SalesManMasterSalesManId into svtosm
        //                     from sm in svtosm.DefaultIfEmpty()
        //                     join c in _customerMaster.GetAll() on sv?.SalesVoucherPartyId equals c.CustomerMasterCustomerUserId into svtoc
        //                     from c in svtoc.DefaultIfEmpty() where !_journalVoucher.GetAll().Any(jv => jv.JournalVoucher_VNO.ToString() == at.AccountsTransactionsVoucherNo  && jv.JournalVoucher_VoucherType == "I")
        //                     && at.AccountsTransactionsAllocBalance > 0
        //                     && at.AccountsTransactionsTransDate >= new DateTime(2022, 4, 1)
        //                     && at.AccountsTransactionsTransDate <= new DateTime(2023, 4, 5)

        //        //                _accountTrans.GetAll().Where(mat => mat.RefNo == "AS3500")
        //        //                .Select(mat => mat.AccountsTransactionsAccNo)
        //        //                .Contains(at.AccountsTransactionsAccNo.ToUpper())
                             
        //                     orderby at.AccountsTransactionsTransDate
        //                     select new
        //                     {
        //                         Salesman = sm?.SalesManMasterSalesManName,
        //                         Customer = sv?.SalesVoucherType == "CASH" ? sv.SalesVoucherPartyName: c?.CustomerMasterCustomerName,
        //                         VoucherNo = at.AccountsTransactionsVoucherNo,
        //                         VoucherType = at.AccountsTransactionsVoucherType,
        //                         AccNo = at.AccountsTransactionsAccNo,
        //                         TransDate = at.AccountsTransactionsTransDate,
        //                         Description = at.AccountsTransactionsDescription,
        //                         Debit = at.AccountsTransactionsFcDebit,
        //                         Credit = at.AccountsTransactionsFcCredit,
        //                         Settled = at.AccountsTransactionsAllocDebit,
        //                         Balance = at.AccountsTransactionsFcDebit > at.AccountsTransactionsFcCredit? at.AccountsTransactionsAllocBalance: at.AccountsTransactionsAllocBalance* -1,
        //                         RunningBal = 0,
        //                         Days = (DateTime.Parse("2023/April/03") - at.AccountsTransactionsTransDate).Value.Days
        //                     };

               
        //        return res;
        //    }catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
