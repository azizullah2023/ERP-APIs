////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Microsoft.AspNetCore.Mvc;

////namespace Inspire.Erp.Web.Controllers.Store
////{
////    public class OpeningStockController : Controller
////    {
       
////    }
////}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels.Store;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Web.MODULE;

namespace Inspire.Erp.Web.Controllers.Store
{
    [Route("api/OpeningStock")]
    [Produces("application/json")]
    [ApiController]
    public class OpeningStockController : ControllerBase
    {
        private IOpeningStockService _openingStockService;
        private readonly IMapper _mapper;
        public OpeningStockController(IOpeningStockService openingStockService, IMapper mapper)
        {
            _openingStockService = openingStockService;
            _mapper = mapper;
        }




        [HttpPost]
        [Route("InsertOpeningStockVoucher")]
        public async Task<ApiResponse<OpeningStockVoucherViewModel>> InsertOpeningStockVoucher([FromBody] OpeningStockVoucherViewModel voucherCompositeView)
        {


            ApiResponse<OpeningStockVoucherViewModel> apiResponse = new ApiResponse<OpeningStockVoucherViewModel>();
            if (_openingStockService.GetVouchersNumbers(voucherCompositeView.OpeningStockVoucherNo) == null)
            {
                var param1 = _mapper.Map<OpeningStockVoucher>(voucherCompositeView);
                var param2 = _mapper.Map<List<OpeningStockVoucherDetails>>(voucherCompositeView.OpeningStockVoucherDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
                //var param3 = new List<AccountsTransactions>();
                //var param4 = new List<StockRegister>();
                //clsAccountAndStock.OpeningStockVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

                var xs = await _openingStockService.InsertOpeningStockVoucher(param1, param3, param2
               , param4
               );
                
                OpeningStockVoucherViewModel openingStockVoucherViewModel = new OpeningStockVoucherViewModel
                {

                    OpeningStockVoucherId = xs.openingstockVoucher.OpeningStockVoucherId,
                    OpeningStockVoucherNo = xs.openingstockVoucher.OpeningStockVoucherNo,
                    OpeningStockVoucherDate = xs.openingstockVoucher.OpeningStockVoucherDate,
                    OpeningStockVoucherLocationId = xs.openingstockVoucher.OpeningStockVoucherLocationId,
                    OpeningStockVoucherRemarks = xs.openingstockVoucher.OpeningStockVoucherRemarks,

                };

                openingStockVoucherViewModel.OpeningStockVoucherDetails = _mapper.Map<List<OpeningStockVoucherDetailsViewModel>>(xs.openingstockVoucherDetails);
                openingStockVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<OpeningStockVoucherViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<OpeningStockVoucherViewModel>(openingStockVoucherViewModel),
                    Message = OpeningStockVoucherMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<OpeningStockVoucherViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = OpeningStockVoucherMessage.VoucherAlreadyExist
                };

            }


            return apiResponse;

        }





        //[HttpPost]
        //[Route("InsertOpeningStock")]
        //public ApiResponse<OpeningStockDtViewModel> InsertOpeningStock([FromBody] OpeningStockDtViewModel voucherCompositeView)
        //{
        //    ApiResponse<OpeningStockDtViewModel> apiResponse = new ApiResponse<OpeningStockDtViewModel>();
        //    if (_openingStockService.GetVouchersNumbers(voucherCompositeView.openingStockCreditAcc) == null)
        //    {
        //        var param1 = _mapper.Map<OpeningStock>(voucherCompositeView);
        //        var param2 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
        //        var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
        //        var xs = _openingStockService.InsertOpeningStock(param1, param3, param2);
        //        OpeningStockViewModel openingStockViewModel = new OpeningStockViewModel
        //        {

        //            OpeningStockStockId = xs.openingStock.OpeningStockStockId,
        //            OpeningStockPurchaseId = xs.openingStock.OpeningStockPurchaseId,
        //            OpeningStockSno = xs.openingStock.OpeningStockSno,
        //            OpeningStockBatchCode = xs.openingStock.OpeningStockBatchCode,
        //            OpeningStockMaterialId = xs.openingStock.OpeningStockMaterialId,
        //            OpeningStockQty = xs.openingStock.OpeningStockQty,
        //            OpeningStockCurrencyId = xs.openingStock.OpeningStockCurrencyId,
        //            OpeningStockCRate = xs.openingStock.OpeningStockCRate,
        //            OpeningStockUnitRate = xs.openingStock.OpeningStockUnitRate,
        //            OpeningStockAmount = xs.openingStock.OpeningStockAmount,
        //            OpeningStockFcAmount = xs.openingStock.OpeningStockFcAmount,
        //            OpeningStockRemakrs = xs.openingStock.OpeningStockRemakrs,
        //            OpeningStockFsno = xs.openingStock.OpeningStockFsno,
        //            OpeningStockPosted = xs.openingStock.OpeningStockPosted,
        //            OpeningStockUnitId = xs.openingStock.OpeningStockUnitId,
        //            OpeningStockLocationId = xs.openingStock.OpeningStockLocationId,
        //            OpeningStockJobId = xs.openingStock.OpeningStockJobId,
        //            OpeningStockIsEdit = xs.openingStock.OpeningStockIsEdit,
        //            OpeningStockExpDate = xs.openingStock.OpeningStockExpDate,
        //            OpeningStockDelStatus = xs.openingStock.OpeningStockDelStatus



        //        };

        //        openingStockViewModel.StockRegister = _mapper.Map<List<StockRegisterViewModel>>(xs.StockRegister);
        //        openingStockViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.AccountsTransactions);
        //        apiResponse = new ApiResponse<OpeningStockDtViewModel>
        //        {
        //            Valid = true,
        //            Result = _mapper.Map<OpeningStockDtViewModel>(openingStockViewModel),
        //            Message = PaymentVoucherMessage.SaveVoucher
        //        };
        //    }
        //    else
        //    {
        //        apiResponse = new ApiResponse<OpeningStockDtViewModel>
        //        {
        //            Valid = false,
        //            Error = true,
        //            Exception = null,
        //            Message = PaymentVoucherMessage.VoucherAlreadyExist
        //        };

        //    }
        //    return apiResponse;
        //}



        [HttpPost]
        [Route("UpdateOpeningStockVoucher")]
        public async Task<ApiResponse<OpeningStockVoucherViewModel>> UpdateOpeningStockVoucher([FromBody] OpeningStockVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<OpeningStockVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<OpeningStockVoucherDetails>>(voucherCompositeView.OpeningStockVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);

            param3 = new List<AccountsTransactions>();

            //List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.OpeningStockVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = await _openingStockService.UpdateOpeningStockVoucher(param1, param3, param2
           , param4
           );
           
            OpeningStockVoucherViewModel openingstockVoucherViewModel = new OpeningStockVoucherViewModel
            {
                OpeningStockVoucherId = xs.openingstockVoucher.OpeningStockVoucherId,
                OpeningStockVoucherNo = xs.openingstockVoucher.OpeningStockVoucherNo,
                OpeningStockVoucherDate = xs.openingstockVoucher.OpeningStockVoucherDate,
                OpeningStockVoucherLocationId = xs.openingstockVoucher.OpeningStockVoucherLocationId,
                OpeningStockVoucherRemarks = xs.openingstockVoucher.OpeningStockVoucherRemarks,
            };

            openingstockVoucherViewModel.OpeningStockVoucherDetails = _mapper.Map<List<OpeningStockVoucherDetailsViewModel>>(xs.openingstockVoucherDetails);
            openingstockVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<OpeningStockVoucherViewModel> apiResponse = new ApiResponse<OpeningStockVoucherViewModel>
            {
                Valid = true,
                Result = _mapper.Map<OpeningStockVoucherViewModel>(openingstockVoucherViewModel),
                Message = OpeningStockVoucherMessage.UpdateVoucher
            };

            return apiResponse;

        }


        //[HttpPost]
        //[Route("UpdateOpeningStock")]
        //public ApiResponse<OpeningStockViewModel> UpdateOpeningStock([FromBody] OpeningStockViewModel voucherCompositeView)
        //{

        //    var param1 = _mapper.Map<OpeningStock>(voucherCompositeView);
        //    var param2 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
        //    var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
        //    var xs = _openingStockService.UpdateOpeningStock(param1, param3, param2);

        //    OpeningStockViewModel openingStockViewModel = new OpeningStockViewModel
        //    {
        //        OpeningStockStockId = xs.openingStock.OpeningStockStockId,
        //        OpeningStockPurchaseId = xs.openingStock.OpeningStockPurchaseId,
        //        OpeningStockSno = xs.openingStock.OpeningStockSno,
        //        OpeningStockBatchCode = xs.openingStock.OpeningStockBatchCode,
        //        OpeningStockMaterialId = xs.openingStock.OpeningStockMaterialId,
        //        OpeningStockQty = xs.openingStock.OpeningStockQty,
        //        OpeningStockCurrencyId = xs.openingStock.OpeningStockCurrencyId,
        //        OpeningStockCRate = xs.openingStock.OpeningStockCRate,
        //        OpeningStockUnitRate = xs.openingStock.OpeningStockUnitRate,
        //        OpeningStockAmount = xs.openingStock.OpeningStockAmount,
        //        OpeningStockFcAmount = xs.openingStock.OpeningStockFcAmount,
        //        OpeningStockRemakrs = xs.openingStock.OpeningStockRemakrs,
        //        OpeningStockFsno = xs.openingStock.OpeningStockFsno,
        //        OpeningStockPosted = xs.openingStock.OpeningStockPosted,
        //        OpeningStockUnitId = xs.openingStock.OpeningStockUnitId,
        //        OpeningStockLocationId = xs.openingStock.OpeningStockLocationId,
        //        OpeningStockJobId = xs.openingStock.OpeningStockJobId,
        //        OpeningStockIsEdit = xs.openingStock.OpeningStockIsEdit,
        //        OpeningStockExpDate = xs.openingStock.OpeningStockExpDate,
        //        OpeningStockDelStatus = xs.openingStock.OpeningStockDelStatus
        //    };

        //    openingStockViewModel.StockRegister = _mapper.Map<List<StockRegisterViewModel>>(xs.StockRegister);
        //    openingStockViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.AccountsTransactions);
        //    ApiResponse<OpeningStockViewModel> apiResponse = new ApiResponse<OpeningStockViewModel>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<OpeningStockViewModel>(openingStockViewModel),
        //        Message = PaymentVoucherMessage.UpdateVoucher
        //    };

        //    return apiResponse;
        //}



        [HttpPost]
        [Route("DeleteOpeningStockVoucher")]
        public ApiResponse<int> DeleteOpeningStockVoucher([FromBody] OpeningStockVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<OpeningStockVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<OpeningStockVoucherDetails>>(voucherCompositeView.OpeningStockVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);

            var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //List<StockRegister> param4 = new List<StockRegister>();
            //var xs = _openingstockVoucherService.DeleteOpeningStockVoucher(  param1,    param3, param2
            //    , param4
            //    );

            //==============
            //param3 = new List<AccountsTransactions>();
            //List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.OpeningStockVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _openingStockService.DeleteOpeningStockVoucher(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = OpeningStockVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }



        //[HttpPost]
        //[Route("DeleteOpeningStock")]
        //public ApiResponse<int> DeleteOpeningStock([FromBody] OpeningStockViewModel voucherCompositeView)
        //{
        //    var param1 = _mapper.Map<OpeningStock>(voucherCompositeView);
        //    var param2 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
        //    var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
        //    var xs = _openingStockService.DeleteOpeningStock(param1, param3, param2);
        //    ApiResponse<int> apiResponse = new ApiResponse<int>
        //    {
        //        Valid = true,
        //        Result = 0,
        //        Message = PaymentVoucherMessage.DeleteVoucher
        //    };

        //    return apiResponse;

        //}

        [HttpGet]
        [Route("GetAllAccountTransaction")]
        public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        {
            ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
            {
                Valid = true,
                Result = _mapper.Map<List<AccountsTransactions>>(_openingStockService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetOpeningStocks")]
        public ApiResponse<List<OpeningStock>> GetAllOpeningStock()
        {
            ApiResponse<List<OpeningStock>> apiResponse = new ApiResponse<List<OpeningStock>>
            {
                Valid = true,
                Result = _mapper.Map<List<OpeningStock>>(_openingStockService.GetOpeningStocks()),
                Message = ""
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetOpeningStockVouchers")]
        public ApiResponse<List<OpeningStockVoucher>> GetOpeningStockVouchers()
        {
            ApiResponse<List<OpeningStockVoucher>> apiResponse = new ApiResponse<List<OpeningStockVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<OpeningStockVoucher>>(_openingStockService.GetOpeningStockVouchers()),
                Message = ""
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetSavedOpeningStockVoucherDetails/{id}")]
        public ApiResponse<OpeningStockVoucherViewModel> GetSavedOpeningStockVoucherDetails(string id)
        {
            OpeningStockVoucherModel openingstockVoucher = _openingStockService.GetSavedOpeningStockVoucherDetails(id);

            if (openingstockVoucher != null)
            {
                OpeningStockVoucherViewModel openingstockVoucherViewModel = new OpeningStockVoucherViewModel
                {

                OpeningStockVoucherId = openingstockVoucher.openingstockVoucher.OpeningStockVoucherId,
                OpeningStockVoucherNo = openingstockVoucher.openingstockVoucher.OpeningStockVoucherNo,
                OpeningStockVoucherDate = openingstockVoucher.openingstockVoucher.OpeningStockVoucherDate,
                OpeningStockVoucherLocationId = openingstockVoucher.openingstockVoucher.OpeningStockVoucherLocationId,

                OpeningStockVoucherRemarks = openingstockVoucher.openingstockVoucher.OpeningStockVoucherRemarks,

                OpeningStockVoucherFSNO = openingstockVoucher.openingstockVoucher.OpeningStockVoucherFSNO,

                OpeningStockVoucherVNO = openingstockVoucher.openingstockVoucher.OpeningStockVoucherVNO,

                };
                openingstockVoucherViewModel.OpeningStockVoucherDetails = _mapper.Map<List<OpeningStockVoucherDetailsViewModel>> (openingstockVoucher.openingstockVoucherDetails);
                openingstockVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(openingstockVoucher.accountsTransactions);
                ApiResponse<OpeningStockVoucherViewModel> apiResponse = new ApiResponse<OpeningStockVoucherViewModel>
                {
                    Valid = true,
                    Result = openingstockVoucherViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;



        }


        [HttpGet]
        [Route("GetSavedOPStockDetails/{id}")]
        public ApiResponse<OpeningStockViewModel> GetSavedOPStockDetails(string id)
        {
            var data = _openingStockService.GetSavedOPStockDetails(id);
            OpeningStockViewModel OpStock = _mapper.Map<OpeningStockViewModel>(data);
            if (OpStock != null)
            {
                OpeningStockViewModel openingStockViewModel = new OpeningStockViewModel
                {
                    OpeningStockStockId = OpStock.OpeningStockStockId,
                    OpeningStockPurchaseId = OpStock.OpeningStockPurchaseId,
                    OpeningStockSno = OpStock.OpeningStockSno,
                    OpeningStockBatchCode = OpStock.OpeningStockBatchCode,
                    OpeningStockMaterialId = OpStock.OpeningStockMaterialId,
                    OpeningStockQty = OpStock.OpeningStockQty,
                    OpeningStockCurrencyId = OpStock.OpeningStockCurrencyId,
                    OpeningStockCRate = OpStock.OpeningStockCRate,
                    OpeningStockUnitRate = OpStock.OpeningStockUnitRate,
                    OpeningStockAmount = OpStock.OpeningStockAmount,
                    OpeningStockFcAmount = OpStock.OpeningStockFcAmount,
                    OpeningStockRemakrs = OpStock.OpeningStockRemakrs,
                    OpeningStockFsno = OpStock.OpeningStockFsno,
                    OpeningStockPosted = OpStock.OpeningStockPosted,
                    OpeningStockUnitId = OpStock.OpeningStockUnitId,
                    OpeningStockLocationId = OpStock.OpeningStockLocationId,
                    OpeningStockJobId = OpStock.OpeningStockJobId,
                    OpeningStockIsEdit = OpStock.OpeningStockIsEdit,
                    OpeningStockExpDate = OpStock.OpeningStockExpDate,
                    OpeningStockDelStatus = OpStock.OpeningStockDelStatus

                };
                openingStockViewModel.StockRegister = _mapper.Map<List<StockRegisterViewModel>>(OpStock.StockRegister);
                openingStockViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(OpStock.AccountsTransactions);
                ApiResponse<OpeningStockViewModel> apiResponse = new ApiResponse<OpeningStockViewModel>
                {
                    Valid = true,
                    Result = openingStockViewModel,
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
                Message = PaymentVoucherMessage.VoucherAlreadyExist
            };
            var x = _openingStockService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }
    }
}