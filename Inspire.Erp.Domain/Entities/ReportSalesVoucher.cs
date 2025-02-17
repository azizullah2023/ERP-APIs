﻿using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReportSalesVoucher
    {
        public decimal SalesVoucherId { get; set; }
        public string SalesVoucherNo { get; set; }
        public DateTime SalesVoucherDate { get; set; }
        public string SalesVoucherType { get; set; }
        public decimal? SalesVoucherPartyId { get; set; }
        public string SalesVoucherPartyName { get; set; }
        public string SalesVoucherPartyAddress { get; set; }
        public string SalesVoucherPartyVatNo { get; set; }
        public string SalesVoucherRefNo { get; set; }
        public string SalesVoucherContPerson { get; set; }
        public string SalesVoucherDlvNo { get; set; }
        public DateTime? SalesVoucherDlvDate { get; set; }
        public string SalesVoucherSono { get; set; }
        public DateTime? SalesVoucherSodate { get; set; }
        public string SalesVoucherQtnNo { get; set; }
        public DateTime? SalesVoucherQtnDate { get; set; }
        public int? SalesVoucherSalesManId { get; set; }
        public int? SalesVoucherDptId { get; set; }
        public string SalesVoucherEnqNo { get; set; }
        public DateTime? SalesVoucherEnqDate { get; set; }
        public string SalesVoucherDescription { get; set; }
        public bool? SalesVoucherExcludeVat { get; set; }
        public int? SalesVoucherLocationId { get; set; }
        public long? SalesVoucherUserId { get; set; }
        public int? SalesVoucherCurrencyId { get; set; }
        public int? SalesVoucherCompanyId { get; set; }
        public int? SalesVoucherJobId { get; set; }
        public decimal? SalesVoucherFsno { get; set; }
        public decimal? SalesVoucherFcRate { get; set; }
        public string SalesVoucherStatus { get; set; }
        public decimal? SalesVoucherTotalGrossAmount { get; set; }
        public decimal? SalesVoucherTotalItemDisAmount { get; set; }
        public decimal? SalesVoucherTotalActualAmount { get; set; }
        public decimal? SalesVoucherTotalDisPer { get; set; }
        public decimal? SalesVoucherTotalDisAmount { get; set; }
        public decimal? SalesVoucherVatAmt { get; set; }
        public decimal? SalesVoucherVatPer { get; set; }
        public string SalesVoucherVatRoundSign { get; set; }
        public decimal? SalesVoucherVatRountAmt { get; set; }
        public decimal? SalesVoucherNetDisAmount { get; set; }
        public decimal? SalesVoucherNetAmount { get; set; }
        public string SalesVoucherHeader { get; set; }
        public string SalesVoucherDelivery { get; set; }
        public string SalesVoucherNotes { get; set; }
        public string SalesVoucherFooter { get; set; }
        public string SalesVoucherPaymentTerms { get; set; }
        public string SalesVoucherSubject { get; set; }
        public string SalesVoucherContent { get; set; }
        public string SalesVoucherRemarks1 { get; set; }
        public string SalesVoucherRemarks2 { get; set; }
        public string SalesVoucherTermsAndCondition { get; set; }
        public string SalesVoucherShippinAddress { get; set; }
        public bool? SalesVoucherDelStatus { get; set; }
        public string CurrencyMasterCurrencyName { get; set; }
        public string CustomerMasterCustomerName { get; set; }
        public string CustomerMasterCustomerContactPerson { get; set; }
        public int? CustomerMasterCustomerCountryId { get; set; }
        public int? CustomerMasterCustomerCityId { get; set; }
        public string CustomerMasterCustomerMobile { get; set; }
        public string CustomerMasterCustomerRemarks { get; set; }
        public string CustomerMasterCustomerTel1 { get; set; }
        public string CustomerMasterCustomerFax { get; set; }
        public string CustomerMasterCustomerVatNo { get; set; }
        public string CountryMasterCountryName { get; set; }
        public int? CityMasterCityId { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public long? ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public long? ItemBrandId { get; set; }
        public string ItemBrandName { get; set; }
        public string ItemMasterPartNo { get; set; }
        public string ItemMasterItemName { get; set; }
        public int? SalesManMasterSalesManId { get; set; }
        public string SalesManMasterSalesManName { get; set; }
        public int? DepartmentMasterDepartmentId { get; set; }
        public string DepartmentMasterDepartmentName { get; set; }
        public string LocationMasterLocationName { get; set; }
        public string JobMasterJobName { get; set; }
        public decimal? SalesVoucherDetailsId { get; set; }
        public string SalesVoucherDetailsNo { get; set; }
        public decimal? SalesVoucherDetailsSno { get; set; }
        public int? SalesVoucherDetailsMatId { get; set; }
        public string SalesVoucherDetailsItemName { get; set; }
        public int? SalesVoucherDetailsUnitId { get; set; }
        public string SalesVoucherDetailsUnitName { get; set; }
        public string SalesVoucherDetailsBatchCode { get; set; }
        public DateTime? SalesVoucherDetailsManfDate { get; set; }
        public DateTime? SalesVoucherDetailsExpDate { get; set; }
        public decimal? SalesVoucherDetailsQuantity { get; set; }
        public decimal? SalesVoucherDetailsRate { get; set; }
        public decimal? SalesVoucherDetailsGrossAmount { get; set; }
        public decimal? SalesVoucherDetailsDiscAmount { get; set; }
        public decimal? SalesVoucherDetailsActualAmount { get; set; }
        public decimal? SalesVoucherDetailsVatPer { get; set; }
        public decimal? SalesVoucherDetailsVatAmt { get; set; }
        public decimal? SalesVoucherDetailsNetAmt { get; set; }
        public string SalesVoucherDetailsRemarks { get; set; }
        public bool? SalesVoucherDetailsIsEdit { get; set; }
        public int? SalesVoucherDetailsDlvId { get; set; }
        public int? SalesVoucherDetailsDlvDId { get; set; }
        public int? SalesVoucherDetailsRfqId { get; set; }
        public int? SalesVoucherDetailsRfqdId { get; set; }
        public int? SalesVoucherDetailsEnqId { get; set; }
        public int? SalesVoucherDetailsEnqDId { get; set; }
        public int? SalesVoucherDetailsSoId { get; set; }
        public int? SalesVoucherDetailsSodId { get; set; }
        public int? SalesVoucherDetailsQtnId { get; set; }
        public int? SalesVoucherDetailsQtndId { get; set; }
        public decimal? SalesVoucherDetailsCostPrice { get; set; }
        public bool? SalesVoucherDetailsDelStatus { get; set; }
    }
}
