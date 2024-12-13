using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class UserFile
    {
        public long UserId { get; set; }
        public string LogInId { get; set; }
        public string UserName { get; set; }
        public int? WorkGroupId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string Salt { get; set; }
        public bool? Deleted { get; set; }
        public bool? System { get; set; }
        public bool? Freeze { get; set; }
        [NotMapped]
        public bool? IsValid { get; set; }
        [NotMapped]
        public int? LocId { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public int? FinancialPeriodsFsno { get; set; }

    }

    [NotMapped]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedByIp { get; set; }
    }
}
