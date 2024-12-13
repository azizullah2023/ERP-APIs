using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IPriceLevelMasterService
    {
        public IEnumerable<PriceLevelMaster> InsertPriceLevel(PriceLevelMaster priceLevelMaster);
        public IEnumerable<PriceLevelMaster> UpdatePriceLevel(PriceLevelMaster priceLevelMaster);
        public IEnumerable<PriceLevelMaster> DeletePriceLevel(PriceLevelMaster priceLevelMaster);
        public IEnumerable<PriceLevelMaster> GetAllPriceLevel();
        public IEnumerable<PriceLevelMaster> GetAllPriceLevelById(int id);
    }
}