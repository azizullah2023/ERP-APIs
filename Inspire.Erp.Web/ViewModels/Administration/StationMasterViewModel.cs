using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Administration
{
    public class StationMasterViewModel
    {
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
        public byte[] StationMasterLogoImg { get; set; }
        public byte[] StationMasterSignImg { get; set; }
        public byte[] StationMasterSealImg { get; set; }
        public string StationMasterVatNo { get; set; }
        public bool? StationMasterDelStatus { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }
        public string BankBranch { get; set; }
        public string BankCurrency { get; set; }
    }
}
