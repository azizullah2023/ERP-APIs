using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PosStationSettings
    {
        public long Id { get; set; }
        public string StationName { get; set; }
        public string PrinterName { get; set; }
        public string PrinterType { get; set; }
        public string PrinterPort { get; set; }
        public string PrintHeader1 { get; set; }
        public string PrintHeader2 { get; set; }
        public string PrintHeader3 { get; set; }
        public string PrintHeader4 { get; set; }
        public string PrintFooter1 { get; set; }
        public string PrintFooter2 { get; set; }
        public string PrintFooter3 { get; set; }
        public string PrintFooter4 { get; set; }
        public string VFDDriver { get; set; }
        public long? DisplayLines { get; set; }
        public long? BaudRate { get; set; }
        public string WelcomeMessage { get; set; }
        public string BillClosing { get; set; }
        public string Scanner { get; set; }
        public string Emulation { get; set; }
        public bool? ShowVATColumn { get; set; }
        public bool? ShowDiscountColumn { get; set; }
        public bool? ShowVATTotal { get; set; }
        public bool? ClubGrid { get; set; }
        public string Parity { get; set; }
        public bool? ShowUnit { get; set; }
        public bool? ShowBarCode { get; set; }
        public bool? DisableVat { get; set; }
        public bool? EnablePrinter { get; set; }
        public bool? EnableVFD { get; set; }
        public bool? ReportCashSales { get; set; }
        public bool? ReportCardSales { get; set; }
        public bool? ReportCreditSales { get; set; }
        public bool? ReportItemTotal { get; set; }
        public bool? ReportGroupTotal { get; set; }
        public long? CSBCopy { get; set; }
        public long? CRBCopy { get; set; }
        public decimal? VatPercentage { get; set; }
        public bool? VATInclusive { get; set; }
        public bool? ReorderCheck { get; set; }

        public string DefLocationName { get; set; }

        public int? DefLocation { get; set; }
        public bool? PrintTextReader { get; set; }
        public bool? ShowSalesMan { get; set; }
        public bool? CardNoMandetory { get; set; }
        public bool? CardCommision { get; set; }
        public bool? CrystalInvoice { get; set; }
        public double? CardCommisionPer { get; set; }
        public bool? FilterBillSummHead { get; set; }
        public bool? OptionForCashCustomer { get; set; }
        public int? RoundOff { get; set; }
        public int? ItemRoundOff { get; set; }
        public string POSStationCode { get; set; }

        public bool? VATPrint { get; set; }
        public bool? CustomerSelect { get; set; }
        public bool? DiscountPrint { get; set; }
        public bool? UnitPriceWODisc { get; set; }
        public bool? TotalDiscountApply { get; set; }

        public bool? OnKeyboard { get; set; }
        public bool? EnableSMS { get; set; }
        public bool? StockCheck { get; set; }

        public string? POSColorTheme { get; set; }

        public bool? CustEnableSMS { get; set; }
        public bool? BillEdit { get; set; }
        public bool? QtyEnter { get; set; }
        public bool? DelStatus { get; set; }

    }
}
