using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Interface
{
    public interface IStatementOfAccountSummary
    {
        //public IEnumerable<StatementOfAccountSummaryResponse> StatementOfAccountSummaryResponse(StatementOfAccountSummaryRequest obj);
        public Task<string> StatementOfAccountSummaryResponse(StatementOfAccountSummaryRequest obj);
    }
}
