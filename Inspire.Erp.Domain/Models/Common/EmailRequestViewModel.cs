using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Common
{
    public class EmailRequestViewModel
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public List<string> Emails { get; set; }
        public string Body { get; set; }
    }
}
