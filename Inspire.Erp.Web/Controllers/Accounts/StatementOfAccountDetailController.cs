using AutoMapper;
using Inspire.Erp.Application.Store.Implementation;
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
    public class StatementOfAccountDetailController : ControllerBase
    {
        private Inspire.Erp.Application.Store.Interface.IStatementOfAccountDetail std;
        private readonly IMapper _mapper;
        public StatementOfAccountDetailController(IStatementOfAccountDetail _std)
        {
            std = _std;
        }

        [HttpGet("StatementOfAccountDetailResponse")]
        public async Task<dynamic> StatementOfAccountDetailResponse()
        {
            StatementOfAccountDetailRequest obj = new StatementOfAccountDetailRequest();
            try
            {
                
                obj.StartDate = DateTime.Now.AddMonths(-10);
                obj.EndDate = DateTime.Now;
                return std.StatementOfAccountDetailResponse(obj);
                //return std.StatementOfAccountDetailResponse(obj).Select(k => new StatementOfAccountsDetailViewModel
                //{
                //    AccountsTransactions_AccNo = k.AccountsTransactions_AccNo,
                //    AccountsTransactions_TransDate = k.AccountsTransactions_TransDate,
                //    AccountsTransactions_Particulars = k.AccountsTransactions_Particulars,
                //    AccountsTransactions_Debit = k.AccountsTransactions_Debit,
                //    AccountsTransactions_Credit = k.AccountsTransactions_Credit,
                //    AccountsTransactions_VoucherType = k.AccountsTransactions_VoucherType,
                //    AccountsTransactions_VoucherNo = k.AccountsTransactions_VoucherNo,
                //    AccountsTransactions_Description = k.AccountsTransactions_Description,
                //    AccountsTransactions_FSNo = k.AccountsTransactions_FSNo,
                //    CostCenter = k.CostCenter

                //}).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
