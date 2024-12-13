using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Accounts
{
    public partial class AssetPurchaseVoucherDetailsViewModel
    {

        public int AstPurDetID { get; set; }
        public int AstPurchaseID { get; set; }
        public string AssetPurchaseDetailsVoucherNo { get; set; }
        public decimal? Sno { get; set; }
        public decimal? AstID { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public decimal? FcAmount { get; set; }
        public string Remarks { get; set; }
        public string AstPurRefNo { get; set; }
        public decimal? LifrInYrs { get; set; }
        public DateTime AstPurDate { get; set; }
        public string DepMode { get; set; }
        public decimal? DepAmt { get; set; }
        public decimal? DepPerc { get; set; }
        public int? FSNO { get; set; }
        public int? LocationID { get; set; }
        public decimal? SalesPrice1 { get; set; }
        public decimal? SalesPrice2 { get; set; }
        public decimal? SalesPrice3 { get; set; }
        public int? CompanyId { get; set; }
        public decimal? LPOID { get; set; }
        public string GRNNO { get; set; }
        public string BatchCode { get; set; }
        public decimal? AccDepAmt { get; set; }
        public decimal? BookValue { get; set; }
        public string ModelNo { get; set; }
        public string Specification { get; set; }
        public string Manufacturer { get; set; }
        public string AssetSize { get; set; }
        public string Colour { get; set; }
        public decimal? OPAccDepAmt { get; set; }
        public decimal? OPBookValue { get; set; }

        public bool? AssetPurchaseVoucherDetailsDelStatus { get; set; }

    }
}
