using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Inspire.Erp.Domain.Enums
{
    public enum PriorityLevel
    {
        None,
        Low,
        Medium,
        High
    }
    public enum DbStatus
    {
        inserted,
        updated,
        deleted,
        alreadyexist,
        failed,
        successfull
    }
    public static class VoucherType
    {
        public static String Payment { get { return "PAYMENT"; } }
        public static String BankPayment { get { return "BANK PAYMENT"; } }
        public static String BankReceipt { get { return "BANK RECEIPT"; } }
        public static string Receipt { get { return "RECEIPT"; } }
        public static string Quotation { get { return "QUOTATION"; } }
        public static string Enquiry { get { return "Enquiry"; } }
        public static String CreditNote_TYPE { get { return "CreditNote"; } }
        public static String DebitNote_TYPE { get { return "DebitNote"; } }
        public static String SalesJournal_TYPE { get { return "SalesJournal"; } }
        public static String PurchaseJournal_TYPE { get { return "PurchaseJournal"; } }
        public static String PurchaseReturn_TYPE { get { return "PurchaseReturn"; } }
        public static String OBVoucher { get { return "OPENING BALANCE VOUCHER"; } }
        public static String PurchaseVoucher_TYPE { get { return "PurchaseVoucher"; } }
        public static String DamageEntry_TYPE { get { return "DamageEntry"; } }
        public static String SalesVoucher_TYPE { get { return "SalesVoucher"; } }
        public static String SalesReturn_TYPE { get { return "SalesReturn"; } }
        public static String PurchaseOrder_TYPE { get { return "PurchaseOrder"; } }
        public static String PurchaseQuotation_TYPE { get { return "PurchaseQuotation"; } }
        public static String PurchaseRequisition_TYPE { get { return "PurchaseRequisition"; } }
        public static String IssueVoucher_TYPE { get { return "IssueVoucher"; } }
        public static String OpeningStockVoucher_TYPE { get { return "OpeningStockVoucher"; } }
        public static String IssueReturn_TYPE { get { return "IssueReturn"; } }
        public static String JournalVoucher_TYPE { get { return "JournalVoucher"; } }
        public static String Dep_JournalVoucher_TYPE { get { return "DEPJV"; } }
        public static String ContraVoucher_TYPE { get { return "ContraVoucher"; } }
        public static String StockTransferVoucher_TYPE { get { return "StockTransferVoucher"; } }
        public static String CustomerPurchaseOrder_TYPE { get { return "CustomerPurchaseOrder"; } }
        public static String AssetPurchaseVoucher_TYPE { get { return "AssetPurchaseVoucher"; } }
        public static String PDC_TYPE { get { return "PDCVoucher"; } }
        public static String JobInoviceVoucher_TYPE { get { return "JobInoviceVoucher"; } }
        public static String GeneralPurchaseOrder_TYPE { get { return "GeneralPurchaseOrder"; } }
        public static String StockRequisition_TYPE { get { return "StockRequisition"; } }
        public static String ProgressiveInvoice_TYPE { get { return "ProgressiveInvoice"; } }
        public static String StockAdjustmentVoucher_TYPE
        {
            get { return "StockAdjustmentVoucher"; }

        }
        public static String ReconciliationVoucher_TYPE
        {
            get { return "ReconciliationVoucher"; }

        }
        public static string BOQ { get { return "BOQ"; } }

        public static String PosSalesVoucher_TYPE { get { return "PosSalesVoucher"; } }
        public static String JobMasterVoucher_TYPE { get { return "JobMasterVoucher"; } }
        public static String ManufactureItem_TYPE { get { return "ManufactureItem"; } }
        public static String DamageVoucher_TYPE { get { return "DamageVoucher"; } }
    }

    public static class AccountStatus
    {
        public static String Approved { get { return "A"; } }
        public static String AccStatus { get { return "R"; } }
        public static String Pending { get { return "P"; } }
        public static String PDCStatus { get { return "NC"; } }
        public static String StockAccountNumber { get { return "STOCK_AC_NO"; } }
        public static String ExpenseAccountNo { get { return "EXPENSES_AC_NO"; } }
        public static String BRStatus { get { return "RECEIVED"; } }
        public static String BPStatus { get { return "ISSUED"; } }

    }

    public static class Prefix
    {
        public static String PV_Prefix { get { return "PV_PREFIX"; } }
        public static String RV_Prefix { get { return "RV_PREFIX"; } }
        public static String BPV_Prefix { get { return "BPV_PREFIX"; } }
        public static String BRV_Prefix { get { return "BRV_PREFIX"; } }
        public static String CQ_Prefix { get { return "HIDE_CUSTQUOTATION"; } }
        public static String CE_Prefix { get { return "HIDE_CUSTENQUIRY"; } }
        public static String PurchaseVoucher_Prefix { get { return "PurchaseVoucher_Prefix"; } }
        public static String PurchaseReturn_Prefix { get { return "PurchaseReturn_Prefix"; } }
        public static String SalesVoucher_Prefix { get { return "SV_PREFIX"; } }
        public static String CreditNote_Prefix { get { return "CreditNote_Prefix"; } }
        public static String DebitNote_Prefix { get { return "DebitNote_Prefix"; } }
        public static String SalesJournal_Prefix { get { return "SalesJournal_Prefix"; } }
        public static String PurchaseJournal_Prefix { get { return "PurchaseJournal_Prefix"; } }
        public static String SalesReturn_Prefix { get { return "SalesReturn_Prefix"; } }
        public static String PurchaseOrder_Prefix { get { return "PurchaseOrder_Prefix"; } }
        public static String CustomerPurchaseOrder_Prefix { get { return "CustomerPurchaseOrder_Prefix"; } }
        public static String PurchaseRequisition_Prefix { get { return "PurchaseRequisition_Prefix"; } }
        public static String PurchaseQuotation_Prefix { get { return "PurchaseQuotation_Prefix"; } }
        public static String IssueVoucher_Prefix { get { return "IssueVoucher_Prefix"; } }
        public static String OpeningStockVoucher_Prefix { get { return "OpeningStockVoucher_Prefix"; } }
        public static String StockTransfer_Prefix { get { return "StockTransfer_Prefix"; } }
        public static String IssueReturn_Prefix { get { return "IssueReturn_Prefix"; } }
        public static String ContraVoucher_Prefix { get { return "ContraVoucher_Prefix"; } }
        public static String JournalVoucher_Prefix { get { return "JournalVoucher_Prefix"; } }
        public static String Dep_JournalVoucher_Prefix { get { return "Dep_JournalVoucher_Prefix"; } }
        public static String AssetPurchaseVoucher_Prefix { get { return "AssetPurchaseVoucher_Prefix"; } }
        public static String PDC_Voucher_Prefix { get { return "PDC_Voucher_Prefix"; } }
        public static String JobInoviceVoucher_Prefix { get { return "JobInoviceVoucher_Prefix"; } }
        public static String GeneralPurchaseOrder_Prefix { get { return "GeneralPurchaseOrder_Prefix"; } }

        public static String StockRequisition_Prefix { get { return "StockRequisition_Prefix"; } }

        public static String ManufactureItem_Prefix { get { return "ManufactureItem_Prefix"; } }
        public static String DE_Prefix { get { return "DamageEntry_Prefix"; } }
        public static String BOQ_Prefix { get { return "BOQ_Prefix"; } }

        public static String ProgressiveInvoice_Prefix { get { return "ProgressiveInvoice_Prefix"; } }
    }

    public static class ItemMasterStatus
    {
        public static String Group { get { return "G"; } }
        public static String Item { get { return "I"; } }
        public static String SubItem { get { return "A"; } }
        public static String GroupItem { get { return "Group"; } }
        public static String ItemKey { get { return "Item"; } }


    }


    public static class AccountMasterStatus
    {
        public static String Group { get { return "G"; } }
        public static String Account { get { return "A"; } }
        public static String SubItem { get { return "S"; } }
    }

}
