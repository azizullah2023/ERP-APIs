using System;
using System.Transactions;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Procurement.Implementation;
using Inspire.Erp.Application.Sales.Implementations;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Store.Implementation;
using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Application.Authentication.Implementations;
using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Application.Mapping;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.Administration.Implementations;
using Inspire.Erp.Application.Administration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Inspire.Erp.Application.Master.Implementations;
using Inspire.Erp.Application.Master.Interfaces;
//using Inspire.Erp.Application.UploadService;
using Inspire.Erp.Application.Print;
using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Application.Account.Implementation;
using Inspire.Erp.Application.Account.Interface;
using Inspire.Erp.Application.NewFolder.Implementations;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Application.Settings.Implementations;
using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Application.MIS.Implementations;
using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Application.Dashboard.Implementations;
using Inspire.Erp.Application.Dashboard.Interfaces;
using Inspire.Erp.Application.Sales.Implementation;
using Inspire.Erp.Application.Sales.Interface;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Application.StoreWareHouse.Implementation;
using System.Reflection;
using System.Linq;

namespace Inspire.Erp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMapFrom<>), typeof(MapFrom<>));
            var assembly = Assembly.Load("Inspire.Erp.Application");
            var types = assembly.GetTypes()
                // Filter types that are classes, public, not static, and not generic or generic but closed
                .Where(x => x.IsClass && x.IsPublic && !x.IsSealed &&
                            (!x.IsGenericType || x.IsGenericType && !x.ContainsGenericParameters));
            foreach (var type in types)
            {

                var implementedInterfaces = type.GetInterfaces();

                if (implementedInterfaces.Length > 0)
                {
                    // Choose the first implemented interface for registration.
                    foreach (var interfaceToRegister in implementedInterfaces)
                    {
                        services.AddScoped(interfaceToRegister, type);
                    }

                }
            }
            //services.AddScoped<ICurrencyMasterService, CurrencyMasterService>();
            //services.AddScoped<ISupplierMasterService, SupplierMasterService>();
            //services.AddScoped<ICountryMasterService, CountryMasterService>();
            //services.AddScoped<ICityMasterService, CityMasterService>();
            //services.AddScoped<IModelMasterService, ModelMasterService>();
            //services.AddScoped<IBrandMasterService, BrandMasterService>();
            //services.AddScoped<IBankGuaranteeMasterService, BankGuaranteeMasterService>();
            //services.AddScoped<IBudgetMasterService, BudgetMasterService>();
            //services.AddScoped<IDepartmentMasterService, DepartmentMasterService>();
            //services.AddScoped<IBusinessTypeMasterService, BusinessTypeMasterService>();
            //services.AddScoped<ILocationMasterService, LocationMasterService>();
            //services.AddScoped<ITermsAndConditionService, TermsAndConditionService>();
            //services.AddScoped<ICustomerMasterService, CustomerMasterService>();
            //services.AddScoped<ISalesmanMasterService, SalesmanMasterService>();
            //services.AddScoped<ICustomerTypeService, CustomerTypeService>();
            //services.AddScoped<IItemMasterService, ItemMasterService>();
            //services.AddScoped<IJobMasterService, JobMasterService>();
            //services.AddScoped<IUnitMasterService, UnitMasterSevice>();
            //services.AddScoped<ICostCenterMasterService, CostCenterMasterService>();
            //services.AddScoped<IPriceLevelMasterService, PriceLevelMasterService>();
            //services.AddScoped<IStaffMasterService, StaffMasterService>();
            //services.AddScoped<IWorkGroupMasterService, WorkGroupMasterService>();
            //services.AddScoped<IProjectDescriptionService, ProjectDescriptionService>();
            //services.AddScoped<ITypeMasterService, TypeMasterService>();
            //services.AddScoped<ITaxMasterService, TaxMasterService>();
            //services.AddScoped<IRouteMasterService, RouteMasterService>();
            //services.AddScoped<IChartofAccountsService, ChartofAccountsService>();
            //services.AddScoped<IPaymentVoucherService, PaymentVoucherService>();
            //services.AddScoped<IReceiptVoucherService, ReceiptVoucherService>();
            //services.AddScoped<IEnquiryAboutService, EnquiryAboutService>();
            //services.AddScoped<IEnquiryStatusService, EnquiryStatusService>();
            //services.AddScoped<ICustomerQuotationService, CustomerQuotationService>();
            //services.AddScoped<ICustomerEnquiryService, CustomerEnquiryService>();
            //services.AddScoped<IBankPaymentVoucherService, BankPaymentVoucherService>();
            //services.AddScoped<IBankReceiptVoucherService, BankReceiptVoucherService>();

            //services.AddScoped<IOBVoucherService, OBVoucherService>();

            //services.AddScoped<IDebitNoteService, DebitNoteService>();
            //services.AddScoped<IGRNService, GRNService>();
            //services.AddScoped<ICreditNoteService, CreditNoteService>();

            //services.AddScoped<IloginService, LoginService>();

            //services.AddScoped<IPurchaseJournalService, PurchaseJournalService>();
            //services.AddScoped<ISalesJournalService, SalesJournalService>();

            //services.AddScoped<IStationMasterServices, StationMasterService>();

            //services.AddScoped<ICustomerTypeService, CustomerTypeService>();
            //services.AddScoped<IPurchaseVoucherService, PurchaseVoucherService>();
            //services.AddScoped<ISalesVoucherService, SalesVoucherService>();
            //services.AddScoped<IDeliveryNoteServices, DeliveryNoteServices>();
            //services.AddScoped<IPurchaseReturnService, PurchaseReturnService>();
            //services.AddScoped<ICustomerMasterService, CustomerMasterService>();
            //services.AddScoped<INewCustomerService, NewCustomerService>();
            //services.AddScoped<ISalesReturnService, SalesReturnService>();
            //services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            //services.AddScoped<IPurchaseQuotationService, PurchaseQuotationService>();
            //services.AddScoped<IPurchaseRequisitionService, PurchaseRequisitionService>();
            //services.AddScoped<IIssueVoucherService, IssueVoucherService>();
            //services.AddScoped<IIssueReturnService, IssueReturnService>();
            //services.AddScoped<IJournalVoucherService, JournalVoucherService>();
            //services.AddScoped<IContraVoucherService, ContraVoucherService>();
            //services.AddScoped<IMainReportService, MainReportService>();
            //services.AddScoped<IUpload, ImportService>();

            //services.AddScoped<IMasterAccountsTableService, MasterAccountsTableService>();
            //services.AddScoped<IErpSettings, SettingsService>();
            //services.AddScoped<IUpload, ImportService>();
            //services.AddScoped<IPrint, PrintService>();

            //services.AddScoped<IAccountsSettingsService, AccountsSettingsService>();


            //services.AddScoped<IvoucherPrinting, VoucherPrinting>();
            //services.AddScoped<IBalanceSheet, BalanceSheet>();
            //services.AddScoped<IProfitAndLoss, ProfitAndLoss>();
            //services.AddScoped<IStatementOfAccountSummary, StatementOfAccountSummary>();
            //services.AddScoped<IStatementOfAccountDetail, StatementOfAccountDetail>();

            //services.AddScoped<IAccountStatementService, AccountStatementService>();
            //services.AddScoped<IVoucherPrintService, VoucherPrintService>();
            //services.AddScoped<IFileService, FileService>();
            //services.AddScoped<IProfitLossService, ProfitLossService>();
            //services.AddScoped<IBalanceSheetService, BalanceSheetService>();
            //services.AddScoped<IOldBalanceSheetService, OldBalanceSheetService>();

            //services.AddScoped<IAgeingReport, AgeingReport>();
            //services.AddScoped<IPDC, PDCService>();
            //services.AddScoped<IBankReconcilation, BankReconcilation>();
            //services.AddScoped<IAccountStatementMultiAccountService, AccountStatementMultiAccountService>();
            //services.AddScoped<IDaybookService, DaybookService>();
            //services.AddScoped<IOutstandingStatementService, OutstandingStatementService>();

            //services.AddScoped<IUtilityService, UtilityService>();
            //////services.AddScoped<ICustomerSalesOrderService, CustomerSalesOrderService>();
            //services.AddScoped<IVoucherAllocationService, VoucherAllocationService>();
            //services.AddScoped<IOptionsSettings, OptionsSettingsSeverice>();
            //services.AddScoped<IAvIssueVoucherService, AvIssueVoucherService>();
            ////services.AddScoped<IPurchaseReportService, PurchaseReportService>();
            ////services.AddScoped<IPurchaseOrderListService, PurchaseOrderListService>();

            //services.AddScoped<ITaxMasterService, TaxMasterService>();
            //services.AddScoped<IDashboardService, DashboardService>();
            //services.AddScoped<ISalesmanwiseSalesReport, SalesmanwiseSalesReport>();
            //services.AddScoped<ISalesManWiseOutStandingReport, SalesManWiseOutStandingReport>();
            ////services.AddScoped<IOptionMasterService, OptionMasterService>();
            //services.AddScoped<IPurchaseVoucherService, PurchaseVoucherService>();
            //services.AddScoped<IAssetsOpeningService, AssetsOpeningService>();
            ////services.AddScoped<IAccountsTransactionsService, AccountsTransactionsService>();
            //services.AddScoped<IVatStatementService, VatStatementService>();
            //services.AddScoped<IApprovalService, ApprovalService>();

            //services.AddScoped<IVoucherAllocationService, VoucherAllocationService>();
            //services.AddScoped<IStockTransferService, StockTransferService>();
            ////services.AddScoped<IUser, UserService>();
            //services.AddScoped<ICustomerSalesOrderService, CustomerSalesOrderService>();

            //services.AddScoped<IPurchaseReturnService, PurchaseReturnService>();
            //services.AddScoped<IOpeningStockService, OpeningStockService>();

            //services.AddScoped<ISalesVoucherService, SalesVoucherService>();
            //services.AddScoped<IStockService, StockService>();
            //services.AddScoped<ICustomerPurchaseOrderService, CustomerPurchaseOrderService>();

            //#region Reports
            ////trial Balance 
            //services.AddScoped<ITrialBalanceReport, TrialBalanceReport>();
            //#endregion

            return services;

        }
    }
}
