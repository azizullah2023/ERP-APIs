using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities.POS
{
    public partial class TransactionCodes
    {
        public int Id { get; set; }
        public string Trans_code { get; set; }
        public string Trans_Description { get; set; }
        public int Trans_group { get; set; }
        public string Main_Group { get; set; }
        public string CRDR { get; set; }
        public bool? Taxable { get; set; }
        public bool? Manual_transaction { get; set; }
        public string Acc_Code { get; set; }
        public string Inv_Code { get; set; }
        public string PrefixCode { get; set; }
        public string Refcodefield { get; set; }
        public string Reportfilename { get; set; }
        public bool? Printvoucher { get; set; }
        public bool? ManualTransaction { get; set; }
        public bool? PosttoAccounts { get; set; }
        public double? taxsvc { get; set; }
        public double? tax { get; set; }
        public double? svc { get; set; }
        public int? guestledger { get; set; }
        public int? dailysummary { get; set; }
        public bool? InHouse { get; set; }
        public int? Dept_id { get; set; }
        public int? Sort_order { get; set; }
        public bool? show_in_inv { get; set; }
        public bool? FilterBillSummHead { get; set; }
    }
}
