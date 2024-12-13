using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class ChangePasswordResponse
    {
        public long  Userid { get; set; }
        public string NewPassword { get; set; }
    }
}
