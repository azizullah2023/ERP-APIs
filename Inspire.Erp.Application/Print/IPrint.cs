using Inspire.Erp.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Print
{
    public interface IPrint
    {
       void InsertPrintConncetionByService(PrinterConnection printerConnection);
       Task<List<PrinterConnection>> GetAllAvailablePrinterUsers(string spnameWithparams);
        Task<List<PrinterConnection>> GetAllActiveAvailablePrinterUsers(string spnameWithparams, SqlParameter[] sqlParameters = null);
    }
}
