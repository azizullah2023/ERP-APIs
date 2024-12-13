using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master.Interfaces
{
   
    public interface IUpload
    {
        public void beginUpload();
        public void InsertCity(List<ImportTimeSheet> importTimeSheets);

        public void rollbackUpload();
        public void dispose();
    }
}
