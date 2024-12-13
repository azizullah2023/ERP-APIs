using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class EnquiryDetailsViewModel
    {
        public int? EnquiryDetailsEnquiryDetailsId { get; set; }
        public string? EnquiryDetailsEnquiryDetailsEquiryId { get; set; }
        public int? EnquiryDetailsEnquiryDetailsSlno { get; set; }
        public int? EnquiryDetailsEnquiryDetailsItemId { get; set; }
        public int? EnquiryDetailsEnquiryDetailsUnitId { get; set; }
        public double? EnquiryDetailsEnquiryDetailsQty { get; set; }
        public string EnquiryDetailsEnquiryDetailsDescription { get; set; }
        public bool? EnquiryDetailsEnquiryDetailsIsEdited { get; set; }
        public string EnquiryDetailsEnquiryDetailsMaterialDescription { get; set; }
        public bool? EnquiryDetailsEnquiryDetailsDelStatus { get; set; }
    }
}
