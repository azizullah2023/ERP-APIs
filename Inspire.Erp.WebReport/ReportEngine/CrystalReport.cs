using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Inspire.Erp.WebReport.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;

namespace Inspire.Erp.WebReport.ReportEngine
{
	/// <summary>
	/// 
	/// </summary>
	public class CrystalReport
	{
		// Class Variables
		protected string csServerName, csUserID, csPassword, csDatabaseName;
		protected string csPrefixDatabaseTable;
		protected DataTable csSubReportModify;
		protected bool initParametersToEmptyString;
		protected bool csForceDbPropertiesChange;

		public bool ForceDbPropertiesChange
		{
			get
			{
				return csForceDbPropertiesChange;
			}
			set
			{
				csForceDbPropertiesChange = value;
			}
		}

		
		public bool InitParametersToEmptyString
		{
			set
			{
				initParametersToEmptyString = value;

			}
			get
			{
				return initParametersToEmptyString;
			}
		}

		
		public CrystalReport()
		{
           
		}

	
		public CrystalReport(string serverName, string userID, string password, string databaseName, string prefixDatabaseTable)
		{
			
			SetConnectionInfo(serverName, userID, password, databaseName, prefixDatabaseTable);
			SetDefaults();
		}

		
		public void SetConnectionInfo(string serverName, string userID, string password, string databaseName, string prefixDatabaseTable)
		{
			//
			csServerName = serverName;
			csUserID = userID;
			csPassword = password;
			csDatabaseName = databaseName;
			csPrefixDatabaseTable = prefixDatabaseTable;
		}
	
		public void SetSubReportModify(DataTable subReportModify)
		{
			//
			if (subReportModify != null)
			{
				if (subReportModify.Rows.Count == 0)
				{
					subReportModify = null;
				}
			}
			csSubReportModify = subReportModify;
		}

		
		protected void SetDefaults()
		{
			initParametersToEmptyString = false;
			csForceDbPropertiesChange = false;
		}

	
		public ReportDocument PrepareReport(string reportPath)
		{
			ReportDocument crReportDocument;
			crReportDocument = new ReportDocument();
			//
			try
			{
				crReportDocument.Load(reportPath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
				SetConnectionDB(crReportDocument);
			}
			catch (Exception ex)
			{
				throw (new Exception("CrystalReport.PrepareReport: " + ex.Message, ex));
			}
			return crReportDocument;
		}

	
		protected void SetConnectionDB(ReportDocument ardReportDocument)
		{
			ReportDocument crReportDocument;
			ConnectionInfo crConnectionInfo;
			Database crDatabase;
			Tables crTables;

			string suffixDatabaseTable;
		
			crReportDocument = ardReportDocument;

			try
			{

				crDatabase = crReportDocument.Database;
				crTables = crDatabase.Tables;
				crConnectionInfo = new ConnectionInfo();
				crConnectionInfo.AllowCustomConnection = true;
				crConnectionInfo.ServerName = csServerName;
				if (csDatabaseName != "")
				{
					crConnectionInfo.DatabaseName = csDatabaseName;
				}
				crConnectionInfo.UserID = csUserID;
				crConnectionInfo.Password = csPassword;

				if (csForceDbPropertiesChange)
				{
					ApplyLoginInfo(crReportDocument);
				}
				else
				{
					foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crReportDocument.Database.Tables)
					{
						TableLogOnInfo crTableLogOnInfo = aTable.LogOnInfo;
						crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
						aTable.ApplyLogOnInfo(crTableLogOnInfo);

						if (csPrefixDatabaseTable != "")
						{
							suffixDatabaseTable = aTable.Location.Substring(aTable.Location.LastIndexOf(".") + 1);
							aTable.Location = csPrefixDatabaseTable + suffixDatabaseTable;
						}
					}
				}

			
				SetConnectionForSubReport(crReportDocument);
			}
			catch (Exception ex)
			{
				throw (new Exception("CrystalReport.SetConnectionDB: " + ex.Message, ex));
			}
		}

	
		private void ApplyLoginInfo(ReportDocument document)
		{
			TableLogOnInfo info = null;
			try
			{
			
				info = new TableLogOnInfo();
				info.ConnectionInfo.AllowCustomConnection = true;
				info.ConnectionInfo.ServerName = csServerName;
				info.ConnectionInfo.DatabaseName = csDatabaseName;
				info.ConnectionInfo.Password = csPassword;
				info.ConnectionInfo.UserID = csUserID;


				document.SetDatabaseLogon(info.ConnectionInfo.UserID, info.ConnectionInfo.Password, info.ConnectionInfo.ServerName, info.ConnectionInfo.DatabaseName, false);

				foreach (CrystalDecisions.Shared.IConnectionInfo connection in document.DataSourceConnections)
				{
					connection.SetConnection(csServerName, csDatabaseName, false);
					connection.SetLogon(csUserID, csPassword);
					connection.LogonProperties.Set("Data Source", csServerName);
					connection.LogonProperties.Set("Initial Catalog", csDatabaseName);
				}

		              
				if (!document.IsSubreport)
				{

					foreach (ReportDocument rd in document.Subreports)
					{
						ApplyLoginInfo(rd);
					}
				}

				foreach (CrystalDecisions.CrystalReports.Engine.Table table in document.Database.Tables)
				{
					TableLogOnInfo tableLogOnInfo = table.LogOnInfo;

					tableLogOnInfo.ConnectionInfo = info.ConnectionInfo;
					table.ApplyLogOnInfo(tableLogOnInfo);
					
				}

				
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to apply login information to the report - " +
					ex.Message);
			}
		}


		
		public ReportDocument AddSelectionFormula(ReportDocument ardReportDocument, string selectionFormula)
		{
			ReportDocument crReportDocument;
			crReportDocument = ardReportDocument;
			if (selectionFormula != "" && selectionFormula != null)
			{
				crReportDocument.DataDefinition.RecordSelectionFormula = selectionFormula;
			}
			return crReportDocument;
		}

		
		public ReportDocument SetArrayParameterValue(ReportDocument ardReportDocument, DataTable parameterDataTable)
		{
			ReportDocument crReportDocument;
			crReportDocument = ardReportDocument;
			DataTable tmpParam;
			int count = 0;
			string currentName, currentValue, currentValueInit;
			if (initParametersToEmptyString == true)
			{
				currentValueInit = "";
				foreach (CrystalDecisions.Shared.ParameterField pf in crReportDocument.ParameterFields)
				{
					if (pf.ParameterValueType == ParameterValueKind.StringParameter)
					{
						ParameterDiscreteValue pdv = new ParameterDiscreteValue();
						pdv.Value = currentValueInit;
						crReportDocument.SetParameterValue(pf.Name, pdv);
					}
				}
			}
			if (parameterDataTable != null)
			{
				tmpParam = parameterDataTable;
				count = tmpParam.Rows.Count;
				for (int li = 0; li < count; li++)
				{
					currentName = tmpParam.Rows[li][0].ToString();
					currentValue = tmpParam.Rows[li][1].ToString();
					if (currentName != "" && currentName != null)
					{
						crReportDocument.SetParameterValue(currentName, currentValue);
					}
					else
					{
						break;
					}
				}
			}
			return crReportDocument;
		}

	
		protected void SetConnectionForSubReport(ReportDocument aReportDocument)
		{
			Sections crSections;
			ReportDocument crReportDocument, crSubreportDocument;
			SubreportObject crSubreportObject;
			ReportObjects crReportObjects;
			ConnectionInfo crConnectionInfo;
			Database crDatabase;
			Tables crTables;
			TableLogOnInfo crTableLogOnInfo;
			DataTable tmpSubReportModify;
			string tempPrefixDatabaseTable;
			string tempSubreportName;
			string curSrName, curServerName, curUserID, curPassword, curDatabaseName, curPrefixDatabaseTable;
			int count;

			crReportDocument = aReportDocument;
			tmpSubReportModify = csSubReportModify;
			tempPrefixDatabaseTable = csPrefixDatabaseTable;

		
			crSections = crReportDocument.ReportDefinition.Sections;
			
			foreach (Section crSection in crSections)
			{
				crReportObjects = crSection.ReportObjects;
				
				foreach (ReportObject crReportObject in crReportObjects)
				{
					if (crReportObject.Kind == ReportObjectKind.SubreportObject)
					{
						try
						{
							crSubreportObject = (SubreportObject)crReportObject;
					
							tempSubreportName = crSubreportObject.SubreportName;
							crSubreportDocument = crSubreportObject.OpenSubreport(tempSubreportName);
							crDatabase = crSubreportDocument.Database;
							crTables = crDatabase.Tables;
						
							foreach (CrystalDecisions.CrystalReports.Engine.Table aTable in crTables)
							{
								//
								crConnectionInfo = new ConnectionInfo();
								//
								if (tmpSubReportModify != null)
								{
									
									count = 0;
									count = tmpSubReportModify.Rows.Count;
									for (int li = 0; li < count; li++)
									{
										curSrName = tmpSubReportModify.Rows[li][0].ToString();
										curServerName = tmpSubReportModify.Rows[li][1].ToString();
										curUserID = tmpSubReportModify.Rows[li][2].ToString();
										curPassword = tmpSubReportModify.Rows[li][3].ToString();
										curDatabaseName = tmpSubReportModify.Rows[li][4].ToString();
										curPrefixDatabaseTable = tmpSubReportModify.Rows[li][5].ToString();
										if (tempSubreportName == curSrName)
										{
											crConnectionInfo.ServerName = curServerName;
											crConnectionInfo.UserID = curUserID;
											crConnectionInfo.Password = curPassword;
											if (curDatabaseName != "")
											{
												crConnectionInfo.DatabaseName = curDatabaseName;
											}
											tempPrefixDatabaseTable = curPrefixDatabaseTable;
											break;
										}
										else
										{
											crConnectionInfo.ServerName = csServerName;
											crConnectionInfo.UserID = csUserID;
											crConnectionInfo.Password = csPassword;
											if (csDatabaseName != "")
											{
												crConnectionInfo.DatabaseName = csDatabaseName;
											}
											tempPrefixDatabaseTable = csPrefixDatabaseTable;
										}
									}
								}
								else
								{
									crConnectionInfo.ServerName = csServerName;
									crConnectionInfo.UserID = csUserID;
									crConnectionInfo.Password = csPassword;
									if (csDatabaseName != "")
									{
										crConnectionInfo.DatabaseName = csDatabaseName;
									}
									tempPrefixDatabaseTable = csPrefixDatabaseTable;
								}
								
								crTableLogOnInfo = aTable.LogOnInfo;
								crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
								aTable.ApplyLogOnInfo(crTableLogOnInfo);
								if (tempPrefixDatabaseTable != "")
								{
									aTable.Location = tempPrefixDatabaseTable + aTable.Location.Substring(aTable.Location.LastIndexOf(".") + 1);
								}
							}
						}
						catch (Exception ex)
						{
							throw (new Exception("CrystalReport.SetConnectionForSubReport: " + ex.Message, ex));
						}
					}
				}
			}
		}

	
		public bool ExportDocument(ReportDocument ardReportDocument, ExportFormatType expFormatType,
			string tempDir, string identifier, ref string reportFileExport)
		{
			string lReportFileExport, extensionFile;
			ExportOptions expOptions;
			DiskFileDestinationOptions diskFileDestOptions;
			ReportDocument lReportDocument;
			ExportFormatType lExpFormatType;
			bool isOK;

			lReportFileExport = "";
			extensionFile = "";
			lExpFormatType = expFormatType;
			isOK = false;
			lReportDocument = ardReportDocument;
			if ((expFormatType == ExportFormatType.PortableDocFormat)
				|| (expFormatType == ExportFormatType.Excel)
				|| (expFormatType == ExportFormatType.ExcelRecord)
				|| (expFormatType == ExportFormatType.RichText)
				|| (expFormatType == ExportFormatType.Text)
				|| (expFormatType == ExportFormatType.CrystalReport)
				|| (expFormatType == ExportFormatType.HTML32)
				|| (expFormatType == ExportFormatType.HTML40)
				|| (expFormatType == ExportFormatType.WordForWindows)
				|| (expFormatType == ExportFormatType.EditableRTF)
				|| (expFormatType == ExportFormatType.TabSeperatedText)
				|| (expFormatType == ExportFormatType.CharacterSeparatedValues)
				)
			{
				// Export Format Options and Extension file
				switch (expFormatType)
				{
					case ExportFormatType.PortableDocFormat:
						//
						extensionFile = ".pdf";
						break;
					case ExportFormatType.Excel:
						//
						extensionFile = ".xls";
						break;
					case ExportFormatType.ExcelRecord:
						//
						extensionFile = ".xls";
						break;
					case ExportFormatType.RichText:
						//
						extensionFile = ".rtf";
						break;
					case ExportFormatType.Text:
						//
						extensionFile = ".txt";
						break;
					case ExportFormatType.CrystalReport:
						//
						extensionFile = ".rpt";
						break;
					case ExportFormatType.HTML32:
					case ExportFormatType.HTML40:

						//
						extensionFile = ".txt";
						break;
					case ExportFormatType.CharacterSeparatedValues:
						//
						extensionFile = ".csv";
						//
						CharacterSeparatedValuesFormatOptions lCsvOp = new CharacterSeparatedValuesFormatOptions();
						lCsvOp.PreserveDateFormatting = true;
						lCsvOp.PreserveNumberFormatting = true;
						lCsvOp.Delimiter = "";
						lCsvOp.SeparatorText = ";";
						lReportDocument.ExportOptions.FormatOptions = lCsvOp;
						break;
					default:
						//
						extensionFile = ".pdf";
						break;
				}
				// Export options
				lReportFileExport = tempDir + @"\" + identifier + extensionFile;
				diskFileDestOptions = new DiskFileDestinationOptions();
				diskFileDestOptions.DiskFileName = lReportFileExport;
				expOptions = lReportDocument.ExportOptions;
				expOptions.DestinationOptions = diskFileDestOptions;
				expOptions.ExportDestinationType = ExportDestinationType.DiskFile;
				expOptions.ExportFormatType = lExpFormatType;
				// Export
				//
				try
				{
					lReportDocument.Export();
					isOK = true;
				}
				catch (Exception ex)
				{
					throw (new Exception("CrystalReport.ExportDocument: " + ex.Message, ex));
				}
			}
			reportFileExport = lReportFileExport;

			return isOK;
		}

		public string ExportDocumentSimple(string PathReport, string SelectionFormula, string PathExport, string Namefile, string ExportFormat)
		{
			string retPathExport = "";
			ReportDocument reportDoc;
			ExportFormatType expFormatType;

	
			reportDoc = PrepareReport(PathReport);
			if (reportDoc.ParameterFields.Count > 0)
            {
				var dt = new DataTable();
				dt.Columns.Add("Name", typeof(string));
				dt.Columns.Add("Value", typeof(string));
				DataRow newRow = dt.NewRow();
				newRow["Name"] = "Company";
				newRow["Value"] = "Company";
				dt.Rows.Add(newRow);

				newRow = dt.NewRow();
				newRow["Name"] = "Add1";
				newRow["Value"] = "Add1";
				dt.Rows.Add(newRow);

				newRow = dt.NewRow();
				newRow["Name"] = "Add2";
				newRow["Value"] = "Add2";
				dt.Rows.Add(newRow);

				newRow = dt.NewRow();
				newRow["Name"] = "Email";
				newRow["Value"] = "Email";
				dt.Rows.Add(newRow);

				newRow = dt.NewRow();
				newRow["Name"] = "Heading";
				newRow["Value"] = "Heading";
				dt.Rows.Add(newRow);
				newRow = dt.NewRow();
				newRow["Name"] = "Company";
				newRow["Value"] = "Company";
				dt.Rows.Add(newRow);
				newRow = dt.NewRow();
				newRow["Name"] = "subHeading";
				newRow["Value"] = "subHeading";
				dt.Rows.Add(newRow);


				reportDoc = SetArrayParameterValue(reportDoc, dt);
			}
			reportDoc = AddSelectionFormula(reportDoc, SelectionFormula);

	
			switch (ExportFormat)
			{
				case "pdf":
					//
					expFormatType = ExportFormatType.PortableDocFormat;
					break;
				case "xls":
					//
					expFormatType = ExportFormatType.Excel;
					break;
				case "doc":
					//
					expFormatType = ExportFormatType.WordForWindows;
					break;
				case "rtf":
					//
					expFormatType = ExportFormatType.RichText;
					break;
				case "rte":
					//
					expFormatType = ExportFormatType.EditableRTF;
					break;
				case "xlr":
					//
					expFormatType = ExportFormatType.ExcelRecord;
					break;
				case "txt":
					//
					expFormatType = ExportFormatType.Text;
					break;
				case "ttx":
					//
					expFormatType = ExportFormatType.TabSeperatedText;
					break;
				case "csv":
					//
					expFormatType = ExportFormatType.CharacterSeparatedValues;
					break;
				case "rpt":
					//
					expFormatType = ExportFormatType.CrystalReport;
					break;
				default:
					//
					expFormatType = ExportFormatType.PortableDocFormat;
					break;
			}

	
			string uniqueIdentifier = Namefile;
			ExportDocument(reportDoc, expFormatType, PathExport, uniqueIdentifier, ref retPathExport);

		
			CrystalReport.CloseReportDocument(reportDoc);

			return retPathExport;
		}

	
		public ReportDocument ReportLocalizer(ReportDocument aReportDocument, DataTable aDt)
		{
			ReportDocument lReportDocument = aReportDocument;
			DataTable tmpDt = aDt;

			try
			{
				if (tmpDt != null)
				{
					int count = tmpDt.Rows.Count;
					for (int li = 0; li < count; li++)
					{
						string key = tmpDt.Rows[li][0].ToString();
						string value = tmpDt.Rows[li][1].ToString();
						lReportDocument = AssignKeysToReport(lReportDocument, key, value);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(String.Format("CrystalReport.ReportLocalizer: {0}", ex.Message), ex);
			}

			return lReportDocument;
		}

		private ReportDocument AssignKeysToReport(ReportDocument myReportDocument, string key, string value)
		{
			ReportDocument lReportDocument = myReportDocument;

			string reportObjectName = key;
			string reportObjectValue = value;

			
			try
			{
				ReportObject reportObject = lReportDocument.ReportDefinition.ReportObjects[reportObjectName];
				if (reportObject != null)
				{
					TextObject myLabel = (TextObject)reportObject;
					myLabel.Text = reportObjectValue;
				}
			}
			catch (Exception ex)
			{
			
				string error = ex.Message;
			}

		
			try
			{
				FormulaFieldDefinition reportObject = lReportDocument.DataDefinition.FormulaFields[reportObjectName];
				if (reportObject != null)
				{
					FormulaFieldDefinition myLabel = (FormulaFieldDefinition)reportObject;
					myLabel.Text = reportObjectValue;
				}
			}
			catch (Exception ex)
			{
				
				string error = ex.Message;
			}

			
			Sections crSections;
			ReportDocument crReportDocument, crSubreportDocument;
			SubreportObject crSubreportObject;
			ReportObjects crReportObjects;
			string tempSubreportName;

			string subReportObjectName = key;
			string subRreportObjectValue = value;

			crReportDocument = lReportDocument;
			crSections = crReportDocument.ReportDefinition.Sections;

	
			foreach (Section crSection in crSections)
			{
				crReportObjects = crSection.ReportObjects;
	
				foreach (ReportObject crReportObject in crReportObjects)
				{
					if (crReportObject.Kind == ReportObjectKind.SubreportObject)
					{
						crSubreportObject = (SubreportObject)crReportObject;
						
						tempSubreportName = crSubreportObject.SubreportName;
						crSubreportDocument = crSubreportObject.OpenSubreport(tempSubreportName);

				
						try
						{
							ReportObject subreportObject = crSubreportDocument.ReportDefinition.ReportObjects[subReportObjectName];
							if (subreportObject != null)
							{
								TextObject mySubLabel = (TextObject)subreportObject;
								mySubLabel.Text = subRreportObjectValue;
							}
						}
						catch (Exception ex)
						{
						
							string error = ex.Message;
						}

				
						try
						{
							FormulaFieldDefinition subreportObject = crSubreportDocument.DataDefinition.FormulaFields[subReportObjectName];
							if (subreportObject != null)
							{
								FormulaFieldDefinition mySubLabel = (FormulaFieldDefinition)subreportObject;
								mySubLabel.Text = subRreportObjectValue;
							}
						}
						catch (Exception ex)
						{
					
							string error = ex.Message;
						}
					}
				}
			}

			return lReportDocument;
		}


	
		public static void DirectPrint(ReportDocument aReportDocument, string aPrinterName)
		{
			ReportDocument myReportDocument = aReportDocument;
			string myPrinterName = aPrinterName;

			try
			{
				//
				if (myPrinterName != "")
				{
					myReportDocument.PrintOptions.PrinterName = myPrinterName;
				}
				//
				myReportDocument.PrintToPrinter(1, false, 0, 0);

			}
			catch (Exception ex)
			{
				throw (new Exception(String.Format("CrystalReport.DirectPrint: {0}", ex.Message), ex));
			}

		}

	
		public static void CloseReportDocument(ReportDocument aReportDocument)
		{
			ReportDocument myReportDocument = aReportDocument;

			try
			{
				if (myReportDocument != null)
				{
					if (myReportDocument.IsLoaded == true)
					{
						myReportDocument.Close();
						myReportDocument.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				throw (new Exception(String.Format("CrystalReport.CloseReportDocument: {0}", ex.Message), ex));
			}

		}



		
		public HttpResponseMessage RenderReport(string PathReport, string SelectionFormula, string Namefile, List<Models.ReportParameters> reportParameters, DbConnectionModel dbSettings )
		{
			ReportDocument reportDoc;
			SetConnectionInfo(dbSettings.ServerName, dbSettings.UserName, dbSettings.Password, dbSettings.DBName, "");
			SetDefaults();

			reportDoc = PrepareReport(PathReport);
			if (reportDoc.ParameterFields.Count > 0 && reportParameters.Count > 0)
			{
                var dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Value", typeof(string));
                foreach (Models.ReportParameters item in reportParameters)
				{
                    DataRow newRow = dt.NewRow();
                    newRow["Name"] = item.Key;
                    newRow["Value"] = item.Value;
                    dt.Rows.Add(newRow);
                }
				reportDoc = SetArrayParameterValue(reportDoc, dt);
			}
			reportDoc = AddSelectionFormula(reportDoc, SelectionFormula);

			MemoryStream ms = new MemoryStream();
			using (var stream = reportDoc.ExportToStream(ExportFormatType.PortableDocFormat))
			{
				stream.CopyTo(ms);
			}

			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(ms.ToArray())
			};
			result.Content.Headers.ContentDisposition =
				new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
				{
					FileName = Namefile
				};
			result.Content.Headers.ContentType =
				new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
			return result;
		}


		public DbConnectionModel getDbSettingsFromJSonFle(string filePath)
		{
            DbConnectionModel dbConnectionModel = new DbConnectionModel();
            if (File.Exists(filePath))
            {
                dbConnectionModel = JsonConvert.DeserializeObject<DbConnectionModel>(File.ReadAllText(filePath));
            }
			return dbConnectionModel;
        }
       
    }
}