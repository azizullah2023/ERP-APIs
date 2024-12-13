using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Procurement
{
    [Route("api/PrecurementReports")]
    [Produces("application/json")]
    [ApiController]
    public class PrecurementReportsController : Controller
    {
        private IPurchaseReports _reports;

        public PrecurementReportsController(IPurchaseReports reports)
        {
            _reports = reports;
        }

        //irfan 08 Dec 2023
        /// <summary>
        /// Name : GetPurchaseOrderReport
        /// Desc : this method is used to get data form purchase order report 
        /// </summary>
        /// <param name="model"> Need to fill only the filter property of modal
        /// model.Filter = x=>x.PurchaseOrderDate <= '' && x.PurchaseOrderDate >= '' ===>date filter
        /// model.Filter = x=>x.PurchaseOrderSupInvNo = ''===>supplier filter
        /// model.Filter = x=>x.PurchaseOrderJobId = ''===>job filter
        /// model.Filter = x=>x.PurchaseOrderApproveStatus = ''===>Po Status filter
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPurchaseOrderReport")]
        public async Task<IActionResult> GetPurchaseOrderReport(GenericGridViewModel model)
        {
            return Ok(await _reports.getPurdhaseOrderReport(model));
        }
    }
}
