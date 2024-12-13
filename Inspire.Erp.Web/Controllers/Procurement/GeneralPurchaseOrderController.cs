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
using Inspire.Erp.Application.Procurement.Implementation;

namespace Inspire.Erp.Web.Controllers.Procurement
{
    [Route("api/GeneralPurchaseOrder")]
    [Produces("application/json")]
    [ApiController]
    public class GeneralPurchaseOrderController : ControllerBase
    {
        private IGeneralPurchaseOrderService _GeneralpurchaseOrderService;
        private readonly IMapper _mapper;
        public GeneralPurchaseOrderController(IGeneralPurchaseOrderService GeneralpurchaseOrderService, IMapper mapper)
        {
            _GeneralpurchaseOrderService = GeneralpurchaseOrderService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllUserFile")]
        public List<UserFile> GetAllUserFile()
        {
            return _mapper.Map<List<UserFile>>(_GeneralpurchaseOrderService.GetAllUserFile());
        }
        [HttpPost]
        [Route("InsertGeneralPurchaseOrder")]
        public ApiResponse<GeneralPurchaseOrderViewModel> InsertGeneralPurchaseOrder([FromBody] GeneralPurchaseOrderViewModel voucherCompositeView)
        {

            ApiResponse<GeneralPurchaseOrderViewModel> apiResponse = new ApiResponse<GeneralPurchaseOrderViewModel>();
            if (_GeneralpurchaseOrderService.GetVouchersNumbers(voucherCompositeView.GeneralPurchaseOrderPono) == null)
            {
                var param1 = _mapper.Map<GeneralPurchaseOrder>(voucherCompositeView);
                var param2 = _mapper.Map<List<GeneralPurchaseOrderDetails>>(voucherCompositeView.GeneralPurchaseOrderDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                var xs = _GeneralpurchaseOrderService.InsertGeneralPurchaseOrder(param1, param3, param2

                    );

                GeneralPurchaseOrderViewModel GeneralpurchaseOrderViewModel = new GeneralPurchaseOrderViewModel();
                GeneralpurchaseOrderViewModel = _mapper.Map<GeneralPurchaseOrderViewModel>(xs.GeneralpurchaseOrder);

                GeneralpurchaseOrderViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<GeneralPurchaseOrderViewModel>
                {
                    Valid = true,
                    Result = GeneralpurchaseOrderViewModel,
                    Message = PurchaseOrderMessage.SaveVoucher
                };
            }
            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateGeneralPurchaseOrder")]
        public ApiResponse<GeneralPurchaseOrderViewModel> UpdateGeneralPurchaseOrder([FromBody] GeneralPurchaseOrderViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<GeneralPurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<GeneralPurchaseOrderDetails>>(voucherCompositeView.GeneralPurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _GeneralpurchaseOrderService.UpdateGeneralPurchaseOrder(param1, param3, param2

                );
            //GeneralPurchaseOrderViewModel GeneralpurchaseOrderViewModel = new GeneralPurchaseOrderViewModel
            //{
            //    GeneralPurchaseOrderId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderId,
            //    GeneralPurchaseOrderPono = xs.GeneralpurchaseOrder.GeneralPurchaseOrderPono,
            //    GeneralPurchaseOrderSupplierId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderSupplierId,
            //    GeneralPurchaseOrderPoDate = xs.GeneralpurchaseOrder.GeneralPurchaseOrderPoDate,
            //    GeneralPurchaseOrderDescription = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDescription,
            //    GeneralPurchaseOrderLpoNo = xs.GeneralpurchaseOrder.GeneralPurchaseOrderLpoNo,
            //    GeneralPurchaseOrderLpoDate = xs.GeneralpurchaseOrder.GeneralPurchaseOrderLpoDate,
            //    GeneralPurchaseOrderTotalAmount = xs.GeneralpurchaseOrder.GeneralPurchaseOrderTotalAmount,
            //    GeneralPurchaseOrderStatus = xs.GeneralpurchaseOrder.GeneralPurchaseOrderStatus,
            //    GeneralPurchaseOrderDiscountPercentage = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDiscountPercentage,
            //    GeneralPurchaseOrderDiscount = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDiscount,
            //    GeneralPurchaseOrderLocationId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderLocationId,
            //    GeneralPurchaseOrderCurrencyId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderCurrencyId,
            //    GeneralPurchaseOrderNetAmount = xs.GeneralpurchaseOrder.GeneralPurchaseOrderNetAmount,
            //    GeneralPurchaseOrderFsno = xs.GeneralpurchaseOrder.GeneralPurchaseOrderFsno,
            //    GeneralPurchaseOrderUserId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderUserId,
            //    GeneralPurchaseOrderTermAndCondition = xs.GeneralpurchaseOrder.GeneralPurchaseOrderTermAndCondition,
            //    GeneralPurchaseOrderJobId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderJobId,
            //    GeneralPurchaseOrderApproveDate = xs.GeneralpurchaseOrder.GeneralPurchaseOrderApproveDate,
            //    GeneralPurchaseOrderApproveStatus = xs.GeneralpurchaseOrder.GeneralPurchaseOrderApproveStatus,
            //    GeneralPurchaseOrderApprovedBy = xs.GeneralpurchaseOrder.GeneralPurchaseOrderApprovedBy,
            //    GeneralPurchaseOrderHeader = xs.GeneralpurchaseOrder.GeneralPurchaseOrderHeader,
            //    GeneralPurchaseOrderFooter = xs.GeneralpurchaseOrder.GeneralPurchaseOrderFooter,
            //    GeneralPurchaseOrderTerms = xs.GeneralpurchaseOrder.GeneralPurchaseOrderTerms,
            //    GeneralPurchaseOrderPaymentTerms = xs.GeneralpurchaseOrder.GeneralPurchaseOrderPaymentTerms,
            //    GeneralPurchaseOrderDelivery = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDelivery,
            //    GeneralPurchaseOrderIndentNo = xs.GeneralpurchaseOrder.GeneralPurchaseOrderIndentNo,
            //    GeneralPurchaseOrderDelStatus = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDelStatus,              
            //};


            GeneralPurchaseOrderViewModel GeneralpurchaseOrderViewModel = new GeneralPurchaseOrderViewModel();
            GeneralpurchaseOrderViewModel = _mapper.Map<GeneralPurchaseOrderViewModel>(xs.GeneralpurchaseOrder);

            GeneralpurchaseOrderViewModel.GeneralPurchaseOrderDetails = _mapper.Map<List<GeneralPurchaseOrderDetailsViewModel>>(xs.GeneralpurchaseOrderDetails);
            GeneralpurchaseOrderViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<GeneralPurchaseOrderViewModel> apiResponse = new ApiResponse<GeneralPurchaseOrderViewModel>
            {
                Valid = true,
                Result = GeneralpurchaseOrderViewModel,
                Message = GeneralPurchaseOrderMessage.UpdateVoucher
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("DeleteGeneralPurchaseOrder")]
        public ApiResponse<int> DeleteGeneralPurchaseOrder([FromBody] GeneralPurchaseOrderViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<GeneralPurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<GeneralPurchaseOrderDetails>>(voucherCompositeView.GeneralPurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _GeneralpurchaseOrderService.DeleteGeneralPurchaseOrder(param1, param3, param2
                );
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = GeneralPurchaseOrderMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_GeneralpurchaseOrderService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetGeneralPurchaseOrder")]
        public ApiResponse<List<GeneralPurchaseOrder>> GetAllGeneralPurchaseOrder()
        {
            ApiResponse<List<GeneralPurchaseOrder>> apiResponse = new ApiResponse<List<GeneralPurchaseOrder>>
            {
                Valid = true,
                Result = _GeneralpurchaseOrderService.GetGeneralPurchaseOrder().ToList(),
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetSavedGeneralPurchaseOrderDetails/{id}")]
        public ApiResponse<GeneralPurchaseOrderViewModel> GetSavedGeneralPurchaseOrderDetails(string id)
        {
            GeneralPurchaseOrderModel xs = _GeneralpurchaseOrderService.GetSavedGeneralPurchaseOrderDetails(id);
            if (xs != null)
            {
                //GeneralPurchaseOrderViewModel GeneralpurchaseOrderViewModel = new GeneralPurchaseOrderViewModel
                //{
                //    GeneralPurchaseOrderId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderId,
                //    GeneralPurchaseOrderPono = xs.GeneralpurchaseOrder.GeneralPurchaseOrderPono,
                //    GeneralPurchaseOrderSupplierId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderSupplierId,
                //    GeneralPurchaseOrderPoDate = xs.GeneralpurchaseOrder.GeneralPurchaseOrderPoDate,
                //    GeneralPurchaseOrderDescription = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDescription,
                //    GeneralPurchaseOrderLpoNo = xs.GeneralpurchaseOrder.GeneralPurchaseOrderLpoNo,
                //    GeneralPurchaseOrderLpoDate = xs.GeneralpurchaseOrder.GeneralPurchaseOrderLpoDate,
                //    GeneralPurchaseOrderTotalAmount = xs.GeneralpurchaseOrder.GeneralPurchaseOrderTotalAmount,
                //    GeneralPurchaseOrderStatus = xs.GeneralpurchaseOrder.GeneralPurchaseOrderStatus,
                //    GeneralPurchaseOrderDiscountPercentage = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDiscountPercentage,
                //    GeneralPurchaseOrderDiscount = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDiscount,
                //    GeneralPurchaseOrderLocationId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderLocationId,
                //    GeneralPurchaseOrderCurrencyId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderCurrencyId,
                //    GeneralPurchaseOrderNetAmount = xs.GeneralpurchaseOrder.GeneralPurchaseOrderNetAmount,
                //    GeneralPurchaseOrderFsno = xs.GeneralpurchaseOrder.GeneralPurchaseOrderFsno,
                //    GeneralPurchaseOrderUserId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderUserId,
                //    GeneralPurchaseOrderTermAndCondition = xs.GeneralpurchaseOrder.GeneralPurchaseOrderTermAndCondition,
                //    GeneralPurchaseOrderJobId = xs.GeneralpurchaseOrder.GeneralPurchaseOrderJobId,
                //    GeneralPurchaseOrderApproveDate = xs.GeneralpurchaseOrder.GeneralPurchaseOrderApproveDate,
                //    GeneralPurchaseOrderApproveStatus = xs.GeneralpurchaseOrder.GeneralPurchaseOrderApproveStatus,
                //    GeneralPurchaseOrderApprovedBy = xs.GeneralpurchaseOrder.GeneralPurchaseOrderApprovedBy,
                //    GeneralPurchaseOrderHeader = xs.GeneralpurchaseOrder.GeneralPurchaseOrderHeader,
                //    GeneralPurchaseOrderFooter = xs.GeneralpurchaseOrder.GeneralPurchaseOrderFooter,
                //    GeneralPurchaseOrderTerms = xs.GeneralpurchaseOrder.GeneralPurchaseOrderTerms,
                //    GeneralPurchaseOrderPaymentTerms = xs.GeneralpurchaseOrder.GeneralPurchaseOrderPaymentTerms,
                //    GeneralPurchaseOrderDelivery = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDelivery,
                //    GeneralPurchaseOrderIndentNo = xs.GeneralpurchaseOrder.GeneralPurchaseOrderIndentNo,
                //    GeneralPurchaseOrderDelStatus = xs.GeneralpurchaseOrder.GeneralPurchaseOrderDelStatus,
                //};
                GeneralPurchaseOrderViewModel GeneralpurchaseOrderViewModel = new GeneralPurchaseOrderViewModel();
                GeneralpurchaseOrderViewModel = _mapper.Map<GeneralPurchaseOrderViewModel>(xs.GeneralpurchaseOrder);
                GeneralpurchaseOrderViewModel.GeneralPurchaseOrderDetails = _mapper.Map<List<GeneralPurchaseOrderDetailsViewModel>>(xs.GeneralpurchaseOrderDetails);
                GeneralpurchaseOrderViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                ApiResponse<GeneralPurchaseOrderViewModel> apiResponse = new ApiResponse<GeneralPurchaseOrderViewModel>
                {
                    Valid = true,
                    Result = GeneralpurchaseOrderViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }
        [HttpGet]
        [Route("CheckVnoExist/{id}")]
        public ApiResponse<bool> CheckVnoExist(string id)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Valid = true,
                Result = true,
                Message = GeneralPurchaseOrderMessage.VoucherNumberExist
            };
            var x = _GeneralpurchaseOrderService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }
            return apiResponse;
        }
        [HttpGet]
        [Route("GetGPODetailsForGRN")]
        public IActionResult GetPODetailsForGRN()
        {
            try
            {
                var item = _GeneralpurchaseOrderService.GetGPODetailsForGRN();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

    }
}
