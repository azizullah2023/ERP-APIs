using Inspire.Erp.Application.Sales.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ICustomerSalesOrderService _SaleService;
        private readonly ISalesmanwiseSalesReport _salesmanwiseService;

        public SalesController(ICustomerSalesOrderService SaleService, ISalesmanwiseSalesReport salesmanwiseService)
        {
            _SaleService = SaleService;
            _salesmanwiseService = salesmanwiseService;
        }
        #region Customer Sales 
        [HttpGet("GetCustomerMasterDropdown")]
        public async Task<IActionResult> GetCustomerMasterDropdown()
        {
            return Ok(await _SaleService.GetCustomerMasterDropdown());
        }
        [HttpGet("GetJobMasterDropdown")]
        public async Task<IActionResult> GetJobMasterDropdown()
        {
            return Ok(await _SaleService.GetJobMasterDropdown());
        }
        [HttpGet("GetLocationMasterDropdown")]
        public async Task<IActionResult> GetLocationMasterDropdown()
        {
            return Ok(await _SaleService.GetLocationMasterDropdown());
        }
        [HttpGet("GetDepartmentMasterDropdown")]
        public async Task<IActionResult> GetDepartmentMasterDropdown()
        {
            return Ok(await _SaleService.GetDepartmentMasterDropdown());
        }
        [HttpGet("GetCurrencyMasterDropdown")]
        public async Task<IActionResult> GetCurrencyMasterDropdown()
        {
            return Ok(await _SaleService.GetCurrencyMasterDropdown());
        }
        [HttpGet("GetSalesManMasterDropdown")]
        public async Task<IActionResult> GetSalesManMasterDropdown()
        {
            return Ok(await _SaleService.GetSalesManMasterDropdown());
        }
        [HttpGet("GetItemMasterList")]
        public async Task<IActionResult> GetItemMasterList()
        {
            return Ok(await _SaleService.GetItemMasterList());
        }
        [HttpGet("GetUnitMasterDropdown")]
        public async Task<IActionResult> GetUnitMasterDropdown()
        {
            return Ok(await _SaleService.GetUnitMasterDropdown());
        }
        [HttpGet("GetSpecificOrder")]
        public async Task<IActionResult> GetSpecificOrder(int id = 0)
        {
            return Ok(await _SaleService.GetSpecificOrder(id));
        }
        [HttpPost("AddEditCustomerOrder")]
        public async Task<IActionResult> AddEditCustomerOrder(AddEditCustomerSalesOrderResponse model)
        {

            return Ok(await _SaleService.AddEditCustomerOrder(model));
        }
        //[HttpGet("GetSalesReport")]
        //public IEnumerable<SalesVoucherResponse> GetSalesReport()
        //{
        //    return _sales.GetSalesReport();
        //}

        [HttpPost("GetCustomerOrdersGridList")]
        public async Task<IActionResult> GetCustomerOrdersGridList(GenericGridViewModel model)
        {
            return Ok(await _SaleService.GetCustomerOrdersList(model));
        }
        #endregion
        #region Customer Salesmanwise Outstanding Report
        [HttpGet("GetSalesManMaster")]
        public async Task<IActionResult> GetSalesManMaster()
        {
            return Ok(await _salesmanwiseService.GetSalesManMaster());
        }
        [HttpGet("GetCustomerFromSaleMan")]
        public async Task<IActionResult> GetCustomerFromSaleMan(string query)
        {
            return Ok(await _salesmanwiseService.GetCustomerMasterDropdownFromSalesman(query));
        }

        [HttpPost("AccountTransactionsSalesmanWiseOutstandingReportDetail")]
        public async Task<IActionResult> AccountTransactionsSalesmanWiseOutstandingReportDetail(GenericGridViewModel model)
        {
            return Ok(await _salesmanwiseService.AccountTransactionsSalesmanWiseOutstandingReportDetail(model));
        }
        [HttpPost("AccountTransactionsSalesmanWiseOutstandingReportSummary")]
        public async Task<IActionResult> AccountTransactionsSalesmanWiseOutstandingReportSummary(GenericGridViewModel model)
        {
            return Ok(await _salesmanwiseService.AccountTransactionsSalesmanWiseOutstandingReportSummary(model));
        }
        #endregion
    }
}

