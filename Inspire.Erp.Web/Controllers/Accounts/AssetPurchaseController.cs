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
using Inspire.Erp.Web.ViewModels.Accounts;
using NPOI.SS.Formula.Functions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.Design;
using System.Security.Cryptography;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/AssetPurchase")]
    [Produces("application/json")]
    [ApiController]
    public class AssetPurchaseController : ControllerBase
    {
        private IAssetsOpeningService _salesOrderService;
        private readonly IMapper _mapper;
        public AssetPurchaseController(IAssetsOpeningService salesOrderService, IMapper mapper)
        {
            _salesOrderService = salesOrderService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertAssetPurchase")]
        public async Task<IActionResult> InsertAssetPurchase([FromBody] AssetPurchaseVoucherViewModel voucherCompositeView)
        {
            ApiResponse<AssetPurchaseVoucherViewModel> apiResponse = new ApiResponse<AssetPurchaseVoucherViewModel>();
            try
            {
                var param1 = _mapper.Map<AssetPurchaseVoucher>(voucherCompositeView);
                var xs = await _salesOrderService.InsertAssetPurchaseVoucher(param1);
                var apvm = new AssetPurchaseVoucherViewModel();
                apvm = _mapper.Map<AssetPurchaseVoucherViewModel>(xs.assetPurchaseVoucher);
                apiResponse = new ApiResponse<AssetPurchaseVoucherViewModel>
                {
                    Valid = true,
                    Result = apvm,//_mapper.Map<AssetPurchaseVoucherViewModel>(apvm),
                    Message = AssetPurchaseVoucherMessage.SaveVoucher
                };

                return Ok(apiResponse);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
           
            //var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
           

            //            AssetPurchaseVoucherViewModel salesVoucherViewModel = new AssetPurchaseVoucherViewModel
            //            {

            //                  AstPurID=xs.assetPurchaseVoucher.AstPurID,
            //        AssetPurchaseVoucherNo=xs.assetPurchaseVoucher.AssetPurchaseVoucherNo,
            //    AstPurchaseRef=xs.assetPurchaseVoucher.AstPurchaseRef,
            //AstPurchaseType=xs.assetPurchaseVoucher.AstPurchaseType,
            //         GRNo=xs.assetPurchaseVoucher.GRNo,
            //         GRDate=xs.assetPurchaseVoucher.GRDate,
            //         SPID=xs.assetPurchaseVoucher.SPID,
            //         SPAccNo=xs.assetPurchaseVoucher.SPAccNo,
            //         SPAmount=xs.assetPurchaseVoucher.SPAmount,
            //         FCSPAmount=xs.assetPurchaseVoucher.FCSPAmount,
            //         AstPurDt=xs.assetPurchaseVoucher.AstPurDt,
            //         LPONo=xs.assetPurchaseVoucher.LPONo,
            //         LPODate=xs.assetPurchaseVoucher.LPODate,
            //         QuotationNo=xs.assetPurchaseVoucher.QuotationNo,
            //         QuotationDate=xs.assetPurchaseVoucher.QuotationDate,
            //         ActualAmount=xs.assetPurchaseVoucher.ActualAmount,
            //         NetAmount=xs.assetPurchaseVoucher.NetAmount,
            //         TransportCost=xs.assetPurchaseVoucher.TransportCost,
            //         Handlingcharges=xs.assetPurchaseVoucher.Handlingcharges,
            //         FcActualAmount=xs.assetPurchaseVoucher.
            //         FcNetAmount=xs.assetPurchaseVoucher.
            //         Description=xs.assetPurchaseVoucher.
            //         DrAccNo=xs.assetPurchaseVoucher.
            //         DrAmount=xs.assetPurchaseVoucher.
            //         FcDrAmount=xs.assetPurchaseVoucher.
            //         DisYN=xs.assetPurchaseVoucher.
            //         DisAcNo=xs.assetPurchaseVoucher.
            //         DisAmount=xs.assetPurchaseVoucher.
            //         FcDisAmount=xs.assetPurchaseVoucher.
            //         Status=xs.assetPurchaseVoucher.
            //         FSNO=xs.assetPurchaseVoucher.
            //         UserID=xs.assetPurchaseVoucher.
            //         FcRate=xs.assetPurchaseVoucher.
            //         LocationID=xs.assetPurchaseVoucher.
            //         SupInvNo=xs.assetPurchaseVoucher.
            //         DisPer=xs.assetPurchaseVoucher.
            //         PONO=xs.assetPurchaseVoucher.
            //         CurrencyId=xs.assetPurchaseVoucher.
            //         CompanyId=xs.assetPurchaseVoucher.
            //         VatAMT=xs.assetPurchaseVoucher.
            //         VatPer=xs.assetPurchaseVoucher.
            //         VatRoundSign=xs.assetPurchaseVoucher.
            //         VatRountAmt=xs.assetPurchaseVoucher.
            //         AstVatNo=xs.assetPurchaseVoucher.


            //            };
          
            //salesVoucherViewModel.AssetPurchaseDetails = _mapper.Map<List<AssetPurchaseVoucherDetailsViewModel>>(xs.assetPurchaseVoucher.AssetPurchaseVoucherDetails);
            //salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
           // apvm.AccountsTransactions= _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
        
        }
        [HttpPost]
        [Route("UpdateAssetPurchase")]
        public ApiResponse<AssetPurchaseVoucherViewModel> UpdateAssetPurchase([FromBody] AssetPurchaseVoucherViewModel voucherCompositeView)
        {
            ApiResponse<AssetPurchaseVoucherViewModel> apiResponse = new ApiResponse<AssetPurchaseVoucherViewModel>();
            var param1 = _mapper.Map<AssetPurchaseVoucher>(voucherCompositeView);
      
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _salesOrderService.UpdateAssetPurchaseVoucher(param1, param3 );
            var apvm = new AssetPurchaseVoucherViewModel();
            apvm = _mapper.Map<AssetPurchaseVoucherViewModel>(xs);
            apiResponse = new ApiResponse<AssetPurchaseVoucherViewModel>
            {
                Valid = true,
                Result = apvm,//_mapper.Map<AssetPurchaseVoucherViewModel>(apvm),
                Message = AssetPurchaseVoucherMessage.UpdateVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteAssetPurchase")]
        public ApiResponse<int> DeleteAssetPurchase([FromBody] AssetPurchaseVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<AssetPurchaseVoucher>(voucherCompositeView);
            
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
    
            var xs = _salesOrderService.DeleteAssetPurchaseVoucher(param1, param3);
        
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = AssetPurchaseVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }

        [HttpGet]
        [Route("GetAllAssetPurchase")]
        public IActionResult GetAllAssetPurchase()
        {
            ApiResponse<List<AssetPurchaseVoucher>> apiResponse = new ApiResponse<List<AssetPurchaseVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<AssetPurchaseVoucher>>(_salesOrderService.GetAssetPurchaseVouchers()),
                Message = ""
            };
            return Ok(apiResponse);
        }

        [HttpGet]
        [Route("GetAssetVoucherDetails/{id}")]
        public ApiResponse<AssetPurchaseVoucherViewModel> GetAssetVoucherDetails(string id)
        {
            AssetPurchaseVoucherModel xs = _salesOrderService.GetSavedAssetPurchaseVoucherDetails(id);
            if (xs != null)
            {
                var apvm = new AssetPurchaseVoucherViewModel();
                apvm = _mapper.Map<AssetPurchaseVoucherViewModel>(xs.assetPurchaseVoucher);
                ApiResponse<AssetPurchaseVoucherViewModel> apiResponse = new ApiResponse<AssetPurchaseVoucherViewModel>
                {
                    Valid = true,
                    Result = apvm,
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

    }
}








