using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class GRNVoucherRquest
    {
        public string? poId { get; set; }
        public string? voucherNo { get; set; }
        public string? voucherDate { get; set; }
        public string? supplierInvoiceNo { get; set; }
        public string? currency { get; set; }
        public string? excludeVat { get; set; }
        public string? purchaseType { get; set; }
        public string? supplier { get; set; }
        public string? address { get; set; }
        public string? partyVatNo { get; set; }
        public string? refNo { get; set; }
        public string? pvDesc { get; set; }
        public string? grnNo { get; set; }
        public string? grnDate { get; set; }
        public string? lpoNo { get; set; }
        public string? lpoDate { get; set; }
        public string? qoutNo { get; set; }
        public string? qoutDate { get; set; }
        public string? poNo { get; set; }
        public string? batch { get; set; }
        public string? job { get; set; }
        public string? location { get; set; }
        public string? itemId { get; set; }
        public string? itemName { get; set; }
        public string? unit { get; set; }
        public string? unitId { get; set; }
        public string? price { get; set; }
        public string? purchaseQty { get; set; }
        public string? issueQty { get; set; }
        public string? rate { get; set; }
        public string? disc { get; set; }
        public string? vatAmt { get; set; }
        public string? vatRoundAmt { get; set; }
        public string? vatRoundSign { get; set; }
        public string? vatPer { get; set; }
        public string? netAmt { get; set; }
        public string? costCenter { get; set; }
    }
}
