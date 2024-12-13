using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master.Interfaces
{
    public interface ITaxMasterService
    {
        public IEnumerable<TaxMaster> InsertTaxMaster(TaxMaster taxMaster);
        public IEnumerable<TaxMaster> UpdateTaxMaster(TaxMaster taxMaster);
        public IEnumerable<TaxMaster> DeleteTaxMaster(TaxMaster taxMaster);
        public IEnumerable<TaxMaster> GetAllTax();
        public IEnumerable<TaxMaster> GetAllTaxById(int id);
    }
}