using Inspire.Erp.Domain.Modals.AccountStatement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Stock
{
   public class StockAdjustmentReportResponse
    {

        public DateTime? SADate { get; set; }
        public string SANumber { get; set; }

        public string JobName { get; set; }
        public string ItemName { get; set; }
        public double? ManualQuantity { get; set; }
        public double? CostPrice { get; set; }
        public double? AdjustedQuantity { get; set; }
        public double? AdjustedValue { get; set; }

        public double? ExistingQuantity { get; set; }

    }

   


}
