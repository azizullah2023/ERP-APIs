using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inspire.Erp.Domain.Entities
{
    public partial class EqnuiryAbout
    {
        [Key]
        public long EqnuiryAboutEnquiryAboutId { get; set; }
        public string EqnuiryAboutEnquiryAbout { get; set; }
        public bool? EqnuiryAboutEnquiryAboutStatus { get; set; }
        public bool? EqnuiryAboutEnquiryAbountDelStatus { get; set; }
    }
}
