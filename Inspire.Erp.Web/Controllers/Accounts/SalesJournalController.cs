using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/SalesJournal")]
    [Produces("application/json")]
    [ApiController]
    public class SalesJournalController : ControllerBase
    {
        private ISalesJournalService _salesJournalService;
        private readonly IMapper _mapper;
        public SalesJournalController(ISalesJournalService salesJournalService, IMapper mapper)
        {
            _salesJournalService = salesJournalService;
            _mapper = mapper;
        }
        //[HttpPost]
        //[Route("InsertSalesJournal")]
        //public SalesJournalViewModel InsertSalesJournal([FromBody] SalesJournalCompositeView voucherCompositeView)
        //{
        //    List <SalesJournalDetailsViewModel> salesJournalDetailsViewModels = new List<SalesJournalDetailsViewModel>();
        //    var param1 = _mapper.Map<SalesJournal>(voucherCompositeView.SalesJournal);
        //    var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);    
        //    var xs = _salesJournalService.InsertSalesJournal(param1, param2);
        //    return _mapper.Map<SalesJournalViewModel>(xs);

        //}

        [HttpPost]
        [Route("InsertSalesJournal")]
        public ApiResponse<SalesJournalViewModel> InsertSalesJournal([FromBody] SalesJournalViewModel voucherCompositeView)
        {
            //var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
            //var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
            //var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var xs = _salesJournalService.InsertSalesJournal(param1, param3, param2);
            //SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
            //{
            //    SalesJournalVno = xs.salesJournal.SalesJournalVno,
            //    SalesJournalCrAmount = xs.salesJournal.SalesJournalCrAmount,
            //    SalesJournalFcCrAmount = xs.salesJournal.SalesJournalFcCrAmount,
            //    SalesJournalDrAmount = xs.salesJournal.SalesJournalDrAmount,
            //    SalesJournalFcDrAmount = xs.salesJournal.SalesJournalFcDrAmount,
            //    SalesJournalAcNo = xs.salesJournal.SalesJournalAcNo,
            //    SalesJournalDate = xs.salesJournal.SalesJournalDate,
            //    SalesJournalId = xs.salesJournal.SalesJournalId,
            //    SalesJournalNarration = xs.salesJournal.SalesJournalNarration,
            //    SalesJournalCurrencyId = xs.salesJournal.SalesJournalCurrencyId,

            //    SalesJournalRefNo = xs.salesJournal.SalesJournalRefNo 

            //};

            //salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(xs.salesJournalDetails);
            //salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);
            //return _mapper.Map<SalesJournalViewModel>(salesJournalViewModel);


            ApiResponse<SalesJournalViewModel> apiResponse = new ApiResponse<SalesJournalViewModel>();
            if (_salesJournalService.GetVouchersNumbers(voucherCompositeView.SalesJournalVno) == null)
            {
                var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
                var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                var xs = _salesJournalService.InsertSalesJournal(param1, param3, param2);
                SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
                {
                    SalesJournalVno = xs.salesJournal.SalesJournalVno,
                    SalesJournalCrAmount = xs.salesJournal.SalesJournalCrAmount,
                    SalesJournalFcCrAmount = xs.salesJournal.SalesJournalFcCrAmount,
                    SalesJournalDrAmount = xs.salesJournal.SalesJournalDrAmount,
                    SalesJournalFcDrAmount = xs.salesJournal.SalesJournalFcDrAmount,
                    //SalesJournalAcNo = xs.salesJournal.SalesJournalAcNo,
                    SalesJournalDate = xs.salesJournal.SalesJournalDate,
                    SalesJournalId = xs.salesJournal.SalesJournalId,
                    SalesJournalNarration = xs.salesJournal.SalesJournalNarration,
                    SalesJournalCurrencyId = xs.salesJournal.SalesJournalCurrencyId,

                    SalesJournalRefNo = xs.salesJournal.SalesJournalRefNo
                };

                salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(xs.salesJournalDetails);
                salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<SalesJournalViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<SalesJournalViewModel>(salesJournalViewModel),
                    Message = SalesJournalMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<SalesJournalViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = SalesJournalMessage.VoucherAlreadyExist
                };

            }
            return apiResponse;

        }

        [HttpPost]
        [Route("UpdateSalesJournal")]
        public ApiResponse<SalesJournalViewModel> UpdateSalesJournal([FromBody] SalesJournalViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
            var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _salesJournalService.UpdateSalesJournal(param1, param3, param2);

            SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
            {
                SalesJournalVno = xs.salesJournal.SalesJournalVno,
                SalesJournalCrAmount = xs.salesJournal.SalesJournalCrAmount,
                //SalesJournalAcNo = xs.salesJournal.SalesJournalAcNo,
                SalesJournalDate = xs.salesJournal.SalesJournalDate,
                SalesJournalId = xs.salesJournal.SalesJournalId,
                SalesJournalNarration = xs.salesJournal.SalesJournalNarration,
                SalesJournalCurrencyId = xs.salesJournal.SalesJournalCurrencyId,
                SalesJournalRefNo = xs.salesJournal.SalesJournalRefNo,
                SalesJournalDrAmount = xs.salesJournal.SalesJournalDrAmount
            };

            salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(xs.salesJournalDetails);
            salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<SalesJournalViewModel> apiResponse = new ApiResponse<SalesJournalViewModel>
            {
                Valid = true,
                Result = _mapper.Map<SalesJournalViewModel>(salesJournalViewModel),
                Message = SalesJournalMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteSalesJournal")]
        public ApiResponse<int> DeleteSalesJournal([FromBody] SalesJournalViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
            var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _salesJournalService.DeleteSalesJournal(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = SalesJournalMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_salesJournalService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;




        }

        [HttpGet]
        [Route("GetSalesJournal")]
        public ApiResponse<List<SalesJournal>> GetAllSalesJournal()
        {


            ApiResponse<List<SalesJournal>> apiResponse = new ApiResponse<List<SalesJournal>>
            {
                Valid = true,
                Result = _mapper.Map<List<SalesJournal>>(_salesJournalService.GetSalesJournal()),
                Message = ""
            };
            return apiResponse;





        }

        [HttpGet]
        [Route("GetSavedSalesJournalDetails/{id}")]
        public ApiResponse<SalesJournalViewModel> GetSavedSalesJournalDetails(string id)
        {
            SalesJournalModel salesJournal = _salesJournalService.GetSavedSalesJournalDetails(id);

            if (salesJournal != null)
            {
                SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
                {
                    SalesJournalVno = salesJournal.salesJournal.SalesJournalVno,
                    SalesJournalCrAmount = salesJournal.salesJournal.SalesJournalCrAmount,
                    //SalesJournalAcNo = salesJournal.salesJournal.SalesJournalAcNo,
                    SalesJournalDate = salesJournal.salesJournal.SalesJournalDate,
                    SalesJournalId = salesJournal.salesJournal.SalesJournalId,
                    SalesJournalNarration = salesJournal.salesJournal.SalesJournalNarration,
                    SalesJournalCurrencyId = salesJournal.salesJournal.SalesJournalCurrencyId,
                    SalesJournalRefNo = salesJournal.salesJournal.SalesJournalRefNo,
                    SalesJournalDrAmount = salesJournal.salesJournal.SalesJournalDrAmount
                };
                salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(salesJournal.salesJournalDetails);
                salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(salesJournal.accountsTransactions);
                ApiResponse<SalesJournalViewModel> apiResponse = new ApiResponse<SalesJournalViewModel>
                {
                    Valid = true,
                    Result = salesJournalViewModel,
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
                Message = SalesJournalMessage.VoucherNumberExist
            };
            var x = _salesJournalService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }



    }
}




//using Inspire.Erp.Application.Account.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Inspire.Erp.Application.Account.Interfaces;
//using Inspire.Erp.Application.Common;
//using Inspire.Erp.Domain.Entities;
//using Inspire.Erp.Web.ViewModels;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Inspire.Erp.Web.Controllers
//{
//    [Route("api/SalesJournal")]
//    [Produces("application/json")]
//    [ApiController]
//    public class SalesJournalController : ControllerBase
//    {
//        private ISalesJournalService _salesJournalService;
//        private readonly IMapper _mapper;
//        public SalesJournalController(ISalesJournalService salesJournalService, IMapper mapper)
//        {
//            _salesJournalService = salesJournalService;
//            _mapper = mapper;
//        }
//        //[HttpPost]
//        //[Route("InsertSalesJournal")]
//        //public SalesJournalViewModel InsertSalesJournal([FromBody] SalesJournalCompositeView voucherCompositeView)
//        //{
//        //    List <SalesJournalDetailsViewModel> salesJournalDetailsViewModels = new List<SalesJournalDetailsViewModel>();
//        //    var param1 = _mapper.Map<SalesJournal>(voucherCompositeView.SalesJournal);
//        //    var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);    
//        //    var xs = _salesJournalService.InsertSalesJournal(param1, param2);
//        //    return _mapper.Map<SalesJournalViewModel>(xs);

//        //}

//        [HttpPost]
//        [Route("InsertSalesJournal")]
//        public SalesJournalViewModel InsertSalesJournal([FromBody] SalesJournalViewModel voucherCompositeView)
//        {
//            var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
//            var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
//            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
//            var xs = _salesJournalService.InsertSalesJournal(param1, param3, param2);
//            SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
//            {
//                SalesJournalVno = xs.salesJournal.SalesJournalVno,
//                SalesJournalCrAmount = xs.salesJournal.SalesJournalCrAmount,
//                //SalesJournalAcNo = xs.salesJournal.SalesJournalAcNo,
//                SalesJournalDate = xs.salesJournal.SalesJournalDate,
//                SalesJournalId = xs.salesJournal.SalesJournalId,
//                SalesJournalNarration = xs.salesJournal.SalesJournalNarration,
//                SalesJournalCurrencyId = xs.salesJournal.SalesJournalCurrencyId,
//                SalesJournalRefNo = xs.salesJournal.SalesJournalRefNo,
//                SalesJournalDrAmount = xs.salesJournal.SalesJournalDrAmount
//            };

//            salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(xs.salesJournalDetails);
//            salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);
//            return _mapper.Map<SalesJournalViewModel>(salesJournalViewModel);

//        }

//        [HttpPost]
//        [Route("UpdateSalesJournal")]
//        public SalesJournalViewModel UpdateSalesJournal([FromBody] SalesJournalViewModel voucherCompositeView)
//        {
//            var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
//            var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
//            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
//            var xs = _salesJournalService.UpdateSalesJournal(param1, param3, param2);

//            SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
//            {
//                SalesJournalVno = xs.salesJournal.SalesJournalVno,
//                SalesJournalCrAmount = xs.salesJournal.SalesJournalCrAmount,
//                //SalesJournalAcNo = xs.salesJournal.SalesJournalAcNo,
//                SalesJournalDate = xs.salesJournal.SalesJournalDate,
//                SalesJournalId = xs.salesJournal.SalesJournalId,
//                SalesJournalNarration = xs.salesJournal.SalesJournalNarration,
//                SalesJournalCurrencyId = xs.salesJournal.SalesJournalCurrencyId,
//                SalesJournalRefNo = xs.salesJournal.SalesJournalRefNo,
//                SalesJournalDrAmount = xs.salesJournal.SalesJournalDrAmount
//            };

//            salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(xs.salesJournalDetails);
//            salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);

//            return _mapper.Map<SalesJournalViewModel>(salesJournalViewModel);

//        }

//        [HttpPost]
//        [Route("DeleteSalesJournal")]
//        public int DeleteSalesJournal([FromBody] SalesJournalViewModel voucherCompositeView)
//        {
//            var param1 = _mapper.Map<SalesJournal>(voucherCompositeView);
//            var param2 = _mapper.Map<List<SalesJournalDetails>>(voucherCompositeView.SalesJournalDetails);
//            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
//            var xs = _salesJournalService.DeleteSalesJournal(param1, param3, param2);
//            return xs;

//        }

//        [HttpGet]
//        [Route("GetAllAccountTransaction")]
//        public List<AccountsTransactions> GetAllAccountTransaction()
//        {
//            return _mapper.Map<List<AccountsTransactions>>(_salesJournalService.GetAllTransaction());
//        }

//        [HttpGet]
//        [Route("GetSalesJournal")]
//        public List<SalesJournal> GetAllSalesJournal()
//        {
//            return _mapper.Map<List<SalesJournal>>(_salesJournalService.GetSalesJournal());
//        }

//        [HttpGet]
//        [Route("GetSavedSalesJournalDetails/{id}")]
//        public SalesJournalViewModel GetSavedSalesJournalDetails(string id)
//        {
//            SalesJournalModel salesJournal = _salesJournalService.GetSavedSalesJournalDetails(id);
//            SalesJournalViewModel salesJournalViewModel = new SalesJournalViewModel
//            {
//                SalesJournalVno = salesJournal.salesJournal.SalesJournalVno,
//                SalesJournalCrAmount = salesJournal.salesJournal.SalesJournalCrAmount,
//                //SalesJournalAcNo = salesJournal.salesJournal.SalesJournalAcNo,
//                SalesJournalDate = salesJournal.salesJournal.SalesJournalDate,
//                SalesJournalId = salesJournal.salesJournal.SalesJournalId,
//                SalesJournalNarration = salesJournal.salesJournal.SalesJournalNarration,
//                SalesJournalCurrencyId = salesJournal.salesJournal.SalesJournalCurrencyId,
//                SalesJournalRefNo = salesJournal.salesJournal.SalesJournalRefNo,
//                SalesJournalDrAmount = salesJournal.salesJournal.SalesJournalDrAmount
//            };
//            salesJournalViewModel.SalesJournalDetails = _mapper.Map<List<SalesJournalDetailsViewModel>>(salesJournal.salesJournalDetails);
//            salesJournalViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(salesJournal.accountsTransactions);
//            return salesJournalViewModel;
//        }
//    }
//}
