using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using Inspire.Erp.Application.Master.Interfaces;
using System.Text;

namespace Inspire.Erp.Application.Master.Implementations
{
    
    public class ImportService: IUpload
    {
        private IRepository<ImportTimeSheet> importTimerepository;
        public ImportService(IRepository<ImportTimeSheet> _importTimerepository)
        {
            importTimerepository = _importTimerepository;

        }
        public void beginUpload()
        {
            this.importTimerepository.BeginTransaction();
        }
        public void InsertCity(List<ImportTimeSheet> importTimeSheets)
        {
            this.importTimerepository.InsertList(importTimeSheets);
        }
        public void rollbackUpload()
        {
            this.importTimerepository.TransactionRollback();
        }

        public void dispose()
        {
            this.importTimerepository.Dispose();
        }
    }
}
