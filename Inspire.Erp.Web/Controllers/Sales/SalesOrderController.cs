using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.MODULE;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.WebReport.ReportEngine;
using Inspire.Erp.Domain.Modals.Sales;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/SalesOrder")]
    [Produces("application/json")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private ICustomerPurchaseOrderService _salesOrderService;
        private readonly IMapper _mapper;
        public SalesOrderController(ICustomerPurchaseOrderService salesOrderService, IMapper mapper)
        {
            _salesOrderService = salesOrderService;
            _mapper = mapper;
        }
        //[HttpGet]
        //[Route("GetSalesOrderDetailsByMasterNo")]
        //public IActionResult GetSalesOrderDetailsByMasterNo(string SalesOrderNo)
        //{
        //    try
        //    {
        //        var item = _salesOrderService.GetSalesOrderDetailsByMasterNo(SalesOrderNo);
        //        return Ok(item);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(500, ex.Message.ToString());
        //    }
        //}
        [HttpPost]
        [Route("InsertSalesOrder")]
        public ApiResponse<CustomerPurchaseOrderViewModel> InsertSalesOrder([FromBody] CustomerPurchaseOrderViewModel voucherCompositeView)
        {
            ApiResponse<CustomerPurchaseOrderViewModel> apiResponse = new ApiResponse<CustomerPurchaseOrderViewModel>();
            //if (_salesOrderService.GetVouchersNumbers(voucherCompositeView.CustomerPurchaseOrderVoucherNo) == null)
            //{
                var param1 = _mapper.Map<CustomerPurchaseOrder>(voucherCompositeView);
                var param2 = _mapper.Map<List<CustomerPurchaseOrderDetails>>(voucherCompositeView.CustomerPurchaseOrderDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
                //var xs = _salesOrderService.InsertSalesOrder(param1, param3, param2
                //    //, param4
                //    );
                //==============
                //param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
                //clsAccountAndStock.SalesOrder_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);
                var xs = _salesOrderService.InsertSalesOrder(param1, param3, param2, param4);
                //========================
                CustomerPurchaseOrderViewModel salesVoucherViewModel = new CustomerPurchaseOrderViewModel
                {
                    CustomerPurchaseOrderId = xs.customerPurchaseOrder.CustomerPurchaseOrderId,
                    CustomerPurchaseOrderVoucherNo = xs.customerPurchaseOrder.CustomerPurchaseOrderVoucherNo,
                    CustomerPurchaseOrderDate = xs.customerPurchaseOrder.CustomerPurchaseOrderDate,
                    CustomerPurchaseOrderLocationId = xs.customerPurchaseOrder.CustomerPurchaseOrderLocationId,
                    CustomerPurchaseOrderCustomerId= xs.customerPurchaseOrder.CustomerPurchaseOrderCustomerId,
                    CustomerPurchaseOrderDepartmentId = xs.customerPurchaseOrder.CustomerPurchaseOrderDepartmentId,
                    CustomerPurchaseOrderCustomerName = xs.customerPurchaseOrder.CustomerPurchaseOrderCustomerName,
                    CustomerPurchaseOrderSalesManId= xs.customerPurchaseOrder.CustomerPurchaseOrderSalesManId,
                    CustomerPurchaseOrderLpoNo= xs.customerPurchaseOrder.CustomerPurchaseOrderLpoNo,
                    CustomerPurchaseOrderLpoDate= xs.customerPurchaseOrder.CustomerPurchaseOrderLpoDate,
                    CustomerPurchaseOrderCurrencyId= xs.customerPurchaseOrder.CustomerPurchaseOrderCurrencyId,
                    CustomerPurchaseOrderRemarks= xs.customerPurchaseOrder.CustomerPurchaseOrderRemarks,
                    CustomerPurchaseOrderDiscountPercentage= xs.customerPurchaseOrder.CustomerPurchaseOrderDiscountPercentage,
                    CustomerPurchaseOrderDiscountAmount= xs.customerPurchaseOrder.CustomerPurchaseOrderDiscountAmount,
                    CustomerPurchaseOrderTotal= xs.customerPurchaseOrder.CustomerPurchaseOrderTotal,

                    CustomerPurchaseOrderVat = xs.customerPurchaseOrder.CustomerPurchaseOrderVat,
                    CustomerPurchaseOrderVAT_Percen = xs.customerPurchaseOrder.CustomerPurchaseOrderVAT_Percen,
                    CustomerPurchaseOrderGrandTotal = xs.customerPurchaseOrder.CustomerPurchaseOrderGrandTotal,
                    CustomerPurchaseOrderFsno= xs.customerPurchaseOrder.CustomerPurchaseOrderFsno,
                    CustomerPurchaseOrderUserId= xs.customerPurchaseOrder.CustomerPurchaseOrderUserId,
                    CustomerPurchaseOrderSubject= xs.customerPurchaseOrder.CustomerPurchaseOrderSubject,
                    CustomerPurchaseOrderNote= xs.customerPurchaseOrder.CustomerPurchaseOrderNote,
                    CustomerPurchaseOrderWarranty = xs.customerPurchaseOrder.CustomerPurchaseOrderWarranty,
                    CustomerPurchaseOrderTraining= xs.customerPurchaseOrder.CustomerPurchaseOrderTraining,
                    CustomerPurchaseOrderTechnicalDetails= xs.customerPurchaseOrder.CustomerPurchaseOrderTechnicalDetails,
                    CustomerPurchaseOrderTerms= xs.customerPurchaseOrder.CustomerPurchaseOrderTerms,
                    CustomerPurchaseOrderCpoAboutId= xs.customerPurchaseOrder.CustomerPurchaseOrderCpoAboutId,
                    CustomerPurchaseOrderCpoDeliveryTimeDate= xs.customerPurchaseOrder.CustomerPurchaseOrderCpoDeliveryTimeDate,
                    CustomerPurchaseOrderPacking= xs.customerPurchaseOrder.CustomerPurchaseOrderPacking,
                    CustomerPurchaseOrderQuotationId= xs.customerPurchaseOrder.CustomerPurchaseOrderQuotationId,
                    CustomerPurchaseOrderQuantity= xs.customerPurchaseOrder.CustomerPurchaseOrderQuantity,
                    CustomerPurchaseOrderIslocalPurchase= xs.customerPurchaseOrder.CustomerPurchaseOrderIslocalPurchase,
                    CustomerPurchaseOrderCpoTermsV= xs.customerPurchaseOrder.CustomerPurchaseOrderCpoTermsV,
                    CustomerPurchaseOrderJobId= xs.customerPurchaseOrder.CustomerPurchaseOrderJobId,
                    CustomerPurchaseOrderPoEnquiryIdN= xs.customerPurchaseOrder.CustomerPurchaseOrderPoEnquiryIdN,
                    CustomerPurchaseOrderPoQuotationIdN= xs.customerPurchaseOrder.CustomerPurchaseOrderPoQuotationIdN,
                    CustomerPurchaseOrderDelStatus= xs.customerPurchaseOrder.CustomerPurchaseOrderDelStatus,
                   
                };

                salesVoucherViewModel.CustomerPurchaseOrderDetails = _mapper.Map<List<CustomerPurchaseOrderDetailsViewModel>>(xs.customerPurchaseOrderDetails);
                salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<CustomerPurchaseOrderViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<CustomerPurchaseOrderViewModel>(salesVoucherViewModel),
                    Message = CustomerPurchaseOrderMessage.SavePurchaseOrder
                };
            //}
            //else
            //{
            //    apiResponse = new ApiResponse<CustomerPurchaseOrderViewModel>
            //    {
            //        Valid = false,
            //        Error = true,
            //        Exception = null,
            //        Message = CustomerPurchaseOrderMessage.PurchaseOrderAlreadyExist
            //    };

            //}

            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateSalesOrder")]
        public ApiResponse<CustomerPurchaseOrderViewModel> UpdateSalesOrder([FromBody] CustomerPurchaseOrderViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<CustomerPurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<CustomerPurchaseOrderDetails>>(voucherCompositeView.CustomerPurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);

            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
           // clsAccountAndStock.SalesOrder_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _salesOrderService.UpdateSalesOrder(param1, param3, param2
           , param4
           );
           
            CustomerPurchaseOrderViewModel salesVoucherViewModel = new CustomerPurchaseOrderViewModel
            {
                CustomerPurchaseOrderId = xs.customerPurchaseOrder.CustomerPurchaseOrderId,
                CustomerPurchaseOrderVoucherNo = xs.customerPurchaseOrder.CustomerPurchaseOrderVoucherNo,
                CustomerPurchaseOrderDate = xs.customerPurchaseOrder.CustomerPurchaseOrderDate,
                CustomerPurchaseOrderLocationId = xs.customerPurchaseOrder.CustomerPurchaseOrderLocationId,
                CustomerPurchaseOrderCustomerId = xs.customerPurchaseOrder.CustomerPurchaseOrderCustomerId,
                CustomerPurchaseOrderDepartmentId = xs.customerPurchaseOrder.CustomerPurchaseOrderDepartmentId,
                CustomerPurchaseOrderCustomerName = xs.customerPurchaseOrder.CustomerPurchaseOrderCustomerName,
                CustomerPurchaseOrderSalesManId = xs.customerPurchaseOrder.CustomerPurchaseOrderSalesManId,
                CustomerPurchaseOrderLpoNo = xs.customerPurchaseOrder.CustomerPurchaseOrderLpoNo,
                CustomerPurchaseOrderLpoDate = xs.customerPurchaseOrder.CustomerPurchaseOrderLpoDate,
                CustomerPurchaseOrderCurrencyId = xs.customerPurchaseOrder.CustomerPurchaseOrderCurrencyId,
                CustomerPurchaseOrderRemarks = xs.customerPurchaseOrder.CustomerPurchaseOrderRemarks,
                CustomerPurchaseOrderDiscountPercentage = xs.customerPurchaseOrder.CustomerPurchaseOrderDiscountPercentage,
                CustomerPurchaseOrderDiscountAmount = xs.customerPurchaseOrder.CustomerPurchaseOrderDiscountAmount,
                CustomerPurchaseOrderTotal = xs.customerPurchaseOrder.CustomerPurchaseOrderTotal,
                CustomerPurchaseOrderGrandTotal = xs.customerPurchaseOrder.CustomerPurchaseOrderGrandTotal,
                CustomerPurchaseOrderFsno = xs.customerPurchaseOrder.CustomerPurchaseOrderFsno,
                CustomerPurchaseOrderUserId = xs.customerPurchaseOrder.CustomerPurchaseOrderUserId,
                CustomerPurchaseOrderSubject = xs.customerPurchaseOrder.CustomerPurchaseOrderSubject,
                CustomerPurchaseOrderNote = xs.customerPurchaseOrder.CustomerPurchaseOrderNote,
                CustomerPurchaseOrderWarranty = xs.customerPurchaseOrder.CustomerPurchaseOrderWarranty,
                CustomerPurchaseOrderTraining = xs.customerPurchaseOrder.CustomerPurchaseOrderTraining,
                CustomerPurchaseOrderTechnicalDetails = xs.customerPurchaseOrder.CustomerPurchaseOrderTechnicalDetails,
                CustomerPurchaseOrderTerms = xs.customerPurchaseOrder.CustomerPurchaseOrderTerms,
                CustomerPurchaseOrderCpoAboutId = xs.customerPurchaseOrder.CustomerPurchaseOrderCpoAboutId,
                CustomerPurchaseOrderCpoDeliveryTimeDate = xs.customerPurchaseOrder.CustomerPurchaseOrderCpoDeliveryTimeDate,
                CustomerPurchaseOrderPacking = xs.customerPurchaseOrder.CustomerPurchaseOrderPacking,
                CustomerPurchaseOrderQuotationId = xs.customerPurchaseOrder.CustomerPurchaseOrderQuotationId,
                CustomerPurchaseOrderQuantity = xs.customerPurchaseOrder.CustomerPurchaseOrderQuantity,
                CustomerPurchaseOrderIslocalPurchase = xs.customerPurchaseOrder.CustomerPurchaseOrderIslocalPurchase,
                CustomerPurchaseOrderCpoTermsV = xs.customerPurchaseOrder.CustomerPurchaseOrderCpoTermsV,
                CustomerPurchaseOrderJobId = xs.customerPurchaseOrder.CustomerPurchaseOrderJobId,
                CustomerPurchaseOrderPoEnquiryIdN = xs.customerPurchaseOrder.CustomerPurchaseOrderPoEnquiryIdN,
                CustomerPurchaseOrderPoQuotationIdN = xs.customerPurchaseOrder.CustomerPurchaseOrderPoQuotationIdN,
                CustomerPurchaseOrderDelStatus = xs.customerPurchaseOrder.CustomerPurchaseOrderDelStatus,

            };

            salesVoucherViewModel.CustomerPurchaseOrderDetails = _mapper.Map<List<CustomerPurchaseOrderDetailsViewModel>>(xs.customerPurchaseOrderDetails);
            salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<CustomerPurchaseOrderViewModel> apiResponse = new ApiResponse<CustomerPurchaseOrderViewModel>
            {
                Valid = true,
                Result = _mapper.Map<CustomerPurchaseOrderViewModel>(salesVoucherViewModel),
                Message = CustomerPurchaseOrderMessage.UpdatePurchaseOrder
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteSalesOrder")]
        public ApiResponse<int> DeletePurchaseOrder([FromBody] CustomerPurchaseOrderViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<CustomerPurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<CustomerPurchaseOrderDetails>>(voucherCompositeView.CustomerPurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //var xs = _salesOrderService.DeleteSalesOrder(  param1,    param3, param2
            //    //, param4
            //    );

            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.SalesOrder_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _salesOrderService.DeleteSalesOrder(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = CustomerPurchaseOrderMessage.DeletePurchaseOrder
            };

            return apiResponse;

        }
        //[HttpGet]
        //[Route("GetAllAccountTransaction")]
        //public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        //{
        //    ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<List<AccountsTransactions>>(_salesOrderService.GetAllTransaction()),
        //        Message = ""
        //    };
        //    return apiResponse;
        //}

        [HttpGet]
        [Route("GetAllSalesOrder")]
        public IActionResult GetAllSalesOrder()
        {
           ApiResponse<List<CustomerPurchaseOrder>> apiResponse = new ApiResponse<List<CustomerPurchaseOrder>>
            {
                Valid = true,
                Result = _mapper.Map<List<CustomerPurchaseOrder>>(_salesOrderService.GetCustomerPurchaseOrders()),
                Message = ""
            };
             return Ok( apiResponse);
        }

        [HttpGet]
        [Route("GetSavedSalesOrderDetails/{id}")]
        public ApiResponse<CustomerPurchaseOrderViewModel> GetSavedSalesOrderDetails(string id)
        {
            CustomerPurchaseOrderModel xs = _salesOrderService.GetSavedCustomerPurchaseOrderDetails(id);

            if (xs != null)
            {
                CustomerPurchaseOrderViewModel salesVoucherViewModel = new CustomerPurchaseOrderViewModel
                {
                    CustomerPurchaseOrderId = xs.customerPurchaseOrder.CustomerPurchaseOrderId,
                    CustomerPurchaseOrderVoucherNo = xs.customerPurchaseOrder.CustomerPurchaseOrderVoucherNo,
                    CustomerPurchaseOrderDate = xs.customerPurchaseOrder.CustomerPurchaseOrderDate,
                    CustomerPurchaseOrderLocationId = xs.customerPurchaseOrder.CustomerPurchaseOrderLocationId,
                    CustomerPurchaseOrderCustomerId = xs.customerPurchaseOrder.CustomerPurchaseOrderCustomerId,
                    CustomerPurchaseOrderDepartmentId = xs.customerPurchaseOrder.CustomerPurchaseOrderDepartmentId,
                    CustomerPurchaseOrderCustomerName = xs.customerPurchaseOrder.CustomerPurchaseOrderCustomerName,
                    CustomerPurchaseOrderSalesManId = xs.customerPurchaseOrder.CustomerPurchaseOrderSalesManId,
                    CustomerPurchaseOrderLpoNo = xs.customerPurchaseOrder.CustomerPurchaseOrderLpoNo,
                    CustomerPurchaseOrderLpoDate = xs.customerPurchaseOrder.CustomerPurchaseOrderLpoDate,
                    CustomerPurchaseOrderCurrencyId = xs.customerPurchaseOrder.CustomerPurchaseOrderCurrencyId,
                    CustomerPurchaseOrderRemarks = xs.customerPurchaseOrder.CustomerPurchaseOrderRemarks,
                    CustomerPurchaseOrderDiscountPercentage = xs.customerPurchaseOrder.CustomerPurchaseOrderDiscountPercentage,
                    CustomerPurchaseOrderDiscountAmount = xs.customerPurchaseOrder.CustomerPurchaseOrderDiscountAmount,
                    CustomerPurchaseOrderTotal = xs.customerPurchaseOrder.CustomerPurchaseOrderTotal,
                    CustomerPurchaseOrderGrandTotal = xs.customerPurchaseOrder.CustomerPurchaseOrderGrandTotal,
                    CustomerPurchaseOrderFsno = xs.customerPurchaseOrder.CustomerPurchaseOrderFsno,
                    CustomerPurchaseOrderUserId = xs.customerPurchaseOrder.CustomerPurchaseOrderUserId,
                    CustomerPurchaseOrderSubject = xs.customerPurchaseOrder.CustomerPurchaseOrderSubject,
                    CustomerPurchaseOrderNote = xs.customerPurchaseOrder.CustomerPurchaseOrderNote,
                    CustomerPurchaseOrderWarranty = xs.customerPurchaseOrder.CustomerPurchaseOrderWarranty,
                    CustomerPurchaseOrderTraining = xs.customerPurchaseOrder.CustomerPurchaseOrderTraining,
                    CustomerPurchaseOrderTechnicalDetails = xs.customerPurchaseOrder.CustomerPurchaseOrderTechnicalDetails,
                    CustomerPurchaseOrderTerms = xs.customerPurchaseOrder.CustomerPurchaseOrderTerms,
                    CustomerPurchaseOrderCpoAboutId = xs.customerPurchaseOrder.CustomerPurchaseOrderCpoAboutId,
                    CustomerPurchaseOrderCpoDeliveryTimeDate = xs.customerPurchaseOrder.CustomerPurchaseOrderCpoDeliveryTimeDate,
                    CustomerPurchaseOrderPacking = xs.customerPurchaseOrder.CustomerPurchaseOrderPacking,
                    CustomerPurchaseOrderQuotationId = xs.customerPurchaseOrder.CustomerPurchaseOrderQuotationId,
                    CustomerPurchaseOrderQuantity = xs.customerPurchaseOrder.CustomerPurchaseOrderQuantity,
                    CustomerPurchaseOrderIslocalPurchase = xs.customerPurchaseOrder.CustomerPurchaseOrderIslocalPurchase,
                    CustomerPurchaseOrderCpoTermsV = xs.customerPurchaseOrder.CustomerPurchaseOrderCpoTermsV,
                    CustomerPurchaseOrderJobId = xs.customerPurchaseOrder.CustomerPurchaseOrderJobId,
                    CustomerPurchaseOrderPoEnquiryIdN = xs.customerPurchaseOrder.CustomerPurchaseOrderPoEnquiryIdN,
                    CustomerPurchaseOrderPoQuotationIdN = xs.customerPurchaseOrder.CustomerPurchaseOrderPoQuotationIdN,
                    CustomerPurchaseOrderDelStatus = xs.customerPurchaseOrder.CustomerPurchaseOrderDelStatus,

                };
                salesVoucherViewModel.CustomerPurchaseOrderDetails = _mapper.Map<List<CustomerPurchaseOrderDetailsViewModel>>(xs.customerPurchaseOrderDetails);
                salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                ApiResponse<CustomerPurchaseOrderViewModel> apiResponse = new ApiResponse<CustomerPurchaseOrderViewModel>
                {
                    Valid = true,
                    Result = salesVoucherViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;



        }
        //[HttpGet]
        //[Route("CheckVnoExist/{id}")]
        //public ApiResponse<bool> CheckVnoExist(string id)
        //{
        //    ApiResponse<bool> apiResponse = new ApiResponse<bool>
        //    {
        //        Valid = true,
        //        Result = true,
        //        Message = SalesOrderMessage.VoucherNumberExist



        //    };
        //    var x = _salesOrderService.GetVouchersNumbers(id);
        //    if (x == null)
        //    {
        //        apiResponse.Result = false;
        //        apiResponse.Message = "";
        //    }

        //    return apiResponse;
        //}


        [HttpPost]
        [Route("GetPurchaseOrderTracking")]
        public async Task<IActionResult> GetPurchaseOrderTracking(CustPurchOrderFilterModel model)
        {

            var response = await _salesOrderService.GetCustomerPurchaseOrderTracking(model);

            //return null;

            ApiResponse<List<GetCustomerPurchaseOrderTrackingResponse>> apiResponse = new ApiResponse<List<GetCustomerPurchaseOrderTrackingResponse>>
            {
                Valid = true,
                Result = response.Result,
                Message = ""
            };
            return Ok(apiResponse);
        }



        [HttpPost]
        [Route("GetPurchaseOrderStatuses")]
        public async Task<IActionResult> GetPurchaseOrderStatuses(CustPurchOrderFilterModel model)
        {

            var response = await _salesOrderService.GetCustomerPOStatuses(model);
            ApiResponse<List<GetCustomerPurchaseOrderTrackingResponse>> apiResponse = new ApiResponse<List<GetCustomerPurchaseOrderTrackingResponse>>
            {
                Valid = true,
                Result = response.Result,
                Message = ""
            };
            return Ok(apiResponse);
        }
        [HttpPost]
        [Route("GetCustomerQuotationStatuses")]
        public async Task<IActionResult> GetCustomerQuotationStatuses(CustQuotationFilterModel model)
        {

            var response = await _salesOrderService.GetCustomerQuotationStatuses(model);
            ApiResponse<List<CustomerSalesQuotation>> apiResponse = new ApiResponse<List<CustomerSalesQuotation>>
            {
                Valid = true,
                Result = response.Result,
                Message = ""
            };
            return Ok(apiResponse);
        }
    }
}








