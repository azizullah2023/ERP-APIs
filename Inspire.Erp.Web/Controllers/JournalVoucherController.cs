using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class JournalVoucherController : ControllerBase
    {
        private IReceiptVoucherService _receiptVoucherService;
        private readonly IMapper _mapper;
        public JournalVoucherController(IReceiptVoucherService receiptVoucherService, IMapper mapper)
        {
            _receiptVoucherService = receiptVoucherService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("InsertReceiptVoucher")]
        ////public List<AccountsTransactions> InsertReceiptVoucher([FromBody] ReceiptVoucherCompositeViewModel receiptCompositeView)
        ////{
        ////    List<ReceiptVoucherDetailsViewModel> receiptVoucherDetailsViewModels = new List<ReceiptVoucherDetailsViewModel>();
        ////    var param1 = _mapper.Map<ReceiptVoucherMaster>(receiptCompositeView.ReceiptVoucherMasterViewModel);
        ////    var param2 = _mapper.Map<List<ReceiptVoucherDetails>>(receiptCompositeView.ReceiptVoucherDetails);
        ////    List<AccountsTransactions> accountsTransactions = _receiptVoucherService.InsertReceiptVoucher(param1, param2).ToList();
        ////    return _mapper.Map<List<AccountsTransactions>>(accountsTransactions);

        ////}

        [HttpGet]
        [Route("GetAllAccountTransaction")]
        public List<AccountsTransactions> GetAllAccountTransaction()
        {
            return _mapper.Map<List<AccountsTransactions>>(_receiptVoucherService.GetAllTransaction());
        }
    }
}