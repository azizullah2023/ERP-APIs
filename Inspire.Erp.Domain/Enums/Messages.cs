using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Enums
{

    public static class DbMessage
    {
        public static String Failed { get { return "DB Failed"; } }
    }
    public static class PaymentVoucherMessage
    {

        public static String SaveVoucher { get { return "Payment Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Payment Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Payment Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Payment Voucher is already exist"; } }

    }

    public static class ReceiptVoucherMessage
    {
        public static String SaveReceiptVoucher { get { return "Receipt Voucher saved successfully"; } }
        public static string UpdateReceiptVoucher { get { return "Receipt Voucher updated successfully"; } }
        public static string DeleteReceiptVoucher { get { return "Receipt Voucher updated successfully"; } }
        public static string ReceiptVoucherAlreadyExist { get { return "Receipt Voucher is already exist"; } }
    }


    public static class BankPaymentVoucherMessage
    {

        public static String SaveVoucher { get { return "Bank Payment Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Bank Payment Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Bank Payment Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Bank Payment Voucher is already exist"; } }

    }

    public static class BankReceiptVoucherMessage
    {
        public static String SaveReceiptVoucher { get { return "Bank Receipt Voucher saved successfully"; } }
        public static string UpdateReceiptVoucher { get { return "Bank Receipt Voucher updated successfully"; } }
        public static string DeleteReceiptVoucher { get { return "Bank Receipt Voucher updated successfully"; } }
        public static string ReceiptVoucherAlreadyExist { get { return "Bank Receipt Voucher is already exist"; } }
    }

    public static class CustomerQuotationMessage
    {
        public static String SaveCustomerQuotation { get { return "Customer Quotation saved successfully"; } }
        public static string UpdateCustomerQuotation { get { return "Customer Quotation updated successfully"; } }
        public static string DeleteCustomerQuotation { get { return "Customer Quotation Deleted successfully"; } }
        public static string CustomerQuotationAlreadyExist { get { return "Custome rQuotation is already exist"; } }
        public static string CustomerQuotationError { get { return "Error in Insert/Update/Delete Quotation"; } }
    }

    public static class BillofQtyMessage
    {
        public static String SaveBillofQty { get { return "BillofQty saved successfully"; } }
        public static string UpdateBillofQty { get { return "BillofQty updated successfully"; } }
        public static string DeleteBillofQty { get { return "BillofQty Deleted successfully"; } }
        public static string BillofQtyAlreadyExist { get { return "BillofQty is already exist"; } }
        public static string BillofQtyError { get { return "Error in Insert/Update/Delete BillofQty"; } }
    }
    public static class CustomerEnquiryMessage
    {
        public static String SaveCustomerEnquiry { get { return "Customer Enquiry saved successfully"; } }
        public static string UpdateCustomerEnquiry { get { return "Customer Enquiry updated successfully"; } }
        public static string DeleteCustomerEnquiry { get { return "Customer Enquiryr Deleted successfully"; } }
        public static string CustomerEnquiryAlreadyExist { get { return "Customer Enquiry is already exist"; } }
    }



    public static class ItemMasterMessage
    {
        public static String SaveItem { get { return "Item saved successfully"; } }
        public static string UpdateItem { get { return "Item updated sucessfully"; } }

        public static String SaveFailed { get { return "Item save failed"; } }
        public static string UpdateFailed { get { return "Item update failed"; } }
        public static string DeleteFailed { get { return "Item delete failed"; } }
        public static string DeleteFaileds { get { return "Item delete sucessfully"; } }
    }


    public static class LoginMessage
    {
        public static String Failed { get { return "Credential Failed"; } }
        public static String Successfull { get { return "Credential Successfull"; } }
    }

    public static class CreditNoteMessage
    {

        public static String SaveVoucher { get { return "Credit Note saved successfully"; } }
        public static string UpdateVoucher { get { return "Credit Note updated successfully"; } }
        public static string DeleteVoucher { get { return "Credit Note Deleted successfully"; } }
        public static string VoucherAlreadyExist { get { return "Credit Note No. is already exist"; } }

        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }


    public static class DebitNoteMessage
    {

        public static String SaveVoucher { get { return "Debite Note saved successfully"; } }
        public static string UpdateVoucher { get { return "Debit Note updated successfully"; } }
        public static string DeleteVoucher { get { return "Debit Note Deleted successfully"; } }
        public static string VoucherAlreadyExist { get { return "Debite Note No. is already exist"; } }

        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class PurchaseJournalMessage
    {
        public static String SaveVoucher { get { return "Purchase Journal saved successfully"; } }
        public static string UpdateVoucher { get { return "Purchase Journalr updated successfully"; } }
        public static string DeleteVoucher { get { return "Purchase Journal Deleted successfully"; } }
        public static string VoucherAlreadyExist { get { return "Purchase Journal is already exist"; } }

        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class SalesJournalMessage
    {
        public static String SaveVoucher { get { return "Sales Journal saved successfully"; } }
        public static string UpdateVoucher { get { return "Sales Journal updated successfully"; } }
        public static string DeleteVoucher { get { return "Sales Journal Deleted successfully"; } }
        public static string VoucherAlreadyExist { get { return "Sales Journal is already exist"; } }

        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }


    public static class PurchaseVoucherMessage
    {

        public static String SaveVoucher { get { return "Purchase Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Purchase Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Purchase Voucher updated successfully"; } }
        public static string VoucherNumberExist { get { return "Purchase Voucher is already exist"; } }

    }

    public static class SalesVoucherMessage
    {

        public static String SaveVoucher { get { return "Sales Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Sales Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Sales Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Sales Voucher is already exist"; } }

        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class SalesReturnMessage
    {
        public static String SaveVoucher { get { return "Sales Return saved successfully"; } }
        public static string UpdateVoucher { get { return "Sales Return updated successfully"; } }
        public static string DeleteVoucher { get { return "Sales Return updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Sales Return is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }
    public static class PurchaseReturnMessage
    {

        public static String SaveVoucher { get { return "Purchase Return saved successfully"; } }
        public static string UpdateVoucher { get { return "Purchase Return updated successfully"; } }
        public static string DeleteVoucher { get { return "Purchase Return updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Purchase Return is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }
    }


    public static class PurchaseOrderMessage
    {
        public static String SaveVoucher { get { return "Purchase Order saved successfully"; } }
        public static string UpdateVoucher { get { return "Purchase Order updated successfully"; } }
        public static string DeleteVoucher { get { return "Purchase Order deleted successfully"; } }
        public static string VoucherAlreadyExist { get { return "Purchase Order is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class PurchaseQuotationMessage
    {
        public static String SaveVoucher { get { return "Purchase Quotation saved successfully"; } }
        public static string UpdateVoucher { get { return "Purchase Quotation updated successfully"; } }
        public static string DeleteVoucher { get { return "Purchase Quotation updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Purchase Quotation is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class PurchaseRequisitionMessage
    {
        public static String SaveVoucher { get { return "Purchase Requisition saved successfully"; } }
        public static string UpdateVoucher { get { return "Purchase Requisition updated successfully"; } }
        public static string DeleteVoucher { get { return "Purchase Requisition updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Purchase Requisition is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }



    public static class IssueVoucherMessage
    {
        public static String SaveVoucher { get { return "Issue Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Issue Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Issue Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Issue Voucher is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }
    }


    public static class CustomerPurchaseOrderMessage
    {
        public static String SavePurchaseOrder { get { return "Customer Purchase Order saved successfully"; } }
        public static string UpdatePurchaseOrder { get { return "Customer Purchase Order updated successfully"; } }
        public static string DeletePurchaseOrder { get { return "Customer Purchase Order updated successfully"; } }
        public static string PurchaseOrderAlreadyExist { get { return "Customer Purchase Order is already exist"; } }
        public static string PurchaseOrderNumberExist { get { return "Customer Purchase Order is exist"; } }
    }

    public static class OpeningStockVoucherMessage
    {
        public static String SaveVoucher { get { return "OpeningStock Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "OpeningStock Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "OpeningStock Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "OpeningStock Voucher is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }
    }

    public static class AssetPurchaseVoucherMessage
    {
        public static String SaveVoucher { get { return "AssetPurchase Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "AssetPurchase Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "AssetPurchase Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "AssetPurchase Voucher is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }
    }




    public static class IssueReturnMessage
    {
        public static String SaveVoucher { get { return "Issue Return saved successfully"; } }
        public static string UpdateVoucher { get { return "Issue Return updated successfully"; } }
        public static string DeleteVoucher { get { return "Issue Return updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Issue Return is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class StockTransferMessage
    {
        public static String SaveStockTransfer { get { return "Stock Transfered successfully"; } }
        public static string UpdateStockTransfer { get { return "Stock Transfer updated successfully"; } }
        public static string DeleteStockTransfer { get { return "Stock Transfer deleted successfully"; } }
        public static string StockTransferAlreadyExist { get { return "StockTransfer is already exist"; } }
        public static string StockTransferNumberExist { get { return "StockTransfer Number is exist"; } }

    }

    public static class ContraVoucherMessage
    {
        public static String SaveVoucher { get { return "Payment Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Payment Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Payment Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Payment Voucher is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }
    }

    public static class JournalVoucherMessage
    {
        public static String SaveVoucher { get { return "Payment Voucher saved successfully"; } }
        public static string UpdateVoucher { get { return "Payment Voucher updated successfully"; } }
        public static string DeleteVoucher { get { return "Payment Voucher updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Payment Voucher is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }
    }

    public static class AccountMasterMessage
    {
        public static String SaveAccount { get { return "Account saved successfully"; } }
        public static string UpdateAccount { get { return "Account updated sucessfully"; } }

        public static String SaveFailed { get { return "Account Item save failed"; } }
        public static string UpdateFailed { get { return "Account Item update failed"; } }
        public static string DeleteFailed { get { return "Account Item delete failed"; } }
    }
    public static class JobMasterMessage
    {
        public static String SaveJob { get { return "Job saved successfully"; } }
        public static string UpdateJob { get { return "Job updated sucessfully"; } }
        public static string DeleteJob { get { return "Job deleted sucessfully"; } }

        public static String SaveFailed { get { return "Job Item save failed"; } }
        public static string UpdateFailed { get { return "Job Item update failed"; } }
        public static string DeleteFailed { get { return "Job Item delete failed"; } }
    }
    public static class JobInvoiceMessage
    {
        public static String SaveJobInvoice { get { return "Job Invoice saved successfully"; } }
        public static string UpdateJobInvoice { get { return "Job Invoice updated sucessfully"; } }
        public static string DeleteJobInvoice { get { return "Job Invoice deleted sucessfully"; } }

        public static String SaveFailed { get { return "Job Invoice save failed"; } }
        public static string UpdateFailed { get { return "Job Invoice update failed"; } }
        public static string DeleteFailed { get { return "Job Invoice delete failed"; } }
    }
    public static class ManufactureItemMessage
    {
        public static String SaveManufactureItem { get { return "Manufacture Items saved successfully"; } }
        public static string UpdateManufactureItem { get { return "Manufacture Items updated sucessfully"; } }
        public static string DeleteManufactureItem { get { return "Manufacture Items deleted sucessfully"; } }

        public static String SaveFailed { get { return "Manufacture Items save failed"; } }
        public static string UpdateFailed { get { return "Manufacture Items update failed"; } }
        public static string DeleteFailed { get { return "Manufacture Items delete failed"; } }
    }
    public static class StockeAdjustmentMessage
    {
        public static string SaveStockAdjustmentVoucher { get { return "Stock Adjustment Voucher saved successfully"; } }
        public static string UpdateStockAdjustmentVoucher { get { return "Stock Adjustment Voucher updated successfully"; } }
        public static string DeleteStockAdjustmentVoucher { get { return "Stock Adjustment Vouchar deleted successfully"; } }

    }
    public static class GeneralPurchaseOrderMessage
    {
        public static String SaveVoucher { get { return "General Purchase Order saved successfully"; } }
        public static string UpdateVoucher { get { return "General Purchase Order updated successfully"; } }
        public static string DeleteVoucher { get { return "General Purchase Order deleted successfully"; } }
        public static string VoucherAlreadyExist { get { return "General Purchase Order is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }
    public static class StockRequisitionMessage
    {
        public static String SaveVoucher { get { return "Stock Requisition saved successfully"; } }
        public static string UpdateVoucher { get { return "Stock Requisition updated successfully"; } }
        public static string DeleteVoucher { get { return "Stock Requisition updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "Stock Requisition is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }

    public static class UserFileMessage
    {
        public static String SaveUser { get { return "User  saved successfully"; } }
        public static string UpdateUser { get { return "User updated successfully"; } }
        public static string DeleteUser { get { return "User deleted successfully"; } }

    }
    public static class WorkPeriodsMessage
    {
        public static String StartWP { get { return "Work Period   Starts successfully"; } }
        public static string EndWP { get { return "Work Period Ends successfully"; } }


    }
    public static class GRNMessage
    {
        public static String SaveVoucher { get { return "GRN saved successfully"; } }
        public static string UpdateVoucher { get { return "GRN updated successfully"; } }
        public static string DeleteVoucher { get { return "GRN updated successfully"; } }
        public static string VoucherAlreadyExist { get { return "GRN is already exist"; } }
        public static string VoucherNumberExist { get { return "Voucher Number is exist"; } }

    }
    public static class DamageMessage
    {
        public static String SaveDamage { get { return "Damage saved successfully"; } }
        public static string UpdateDamage { get { return "Damage updated successfully"; } }
        public static string DeleteDamage { get { return "Damage Delete successfully"; } }

    }



}
