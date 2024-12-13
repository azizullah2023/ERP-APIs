using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Domain.Models.ViewModels
{
    public class IssueWithStockTransfer
    {
        public string AV_Issue_Voucher_AVSV_NO { get; set; }
        public DateTime? AV_Issue_Voucher_AVSV_Date { get; set; }
        public string PartNo { get; set; }
        public string Item_Master_Item_Name { get; set; }
        public double Quantity { get; set; }
        public double AV_Issue_Voucher_Details_Rate { get; set; }
        public double Amount { get; set; }
        public string Job_Master_Job_Name { get; set; }
        public string Job_Master_Job_Number { get; set; }
        public int AV_Issue_Voucher_Job_ID { get; set; }
        public long Item_Master_Account_No { get; set; }
        public long Item_Master_Item_ID { get; set; }
        public int DepId { get; set; }
        public int CostCenter { get; set; }
        public bool Item_Master_Services { get; set; }
    }
}