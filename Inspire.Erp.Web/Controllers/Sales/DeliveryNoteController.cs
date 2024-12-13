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
using Inspire.Erp.Web.ViewModels.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.MODULE;

using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Application.Account.Implementations;


namespace Inspire.Erp.Web.Controllers.Sales
{
    [Route("api/DeliveryNote")]
    [Produces("application/json")]
    [ApiController]
    public class DeliveryNoteController : ControllerBase
    {
        private IDeliveryNoteServices _deliveryNoteService;
        private readonly IMapper _mapper;
        public DeliveryNoteController(IDeliveryNoteServices deliveryNoteService, IMapper mapper)
        {
            _deliveryNoteService = deliveryNoteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertDeliveryNote")]
        public ApiResponse<CustomerDeliveryNoteViewModel> InsertSalesVoucher([FromBody] CustomerDeliveryNoteViewModel voucherCompositeView)
        {


            ApiResponse<CustomerDeliveryNoteViewModel> apiResponse = new ApiResponse<CustomerDeliveryNoteViewModel>();
            {
                var param1 = _mapper.Map<CustomerDeliveryNote>(voucherCompositeView);
                var param2 = _mapper.Map<List<CustomerDeliveryNoteDetails>>(voucherCompositeView.CustomerDeliveryNoteDetails);

                var xs = _deliveryNoteService.InsertDeliveryNote(param1, param2);


                CustomerDeliveryNoteViewModel deliveryNoteViewModel = new CustomerDeliveryNoteViewModel
                {

                    CustomerDeliveryNoteDeliveryId = xs.deliveryNote.CustomerDeliveryNoteDeliveryID,
                    CustomerDeliveryNoteDeliveryDate = xs.deliveryNote.CustomerDeliveryNoteDeliveryDate,
                    CustomerDeliveryNoteCpoId = xs.deliveryNote.CustomerDeliveryNoteCPOID,
                    CustomerDeliveryNoteCpoDate = xs.deliveryNote.CustomerDeliveryNoteCPODate,
                    CustomerDeliveryNoteLocationId = xs.deliveryNote.CustomerDeliveryNoteLocationID,
                    CustomerDeliveryNoteCustomerCode = xs.deliveryNote.CustomerDeliveryNoteCustomerCode,
                    CustomerDeliveryNoteCustomerName = xs.deliveryNote.CustomerDeliveryNoteCustomerName,
                    CustomerDeliveryNoteSalesManId = xs.deliveryNote.CustomerDeliveryNoteSalesManID,
                    CustomerDeliveryNoteCurrencyId = xs.deliveryNote.CustomerDeliveryNoteCurrencyID,
                    CustomerDeliveryNoteDeliveryAddress = xs.deliveryNote.CustomerDeliveryNoteDeliveryAddress,
                    CustomerDeliveryNoteRemarks = xs.deliveryNote.CustomerDeliveryNoteRemarks,
                    CustomerDeliveryNoteFsno = xs.deliveryNote.CustomerDeliveryNoteFSNO,
                    CustomerDeliveryNoteUserId = xs.deliveryNote.CustomerDeliveryNoteUserID,
                    CustomerDeliveryNoteNote = xs.deliveryNote.CustomerDeliveryNoteNote,
                    CustomerDeliveryNoteWarranty = xs.deliveryNote.CustomerDeliveryNoteWarranty,
                    CustomerDeliveryNoteTraining = xs.deliveryNote.CustomerDeliveryNoteTraining,
                    CustomerDeliveryNoteTechnicalDetails = xs.deliveryNote.CustomerDeliveryNoteTechnicalDetails,
                    CustomerDeliveryNoteTerms = xs.deliveryNote.CustomerDeliveryNoteTerms,
                    CustomerDeliveryNotePacking = xs.deliveryNote.CustomerDeliveryNotePacking,
                    CustomerDeliveryNoteQuality = xs.deliveryNote.CustomerDeliveryNoteQuality,
                    CustomerDeliveryNoteIssuedStatus = xs.deliveryNote.CustomerDeliveryNoteIssuedStatus,
                    CustomerDeliveryNoteAttention = xs.deliveryNote.CustomerDeliveryNoteAttention,
                    CustomerDeliveryNotePodId = xs.deliveryNote.CustomerDeliveryNotePODID,
                    CustomerDeliveryNoteDelDetId = xs.deliveryNote.CustomerDeliveryNoteDelDetID,
                    CustomerDeliveryNoteManuelPo = xs.deliveryNote.CustomerDeliveryNoteManuelPO,
                    CustomerDeliveryNoteDeliveryStatus = xs.deliveryNote.CustomerDeliveryNoteDeliveryStatus,
                    CustomerDeliveryNoteDelStatus = xs.deliveryNote.CustomerDeliveryNoteDelStatus,

                };

                deliveryNoteViewModel.CustomerDeliveryNoteDetails = _mapper.Map<List<CustomerDeliveryNoteDetailsViewModel>>(xs.deliveryNoteDetails);
                //deliveryNoteViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<CustomerDeliveryNoteViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<CustomerDeliveryNoteViewModel>(deliveryNoteViewModel),
                    Message = SalesVoucherMessage.SaveVoucher
                };
            }

            return apiResponse;


        }

        [HttpPost]
        [Route("UpdateDeliveryNote")]
        public ApiResponse<CustomerDeliveryNoteViewModel> UpdateSalesVoucher([FromBody] CustomerDeliveryNoteViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<CustomerDeliveryNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<CustomerDeliveryNoteDetails>>(voucherCompositeView.CustomerDeliveryNoteDetails);

            var xs = _deliveryNoteService.UpdateDeliveryNote(param1, param2);
            //========================


            CustomerDeliveryNoteViewModel deliveryNoteViewModel = new CustomerDeliveryNoteViewModel
            {

                CustomerDeliveryNoteDeliveryId = xs.deliveryNote.CustomerDeliveryNoteDeliveryID,
                CustomerDeliveryNoteDeliveryDate = xs.deliveryNote.CustomerDeliveryNoteDeliveryDate,
                CustomerDeliveryNoteCpoId = xs.deliveryNote.CustomerDeliveryNoteCPOID,
                CustomerDeliveryNoteCpoDate = xs.deliveryNote.CustomerDeliveryNoteCPODate,
                CustomerDeliveryNoteLocationId = xs.deliveryNote.CustomerDeliveryNoteLocationID,
                CustomerDeliveryNoteCustomerCode = xs.deliveryNote.CustomerDeliveryNoteCustomerCode,
                CustomerDeliveryNoteCustomerName = xs.deliveryNote.CustomerDeliveryNoteCustomerName,
                CustomerDeliveryNoteSalesManId = xs.deliveryNote.CustomerDeliveryNoteSalesManID,
                CustomerDeliveryNoteCurrencyId = xs.deliveryNote.CustomerDeliveryNoteCurrencyID,
                CustomerDeliveryNoteDeliveryAddress = xs.deliveryNote.CustomerDeliveryNoteDeliveryAddress,
                CustomerDeliveryNoteRemarks = xs.deliveryNote.CustomerDeliveryNoteRemarks,
                CustomerDeliveryNoteFsno = xs.deliveryNote.CustomerDeliveryNoteFSNO,
                CustomerDeliveryNoteUserId = xs.deliveryNote.CustomerDeliveryNoteUserID,
                CustomerDeliveryNoteNote = xs.deliveryNote.CustomerDeliveryNoteNote,
                CustomerDeliveryNoteWarranty = xs.deliveryNote.CustomerDeliveryNoteWarranty,
                CustomerDeliveryNoteTraining = xs.deliveryNote.CustomerDeliveryNoteTraining,
                CustomerDeliveryNoteTechnicalDetails = xs.deliveryNote.CustomerDeliveryNoteTechnicalDetails,
                CustomerDeliveryNoteTerms = xs.deliveryNote.CustomerDeliveryNoteTerms,
                CustomerDeliveryNotePacking = xs.deliveryNote.CustomerDeliveryNotePacking,
                CustomerDeliveryNoteQuality = xs.deliveryNote.CustomerDeliveryNoteQuality,
                CustomerDeliveryNoteIssuedStatus = xs.deliveryNote.CustomerDeliveryNoteIssuedStatus,
                CustomerDeliveryNoteAttention = xs.deliveryNote.CustomerDeliveryNoteAttention,
                CustomerDeliveryNotePodId = xs.deliveryNote.CustomerDeliveryNotePODID,
                CustomerDeliveryNoteDelDetId = xs.deliveryNote.CustomerDeliveryNoteDelDetID,
                CustomerDeliveryNoteManuelPo = xs.deliveryNote.CustomerDeliveryNoteManuelPO,
                CustomerDeliveryNoteDeliveryStatus = xs.deliveryNote.CustomerDeliveryNoteDeliveryStatus,
                CustomerDeliveryNoteDelStatus = xs.deliveryNote.CustomerDeliveryNoteDelStatus,
            };

            deliveryNoteViewModel.CustomerDeliveryNoteDetails = _mapper.Map<List<CustomerDeliveryNoteDetailsViewModel>>(xs.deliveryNoteDetails);
            //salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);

            ApiResponse<CustomerDeliveryNoteViewModel> apiResponse = new ApiResponse<CustomerDeliveryNoteViewModel>
            {
                Valid = true,
                Result = _mapper.Map<CustomerDeliveryNoteViewModel>(deliveryNoteViewModel),
                Message = SalesVoucherMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteDeliveryNote")]
        public ApiResponse<int> DeleteDeliveryNote([FromBody] CustomerDeliveryNoteViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<CustomerDeliveryNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<CustomerDeliveryNoteDetails>>(voucherCompositeView.CustomerDeliveryNoteDetails);
            var xs = _deliveryNoteService.DeleteDeliveryNote(param1, param2);

            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = SalesVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }


        [HttpGet]
        [Route("GetDeliveryNote")]
        public ApiResponse<List<CustomerDeliveryNote>> GetAllDeliveryNote()
        {
            ApiResponse<List<CustomerDeliveryNote>> apiResponse = new ApiResponse<List<CustomerDeliveryNote>>
            {
                Valid = true,
                Result = _mapper.Map<List<CustomerDeliveryNote>>(_deliveryNoteService.GetDeliveryNote()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedDeliveryNoteDetails/{id}")]
        public ApiResponse<CustomerDeliveryNoteViewModel> GetSavedDeliveryNoteDetails(long id)
        {
            DeliveryNoteModel deliveryNote = _deliveryNoteService.GetSavedDeliveryNoteDetails(id);

            if (deliveryNote != null)
            {
                CustomerDeliveryNoteViewModel deliveryNoteViewModel = new CustomerDeliveryNoteViewModel
                {

                    CustomerDeliveryNoteDeliveryId = deliveryNote.deliveryNote.CustomerDeliveryNoteDeliveryID,
                    CustomerDeliveryNoteDeliveryDate = deliveryNote.deliveryNote.CustomerDeliveryNoteDeliveryDate,
                    CustomerDeliveryNoteCpoId = deliveryNote.deliveryNote.CustomerDeliveryNoteCPOID,
                    CustomerDeliveryNoteCpoDate = deliveryNote.deliveryNote.CustomerDeliveryNoteCPODate,
                    CustomerDeliveryNoteLocationId = deliveryNote.deliveryNote.CustomerDeliveryNoteLocationID,
                    CustomerDeliveryNoteCustomerCode = deliveryNote.deliveryNote.CustomerDeliveryNoteCustomerCode,
                    CustomerDeliveryNoteCustomerName = deliveryNote.deliveryNote.CustomerDeliveryNoteCustomerName,
                    CustomerDeliveryNoteSalesManId = deliveryNote.deliveryNote.CustomerDeliveryNoteSalesManID,
                    CustomerDeliveryNoteCurrencyId = deliveryNote.deliveryNote.CustomerDeliveryNoteCurrencyID,
                    CustomerDeliveryNoteDeliveryAddress = deliveryNote.deliveryNote.CustomerDeliveryNoteDeliveryAddress,
                    CustomerDeliveryNoteRemarks = deliveryNote.deliveryNote.CustomerDeliveryNoteRemarks,
                    CustomerDeliveryNoteFsno = deliveryNote.deliveryNote.CustomerDeliveryNoteFSNO,
                    CustomerDeliveryNoteUserId = deliveryNote.deliveryNote.CustomerDeliveryNoteUserID,
                    CustomerDeliveryNoteNote = deliveryNote.deliveryNote.CustomerDeliveryNoteNote,
                    CustomerDeliveryNoteWarranty = deliveryNote.deliveryNote.CustomerDeliveryNoteWarranty,
                    CustomerDeliveryNoteTraining = deliveryNote.deliveryNote.CustomerDeliveryNoteTraining,
                    CustomerDeliveryNoteTechnicalDetails = deliveryNote.deliveryNote.CustomerDeliveryNoteTechnicalDetails,
                    CustomerDeliveryNoteTerms = deliveryNote.deliveryNote.CustomerDeliveryNoteTerms,
                    CustomerDeliveryNotePacking = deliveryNote.deliveryNote.CustomerDeliveryNotePacking,
                    CustomerDeliveryNoteQuality = deliveryNote.deliveryNote.CustomerDeliveryNoteQuality,
                    CustomerDeliveryNoteIssuedStatus = deliveryNote.deliveryNote.CustomerDeliveryNoteIssuedStatus,
                    CustomerDeliveryNoteAttention = deliveryNote.deliveryNote.CustomerDeliveryNoteAttention,
                    CustomerDeliveryNotePodId = deliveryNote.deliveryNote.CustomerDeliveryNotePODID,
                    CustomerDeliveryNoteDelDetId = deliveryNote.deliveryNote.CustomerDeliveryNoteDelDetID,
                    CustomerDeliveryNoteManuelPo = deliveryNote.deliveryNote.CustomerDeliveryNoteManuelPO,
                    CustomerDeliveryNoteDeliveryStatus = deliveryNote.deliveryNote.CustomerDeliveryNoteDeliveryStatus,
                    CustomerDeliveryNoteDelStatus = deliveryNote.deliveryNote.CustomerDeliveryNoteDelStatus,

                };
                deliveryNoteViewModel.CustomerDeliveryNoteDetails = _mapper.Map<List<CustomerDeliveryNoteDetailsViewModel>>(deliveryNote.deliveryNoteDetails);
                ApiResponse<CustomerDeliveryNoteViewModel> apiResponse = new ApiResponse<CustomerDeliveryNoteViewModel>
                {
                    Valid = true,
                    Result = deliveryNoteViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;



        }

        [HttpGet]
        [Route("CheckVnoExist/{id}")]
        public ApiResponse<bool> CheckVnoExist(long id)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Valid = true,
                Result = true,
                Message = SalesVoucherMessage.VoucherNumberExist



            };
            var x = 0;
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }



    }
}








