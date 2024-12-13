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
    public class StatementOfAccountSummaryController : ControllerBase
    {
        private IStatementOfAccountSummary sts;
        private readonly IMapper _mapper;
        public StatementOfAccountSummaryController(IStatementOfAccountSummary _sts)
        {
            sts = _sts;
        }

        [HttpGet,Route("StatementOfAccountSummaryResponse")]
        public async Task<dynamic> StatementOfAccountSummaryResponse()
        {
            StatementOfAccountSummaryRequest obj = new StatementOfAccountSummaryRequest();
            obj.StartDate = DateTime.Now.AddYears(-3);
            obj.AcNo = "001002002001002";
            try
            {
                return sts.StatementOfAccountSummaryResponse(obj);
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
