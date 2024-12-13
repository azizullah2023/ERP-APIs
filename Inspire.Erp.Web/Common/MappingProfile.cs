using AutoMapper;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Accounts;
using Inspire.Erp.Web.ViewModels.Printer;
using Inspire.Erp.Web.ViewModels.Procurement;
using Inspire.Erp.Web.ViewModels.sales;
using Inspire.Erp.Web.ViewModels.Sales;
using Inspire.Erp.Web.ViewModels.Store;
using Inspire.Erp.Web.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Web.ViewModels.Settings;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Entities.POS;
using Inspire.Erp.Domain.Modals;

namespace Inspire.Erp.Web.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects

            //******************** Masters

            CreateMap<CurrencyMaster, CurrencyMasterViewModel>();
            CreateMap<CurrencyMasterViewModel, CurrencyMaster>();

            CreateMap<CountryMasterViewModel, CountryMaster>();
            CreateMap<CountryMaster, CountryMasterViewModel>();

            CreateMap<SuppliersMasterViewModel, SuppliersMaster>();
            CreateMap<SuppliersMaster, SuppliersMasterViewModel>();

            CreateMap<CityMasterViewModel, CityMaster>();
            CreateMap<CityMaster, CityMasterViewModel>();

            CreateMap<ModelsViewModel, ModelMaster>();
            CreateMap<ModelMaster, ModelsViewModel>();

            CreateMap<BrandMasterViewModel, VendorMaster>();
            CreateMap<VendorMaster, BrandMasterViewModel>();

            CreateMap<BudgetMasterViewModel, BudgetMaster>();
            CreateMap<BudgetMaster, BudgetMasterViewModel>();

            CreateMap<BankGuaranteeMasterViewModel, BankGuaranteeMaster>();
            CreateMap<BankGuaranteeMaster, BankGuaranteeMasterViewModel>();


            CreateMap<DepartmentMaster, DepartmentMasterViewModel>();
            CreateMap<DepartmentMasterViewModel, DepartmentMaster>();

            CreateMap<BusinessMasterViewModel, BusinessTypeMaster>();
            CreateMap<BusinessTypeMaster, BusinessMasterViewModel>();

            CreateMap<LocationMaster, LocationMasterViewModel>();
            CreateMap<LocationMasterViewModel, LocationMaster>();

            CreateMap<TermsAndCondition, TermsAndConditionViewModel>();
            CreateMap<TermsAndConditionViewModel, TermsAndCondition>();

            CreateMap<CustomerMaster, CustomerMasterViewModel>();
            CreateMap<CustomerMasterViewModel, CustomerMaster>();

            CreateMap<SalesManMaster, SalesmanMasterViewModel>();
            CreateMap<SalesmanMasterViewModel, SalesManMaster>();

            CreateMap<ChartofAccountsViewModel, MasterAccountsTable>();
            CreateMap<MasterAccountsTable, ChartofAccountsViewModel>();

            CreateMap<ItemMaster, ItemMasterViewModel>();
            CreateMap<ItemMasterViewModel, ItemMaster>();

            CreateMap<JobMaster, JobMasterViewModel>();
            CreateMap<JobMasterViewModel, JobMaster>();

            CreateMap<JobMasterBudgetDetails, JobMasterBudgetDetailsViewModels>();
            CreateMap<JobMasterBudgetDetailsViewModels, JobMasterBudgetDetails>();

            CreateMap<JobMasterJobDetails, JobMasterJobDetailsViewModels>();
            CreateMap<JobMasterJobDetailsViewModels, JobMasterJobDetails>();

            CreateMap<JobMasterJobWiseBankGuarantees, JobMasterJobWiseBankGuaranteesViewModels>();
            CreateMap<JobMasterJobWiseBankGuaranteesViewModels, JobMasterJobWiseBankGuarantees>();

            CreateMap<JobStaff, JobStaffViewModels>();
            CreateMap<JobStaffViewModels, JobStaff>();

            CreateMap<JobEquipment, JobEquipmentViewModels>();
            CreateMap<JobEquipmentViewModels, JobEquipment>();

            CreateMap<JobExcutionDetails, JobExcutionDetailsViewModels>();
            CreateMap<JobExcutionDetailsViewModels, JobExcutionDetails>();

            CreateMap<UnitMaster, UnitMasterViewModel>();
            CreateMap<UnitMasterViewModel, UnitMaster>();

            CreateMap<CostCenterMaster, CostCenterViewModel>();
            CreateMap<CostCenterViewModel, CostCenterMaster>();

            CreateMap<PriceLevelMaster, PriceLevelMasterViewModel>();
            CreateMap<PriceLevelMasterViewModel, PriceLevelMaster>();

            CreateMap<StaffMaster, StaffMasterViewModel>();
            CreateMap<StaffMasterViewModel, StaffMaster>();

            CreateMap<WorkGroupMaster, WorkGroupMasterViewModel>();
            CreateMap<WorkGroupMasterViewModel, WorkGroupMaster>();

            CreateMap<ProjectDescriptionMaster, ProjectDescriptionViewModel>();
            CreateMap<ProjectDescriptionViewModel, ProjectDescriptionMaster>();

            CreateMap<TypeMaster, TypeMasterViewModel>();
            CreateMap<TypeMasterViewModel, TypeMaster>();

            CreateMap<ItemStockType, ItemStockTypeViewModel>();

            CreateMap<SupplierDetails, SupplierDetailsViewModel>();
            CreateMap<SupplierDetailsViewModel, SupplierDetails>();


            CreateMap<UnitDetails, UnitDetailsViewModel>();
            CreateMap<UnitDetailsViewModel, UnitDetails>();

            CreateMap<ItemPriceLevelDetails, ItemPriceLevelDetailsViewModel>();
            CreateMap<ItemPriceLevelDetailsViewModel, ItemPriceLevelDetails>();

            CreateMap<ItemImages, ItemImagesViewModel>();
            CreateMap<ItemImagesViewModel, ItemImages>();

            CreateMap<CustomerMaster, CustomerMasterViewModel>();
            CreateMap<CustomerMasterViewModel, CustomerMaster>();

            CreateMap<CustomerContacts, CustomerContactViewModel>();
            CreateMap<CustomerContactViewModel, CustomerContacts>();

            CreateMap<CustomerDepartments, CustomerDepartmentsViewModel>();
            CreateMap<CustomerDepartmentsViewModel, CustomerDepartments>();

            CreateMap<CustomerType, CustomerTypeViewModel>();
            CreateMap<CustomerTypeViewModel, CustomerType>();

            CreateMap<MasterAccountsTable, MasterAccountsTableViewModel>();
            CreateMap<MasterAccountsTableViewModel, MasterAccountsTable>();

            CreateMap<TaxMaster, TaxMasterViewModel>();
            CreateMap<TaxMasterViewModel, TaxMaster>();




            //**********************************Masters ending **********
            //**********************************Accounts ****************
            CreateMap<OpeningVoucherMaster, OpeningBalanceVoucherViewModel>();
            CreateMap<OpeningBalanceVoucherViewModel, OpeningVoucherMaster>();

            CreateMap<OpeningVoucherDetails, OpeningBalanceVoucherDetailsViewModel>();
            CreateMap<OpeningBalanceVoucherDetailsViewModel, OpeningVoucherDetails>();

            CreateMap<PaymentVoucher, PaymentVoucherViewModel>();
            CreateMap<PaymentVoucherViewModel, PaymentVoucher>();

            CreateMap<PaymentVoucherDetails, PaymentVoucherDetailsViewModel>();
            CreateMap<PaymentVoucherDetailsViewModel, PaymentVoucherDetails>();

            CreateMap<AccountsTransactions, AccountTransactionsViewModel>();
            CreateMap<AccountTransactionsViewModel, AccountsTransactions>();
            //==========
            CreateMap<AccountsTransactions, AccountTransactionViewModel>();
            CreateMap<AccountTransactionViewModel, AccountsTransactions>();
            //==========

            CreateMap<ReceiptVoucherMaster, ReceiptVoucherMasterViewModel>();
            CreateMap<ReceiptVoucherMasterViewModel, ReceiptVoucherMaster>();

            CreateMap<ReceiptVoucherDetails, ReceiptVoucherDetailsViewModel>();
            CreateMap<ReceiptVoucherDetailsViewModel, ReceiptVoucherDetails>();

            CreateMap<BankPaymentVoucher, BankPaymentVoucherViewModel>();
            CreateMap<BankPaymentVoucherViewModel, BankPaymentVoucher>();

            CreateMap<BankPaymentVoucherDetails, BankPaymentVoucherDetailsViewModel>();
            CreateMap<BankPaymentVoucherDetailsViewModel, BankPaymentVoucherDetails>();

            CreateMap<BankReceiptVoucher, BankReceiptVoucherViewModel>();
            CreateMap<BankReceiptVoucherViewModel, BankReceiptVoucher>();

            CreateMap<BankReceiptVoucherDetails, BankReceiptVoucherDetailsViewModel>();
            CreateMap<BankReceiptVoucherDetailsViewModel, BankReceiptVoucherDetails>();

            CreateMap<CreditNote, CreditNoteViewModel>();
            CreateMap<CreditNoteViewModel, CreditNote>();

            CreateMap<CreditNoteDetails, CreditNoteDetailsViewModel>();
            CreateMap<CreditNoteDetailsViewModel, CreditNoteDetails>();


            CreateMap<DebitNote, CreditNoteViewModel>();
            CreateMap<DebitNoteViewModel, DebitNote>();

            CreateMap<DebitNoteDetails, DebitNoteDetailsViewModel>();
            CreateMap<DebitNoteDetailsViewModel, DebitNoteDetails>();


            CreateMap<PurchaseVoucher, PurchaseVoucherViewModel>();
            CreateMap<PurchaseVoucherViewModel, PurchaseVoucher>();

            CreateMap<PurchaseVoucherDetails, PurchaseVoucherDetailsViewModel>();
            CreateMap<PurchaseVoucherDetailsViewModel, PurchaseVoucherDetails>();

            CreateMap<ContraVoucher, ContraVoucherViewModel>();
            CreateMap<ContraVoucherViewModel, ContraVoucher>();

            CreateMap<ContraVoucherDetails, ContraVoucherDetailsViewModel>();
            CreateMap<ContraVoucherDetailsViewModel, ContraVoucherDetails>();

            CreateMap<JournalVoucher, JournalVoucherViewModel>();
            CreateMap<JournalVoucherViewModel, JournalVoucher>();

            CreateMap<JournalVoucherDetails, JournalVoucherDetailsViewModel>();
            CreateMap<JournalVoucherDetailsViewModel, JournalVoucherDetails>();

            CreateMap<AccountStatementParameters, AccountStatementParametersViewModels>();
            CreateMap<AccountStatementParametersViewModels, AccountStatementParameters>();


            CreateMap<PurchaseJournal, PurchaseJournalViewModel>();
            CreateMap<PurchaseJournalViewModel, PurchaseJournal>();

            CreateMap<PurchaseJournalDetails, PurchaseJournalDetailsViewModel>();
            CreateMap<PurchaseJournalDetailsViewModel, PurchaseJournalDetails>();

            CreateMap<SalesJournal, SalesJournalViewModel>();
            CreateMap<SalesJournalViewModel, SalesJournal>();

            CreateMap<SalesJournalDetails, SalesJournalDetailsViewModel>();
            CreateMap<SalesJournalDetailsViewModel, SalesJournalDetails>();


            CreateMap<CustomerPurchaseOrder, CustomerPurchaseOrderViewModel>();
            CreateMap<CustomerPurchaseOrderViewModel, CustomerPurchaseOrder>();

            CreateMap<CustomerPurchaseOrderDetails, CustomerPurchaseOrderDetailsViewModel>();
            CreateMap<CustomerPurchaseOrderDetailsViewModel, CustomerPurchaseOrderDetails>();


            CreateMap<UserFile, UserFileViewModel>();
            CreateMap<UserFileViewModel, UserFile>();

            // ********************* Accounts ending *************
            // ********************* Procurement *****************

            CreateMap<EnquiryAbout, EnquiryAboutViewModel>();
            CreateMap<EnquiryAboutViewModel, EnquiryAbout>();

            CreateMap<EnquiryStatus, EnquiryStatusViewModel>();
            CreateMap<EnquiryStatusViewModel, EnquiryStatus>();

            CreateMap<CustomerQuotation, CustomerQuotationViewModel>();
            CreateMap<CustomerQuotationViewModel, CustomerQuotation>();

            CreateMap<CustomerQuotationDetails, CustomerQuotationDetailsViewModel>();
            CreateMap<CustomerQuotationDetailsViewModel, CustomerQuotationDetails>();

            CreateMap<EnquiryDetails, EnquiryDetailsViewModel>();
            CreateMap<EnquiryDetailsViewModel, EnquiryDetails>();

            CreateMap<EnquiryMasterViewModel, CustomerEnquiryModel>();
            CreateMap<CustomerEnquiryModel, EnquiryMasterViewModel>();

            CreateMap<EnquiryMasterViewModel, EnquiryMaster>();
            CreateMap<EnquiryMaster, EnquiryMasterViewModel>();

            CreateMap<PurchaseReturn, PurchaseReturnViewModel>();
            CreateMap<PurchaseReturnViewModel, PurchaseReturn>();

            CreateMap<PurchaseReturnDetails, PurchaseReturnDetailsViewModel>();
            CreateMap<PurchaseReturnDetailsViewModel, PurchaseReturnDetails>();

            CreateMap<PurchaseOrder, PurchaseOrderViewModel>();
            CreateMap<PurchaseOrderViewModel, PurchaseOrder>();

            CreateMap<PurchaseOrderDetails, PurchaseOrderDetailsViewModel>();
            CreateMap<PurchaseOrderDetailsViewModel, PurchaseOrderDetails>();

            CreateMap<PurchaseQuotation, PurchaseQuotationViewModel>();
            CreateMap<PurchaseQuotationViewModel, PurchaseQuotation>();

            CreateMap<PurchaseQuotationDetails, PurchaseQuotationDetailsViewModel>();
            CreateMap<PurchaseQuotationDetailsViewModel, PurchaseQuotationDetails>();

            CreateMap<PurchaseRequisition, PurchaseRequisitionViewModel>();
            CreateMap<PurchaseRequisitionViewModel, PurchaseRequisition>();

            CreateMap<PurchaseRequisitionDetails, PurchaseRequisitionDetailsViewModel>();
            CreateMap<PurchaseRequisitionDetailsViewModel, PurchaseRequisitionDetails>();


            CreateMap<OpeningStockVoucher, OpeningStockVoucherViewModel>();
            CreateMap<OpeningStockVoucherViewModel, OpeningStockVoucher>();

            CreateMap<OpeningStockVoucherDetails, OpeningStockVoucherDetailsViewModel>();
            CreateMap<OpeningStockVoucherDetailsViewModel, OpeningStockVoucherDetails>();


            CreateMap<GeneralPurchaseOrder, GeneralPurchaseOrderViewModel>();
            CreateMap<GeneralPurchaseOrderViewModel, GeneralPurchaseOrder>();

            CreateMap<GeneralPurchaseOrderDetails, GeneralPurchaseOrderDetailsViewModel>();
            CreateMap<GeneralPurchaseOrderDetailsViewModel, GeneralPurchaseOrderDetails>();

            CreateMap<StockRequisition, StockRequisitionViewModel>();
            CreateMap<StockRequisitionViewModel, StockRequisition>();

            CreateMap<StockRequisitionDetails, StockRequisitionDetailsViewModel>();
            CreateMap<StockRequisitionDetailsViewModel, StockRequisitionDetails>();

            //*********************** Procurement Ending ******
            //*********************** Sales *******************

            CreateMap<SalesVoucher, SalesVoucherViewModel>();
            CreateMap<SalesVoucherViewModel, SalesVoucher>();

            CreateMap<SalesVoucherDetails, SalesVoucherDetailsViewModel>();
            CreateMap<SalesVoucherDetailsViewModel, SalesVoucherDetails>();

            CreateMap<SalesReturn, SalesReturnViewModel>();
            CreateMap<SalesReturnViewModel, SalesReturn>();

            CreateMap<SalesReturnDetails, SalesReturnDetailsViewModel>();
            CreateMap<SalesReturnDetailsViewModel, SalesReturnDetails>();

            CreateMap<CustomerDeliveryNote, CustomerDeliveryNoteViewModel>();
            CreateMap<CustomerDeliveryNoteViewModel, CustomerDeliveryNote>();

            CreateMap<CustomerDeliveryNoteDetails, CustomerDeliveryNoteDetailsViewModel>();
            CreateMap<CustomerDeliveryNoteDetailsViewModel, CustomerDeliveryNoteDetails>();

            //******************* Sales Ending ************
            //******************* Stores ******************

            CreateMap<IssueVoucher, IssueVoucherViewModel>();
            CreateMap<IssueVoucherViewModel, IssueVoucher>();

            CreateMap<IssueVoucherDetails, IssueVoucherDetailsViewModel>();
            CreateMap<IssueVoucherDetailsViewModel, IssueVoucherDetails>();

            CreateMap<IssueReturn, IssueReturnViewModel>();
            CreateMap<IssueReturnViewModel, IssueReturn>();

            CreateMap<IssueReturnDetails, IssueReturnDetailsViewModel>();
            CreateMap<IssueReturnDetailsViewModel, IssueReturnDetails>();

            CreateMap<StockRegister, StockRegisterViewModel>();
            CreateMap<StockRegisterViewModel, StockRegister>();

            //******************Stores Ending ***************
            //******************Reports**********************


            CreateMap<ViewAccountsTransactionsVoucherType, ViewAccountsTransactionsVoucherTypeViewModels>();
            CreateMap<ViewAccountsTransactionsVoucherTypeViewModels, ViewAccountsTransactionsVoucherType>();

            CreateMap<StockLedgerParameters, StockLedgerParametersViewModel>();
            CreateMap<StockLedgerParametersViewModel, StockLedgerParameters>();


            CreateMap<ViewStockTransferType, ViewStockTransferTypeViewModel>();
            CreateMap<ViewStockTransferTypeViewModel, ViewStockTransferType>();

            CreateMap<StockTransfer, StockTransferViewModel>();
            CreateMap<StockTransferViewModel, StockTransfer>();

            CreateMap<StockTransferDetails, StockTransferDetailsViewModel>();
            CreateMap<StockTransferDetailsViewModel, StockTransferDetails>();

            CreateMap<ReportStockBaseUnit, ReportStockBaseUnitViewModel>();
            CreateMap<ReportStockBaseUnitViewModel, ReportStockBaseUnit>();


            CreateMap<ReportSalesVoucher, ReportSalesVoucherViewModel>();
            CreateMap<ReportSalesVoucherViewModel, ReportSalesVoucher>();

            CreateMap<SalesvoucherreportParameters, SalesvoucherreportParametersViewModel>();
            CreateMap<SalesvoucherreportParametersViewModel, SalesvoucherreportParameters>();

            CreateMap<PurchasevoucherreportParameters, PurchasevoucherreportParametersViewModel>();
            CreateMap<PurchasevoucherreportParametersViewModel, PurchasevoucherreportParameters>();

            CreateMap<ReportPurchaseRequisition, ReportPurchaseRequisitionViewModel>();
            CreateMap<ReportPurchaseRequisitionViewModel, ReportPurchaseRequisition>();

            CreateMap<PurchaserequisitionReportParameters, PurchaserequisitionReportParametersViewModel>();
            CreateMap<PurchaserequisitionReportParametersViewModel, PurchaserequisitionReportParameters>();

            CreateMap<PurchaseOrderReportParameters, PurchaseOrderReportParametersViewModel>();
            CreateMap<PurchaseOrderReportParametersViewModel, PurchaseOrderReportParameters>();

            //*****************Reports Ending **************
            //*****************Settings *******************

            CreateMap<ImportTimeSheet, ImportTimeSheetViewModel>();
            CreateMap<ImportTimeSheetViewModel, ImportTimeSheet>();

            CreateMap<PrinterConnection, PrinterConnectionViewModel>();
            CreateMap<PrinterConnectionViewModel, PrinterConnection>();

            CreateMap<AccountSettings, ViewModels.AccountsSettingsViewModel>();
            CreateMap<ViewModels.AccountsSettingsViewModel, AccountSettings>();

            CreateMap<GeneralSettings, GeneralSettingsViewModel>();
            CreateMap<GeneralSettingsViewModel, GeneralSettings>();

            CreateMap<ProgramSettings, ProgramSettingsViewModel>();
            CreateMap<ProgramSettingsViewModel, ProgramSettings>();

            CreateMap<StationMaster, StationMasterViewModel>();
            CreateMap<StationMasterViewModel, StationMaster>();

            CreateMap<TaxMaster, TaxMasterViewModel>();
            CreateMap<TaxMasterViewModel, TaxMaster>();

            CreateMap<RouteMaster, RouteMasterViewModel>();
            CreateMap<RouteMasterViewModel, RouteMaster>();

            CreateMap<OptionsMaster, OptionsMasterViewModel>();
            CreateMap<OptionsMasterViewModel, OptionsMaster>();


            CreateMap<AssetPurchaseVoucher, AssetPurchaseVoucherViewModel>();
            CreateMap<AssetPurchaseVoucherViewModel, AssetPurchaseVoucher>();

            CreateMap<AssetPurchaseVoucherDetails, AssetPurchaseVoucherDetailsViewModel>();
            CreateMap<AssetPurchaseVoucherDetailsViewModel, AssetPurchaseVoucherDetails>();

            CreateMap<AssetPurchaseVoucherModel, AssetPurchaseVoucherViewModel>();
            CreateMap<AssetPurchaseVoucherViewModel, AssetPurchaseVoucherModel>();



            CreateMap<GetUnitDetailsMasterList, UnitMasterViewModel>();
            CreateMap<UnitMasterViewModel, GetUnitDetailsMasterList>();


            //*******************Settings Ending ****************


            #region MIS Rports Mappings
            //trial balance
            CreateMap<ReportFilter, ReportFilterViewModel>();
            CreateMap<ReportFilterViewModel, ReportFilter>();

            #endregion

            CreateMap<POS_WorkPeriod, WorkPeriodResponse>();
            CreateMap<WorkPeriodResponse, POS_WorkPeriod>();

            CreateMap<SalesHold, SalesHoldViewModel>();
            CreateMap<SalesHoldViewModel, SalesHold>();

            CreateMap<SalesHoldDetails, SalesHoldDetailsViewModel>();
            CreateMap<SalesHoldDetailsViewModel, SalesHoldDetails>();

            CreateMap<BillofQtyMasterModel, BillofQtyMaster>();
            CreateMap<BillofQtyMaster, BillofQtyMasterModel>();

            CreateMap<BillOfQTyDetailsModel, BillOfQTyDetails>();
            CreateMap<BillOfQTyDetails, BillOfQTyDetailsModel>();

        }
    }
}
