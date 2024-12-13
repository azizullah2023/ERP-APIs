using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Procurement
{
    [Route("api/PurchaseRequisitionReports")]
    [Produces("application/json")]
    [ApiController]
    public class PurchaseRequisitionReportsController : Controller
    {

        IPurchaseRequisitionStatusReport purchaseRequisitionStatus;

        public PurchaseRequisitionReportsController(IPurchaseRequisitionStatusReport purchaseRequisitionStatus)
        {
            this.purchaseRequisitionStatus = purchaseRequisitionStatus;
        }

        [HttpPost]
        [Route("GetPurchaseRequisitionStatusReport")]
        public async Task<IActionResult> GetPurchaseRequisitionStatusReport(ReportFilter model)
        {
            return Ok(await this.purchaseRequisitionStatus.GetPurchaseRequisitionStatusReport(model));
        }
    }
}
