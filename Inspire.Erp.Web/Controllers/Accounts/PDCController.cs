using Inspire.Erp.Application.Account.Interface;
using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Inspire.Erp.Domain.Modals.PDCResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDCController : ControllerBase
    {
        private readonly IPDC _pdc;
        public PDCController(IPDC pdc)
        {
            _pdc = pdc;
        }
        [HttpGet("PDCPostedReturnList/{chequeStatus}")]
        public async Task<IEnumerable<PDCPostedList>> PDCPostedReturnList(string chequeStatus)
        {
            try
            {
                return await _pdc.PDCPostedReturnList(chequeStatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("PDCReturnList")]
        public async Task<IEnumerable<PDCPostedList>> PDCReturnList()
        {
            try
            {
                return await _pdc.PDCReturnLists();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("PDCGetList")]
        public async Task<IEnumerable<PDCGetList>> PDCGetList()
        {
            try
            {
                return await _pdc.PDCGetList();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        [HttpPost("PDCGetReturnList")]
        public async Task<IEnumerable<PDCGetList>> PDCGetReturnList(PDCFilters obj)
        {
            try
            {
                return await _pdc.PDCGetReturnList(obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpGet("GetBankNames")]
        public async Task<List<BankNamesModel>> GetBankNames()
        {
            return await _pdc.GetBankNames();
        }

        [HttpPost("getFilteredPDCList")]
        public async Task<IEnumerable<PDCGetList>> getFilteredPDCList(PDCFilters obj)
        {
            return await _pdc.getFilteredPDCList(obj);
        }

        [HttpPost("PostPDC")]
        public async Task<IEnumerable<PDCGetList>> PostPDC([FromBody] List<PDCGetList> pDCGetList)
        {
            return await _pdc.PostPDC(pDCGetList);
        }



    }
}