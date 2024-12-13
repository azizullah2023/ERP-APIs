using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class VoucherPrintingRequest
    {
        public decimal? voucherNoFROM { get; set; }
        public decimal? VoucherNOTo { get; set; }
        public string? VoucherType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? chequeNo { get; set; }
        public string? VoucherNo_NU { get; set; }
    }
}
