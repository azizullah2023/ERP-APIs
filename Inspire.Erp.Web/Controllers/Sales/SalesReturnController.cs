using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels.sales;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.MODULE;

using Microsoft.AspNetCore.Mvc.Rendering;



namespace Inspire.Erp.Web.Controllers
{
    [Route("api/SalesReturn")]
    [Produces("application/json")]
    [ApiController]
    public class SalesReturnController : ControllerBase
    {
        private ISalesReturnService _salesReturnService;
        private readonly IMapper _mapper;
        public SalesReturnController(ISalesReturnService salesReturnService, IMapper mapper)
        {
            _salesReturnService = salesReturnService;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Route("SalesReturn_GetReportSalesReturn")]

        //public List<ReportSalesReturn> SalesReturn_GetReportSalesReturn()
        //{
        //    return _mapper.Map<List<ReportSalesReturn>>(_salesReturnService.SalesReturn_GetReportSalesReturn());


        //}



        //[HttpGet]
        //[Route("SalesReturn_GetReportSalesReturn")]
        //public ApiResponse<List<ReportSalesReturn>> SalesReturn_GetReportSalesReturn()
        //{



        //    ApiResponse<List<ReportSalesReturn>> apiResponse = new ApiResponse<List<ReportSalesReturn>>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<List<ReportSalesReturn>>(_salesReturnService.SalesReturn_GetReportSalesReturn()),
        //        Message = ""
        //    };
        //    return apiResponse;




        //}


        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                var item = _salesReturnService.GenerateVoucherNo(null);
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("InsertSalesReturn")]
        public IActionResult InsertSalesReturn([FromBody] DTOSalesReturnSave dataObj)
        {
            try
            {
                ApiResponse<SalesReturnViewModel> apiResponse = new ApiResponse<SalesReturnViewModel>();
                var param1 = _mapper.Map<SalesReturn>(dataObj.salesReturn);
                var param2 = _mapper.Map<List<SalesReturnDetails>>(dataObj.salesReturnDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(dataObj.accountsTransactions);
                //param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
                //clsAccountAndStock.SalesReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);
                var xs = _salesReturnService.InsertSalesReturn(param1, param3, param2, param4);
                return Ok(xs);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("UpdateSalesReturn")]
        public IActionResult UpdateSalesReturn([FromBody] DTOSalesReturnSave dataObj)
        {
            try
            {
                var param1 = _mapper.Map<SalesReturn>(dataObj.salesReturn);
                var param2 = _mapper.Map<List<SalesReturnDetails>>(dataObj.salesReturnDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(dataObj.accountsTransactions);
                param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
                //clsAccountAndStock.SalesReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);
                var xs = _salesReturnService.UpdateSalesReturn(param1, param3, param2, param4);
                return Ok(xs);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("DeleteSalesReturn")]
        public ApiResponse<int> DeleteSalesReturn([FromBody] SalesReturnViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<SalesReturn>(voucherCompositeView);
            var param2 = _mapper.Map<List<SalesReturnDetails>>(voucherCompositeView.SalesReturnDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //var xs = _salesReturnService.DeleteSalesReturn(  param1,    param3, param2
            //    //, param4
            //    );

            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.SalesReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _salesReturnService.DeleteSalesReturn(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = SalesReturnMessage.DeleteVoucher
            };

            return apiResponse;

        }



        [HttpGet]
        [Route("GetAllAccountTransaction")]
        public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        {



            ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
            {
                Valid = true,
                Result = _mapper.Map<List<AccountsTransactions>>(_salesReturnService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetSalesReturn")]
        public ApiResponse<List<SalesReturn>> GetAllSalesReturn()
        {


            ApiResponse<List<SalesReturn>> apiResponse = new ApiResponse<List<SalesReturn>>
            {
                Valid = true,
                Result = _salesReturnService.GetSalesReturn(),
                Message = ""
            };
            return apiResponse;





        }

        [HttpGet]
        [Route("GetSavedSalesReturnDetails/{id}")]
        public IActionResult GetSavedSalesReturnDetails(string id)
        {
            try
            {
                var salesReturn = _salesReturnService.GetSavedSalesReturnDetails(id);
                return Ok(salesReturn);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }



        [HttpGet]
        [Route("CheckVnoExist/{id}")]
        public ApiResponse<bool> CheckVnoExist(string id)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Valid = true,
                Result = true,
                Message = SalesReturnMessage.VoucherNumberExist



            };
            var x = _salesReturnService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }



    }
}








