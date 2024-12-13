using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class EnquiryMasterViewModel
    {
        public string? EnquiryMasterEnquiryId { get; set; }
        public DateTime? EnquiryMasterEnquiryDate { get; set; }
        public long? EnquiryMasterEnquiryAboutId { get; set; }
        public string EnquiryMasterRemarks { get; set; }
        public long? EnquiryMasterSalesManId { get; set; }
        public long? EnquiryMasterCurrencyId { get; set; }
        public long? EnquiryMasterLocationId { get; set; }
        public string EnquiryMasterCustomerReference { get; set; }
        public long? EnquiryMasterEnquiryStatusId { get; set; }
        public string EnquiryMasterConsultingEngineer { get; set; }
        public long? EnquiryMasterJobNo { get; set; }
        public long? EnquiryMasterBusineesTypeId { get; set; }
        public long? EnquiryMasterEnquiryVoucherNo { get; set; }
        public long? EnquiryMasterVesselId { get; set; }
        public string EnquiryMasterBuilder { get; set; }
        public string EnquiryMasterSerialNo { get; set; }
        public long? EnquiryMasterVendorId { get; set; }
        public long? EnquiryMasterTypeId { get; set; }
        public long? EnquiryMasterModelId { get; set; }
        public long? EnquiryMasterContactId { get; set; }
        public string EnquiryMasterContactName { get; set; }
        public string EnquiryMasterContactEmail { get; set; }
        public bool? EnquiryMasterDelStatus { get; set; }

          public List<EnquiryDetailsViewModel> CustomerEnquiryDetails { get; set; }
    }
}
