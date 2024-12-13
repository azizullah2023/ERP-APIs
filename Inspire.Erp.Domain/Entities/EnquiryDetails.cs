using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class EnquiryDetails
    {
        public int EnquiryDetailsEnquiryDetailsId { get; set; }
        public string EnquiryDetailsEnquiryDetailsEquiryId { get; set; }
        public int? EnquiryDetailsEnquiryDetailsSlno { get; set; }
        public int? EnquiryDetailsEnquiryDetailsItemId { get; set; }
        public int? EnquiryDetailsEnquiryDetailsUnitId { get; set; }
        public double? EnquiryDetailsEnquiryDetailsQty { get; set; }
        public string EnquiryDetailsEnquiryDetailsDescription { get; set; }
        public bool? EnquiryDetailsEnquiryDetailsIsEdited { get; set; }
        public string EnquiryDetailsEnquiryDetailsMaterialDescription { get; set; }
        public bool? EnquiryDetailsEnquiryDetailsDelStatus { get; set; }

        public virtual VouchersNumbers EnquiryDetailsEnquiryDetailsEquiry { get; set; }
    }
}
