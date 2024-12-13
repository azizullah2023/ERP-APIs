using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IItemListService
    {
        
        public IEnumerable<ItemListViewModel> GetAllItemListSearch(ItemFilterModel filteModle);
        public IEnumerable<ItemListViewModel> UpdateRateItemList(List<RateItemListrModel> dates);

    }
}