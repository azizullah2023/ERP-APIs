using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PdcDetails
    {
        public int PDC_Details_ID { get; set; }
        public string PDC_Details_V_No { get; set; }
        public string PDC_Details_V_Type { get; set; }
        public string PDC_Details_Trans_Type { get; set; }
        public DateTime? PDC_Details_Trans_Date { get; set; }
        public DateTime? PDC_Details_PDC_Date { get; set; }
        public string PDC_Details_Cheque_No { get; set; }
        public double? PDC_Details_Cheque_Amount { get; set; }
        public double? PDC_Details_FC_Cheque_Amount { get; set; }
        public string PDC_Details_Cheque_Bank_Name { get; set; }
        public string PDC_Details_Bank_Account_No { get; set; }
        public string PDC_Details_Cheque_Status { get; set; }
        public string PDC_Details_PDC_Voucher_ID { get; set; }
        public DateTime? PDC_Details_PDC_Voucher_Date { get; set; }
        public string PDC_Details_PDC_Voucher_Narration { get; set; }
        public int? PDC_Details_FSNO { get; set; }
        public int? PDC_Details_User_ID { get; set; }
        public int? PDC_Details_Flat_ID { get; set; }
        public int? PDC_Details_Building_ID { get; set; }
        public string PDC_Details_Contract { get; set; }
        public string PDC_Details_Old_Cheque_Status { get; set; }
        public string PDC_Details_PartyAccNo { get; set; }
        public bool? PDC_Details_DelStatus { get; set; }
    }
}
