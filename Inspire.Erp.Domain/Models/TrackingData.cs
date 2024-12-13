using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models
{
    public class TrackingData
    {
     
       public string VPAction { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string VPType { get; set; }
        public DateTime? ChangeDt { get; set; }
        public DateTime? ChangeTime { get; set; }
        public string VPNo { get; set; }
    }
}
