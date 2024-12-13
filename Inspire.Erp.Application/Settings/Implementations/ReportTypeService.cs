using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Settings.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Administration;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Implementations
{
    public class ReportTypeService : IReportTypeService
    {
        private readonly IRepository<ReportType> _reportType;
        private readonly IUtilityService _utilityService;
        public ReportTypeService(IUtilityService utilityService, IRepository<ReportType> reportType)
        {
            _utilityService = utilityService;
            _reportType = reportType;
        }
        public async Task<Response<bool>> AddEditReportType(AddEditReportTypeResponse model)
        {
            try
            {
                if (model.ReportTypeId==0)
                {
                    var reportType = new ReportType()
                    {
                        ReportTypeIsImage=model.ReportTypeIsImage,
                        ReportTypeDescription=model.ReportTypeDescription,
                        ReportTypeFileName=model.ReportTypeFileName,
                        ReportTypeIsDefault=model.ReportTypeIsDefault,
                        ReportTypeDelStatus=false,
                        ReportTypeIsVisible=model.ReportTypeIsVisible,
                        ReportTypeVoucherName=model.ReportTypeVoucherName
                    };
                    _reportType.Insert(reportType);

                }
                else
                {
                    var reportType= await _reportType.GetAsQueryable().Where(x => x.ReportTypeId == model.ReportTypeId).FirstOrDefaultAsync();
                    reportType.ReportTypeIsImage = model.ReportTypeIsImage;
                    reportType.ReportTypeDescription = model.ReportTypeDescription;
                    reportType.ReportTypeFileName = model.ReportTypeFileName;
                    reportType.ReportTypeIsDefault = model.ReportTypeIsDefault;
                    reportType.ReportTypeIsVisible = model.ReportTypeIsVisible;
                    reportType.ReportTypeVoucherName = model.ReportTypeVoucherName;
                    _reportType.Update(reportType);
                }
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Report Type",
                    Section = "Update Report Type",
                    Entity = "Report Type",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                _reportType.SaveChangesAsync();
                return Response<bool>.Success(true, "Successfully Saved");
            }
            catch (Exception ex)
            {

                return Response<bool>.Fail(false, ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<GetReportTypeMasterResponse>>>> GetReportTypes(GenericGridViewModel model)
        {
            try
            {
                string query = @$" 1==1 {model.Filter}";
                //var a = await _accountTransaction.GetAsQueryable().Where(x =>( x.AccountsTransactionsTransDate >= Convert.ToDateTime("")) &&  )
                //    .ToListAsync();
                var result = await _reportType.GetAsQueryable().Where(query).Select(x => new GetReportTypeMasterResponse
                {
                    ReportTypeId=x.ReportTypeId,
                    ReportTypeIsImage = x.ReportTypeIsImage,
                    ReportTypeDescription = x.ReportTypeDescription,
                    ReportTypeFileName = x.ReportTypeFileName,
                    ReportTypeIsDefault = x.ReportTypeIsDefault,
                    ReportTypeIsVisible = x.ReportTypeIsVisible,
                    ReportTypeVoucherName = x.ReportTypeVoucherName

                }).ToListAsync();

                var gridReponse = new GridWrapperResponse<List<GetReportTypeMasterResponse>>();
                gridReponse.Data = result;
                var total = 0;
                gridReponse.Total = Convert.ToInt32(total);
                return Response< GridWrapperResponse<List<GetReportTypeMasterResponse>>>.Success(gridReponse, "Successfully Saved");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetReportTypeMasterResponse>>>.Fail(new GridWrapperResponse<List<GetReportTypeMasterResponse>>(), ex.Message);
            }
        }
        public async Task<Response<AddEditReportTypeResponse>> GetSpecificVoucherType(int id = 0)
        {
            try
            {
                var reportType = new AddEditReportTypeResponse();
                    var reportTypeData = await _reportType.GetAsQueryable().Where(x => x.ReportTypeId == id).FirstOrDefaultAsync();
                    reportType.ReportTypeIsImage = reportTypeData.ReportTypeIsImage;
                reportType.ReportTypeId = reportTypeData.ReportTypeId;
                reportType.ReportTypeDescription = reportTypeData.ReportTypeDescription;
                    reportType.ReportTypeFileName = reportTypeData.ReportTypeFileName;
                    reportType.ReportTypeIsDefault = reportTypeData.ReportTypeIsDefault;
                    reportType.ReportTypeIsVisible = reportTypeData.ReportTypeIsVisible;
                    reportType.ReportTypeVoucherName = reportTypeData.ReportTypeVoucherName;
                return Response<AddEditReportTypeResponse>.Success(reportType, "Successfully Saved");
            }
            catch (Exception ex)
            {

                return Response<AddEditReportTypeResponse>.Fail(new AddEditReportTypeResponse(), ex.Message);
            }
        }
        public async Task<Response<bool>> DeleteReportType(List<int> ids)
        {
            try
            {
                var reportTypeData = await _reportType.GetAsQueryable().Where(x => ids.Contains(x.ReportTypeId)).ToListAsync();
                reportTypeData.ForEach((x) =>
                {
                    x.ReportTypeDelStatus = true;
                });
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Report Type",
                    Section = "Delete Report Type",
                    Entity = "Report Type",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                _reportType.SaveChangesAsync();
                return Response<bool>.Success(true, "Successfully Saved");
            }
            catch (Exception ex)
            {

                return Response<bool>.Fail(false, ex.Message);
            }
        }
    }
}
