using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Sales.Interface
{
  public  interface ICustomerSalesOrderService
    {
        Task<Response<List<DropdownResponse>>> GetCustomerMasterDropdown();
     Task<Response<List<DropdownResponse>>> GetLocationMasterDropdown();
         Task<Response<List<DropdownResponse>>> GetJobMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetDepartmentMasterDropdown();
         Task<Response<List<DropdownResponse>>> GetCurrencyMasterDropdown();
        Task<Response<List<DropdownResponse>>> GetSalesManMasterDropdown();
        Task<Response<GridWrapperResponse<List<GetSalesItemMasterResponse>>>> GetItemMasterList();
        Task<Response<bool>> AddEditCustomerOrder(AddEditCustomerSalesOrderResponse model);
        Task<Response<List<DropdownResponse>>> GetUnitMasterDropdown();
        Task<Response<GridWrapperResponse<List<GetCustomerSalesOrderListResponse>>>> GetCustomerOrdersList(GenericGridViewModel model);
        Task<Response<GetCustomerSalesOrderListResponse>> GetSpecificOrder(int id = 0);
    }
}
