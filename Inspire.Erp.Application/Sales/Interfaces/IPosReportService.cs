using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Entities.POS;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Application.Sales.Interfaces
{
    public interface IPosReportService
    {

        Task<List<POS_SummaryDateWiseReport>> GetSalesTransactionCash(int wpId, int stationId, DateTime fromDate, DateTime toDate);

        Task<List<POS_SummaryDateWiseReport>> GetSalesTransactionCard(int wpId, int stationId, DateTime fromDate, DateTime toDate);


        Task<POS_MaintodaySales> GetTodaySalesByDate(int wpId, int stationId, DateTime fromDate, DateTime toDate);

        Task<List<TodaySalesByCounterWise>> GetTodaySalesByCounterWise(DateTime fromDate, DateTime toDate);








    }
}
