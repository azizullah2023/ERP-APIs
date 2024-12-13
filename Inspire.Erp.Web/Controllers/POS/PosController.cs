using AutoMapper;
using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Entities.POS;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models;
//using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Inspire.Erp.Web.Controllers.POS
{
    [Route("api/pos/item")]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class POSController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IItemMasterService itemMasterService;
        private ISalesVoucherService _salesVoucherService;
        private readonly IloginService _loginsvc;
        public POSController(IItemMasterService _itemMasterService, IMapper mapper, ISalesVoucherService salesVoucherService, IloginService loginsvc)
        {

            itemMasterService = _itemMasterService;
            _mapper = mapper;
            _salesVoucherService = salesVoucherService;
            _loginsvc = loginsvc;
        }


        [HttpPost]
        [Route("SearchItemByBarcodeLink")]
        public async Task<IActionResult> SearchItemByBarcodeLink(ItemBarCodeSearchFilter model)
        {
            return Ok(await itemMasterService.SearchItemByBarcode(model));
        }

        [HttpPost]
        [Route("GetWorkPeriods")]
        public async Task<IActionResult> GetWorkPeriods(WorkPeriodFilter model)
        {
            return Ok(await itemMasterService.GetWorkPeriod(model));

        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<IActionResult> GetItems(string? barCode)
        {
            return Ok(await itemMasterService.GetItems(barCode));
        }



 

        [HttpGet]
        [Route("Resettlement")]
        public async Task<IActionResult> Resettlement(string voucherNo)
        {
            return Ok(await itemMasterService.Resettlement(voucherNo));
        }


        [HttpGet]
        [Route("BillRecall")]
        public async Task<IActionResult> BillRecall(string voucherNo)
        {
            return Ok(await itemMasterService.BillRecall(voucherNo));
        }

        [HttpGet]
        [Route("LoadHoldBills")]
        public async Task<IActionResult> LoadHoldBills(int workPeriodId)
        {
            return Ok(await itemMasterService.LoadHoldBills(workPeriodId));
        }


        [HttpGet]
        [Route("VoidDiscount")]
        public async Task<IActionResult> VoidDiscount(VoidRequest voidr)
        {
            return Ok(await itemMasterService.VoidBill(voidr));
        }

        [HttpGet]
        [Route("SearchHoldBill")]
        public async Task<IActionResult> SearchHoldBill(string voucherNo)
        {
            return Ok(await itemMasterService.SearchHoldBill(voucherNo));
        }

        [HttpPost("Hold")]
        public async Task<IActionResult> Hold(CardRequest model)
        {
            try
            {
                return Ok(await itemMasterService.Hold(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetCardTypes")]
        public async Task<IActionResult> GetCardTypes()
        {
            return Ok(await itemMasterService.GetCardTypes());
        }

        [HttpPost]
        [Route("SearchItemByItemLink")]
        public async Task<IActionResult> SearchItemByItemLink(ItemBarCodeSearchFilter model)
        {
            return Ok(await itemMasterService.SearchItemByUnitDetailsId(model));

        }

        [HttpPost("AddSettlementDetails")]
        public async Task<IActionResult> AddSettlementDetails(SettlementDetailsRequest model)
        {
            try
            {
                return Ok(await itemMasterService.AddSettlementDetails(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        [HttpPost("CalculateCardAmount")]
        public async Task<IActionResult> CalculateCardAmount(CardAmountRequest model)
        {
            try
            {
                return Ok(await itemMasterService.CalculateCardAmount(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        [HttpPost("ProcessCard")]
        public async Task<IActionResult> ProcessCard(CardRequest model)
        {
            try
            {
                return Ok(await itemMasterService.ProcessCard(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost("ProcessCardResettlement")]
        public async Task<IActionResult> ProcessCardResettlement(CardResettlementRequest model)
        {
            try
            {
                return Ok(await itemMasterService.ProcessCardResettlement(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }

        }

        [HttpPost("ProcessCashResettlement")]
        public async Task<IActionResult> ProcessCashResettlement(CardResettlementRequest model)
        {
            try
            {
                return Ok(await itemMasterService.ProcessCashResettlement(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }

        }

        [HttpPost("ProcessCashCardResettlement")]
        public async Task<IActionResult> ProcessCashCardResettlement(CardResettlementRequest model)
        {
            try
            {
                return Ok(await itemMasterService.ProcessCashCardResettlement(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        
  

        [HttpPost("ProcessCash")]
        public async Task<IActionResult> ProcessCash(CardRequest model)
        {
            try
            {
                return Ok(await itemMasterService.ProcessCashTransaction(model));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }

        }



        //[HttpPost]
        //[Route("InsertPosSalesVoucher")]
        //public ApiResponse<POS_Sales_Voucher> InsertPosSalesVoucher([FromBody] POS_Sales_Voucher voucherCompositeView)
        //{
        //    ApiResponse<POS_Sales_Voucher> apiResponse = new ApiResponse<POS_Sales_Voucher>();
        //    var param1 = _mapper.Map<POS_Sales_Voucher>(voucherCompositeView);
        //    var param2 = _mapper.Map<List<POS_Sales_Voucher_Details>>(voucherCompositeView.posSalesVoucherDetails);
        //    var xs = _salesVoucherService.InsertPosSalesVoucher(param1, param2);
        //    apiResponse = new ApiResponse<POS_Sales_Voucher>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<POS_Sales_Voucher>(xs),
        //        Message = SalesVoucherMessage.SaveVoucher
        //    };
        //    return apiResponse;
        //}
        //[HttpPost]
        //[Route("UpdatePosSalesVoucher")]
        //public ApiResponse<POS_Sales_Voucher> UpdatePosSalesVoucher([FromBody] POS_Sales_Voucher voucherCompositeView)
        //{
        //    ApiResponse<POS_Sales_Voucher> apiResponse = new ApiResponse<POS_Sales_Voucher>();
        //    var param1 = _mapper.Map<POS_Sales_Voucher>(voucherCompositeView);
        //    var param2 = _mapper.Map<List<POS_Sales_Voucher_Details>>(voucherCompositeView.posSalesVoucherDetails);
        //    var xs = _salesVoucherService.UpdatePosSalesVoucher(param1, param2);
        //    apiResponse = new ApiResponse<POS_Sales_Voucher>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<POS_Sales_Voucher>(xs),
        //        Message = SalesVoucherMessage.SaveVoucher
        //    };
        //    return apiResponse;
        //}

        //[HttpGet]
        //[Route("GetPosSalesVoucher")]
        //public ApiResponse<IEnumerable<POS_Sales_Voucher>> GetAllSalesVoucher()
        //{
        //    ApiResponse<IEnumerable<POS_Sales_Voucher>> apiResponse = new ApiResponse<IEnumerable<POS_Sales_Voucher>>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<IEnumerable<POS_Sales_Voucher>>(_salesVoucherService.GetPosSalesVoucher()),
        //        Message = ""
        //    };
        //    return apiResponse;

        //}

        #region  Sale Hold

        [HttpPost]
        [Route("InsertPosSalesHold")]
        public ApiResponse<SalesHoldViewModel> InsertPosSalesVoucher([FromBody] SalesHoldViewModel voucherCompositeView)
        {
            ApiResponse<SalesHoldViewModel> apiResponse = new ApiResponse<SalesHoldViewModel>();
            var param1 = _mapper.Map<SalesHold>(voucherCompositeView);
            var param2 = _mapper.Map<List<SalesHoldDetails>>(voucherCompositeView.SalesHoldDetailsViewModel);
            var xs = _salesVoucherService.InsertPosSalesHold(param1, param2);
            if (xs != null)
            {
                SalesHoldViewModel SalesHoldViewModel = new SalesHoldViewModel();
                SalesHoldViewModel = _mapper.Map<SalesHoldViewModel>(xs);
                SalesHoldViewModel.SalesHoldDetailsViewModel = _mapper.Map<List<SalesHoldDetailsViewModel>>(xs.SalesHoldDetails);


                apiResponse = new ApiResponse<SalesHoldViewModel>
                {
                    Valid = true,
                    Result = SalesHoldViewModel,
                    Message = SalesVoucherMessage.SaveVoucher
                };
            }
            return apiResponse;
        }


        [HttpDelete]
        [Route("DeletePosSalesHold/{id}")]
        public ApiResponse<List<SalesHold>> DeletePosSalesHold(decimal id)
        {

            var xs = _salesVoucherService.DeletePosSalesHold(id);
            if (xs != null)
            {

                //SalesHoldViewModel SalesHoldViewModel = new SalesHoldViewModel();
                //SalesHoldViewModel = _mapper.Map<SalesHoldViewModel>(xs);
                //SalesHoldViewModel.SalesHoldDetailsViewModel = _mapper.Map<List<SalesHoldDetailsViewModel>>(xs.SalesHoldDetails);

                ApiResponse<List<SalesHold>> apiResponse = new ApiResponse<List<SalesHold>>
                {
                    Valid = true,
                    Result = xs,
                    Message = "Deleted"
                };
                return apiResponse;
            }
            return null;
        }

        [HttpGet]
        [Route("GetPosSalesHold/{id}")]
        public ApiResponse<SalesHoldViewModel> GetPosSalesHold(decimal id)
        {
            SalesHold xs = _salesVoucherService.GetPosSalesHold(id);
            if (xs != null)
            {

                SalesHoldViewModel SalesHoldViewModel = new SalesHoldViewModel();
                SalesHoldViewModel = _mapper.Map<SalesHoldViewModel>(xs);
                SalesHoldViewModel.SalesHoldDetailsViewModel = _mapper.Map<List<SalesHoldDetailsViewModel>>(xs.SalesHoldDetails);


                ApiResponse<SalesHoldViewModel> apiResponse = new ApiResponse<SalesHoldViewModel>
                {
                    Valid = true,
                    Result = SalesHoldViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }

        [HttpGet]
        [Route("GetPosSalesHoldList")]
        public ApiResponse<List<SalesHoldList>> GetPosSalesHoldList()
        {

            var result = _salesVoucherService.GetPosSalesHoldList().Select(x => new SalesHoldList
            {

                VoucherNo = x.VoucherNo,
                V_ID = x.V_ID,
                StationID = x.StationID,
                CustomerName = x.CustomerName,
                GrossAmount = x.GrossAmount,
                Discount = x.Discount,
                VatAmount = x.VatAmount,
                NetAmount = x.NetAmount
            }).ToList();
            ApiResponse<List<SalesHoldList>> apiResponse = new ApiResponse<List<SalesHoldList>>
            {
                Valid = true,
                Result = result,
                Message = ""
            };
            return apiResponse;
        }
        #endregion

        #region Authenticate Service

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] PosUser user)
        {
            var response = _loginsvc.PosAuthenticate(new UserFile
            {
                UserName = user.Username,
                Password = user.Password
            });

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            setTokenCookie(response.RefreshToken);
            return Ok(ApiResponse<UserFile>.Success(response));
        }


        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        #endregion

        #region  Work Periods

        [HttpPost]
        [Route("StartPosWP")]
        public ApiResponse<WorkPeriodResponse> StartPosWP([FromBody] WorkPeriodResponse voucherCompositeView)
        {
            ApiResponse<WorkPeriodResponse> apiResponse = new ApiResponse<WorkPeriodResponse>();
            var param1 = _mapper.Map<POS_WorkPeriod>(voucherCompositeView);

            var xs = _salesVoucherService.StartPosWP(param1);
            apiResponse = new ApiResponse<WorkPeriodResponse>
            {
                Valid = true,
                Result = _mapper.Map<WorkPeriodResponse>(xs),
                Message = WorkPeriodsMessage.StartWP,
            };
            return apiResponse;
        }


        [HttpPost]
        [Route("EndPosWP")]
        public ApiResponse<WorkPeriodResponse> EndPosWP([FromBody] WorkPeriodResponse voucherCompositeView)
        {
            ApiResponse<WorkPeriodResponse> apiResponse = new ApiResponse<WorkPeriodResponse>();
            var param1 = _mapper.Map<POS_WorkPeriod>(voucherCompositeView);
            var xs = _salesVoucherService.EndPosWP(param1);
            apiResponse = new ApiResponse<WorkPeriodResponse>
            {
                Valid = true,
                Result = _mapper.Map<WorkPeriodResponse>(xs),
                Message = WorkPeriodsMessage.EndWP,
            };
            return apiResponse;
        }

        #endregion

        #region  "Text Print Reports"

        [HttpGet]
        [Route("PrintReciept")]
        public async Task<IActionResult> PrintReciept(string voucherNo)
        {
            return Ok(await itemMasterService.PrintReciept(voucherNo));
        }

        [HttpGet]
        [Route("SummaryDateWise")]
        public async Task<IActionResult> SummaryDateWise(DateTime fromDate, DateTime toDate, int wpId, int stationId)
        {
            return Ok(await itemMasterService.SummaryDateWise(fromDate, toDate, wpId, stationId));
        }
        

        [HttpGet]
        [Route("SummaryReport")]
        public async Task<IActionResult> SummaryReport( int wpId, int stationId)
        {
            return Ok(await itemMasterService.SummaryReport( wpId, stationId));
        }


        [HttpGet]
        [Route("DayEndReport")]
        public async Task<IActionResult> DayEndReport(DateTime fromDate, DateTime toDate,int wpId, int stationId)
        {
            return Ok(await itemMasterService.DayEndReport(fromDate,toDate,wpId, stationId));
        }

        [HttpGet]
        [Route("TodayItemSales")]
        public async Task<IActionResult> TodayItemSales(DateTime fromDate, DateTime toDate, int wpId, int stationId)
        {
            return Ok(await itemMasterService.TodayItemSales(fromDate, toDate, wpId, stationId));
        }

        [HttpGet]
        [Route("TransactionDateWise")]
        public async Task<IActionResult> TransactionDateWise(DateTime fromDate, DateTime toDate, int wpId, int stationId)
        {
            return Ok(await itemMasterService.TransactionDateWise(fromDate, toDate, wpId, stationId));
        }

        [HttpGet]
        [Route("zReport")]
        public async Task<IActionResult> zReport(DateTime fromDate, DateTime toDate, int wpId, int stationId)
        {
            return Ok(await itemMasterService.ZReport(fromDate, toDate, wpId, stationId));
        }
        #endregion
    }
}