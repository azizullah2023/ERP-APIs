using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.MODULE;
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseVoucherController : ControllerBase
    {

        private readonly IPurchaseVoucherService _service;

        private readonly IMapper _mapper;
        private IRepository<UnitDetails> _UnitDetailsRepository;
        public PurchaseVoucherController(IPurchaseVoucherService service, IMapper mapper, IRepository<UnitDetails> unitDetailsRepository)
        {
            _service = service;
            _mapper = mapper;
            _UnitDetailsRepository = unitDetailsRepository;
        }     

        [HttpGet("GetPurchaseReturnReport")]
        public IActionResult GetPurchaseReturnReport()
        {
            try
            {
                var item = _service.GetPurchaseReturnReport();
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        //
     
        [HttpPost]
        [Route("DeletePurchaseVoucher")]
        public ApiResponse<int> DeletePurchaseVoucher([FromBody] PurchaseVoucherViewModel dataObj)
        {

            var param1 = _mapper.Map<PurchaseVoucher>(dataObj);
            var param2 = _mapper.Map<List<PurchaseVoucherDetails>>(dataObj.PurchaseVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(dataObj.AccountsTransactions);


            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();

            var xs = _service.DeletePurchaseVoucher(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PurchaseVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }     

        [HttpPost("GetPurchaseVoucherReport")]
        public async Task<IActionResult> GetPurchaseVoucherReport(GenericGridViewModel model)
        {
            return Ok(await _service.GetPurchaseVoucherReport(model));
        }

        [HttpGet]
        [Route("GetPurchaseVoucher")]
        public ApiResponse<List<PurchaseVoucherListView>> GetPurchaseVoucher()
        {
            var rst = _mapper.Map<List<PurchaseVoucher>>(_service.GetPurchaseVoucher());

            List<PurchaseVoucherListView> result = (List<PurchaseVoucherListView>)rst.Select(a => new PurchaseVoucherListView
            {
                PurchaseVoucherPurId = a.PurchaseVoucherPurID,
                PurchaseVoucherVoucherNo = a.PurchaseVoucherVoucherNo,
                PurchaseVoucherPurchaseType = a.PurchaseVoucherPurchaseType,
                PurchaseVoucherGrNo = a.PurchaseVoucherGRNo,
                PurchaseVoucherPurchaseDate = a.PurchaseVoucherPurchaseDate,
                PurchaseVoucherNetAmount = a.PurchaseVoucherNetAmount,
                PurchaseVoucherUserId = a.PurchaseVoucherUserID,
                PurchaseVoucherLocationId = a.PurchaseVoucherLocationID,
                PurchaseVoucherPoNo = a.PurchaseVoucherPONo,
                PurchaseVoucherCurrencyId = a.PurchaseVoucherCurrencyID,
                PurchaseVoucherJobId = a.PurchaseVoucherJobID,
                PurchaseVoucherCashSupplierName = a.PurchaseVoucherCashSupplierName,
                PurchaseVoucherVatAmount = a.PurchaseVoucherVATAmount,
                PurchaseVoucherVatRoundSign = a.PurchaseVoucherVATRoundSign,
                PurchaseVoucherVatRoundAmount = a.PurchaseVoucherVATRoundAmount,
                PurchaseVoucherVatNo = a.PurchaseVoucherVATNo,
                PurchaseVoucherPartyId = a.PurchaseVoucherPartyID,
                PurchaseVoucherPartyName = a.PurchaseVoucherPartyName,
                PurchaseVoucherTotalGrossAmt = a.PurchaseVoucherTotalGrossAmt,
                PurchaseVoucherTotalItemDisAmount = a.PurchaseVoucherTotalItemDisAmount,
                PurchaseVoucherTotalDiscountAmt = a.PurchaseVoucherTotalDiscountAmt

            }).ToList();
            ApiResponse<List<PurchaseVoucherListView>> apiResponse = new ApiResponse<List<PurchaseVoucherListView>>
            {
                Valid = true,
                Result = result,
                Message = ""
            };
            return apiResponse;
        }

        [HttpPost("InsertPurchaseVoucher")]
        public ApiResponse<PurchaseVoucher> InsertPurchaseVoucher([FromBody] PurchaseVoucher purchaseVoucher)
        {
            var param1 = _mapper.Map<PurchaseVoucher>(purchaseVoucher);
            var param2 = _mapper.Map<List<PurchaseVoucherDetails>>(purchaseVoucher.PurchaseVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(purchaseVoucher.AccountsTransactions);

            var response = _service.InsertPurchaseVoucher(param1, param3, param2);
            var res = new ApiResponse<PurchaseVoucher>
            {
                Valid = true,
                Message = PurchaseVoucherMessage.SaveVoucher,
                Result = response,
            };

            return res;
        }

        [HttpPost("UpdatePurchaseVoucher")]
        public ApiResponse<PurchaseVoucher> UpdatePurchaseVoucher([FromBody] PurchaseVoucher purchaseVoucher)
        {
            var param1 = _mapper.Map<PurchaseVoucher>(purchaseVoucher);
            var param2 = _mapper.Map<List<PurchaseVoucherDetails>>(purchaseVoucher.PurchaseVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(purchaseVoucher.AccountsTransactions);

            var response = _service.UpdatePurchaseVoucher(param1, param3, param2);
            var res = new ApiResponse<PurchaseVoucher>
            {
                Valid = true,
                Message = PurchaseVoucherMessage.UpdateVoucher,
                Result = response,
            };

            return res;
        }
        [HttpGet("getDetailsOfVoucher")]
        public object getDetailsOfVoucher()
        {
            var result = _service.GetPurchaseVoucherDetails();
            return Ok(result) ;
        }


        [HttpGet("GetSavedPurchaseVoucherDetails/{id}")]
        public ApiResponse<PurchaseVoucher> GetSavedPurchaseVoucherDetails(string id)
        {
            var purchaseVoucher = _service.GetSavedPurchaseVoucherDetailsV2(id);

            ApiResponse<PurchaseVoucher> apiResponse = new ApiResponse<PurchaseVoucher>
            {
                Valid = true,
                Result = purchaseVoucher,
                Message = ""
            };
            return apiResponse;
        }
    }
}