using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ICurrencyMasterService
    {
        public IEnumerable<CurrencyMaster> InsertCurrency(CurrencyMaster currencyMaster);
        public IEnumerable<CurrencyMaster> UpdateCurrency(CurrencyMaster currencyMaster);
        public IEnumerable<CurrencyMaster> DeleteCurrency(CurrencyMaster currencyMaster);
        public IEnumerable<CurrencyMaster> GetAllCurrency();
        public IEnumerable<CurrencyMaster> GetAllCurrencyById(int id);

    }
}
