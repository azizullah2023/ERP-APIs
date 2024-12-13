using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Domain.Modals.Common;
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
    public class MISController : ControllerBase
    {
        private readonly IProfitLossService _profitLossService;
        private readonly IBalanceSheetService _balanceSheet;
        private readonly IUtilityService _utilityService;
        public MISController(IProfitLossService profitLossService, IBalanceSheetService balanceSheet, IUtilityService utilityService)
        {
            _profitLossService = profitLossService;
            _balanceSheet = balanceSheet;
            _utilityService = utilityService;
        }

        #region Profit Loss 
        [HttpGet("GetFinancialYearResponse")]
        public async Task<IActionResult> GetFinancialYearResponse()
        {
            return Ok(await _profitLossService.GetFinancialYearResponse());
        }
        [HttpPost("GetAccountTransactionsProfitLoss")]
        public async Task<IActionResult> GetAccountTransactionsProfitLoss(GenericGridViewModel model)
        {
            //return Ok(await _profitLossService.GetAccountTransactionsProfitLoss(model));
            return Ok(await _profitLossService.GetAccountTransactionsProfitAndLoss(model));
        }
        [HttpPost("GetAccountTransactionsProfitLossPrintMonthWise")]
        public async Task<IActionResult> GetAccountTransactionsProfitLossPrintMonthWise(GenericGridViewModel model)
        {
            return Ok(await _profitLossService.GetAccountTransactionsProfitLossPrintMonthWise(model));
        }
        [HttpPost("GetAccountTransactionsProfitLossPrintSimple")]
        public async Task<IActionResult> GetAccountTransactionsProfitLossPrintSimple(GenericGridViewModel model)
        {
            return Ok(await _profitLossService.GetAccountTransactionsProfitLossPrintSimple(model));
        }
        [HttpPost("GetAccountTransactionsProfitLossPrintSummary")]
        public async Task<IActionResult> GetAccountTransactionsProfitLossPrintSummary(GenericGridViewModel model)
        {
            return Ok(await _profitLossService.GetAccountTransactionsProfitLossPrintSummary(model));
        }
        #endregion

        #region Balance Sheet 

        [HttpPost("GetAccountTransactionsBalanceSheetSummary")]
        public async Task<IActionResult> GetAccountTransactionsBalanceSheetSummary(GenericGridViewModel model)
        {
            return Ok(await _balanceSheet.GetAccountTransactionsBalanceSheetsSummary(model));
         //   return Ok(await _balanceSheet.GetAccountTransactionsBalanceSheetSummary(model));
        }
        [HttpPost("GetAccountTransactionsBalanceSheetDetail")]
        public async Task<IActionResult> GetAccountTransactionsBalanceSheetDetail(GenericGridViewModel model)
        {
            //return Ok(await _balanceSheet.GetAccountTransactionsBalanceSheetDetail(model));
            return Ok(await _balanceSheet.GetAccountTransactionsBalanceSheetsDetails(model));
        }
        [HttpPost("BalanceSheetPrint")]
        public async Task<IActionResult> BalanceSheetPrint(GenericGridViewModel model)
        {
            var result = await _balanceSheet.BalanceSheetPrint(model);
            if (result.Valid)
            {
                return File(result.Result.stream, result.Result.ContentType, result.Result.Path);
            }
            return File(result.Result.stream, result.Result.ContentType, result.Result.Path);
        }
        [HttpPost("BalanceSheetPrintSummary")]
        public async Task<IActionResult> BalanceSheetPrintSummary(GenericGridViewModel model)
        {
            var result = await _balanceSheet.BalanceSheetPrintSummary(model);
            if (result.Valid)
            {
                return File(result.Result.stream, result.Result.ContentType, result.Result.Path);
            }
            return File(result.Result.stream, result.Result.ContentType, result.Result.Path);
        }
        #endregion

    }
}
