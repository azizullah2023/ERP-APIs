using Microsoft.EntityFrameworkCore;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Application.Sales.Interface;
using Inspire.Erp.Application.Account.Interface;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Infrastructure.Database;
using System;
using Inspire.Erp.Domain.Modals.Stock;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals.AccountStatement;
using System.Linq.Dynamic.Core;
using Inspire.Erp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Inspire.Erp.Domain.Modals.Common;

namespace Inspire.Erp.Application.Account.implementations
{
    public class jobICastReports : IJobCastReport
    {
        private InspireErpDBContext _dbcontext;
        private readonly IRepository<JobCastDetailsSummary> _JobCastDetailsSummary;
        private readonly IRepository<AccountsTransactions> _AccountTransacions;
        public jobICastReports(IRepository<JobCastDetailsSummary> JobCastDetailsSummary, InspireErpDBContext context, IRepository<AccountsTransactions> AccountTransacions)
        {
            _JobCastDetailsSummary = JobCastDetailsSummary;
            _dbcontext = context;
            _AccountTransacions = AccountTransacions;
        }
        public async Task<Response<List<JobCastDetailsSummary>>> GetjobCastDetials(JobCastFilterModel model)
        {
           try
            {

              

               
                    string qry = string.Empty;
                    string JobLocId = string.Empty;

                    if (model.JobLocId > 0) JobLocId += $" and MT.MaAccType ='A' and MT.MaStatus ='R' and MT.MaSubHead ='Expenses' and mt.JobLocId = {model.JobLocId}";

                    if (model.JobNo!="" && model.JobNo != null) qry += $" and Convert.ToString(mt.AccountsTransactionsJobNo) = {model.JobNo}";

                    if (model.IsDateChecked == true)
                    {
                        qry += $" and AccountsTransactionsTransDate >  DateTime({model.fromDate}) and AccountsTransactionsTransDate <=  DateTime({model.toDate})";
                    }
                if (model.IsSummaryChecked == false)
                {
                    var queryDe = (from AT in _AccountTransacions.GetAsQueryable().Where($"1==1 {qry}")
                                   join MT in _dbcontext.MasterAccountsTable on Convert.ToString(AT.AccountsTransactionsAccNo) equals MT.MaAccNo
                                   join CC in _dbcontext.CostCenterMaster on AT.AccountsTransactionsCostCenterId equals CC.CostCenterMasterCostCenterId into ccGroup
                                   from CC in ccGroup.DefaultIfEmpty()
                                   select new JobCastDetailsSummary
                                   {
                                       AcHead = MT.MaAccName,
                                       AcNo = AT.AccountsTransactionsAccNo,
                                       VNo = AT.AccountsTransactionsVoucherNo,
                                       Labour = AT.RefNo,
                                       Date = Convert.ToString(AT.AccountsTransactionsTransDate),
                                       Particular = AT.AccountsTransactionsParticulars,
                                       Miscellaneous = AT.AccountsTransactionsDescription,
                                       VType = AT.AccountsTransactionsVoucherType,
                                       Amount = (AT.AccountsTransactionsFcDebit != null) ? AT.AccountsTransactionsFcDebit : 0,
                                       RunningBal = (AT.AccountsTransactionsAllocCredit != null) ? AT.AccountsTransactionsAllocCredit : 0,
                                       CostCenter = (CC != null) ? CC.CostCenterMasterCostCenterName : null
                                   }).ToList();
                    return Response<List<JobCastDetailsSummary>>.Success(queryDe, "Data Found");

                }
                else
                {
                    var querySummary = (from AT in _AccountTransacions.GetAsQueryable().Where($"1==1 {qry}")
                                   join MT in _dbcontext.MasterAccountsTable on Convert.ToString(AT.AccountsTransactionsAccNo) equals MT.MaAccNo
                                   join CC in _dbcontext.CostCenterMaster on AT.AccountsTransactionsCostCenterId equals CC.CostCenterMasterCostCenterId into ccGroup
                                   from CC in ccGroup.DefaultIfEmpty()
                                   select new JobCastDetailsSummary
                                   {
                                       AcHead = MT.MaAccName,
                                       AcNo = AT.AccountsTransactionsAccNo,
                                       VNo = AT.AccountsTransactionsVoucherNo,
                                       Labour = AT.RefNo,
                                       Date = Convert.ToString(AT.AccountsTransactionsTransDate),
                                       Particular = AT.AccountsTransactionsParticulars,
                                       Miscellaneous = AT.AccountsTransactionsDescription,
                                       VType = AT.AccountsTransactionsVoucherType,
                                       Amount = (AT.AccountsTransactionsFcDebit != null) ? AT.AccountsTransactionsFcDebit : 0,
                                       RunningBal = (AT.AccountsTransactionsAllocCredit != null) ? AT.AccountsTransactionsAllocCredit : 0,
                                       CostCenter = (CC != null) ? CC.CostCenterMasterCostCenterName : null
                                   }).ToList();
                    return Response<List<JobCastDetailsSummary>>.Success(querySummary, "Data Found");
                }
            }
            catch (Exception ex)
            {
                return Response<List<JobCastDetailsSummary>>.Fail(new List<JobCastDetailsSummary>(), ex.Message);
            }
        }
       
    }
}
       