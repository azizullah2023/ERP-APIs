using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using NPOI.SS.UserModel;
using Inspire.Erp.Application.Master.Interfaces;

namespace Inspire.Erp.Web.Controllers.Procurement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressInvoiceController : ControllerBase
    {
        private InspireErpDBContext _context;
        private IRepository<ProgressiveInvoice> _progressiveInvoice; private IRepository<CurrencyMaster> currencyrepository;
        private IRepository<ViewAccountsTransactionsVoucherType> _viewAccountsTransactionsVoucherTypeRepository;
        private readonly IProgressiveInvoiceService _ProgressiveInvoice;
        private readonly IMapper _mapper;
        public ProgressInvoiceController(IProgressiveInvoiceService Progressiveservice, IMapper mapper, IRepository<ProgressiveInvoice> progressiveInvoice)
        {
                 _mapper = mapper;
               _ProgressiveInvoice = Progressiveservice;
        }  
        [HttpPost]
        [Route("InsertProgressInvoice")]
        public ApiResponse<ProgressiveInvoice> InsertProgressInvoice([FromBody] ProgressiveInvoice progressiveInvoice)
        {

            var param1 = _mapper.Map<ProgressiveInvoice>(progressiveInvoice);
            var param2 = _mapper.Map<List<AccountsTransactions>>(progressiveInvoice.AccountsTransactions);
            var response = _ProgressiveInvoice.InsertProgressInvoice(param1,param2);
            var res = new ApiResponse<ProgressiveInvoice>
            {
                Valid = true,
                Message = "Record has been Saved.",
                Result = response,
            };

            return res;
        }
        [HttpPost("UpdateProgressInvoice")]
        public ApiResponse<ProgressiveInvoice> UpdateProgressInvoice([FromBody] ProgressiveInvoice progressiveInvoice)
        {
            var param1 = _mapper.Map<ProgressiveInvoice>(progressiveInvoice);
            var param2 = _mapper.Map<List<AccountsTransactions>>(progressiveInvoice.AccountsTransactions);
            var response = _ProgressiveInvoice.UpdateProgressInvoice(param1, param2);
            var res = new ApiResponse<ProgressiveInvoice>
            {
                Valid = true,
                Message = "Record has been Updated.",
                Result = response,
            };
            return res;
        }
        [HttpPost]
        [Route("DeleteProgressInvoice")]
        public ApiResponse<int> DeleteProgressInvoice([FromBody] ProgressiveInvoice dataObj)
        {

            var param1 = _mapper.Map<ProgressiveInvoice>(dataObj);
            var param2 = _mapper.Map<List<AccountsTransactions>>(dataObj.AccountsTransactions);
            param2 = new List<AccountsTransactions>();
            var xs = _ProgressiveInvoice.DeleteProgressInvoice(param1, param2);
            
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = "Record has been Deleted."
            };
            return apiResponse;

        }
        [HttpGet("GetProgressInvoiceReport")]
        public IActionResult GetProgressInvoiceReport()
        {
            try
            {
                var item = _ProgressiveInvoice.GetProgressInvoiceReport();
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpGet("GetSavedProgressiveInoices/{id}")]
        public ApiResponse<ProgressiveInvoice> GetSavedProgressiveInoices(string id)
        {
            var ProgressiveInvoice = _ProgressiveInvoice.GetSavedProgressiveInoices(id);

            ApiResponse<ProgressiveInvoice> apiResponse = new ApiResponse<ProgressiveInvoice>
            {
                Valid = true,
                Result = ProgressiveInvoice,
                Message = ""
            };
            return apiResponse;
        }
    }
}
