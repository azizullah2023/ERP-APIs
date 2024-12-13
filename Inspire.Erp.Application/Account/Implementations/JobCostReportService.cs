using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.DTO.Job_Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class JobCostReportService : IJobCostReportService
    {
       
        private IRepository<AccountsTransactions> _accountsTransactionsRepository;
        private IRepository<MasterAccountsTable> _masterAccountsTableRepository;
        private IRepository<CostCenterMaster> _costCenterMasterRepository;

        public JobCostReportService(IRepository<AccountsTransactions> accountsTransactionsRepository, IRepository<MasterAccountsTable> masterAccountsTableRepository, IRepository<CostCenterMaster> costCenterMasterRepository)
        {
            _accountsTransactionsRepository = accountsTransactionsRepository;
            _masterAccountsTableRepository = masterAccountsTableRepository;
            _costCenterMasterRepository = costCenterMasterRepository;

        }
        public async Task<Response<List<JobMasterExpenseDto>>> JobCostReportDetails(JobCostReportSearchFilter filter)
        {
            List<JobMasterExpenseDto> reportDetails = new List<JobMasterExpenseDto>();
            string filteredValue = " && 1 == 1";

            try
            {

                if (filter.JobNo > 0)
                {
                    filteredValue += $" && AccountsTransactionsJobNo == {filter.JobNo} ";
                }
                if (filter.CostCenterId > 0)
                {
                    filteredValue += $" && AccountsTransactionsCostCenterId == {filter.CostCenterId} ";
                }
                if (filter.IsdateSelected)
                {
                    if (!string.IsNullOrEmpty(filter.fromDate) && !string.IsNullOrEmpty(filter.toDate))
                    {
                        filteredValue += $" && AccountsTransactionsTransDate >= \"{Convert.ToDateTime(filter.fromDate)}\" && AccountsTransactionsTransDate <= \"{Convert.ToDateTime(filter.toDate)}\" ";
                    }

                    
                }


                reportDetails = await (from at in _accountsTransactionsRepository.GetAsQueryable().Where($"1 == 1  {filteredValue}")
                       join mat in _masterAccountsTableRepository.GetAsQueryable() on at.AccountsTransactionsAccNo equals mat.MaAccNo into jcGroup
                       from mat in jcGroup.DefaultIfEmpty()
                       join cos in _costCenterMasterRepository.GetAsQueryable() on at.AccountsTransactionsCostCenterId equals cos.CostCenterMasterCostCenterId into ccGroup
                       from cos in ccGroup.DefaultIfEmpty()
                       where (mat.MaMainHead == "Expenses"  && at.AccountstransactionsDelStatus == false)
                       select new JobMasterExpenseDto {

                           accNo  = at.AccountsTransactionsAccNo,
                           accName = mat.MaAccName,
                           Description = at.AccountsTransactionsParticulars,
                           VoucherNo = at.AccountsTransactionsVoucherNo,
                           VoucherType = at.AccountsTransactionsVoucherType,
                           transDate = at.AccountsTransactionsTransDate,
                           CrAmt = at.AccountsTransactionsCredit,
                           DrAmt = at.AccountsTransactionsDebit,
                           CostName = cos.CostCenterMasterCostCenterName
                       }).ToListAsync();

                return new Response<List<JobMasterExpenseDto>>
                {
                    Valid = true,
                    Result = reportDetails,
                    Message = "Job Cost Data Fonud"
                };
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
