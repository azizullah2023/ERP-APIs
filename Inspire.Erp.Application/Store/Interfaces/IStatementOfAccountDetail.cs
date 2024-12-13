using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Store.Interface
{
    public interface IStatementOfAccountDetail
    {
        public Task<dynamic> StatementOfAccountDetailResponse(StatementOfAccountDetailRequest obj);
    }
}
