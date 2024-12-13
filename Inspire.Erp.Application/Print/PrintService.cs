using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Print
{
    public class PrintService : IPrint
    {
        private IRepository<PrinterConnection> repository;
        public PrintService(IRepository<PrinterConnection> _repository)
        {
            repository = _repository;
        }

        public void InsertPrintConncetionByService(PrinterConnection printerConnection)
        {
            var data = repository.GetAsQueryable().FirstOrDefault(x => x.SystemUser == printerConnection.SystemUser);
            if (data != null)
            {
                data.SytemConnectionId = printerConnection.SytemConnectionId;
                this.repository.Update(data);
            }
            else
            {
                repository.Insert(printerConnection);
            }
            return;
        }

        public async Task<List<PrinterConnection>> GetAllAvailablePrinterUsers(string spnameWithparams)
        {
             return await repository.ExecuteSpReaderWithNoParam(spnameWithparams);
              
            
        }

        public async Task<List<PrinterConnection>> GetAllActiveAvailablePrinterUsers(string spnameWithparams, SqlParameter[] sqlParameters = null)
        {
            return await repository.ExecuteSpReader(spnameWithparams, sqlParameters);


        }

    }
}
