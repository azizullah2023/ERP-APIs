using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Common
{
  public  class AddActivityLogViewModel
    {
        public string Page { get; set; }
        public string Section { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string OtherType { get; set; }
        public string Entity { get; set; }
        public string VpNo { get; set; }
        public string VpType { get; set; }
        public string Newvalue { get; set; }
        public string Oldvalue { get; set; }


        public int VpUserId { get; set; }

    }
}
