using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesRet_Master
    {
        public SalesRet_Master()
        {
            SalesVoucherDetails = new HashSet<POS_SalesVoucherDetails>();
        }
        public string SR_NO { get; set; }

        public decimal? SR_NO_NUM { get; set; }

        public DateTime? SR_Dt { get; set; }

        public string SReturn_Type { get; set; }

        public string Ref_SV_NO { get; set; }

        public DateTime? Ref_SV_Date { get; set; }

        public long? Ref_SV_FSNO { get; set; }

        public string SV_VoucherType { get; set; }

        public decimal? Cust_No { get; set; }

        public string Cust_Name { get; set; }

        public long? Loca_ID { get; set; }

        public decimal? GrossAmount { get; set; }

        public decimal? Discount { get; set; }

        public decimal? NetAmount { get; set; }

        public decimal? Fc_NetAmount { get; set; }

        public string Narration { get; set; }

        public string Status { get; set; }

        public decimal? FSNO { get; set; }

        public decimal? User_ID { get; set; }

        public decimal? Fc_Rate { get; set; }

        public long? Currency_ID { get; set; }

        public decimal? SR_ItemDisc { get; set; }

        public decimal? SR_NetDisc { get; set; }

        public int? WorkPeriodID { get; set; }

        public float? VAT_AMT { get; set; }

        [NotMapped]
        public virtual ICollection<POS_SalesVoucherDetails> SalesVoucherDetails { get; set; }
    }
}
