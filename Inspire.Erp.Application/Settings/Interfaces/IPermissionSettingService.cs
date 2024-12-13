using Inspire.Erp.Domain.DTO.WorkGropPermission;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Interfaces
{
    public interface IPermissionSettingService
    {
        Response<List<WorkGroupPermissionsDto>> GetPermissionSetting(int? groupId);
        Task<Response<List<WorkGroupPermissionsDto>>> UpdatePermissionSetting(List<WorkGroupPermissions> permissionSetting);
    }
}
