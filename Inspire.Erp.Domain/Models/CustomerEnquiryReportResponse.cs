using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class CustomerEnquiryReportResponse
    {
        public string Customer_Master_Customer_Name { get; set; }
        public string Location_Master_Location_Name { get; set; }
        public DateTime? Enquiry_Master_Enquiry_Date { get; set; }
        public string Enquiry_About_Enquiry_About { get; set; }
        public string Enquiry_Status_Enquiry_Status { get; set; }
        public string Currency_Master_Currency_Name { get; set; }
        public string Business_Type_Master_Business_Type_Name { get; set; }
        public string Enquiry_Master_Enquiry_ID { get; set; }
        public string Enquiry_Master_Remarks { get; set; }
        public string Enquiry_Master_Contact_Email { get; set; }

        public long? Enquiry_Master_Status_ID { get; set; }
        public long? Enquiry_Master_SaleMan_ID { get; set; }
    }
}
