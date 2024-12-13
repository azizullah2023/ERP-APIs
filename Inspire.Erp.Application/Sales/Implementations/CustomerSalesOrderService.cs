using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Sales.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Sales.Implementation
{
    public class CustomerSalesOrderService : ICustomerSalesOrderService
    {
        private readonly IRepository<ItemMaster> _itemMaster;
        private readonly IRepository<CustomerMaster> _customerMaster;
        private readonly IRepository<LocationMaster> _locationMaster;
        private readonly IRepository<DepartmentMaster> _departmentMaster;
        private readonly IRepository<CurrencyMaster> _currencyMaster;
        private readonly IRepository<CustomerOrder> _customerOrder;
        private readonly IRepository<VouchersNumbers> _voucherNumber;
        private readonly IRepository<JobMaster> _jobMaster;
        private readonly IRepository<SalesManMaster> _salesManMaster;
        private readonly IRepository<CustomerOrderDetails> _customerOrderDetails;
        private readonly IRepository<UnitMaster> _unitMaster;
        private readonly IUtilityService _utilityService;
        public CustomerSalesOrderService(IRepository<ItemMaster> itemMaster,
            IRepository<LocationMaster> locationMaster, IRepository<CustomerOrder> customerOrder,
             IRepository<CustomerMaster> customerMaster, IRepository<JobMaster> jobMaster, IRepository<DepartmentMaster> departmentMaster,
            IRepository<CustomerOrderDetails> customerOrderDetails, IRepository<SalesManMaster> salesManMaster, IUtilityService utilityService,
            IRepository<CurrencyMaster> currencyMaster, IRepository<VouchersNumbers> voucherNumber, IRepository<UnitMaster> unitMaster)
        {
            _itemMaster = itemMaster;
            _customerOrder = customerOrder;
            _customerOrderDetails = customerOrderDetails;
            _salesManMaster = salesManMaster;
            _departmentMaster = departmentMaster;
            _unitMaster = unitMaster;
            _jobMaster = jobMaster;
            _voucherNumber = voucherNumber;
            _customerMaster = customerMaster;
            _locationMaster = locationMaster;
            _currencyMaster = currencyMaster;
        }
        public async Task<Response<List<DropdownResponse>>> GetCustomerMasterDropdown()
        {
            try
            {
                var response = await _customerMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.CustomerMasterCustomerNo,
                    Name = x.CustomerMasterCustomerName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetLocationMasterDropdown()
        {
            try
            {
                var response = await _locationMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.LocationMasterLocationId != null ? Convert.ToInt32(x.LocationMasterLocationId) : 0,
                    Name = x.LocationMasterLocationName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetJobMasterDropdown()
        {
            try
            {
                var response = await _jobMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.JobMasterJobId != null ? Convert.ToInt32(x.JobMasterJobId) : 0,
                    Name = x.JobMasterJobName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetDepartmentMasterDropdown()
        {
            try
            {
                var response = await _departmentMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.DepartmentMasterDepartmentId != null ? Convert.ToInt32(x.DepartmentMasterDepartmentId) : 0,
                    Name = x.DepartmentMasterDepartmentName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetCurrencyMasterDropdown()
        {
            try
            {
                var response = await _currencyMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.CurrencyMasterCurrencyId,
                    Name = x.CurrencyMasterCurrencyName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetSalesManMasterDropdown()
        {
            try
            {
                var response = await _salesManMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.SalesManMasterSalesManId != null ? Convert.ToInt32(x.SalesManMasterSalesManId) : 0,
                    Name = x.SalesManMasterSalesManName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetUnitMasterDropdown()
        {
            try
            {
                var response = await _unitMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.UnitMasterUnitId,
                    Name = x.UnitMasterUnitShortName
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<GetSalesItemMasterResponse>>>> GetItemMasterList()
        {
            try
            {
                var response = await _itemMaster.ListSelectAsync(x => 1 == 1, x => new GetSalesItemMasterResponse
                {
                    ItemMasterItemId = x.ItemMasterItemId != null? Convert.ToInt32(x.ItemMasterItemId):0,
                    ItemMasterItemName = x.ItemMasterItemName,
                    ItemMasterAccountNo = x.ItemMasterAccountNo,
                    ItemMasterBatchCode = x.ItemMasterBatchCode,
                    ItemMasterLocationId = x.ItemMasterLocationId,
                    ItemMasterUnitPrice = x.ItemMasterUnitPrice,
                    ItemMasterMaterialCode = x.ItemMasterMaterialCode,
                    ItemMasterPartNo=x.ItemMasterPartNo
                });
                var result = new GridWrapperResponse<List<GetSalesItemMasterResponse>>();
                result.Data = response;
                result.Total = 0;
                return Response<GridWrapperResponse<List<GetSalesItemMasterResponse>>>.Success(result, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetSalesItemMasterResponse>>>.Fail(new GridWrapperResponse<List<GetSalesItemMasterResponse>>(), ex.Message);
            }
        }

        public async Task<Response<bool>> AddEditCustomerOrder(AddEditCustomerSalesOrderResponse model)
        {
            try
            {
                string message = null;
                if (model.CustomerOrderId == null || model.CustomerOrderId == 0)
                {
                    
                        message = await AddCustomerOrder(model);
                    
                }
                else
                {
                    
                        message = await EditCustomerOrder(model);
                }
                if (message == null)
                {
                    return Response<bool>.Fail(false, "Something went wrong. Please try again later.");
                }
                return Response<bool>.Success(true, "Changes Saved Successfully.");
            }
            catch (Exception ex)
            {

                return Response<bool>.Fail(false, ex.Message);
            }
        }
        private async Task<string> AddCustomerOrder(AddEditCustomerSalesOrderResponse model)
        {
            try
            {
                var maxid = _voucherNumber.GetAsQueryable();
                Random random = new Random();
                int Orderid = random.Next();
                int OrderDetailid = random.Next();
                //var a= maxid.Max(x => Math.Round(Convert.ToDecimal(x.VouchersNumbersVsno)))+1;
                CustomerOrder order = new CustomerOrder()
                {
                    CustomerOrderCurrencyId = model.CustomerOrderCurrencyId,
                    CustomerOrderCustomerId = model.CustomerOrderCustomerId,
                    CustomerOrderDate = model.CustomerOrderDate,
                    CustomerOrderDelStatus = false,
                    CustomerOrderDescription = model.CustomerOrderDescription,
                    CustomerOrderDiscount = model.CustomerOrderDiscount,
                    CustomerOrderDiscountPercentage = model.CustomerOrderDiscountPercentage,
                    CustomerOrderFsno = model.CustomerOrderFsno,
                    CustomerOrderId = Orderid,
                    CustomerOrderLpoDate=model.CustomerOrderLpoDate,
                    CustomerOrderLpoNo=model.CustomerOrderLpoNo,
                    CustomerOrderLocationId = model.CustomerOrderLocationId,
                    CustomerOrderNetAmount = model.CustomerOrderNetAmount,
                    CustomerOrderNo = Orderid.ToString(),
                    CustomerOrderTotalAmount = model.CustomerOrderTotalAmount,
                };
                _customerOrder.Insert(order);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Customer Sales Order",
                    Section = "Add Customer Sales Order",
                    Entity = "Customer Sales Order",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion

                _customerOrder.SaveChangesAsync();
                List<CustomerOrderDetails> orderDetails = new List<CustomerOrderDetails>();
                orderDetails.AddRange(model.CustomerOrderDetails.Select(x => new CustomerOrderDetails
                {
                    CustomerOrderDetailsAmount = x.CustomerOrderDetailsAmount,
                    CustomerOrderDetailsDelStatus = false,
                    CustomerOrderDetailsDescription = x.CustomerOrderDetailsDescription,
                    CustomerOrderDetailsId = OrderDetailid,
                    CustomerOrderDetailsMaterialDescription = x.CustomerOrderDetailsMaterialDescription,
                    CustomerOrderDetailsRate = x.CustomerOrderDetailsRate,
                    CustomerOrderDetailsSno = Orderid,
                    CustomerOrderDetailsQuantity = x.CustomerOrderDetailsQuantity,
                    CustomerOrderDetailsUnitId = _unitMaster.GetAsQueryable().FirstOrDefault(c => c.UnitMasterUnitShortName == x.CustomerOrderDetailsUnitName).UnitMasterUnitId,
                 //   CustomerOrderDetailsMaterialId = _itemMaster.GetAsQueryable().FirstOrDefault(c => c.ItemMasterItemName == x.CustomerOrderDetailsMaterialName).ItemMasterItemId,
                    CustomerOrderDetailsFcAmount = x.CustomerOrderDetailsFcAmount
                }));
                _customerOrderDetails.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel logs = new AddActivityLogViewModel()
                {
                    Page = "Customer Sales Order",
                    Section = "Add Customer Sales Order Detail",
                    Entity = "Customer Sales Order Detail",

                };
                await _utilityService.AddUserTrackingLog(logs);
                #endregion
                _customerOrderDetails.SaveChangesAsync();
                return "Record Added successfully.";
            }
            catch (Exception)
            {
                return null;
            }
        }
        private async Task<string> EditCustomerOrder(AddEditCustomerSalesOrderResponse model)
        {
            try
            {
                Random random = new Random();
                int OrderDetailid = random.Next();
                var orders = _customerOrder.GetAsQueryable().FirstOrDefault(x => x.CustomerOrderId == model.CustomerOrderId);
                orders.CustomerOrderCurrencyId = model.CustomerOrderCurrencyId;
                orders.CustomerOrderCustomerId = model.CustomerOrderCustomerId;
                orders.CustomerOrderDate = DateTime.Now;
                orders.CustomerOrderDelStatus = false;
                orders.CustomerOrderDescription = model.CustomerOrderDescription;
                orders.CustomerOrderDiscount = model.CustomerOrderDiscount;
                orders.CustomerOrderDiscountPercentage = model.CustomerOrderDiscountPercentage;
                orders.CustomerOrderFsno = model.CustomerOrderFsno;
                orders.CustomerOrderLocationId = model.CustomerOrderLocationId;
                orders.CustomerOrderNetAmount = model.CustomerOrderNetAmount;
                orders.CustomerOrderTotalAmount = model.CustomerOrderTotalAmount;
                _customerOrder.Update(orders);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel logs = new AddActivityLogViewModel()
                {
                    Page = "Customer Sales Order",
                    Section = "Update Customer Sales Order Detail",
                    Entity = "Customer Sales Order Detail",

                };
                await _utilityService.AddUserTrackingLog(logs);
                #endregion
                _customerOrder.SaveChangesAsync();

                var listOrdersDetails = _customerOrderDetails.GetAsQueryable().Where(x => x.CustomerOrderDetailsSno == model.CustomerOrderId).ToList();
                _customerOrderDetails.DeleteList(listOrdersDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Deletelogs = new AddActivityLogViewModel()
                {
                    Page = "Customer Sales Order",
                    Section = "Delete Customer Sales Order ",
                    Entity = "Customer Sales Order ",

                };
                await _utilityService.AddUserTrackingLog(Deletelogs);
                #endregion
                _customerOrderDetails.SaveChangesAsync();
                List<CustomerOrderDetails> orderDetails = new List<CustomerOrderDetails>();
                orderDetails.AddRange(model.CustomerOrderDetails.Select(x => new CustomerOrderDetails
                {
                    CustomerOrderDetailsAmount = x.CustomerOrderDetailsAmount,
                    CustomerOrderDetailsDelStatus = false,
                    CustomerOrderDetailsDescription = x.CustomerOrderDetailsDescription,
                    CustomerOrderDetailsId = OrderDetailid,
                    CustomerOrderDetailsMaterialDescription = x.CustomerOrderDetailsMaterialDescription,
                    CustomerOrderDetailsRate = x.CustomerOrderDetailsRate,
                    CustomerOrderDetailsSno = model.CustomerOrderId,
                    CustomerOrderDetailsQuantity = x.CustomerOrderDetailsQuantity,
                    CustomerOrderDetailsUnitId = _unitMaster.GetAsQueryable().FirstOrDefault(c => c.UnitMasterUnitShortName == x.CustomerOrderDetailsUnitName).UnitMasterUnitId,
                    CustomerOrderDetailsMaterialId = x.CustomerOrderDetailsMaterialId,
                    CustomerOrderDetailsFcAmount = x.CustomerOrderDetailsFcAmount
                }));
                _customerOrderDetails.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel logss = new AddActivityLogViewModel()
                {
                    Page = "Customer Sales Order",
                    Section = "Add Customer Sales Order Detail",
                    Entity = "Customer Sales Order Detail",

                };
                await _utilityService.AddUserTrackingLog(logss);
                #endregion
                _customerOrderDetails.SaveChangesAsync();

                return "Record Updated successfully.";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response<GridWrapperResponse<List<GetCustomerSalesOrderListResponse>>>> GetCustomerOrdersList(GenericGridViewModel model)
        {
            try
            {
                List<GetCustomerSalesOrderListResponse> response = new List<GetCustomerSalesOrderListResponse>();
                var orders = await _customerOrder.GetAsQueryable().Where(x => x.CustomerOrderDelStatus != null ? !Convert.ToBoolean(x.CustomerOrderDelStatus) : 1 == 1).Skip(model.Skip).Take(model.Take).ToListAsync();
                response.AddRange(orders.Select(x => new GetCustomerSalesOrderListResponse
                {
                    CustomerOrderCurrencyId = x.CustomerOrderCurrencyId,
                    CustomerOrderCustomerId = x.CustomerOrderCustomerId,
                    CustomerOrderDate = DateTime.Now,
                    CustomerOrderDescription = x.CustomerOrderDescription,
                    CustomerOrderDiscount = x.CustomerOrderDiscount,
                    CustomerOrderDiscountPercentage = x.CustomerOrderDiscountPercentage,
                    CustomerOrderFsno = x.CustomerOrderFsno,
                    CustomerOrderId = x.CustomerOrderId,
                    CustomerOrderLocationId = x.CustomerOrderLocationId,
                    CustomerOrderNetAmount = x.CustomerOrderNetAmount,
                    CustomerOrderNo = x.CustomerOrderNo,
                    CustomerOrderTotalAmount = x.CustomerOrderTotalAmount,
                }).ToList());
                var gridResponse = new GridWrapperResponse<List<GetCustomerSalesOrderListResponse>>();
                gridResponse.Data = response;
                gridResponse.Total = 0;
                return Response<GridWrapperResponse<List<GetCustomerSalesOrderListResponse>>>.Success(gridResponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetCustomerSalesOrderListResponse>>>.Fail(new GridWrapperResponse<List<GetCustomerSalesOrderListResponse>>(), ex.Message);
            }
        }
        public async Task<Response<GetCustomerSalesOrderListResponse>> GetSpecificOrder(int id = 0)
        {
            try
            {
                GetCustomerSalesOrderListResponse model = new GetCustomerSalesOrderListResponse();
                await Task.Run(() =>
                {
                var orders = _customerOrder.GetAsQueryable().FirstOrDefault(x => x.CustomerOrderId == id);
                model.CustomerOrderCurrencyId = orders.CustomerOrderCurrencyId;
                model.CustomerOrderCustomerId = orders.CustomerOrderCustomerId;
                model.CustomerOrderDate = orders.CustomerOrderDate;
                model.CustomerOrderDescription = orders.CustomerOrderDescription;
                model.CustomerOrderDiscount = orders.CustomerOrderDiscount;
                model.CustomerOrderDiscountPercentage = orders.CustomerOrderDiscountPercentage;
                model.CustomerOrderFsno = orders.CustomerOrderFsno;
                model.CustomerOrderId = orders.CustomerOrderId;
                model.CustomerOrderLocationId = orders.CustomerOrderLocationId;
                model.CustomerOrderLpoNo = orders.CustomerOrderLpoNo;
                model.CustomerOrderLpoDate = orders.CustomerOrderLpoDate;
                model.CustomerOrderNetAmount = orders.CustomerOrderNetAmount;
                model.CustomerOrderNo = orders.CustomerOrderNo;
                model.CustomerOrderTermsAndConditions = orders.CustomerOrderTermsAndConditions;
                model.CustomerOrderTotalAmount = orders.CustomerOrderTotalAmount;
                model.CustomerOrderUserId = orders.CustomerOrderUserId;
                model.CustomerOrderDetails = _customerOrderDetails.GetAsQueryable()
                    .Where(x => x.CustomerOrderDetailsSno == id).Select(x => new CustomerOrderDetailsListResponse
                    {
                        CustomerOrderDetailsAmount = x.CustomerOrderDetailsAmount,
                        CustomerOrderDetailsDescription = x.CustomerOrderDetailsDescription,
                        CustomerOrderDetailsId = x.CustomerOrderDetailsId,
                        CustomerOrderDetailsMaterialDescription = x.CustomerOrderDetailsMaterialDescription,
                        CustomerOrderDetailsRate = x.CustomerOrderDetailsRate,
                        CustomerOrderDetailsSno = model.CustomerOrderId,
                        CustomerOrderDetailsQuantity = x.CustomerOrderDetailsQuantity,
                        CustomerOrderDetailsUnitId=x.CustomerOrderDetailsUnitId,
                        CustomerOrderDetailsUnitName = _unitMaster.GetAsQueryable().FirstOrDefault(c => c.UnitMasterUnitId == x.CustomerOrderDetailsUnitId).UnitMasterUnitShortName,
                        CustomerOrderDetailsMaterialId = x.CustomerOrderDetailsMaterialId,
                        CustomerOrderDetailsFcAmount = x.CustomerOrderDetailsFcAmount
                    }).ToList();
                  
                });
                return Response<GetCustomerSalesOrderListResponse>.Success(model, "Records FOund.");
            }
            catch (Exception ex)
            {
                return Response<GetCustomerSalesOrderListResponse>.Fail(new GetCustomerSalesOrderListResponse(), ex.Message);
            }
        }
    }
}
