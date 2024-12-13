using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspire.Erp.WebReport.Models
{
    public class DbConnectionModel
    {
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DBName { get; set; }
        public string FolderName { get; set; }
    }
}