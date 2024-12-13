using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Store.Interface
{
    public interface IProfitAndLoss
    {
        public IEnumerable<Inspire.Erp.Domain.Modals.ProfitAndLossResponse> ProfitAndLossResponse(ProfitAndLossRequest obj);
    }
}
