using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Infrastructure
{
    

    public class Email
    {
        public string MailServer { get; set; }
        public string MailServerUsername { get; set; }
        public string MailServerPassword { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string AdminSenderEmail { get; set; }
    }

    public class ApplicationSettings
    {
        public string ApplicationName { get; set; }
        public string ApplicationBasePath { get; set; }
        public string ConnectionString { get; set; }
        public Email Email { get; set; }
        public string ContentFilePath { get; set; }
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string Issuer { get; set; }

        public string Audience { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class AppSetting
    {
        public ApplicationSettings ApplicationSettings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
    }
}
