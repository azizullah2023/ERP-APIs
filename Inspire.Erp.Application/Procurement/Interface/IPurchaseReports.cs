using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Procurement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Interface
{
   public interface IPurchaseReports
    {
        Task<Response<List<PurchaseOrderReport>>> getPurdhaseOrderReport(GenericGridViewModel model);

    }
}
