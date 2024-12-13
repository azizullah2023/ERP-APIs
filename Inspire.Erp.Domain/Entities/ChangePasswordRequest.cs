using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public class ChangePasswordRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
