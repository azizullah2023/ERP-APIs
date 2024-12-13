using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{

    public class User
    {
        public FinancialPeriodsViewModel? Financialperiod { get; set; }
        public LocationMasterViewModel? Location { get; set; }
        public string Password { get; set; }
        public bool? Rememberme { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public long? FinancialPeriodsFsno { get; set; }

    }

    public class PosUser
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime ApplicationDate { get; set; }
    }

}
