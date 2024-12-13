using AutoMapper;
using Inspire.Erp.Application.Dashboard.Interfaces;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Dashboard;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;
        private readonly IRepository<GetSummaryResponse> _summaryService;
        private IConfiguration Configuration;

        public DashboardController(IDashboardService dashboardService, IMapper mapper, IRepository<GetSummaryResponse> summaryService, IConfiguration _Configuration)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
            _summaryService = summaryService;
            Configuration = _Configuration;
        }
        [HttpPost("GetDashboardPDCIssueRecieved")]
        public async Task<IActionResult> GetDashboardPDCIssueRecieved(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetDashboardPDCIssueRecieved(model));
        }
        [HttpPost("GetSummaryChart")]
        public async Task<IActionResult> GetSummaryChart(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetSummaryChart(model));
        }
        [HttpPost("GetCustomerSupplier")]
        public async Task<IActionResult> GetCustomerSupplier(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetCustomerSupplier(model));
        }
        [HttpPost("GetDasboardIncomeExpense")]
        public async Task<IActionResult> GetDasboardIncomeExpense(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetDasboardIncomeExpense(model));
        }
        [HttpPost("GetDasboardPurchaseGroupAccount")]
        public async Task<IActionResult> GetDasboardPurchaseGroupAccount(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetDasboardPurchaseGroupAccount(model));
        }
        [HttpPost("GetDasboardIssueCount")]
        public async Task<IActionResult> GetDasboardIssueCount(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetDasboardIssueCount(model));
        }
        [HttpPost("GetDashboardBankBalance")]
        public async Task<IActionResult> GetDashboardBankBalance(DashboardFilterModel model)
        {
            return Ok(await _dashboardService.GetDashboardBankBalance(model));
        }


        [HttpPost("GetSummaryChart1")]
        public async Task<IActionResult> GetSummaryChart1(DashboardFilterModel model)
        {
            {
                try
                {

                    Nullable<DateTime> startDate = null;
                    Nullable<DateTime> endDate = null;
                    if (model.StartDate != null)
                    {
                        startDate = Convert.ToDateTime(model.StartDate);
                    }
                    if (model.EndDate != null)
                    {
                        endDate = Convert.ToDateTime(model.EndDate);
                    }
                    string connectionString = this.Configuration["ApplicationSettings:ConnectionString"];

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        DataTable dtab = new DataTable();
                        var resultList = new List<Dictionary<string, object>>();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetDashboardData", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Choice", model.Type);
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var row = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row[reader.GetName(i)] = reader.GetValue(i);
                                    }
                                    resultList.Add(row);
                                }
                            }
                        }
                        return Ok(new { IsSuccess = true, ResultSet = resultList });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        IsSuccess = false,
                        ErrorMessage = "An unexpected error occurred. Please try again later.",
                        ErrorDetails = ex.Message
                    });
                }
            }
        }
    }
}
