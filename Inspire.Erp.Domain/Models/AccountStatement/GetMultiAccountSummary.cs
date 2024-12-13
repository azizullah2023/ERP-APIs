using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
   public class GetMultiAccountSummaryResponse
    {
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string TotalDebit { get; set; }
        public string TotalCredit { get; set; }
        public string DebitBalance{ get; set; }
        public string CreditBalance { get; set; }
    }
    public class MultiAccountSummaryResponse{
        public List<GetMultiAccountSummaryResponse> Details { get; set; }
        public string AccName { get; set; }
        public string AccNo { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CreditBalance { get; set; }
        public string CompanyName { get; set; }
        public string Total { get; set; }
    }
    public class GetMultiAccountDetailResponse
    {
        public string VoucherType { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string RunningBalance { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string RefNo { get; set; }
        //public List<GetMultiAccountDetailResponse> __children { get; set; }
    }
    public class MultiAccountDetailResponse
    {
        public List<GetMultiAccountDetailResponse> Details { get; set; }
        public int? StationMasterCode { get; set; }
        public string StationMasterStationName { get; set; }
        public string StationMasterAddress { get; set; }
        public string StationMasterCity { get; set; }
        public string StationMasterPostOffice { get; set; }
        public string StationMasterTele1 { get; set; }
        public string StationMasterTele2 { get; set; }
        public string StationMasterFax { get; set; }
        public string StationMasterEmail { get; set; }
        public string StationMasterWebSite { get; set; }
        public string StationMasterCountry { get; set; }
        public string StationMasterLogoPath { get; set; }
        public string StationMasterSignPath { get; set; }
        public string StationMasterVatNo { get; set; }
        public string Total { get; set; }
    }
}
