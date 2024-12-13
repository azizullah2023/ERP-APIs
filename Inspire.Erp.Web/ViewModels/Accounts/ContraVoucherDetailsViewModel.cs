using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public partial class ContraVoucherDetailsViewModel
    {
        public int? ContraVoucherDetailsId { get; set; }
        public int? ContraVoucherId { get; set; }
        public string ContraVoucherDetailsVno { get; set; }
        public string ContraVoucherDetailsAccNo { get; set; }
        public double? ContraVoucherDetailsVatableAmount { get; set; }
        public decimal? ContraVoucherDetailsDrAmount { get; set; }
        public decimal? ContraVoucherDetailsFcDrAmount { get; set; }
        public decimal? ContraVoucherDetailsCrAmount { get; set; }
        public decimal? ContraVoucherDetailsFcCrAmount { get; set; }
        public long? ContraVoucherDetailsJobNo { get; set; }
        public int? ContraVoucherDetailsCostCenterId { get; set; }
        public string ContraVoucherDetailsNarration { get; set; }
        public bool? ContraVoucherDetailsDelStatus { get; set; }
    }
}
