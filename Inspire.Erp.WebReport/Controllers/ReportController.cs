using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Inspire.Erp.WebReport.Models;
using Inspire.Erp.WebReport.ReportEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Inspire.Erp.WebReport.Controllers
{
    public class ReportController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetHelpForReportConfig()
        {
            var result = new ReportConfig
            {
                ReportPath = "/Reports",

                ConnectionServerName = "ServerName",
                ConnectionUserDB = "Db NAme",
                ConnectionPasswordDB = "Pasword",
                ConnectionDBName = "Connection DB Name",
                ConnectionTablePrefix = "Tabe Prefix",

                SelectionFormula = "Selection Formula",

                ExportPath = "Export path",
                ExportNamefile = "Output file name",
                ExportFormat = "pdf,txt,rpt"

            };
            return this.Ok(result);
        }


        [HttpPut]
        public IHttpActionResult ExportReport([FromBody] ReportConfig reportConfig)
        {
            string exportReturn = "";
            exportReturn = ExportDocumentHelper(reportConfig);

            var result = new ResultExport
            {
                ResultCode = exportReturn == "Success" ? 0 : 1,
                ResultMessage = exportReturn == "Success" ? "Success" : exportReturn
            };

            return this.Ok(result);
        }

        public string ExportDocumentHelper(ReportConfig reportConfig)
        {
            string exportPath = "";
            string errorMessage = "";

            try
            {
                CrystalReport report = new CrystalReport(serverName: reportConfig.ConnectionServerName,
                userID: reportConfig.ConnectionUserDB,
                password: reportConfig.ConnectionPasswordDB,
                databaseName: reportConfig.ConnectionDBName,
                prefixDatabaseTable: reportConfig.ConnectionTablePrefix);

                exportPath = report.ExportDocumentSimple(PathReport: reportConfig.ReportPath,
                SelectionFormula: reportConfig.SelectionFormula,
                PathExport: reportConfig.ExportPath,
                Namefile: reportConfig.ExportNamefile,
                ExportFormat: reportConfig.ExportFormat);
            }
            catch (Exception ex)
            {
                errorMessage = String.Format("WRP.API.Controllers.ExportReport: {0}", ex.Message);
            }

            return string.IsNullOrEmpty(errorMessage) ? "Success" : errorMessage;
        }





     
    }
}
