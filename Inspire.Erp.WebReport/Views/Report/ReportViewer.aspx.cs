using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace Inspire.Erp.WebReport.Views.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        public bool PrintWithAttachment;
        public string FormatName { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //AppSettings app = new AppSettings();

            //if (Request.QueryString["ReportName"] != null)
            //{
            //    //string prefix = Request.QueryString["Prefix"].ToString(); 
            //    string path = "/report/" + Request.QueryString["ReportName"].ToString() + ".rpt";

            //    if (File.Exists(Server.MapPath(path)))
            //    {
            //        using (CrystalReportSource source = new CrystalReportSource())
            //        {
            //            CrystalDecisions.Web.Report report = new CrystalDecisions.Web.Report
            //            {
            //                FileName = Server.MapPath(path)
            //            };
            //            source.Report = report;
            //            ConnectionInfo info = new ConnectionInfo
            //            {

            //                //ServerName = app.GetAppSettings(prefix + "Server"),
            //                //DatabaseName = app.GetAppSettings(prefix + "Database"),
            //                //UserID = app.GetAppSettings(prefix + "User"),
            //                //Password = app.GetAppSettings(prefix + "Password"),


            //                ServerName = ".\\SQLEXPRESS",
            //                DatabaseName = "saastrek",
            //                UserID = "sa",
            //                Password = "741#code",
            //            };
            //            foreach (Table table in source.ReportDocument.Database.Tables)
            //            {
            //                TableLogOnInfo logOnInfo = table.LogOnInfo;
            //                logOnInfo.ConnectionInfo = info;
            //                table.ApplyLogOnInfo(logOnInfo);
            //            }
            //            source.ID = path;
            //            FormatName = string.Empty;
            //            string str6 = "";
            //            string val = "";
            //            foreach (string str8 in from p in Request.QueryString.AllKeys
            //                                    where p != "ReportName"
            //                                    select p)
            //            {
            //                switch (str8)
            //                {
            //                    case "format":
            //                        FormatName = Request.QueryString[str8].ToString();
            //                        Response.Write(FormatName);
            //                        break;

            //                    case "printer":
            //                        {
            //                            str6 = Request.QueryString[str8].ToString();
            //                            continue;
            //                        }
            //                    case "Proc":
            //                        {
            //                            //val = ToLettre1(base.Request.QueryString[str8].ToString());
            //                            source.ReportDocument.SetParameterValue("vString", val);
            //                            continue;
            //                        }
            //                    case "SelectionFormula":
            //                        {
            //                            if (Request.QueryString[str8] != "")
            //                                source.ReportDocument.DataDefinition.RecordSelectionFormula = Request.QueryString[str8].ToString();
            //                            continue;
            //                        }
            //                }
            //                if (Request.QueryString[str8].ToString().IndexOf(",") != -1)
            //                {
            //                    char[] separator = new char[] { ',' };
            //                    string[] strArray = Request.QueryString[str8].ToString().Split(separator);
            //                    ParameterValues currentValues = new ParameterValues();
            //                    foreach (string str9 in strArray)
            //                    {
            //                        currentValues.AddValue(str9);
            //                    }
            //                    source.ReportDocument.DataDefinition.ParameterFields[str8].ApplyCurrentValues(currentValues);
            //                }
            //                else if (source.ReportDocument.ParameterFields.Find(str8, "") != null)
            //                {
            //                    source.ReportDocument.SetParameterValue(str8, Request.QueryString[str8].ToString());
            //                    PrintWithAttachment = true;
            //                }
            //            }
            //            try
            //            {
            //                if (FormatName == "Excel")
            //                {
            //                    source.ReportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "Export");
            //                }
            //                else
            //                {
            //                    MemoryStream stream = new MemoryStream();
            //                    stream = source.ReportDocument.ExportToStream(ExportFormatType.PortableDocFormat) as MemoryStream;
            //                    source.ReportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            //                }
            //            }
            //            catch (Exception exception)
            //            {
            //                Response.Write(exception.Message);
            //            }
            //            finally
            //            {
            //                Response.Flush();
            //                Response.End();
            //                Response.Close();
            //            }
            //        }
            //    }
            //}
        }
    }
}