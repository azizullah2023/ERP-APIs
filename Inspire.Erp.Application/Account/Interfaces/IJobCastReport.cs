using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interface
{
    public interface IJobCastReport
    {
        public Task<Response<List<JobCastDetailsSummary>>> GetjobCastDetials(JobCastFilterModel JobCastViewModel);
    }
}
