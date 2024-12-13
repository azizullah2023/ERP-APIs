using AutoMapper;
using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherPrintingController : ControllerBase
    {
        private Inspire.Erp.Application.Store.Interface.IvoucherPrinting vp;
        private readonly IMapper _mapper;
        public VoucherPrintingController(IvoucherPrinting _vp)
        {
            vp = _vp;
        }
        //[HttpGet("VoucherPrintingResponse")]
        //public List<VoucherPrintingResponse> VoucherPrintingResponse()
        //{
        //    VoucherPrintingRequest obj = new VoucherPrintingRequest();  
        //    try
        //    {
        //        return vp.VoucherPrintingResponse(obj).Select(k => new VoucherPrintingResponse
        //        {
        //            AccountsTransactions_VoucherType = k.AccountsTransactions_VoucherType,
        //            AccountsTransactions_VoucherNo = k.AccountsTransactions_VoucherNo,
        //            MA_AccName = k.MA_AccName,
        //            AccountsTransactions_Particulars = k.AccountsTransactions_Particulars,
        //            AccountsTransactions_Debit = k.AccountsTransactions_Debit,
        //            AccountsTransactions_Credit = k.AccountsTransactions_Credit
        //        }).ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    } 
        //}
 
        [HttpGet("VoucherPrintingResponse")]
        public async Task<dynamic> VoucherPrintingResponse()
        {
            try
            {
                VoucherPrintingRequest obj = new VoucherPrintingRequest();
                return vp.VoucherPrintingRequest(obj);
                //.Select(k => new StatementOfAccountsSummaryViewModel
                //return sts.StatementOfAccountSummaryResponse(obj).Select(k => new StatementOfAccountsSummaryViewModel
                //{
                //    Dr = k.Dr,
                //    Cr = k.Cr

                //}).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
