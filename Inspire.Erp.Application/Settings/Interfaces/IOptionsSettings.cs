using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Settings.Interfaces
{
    public interface IOptionsSettings
    {
        Task<IEnumerable<OptionsMaster>> GetOptionsSettings(string search);
        Task<Response<List<OptionsMaster>>> GetOptionsSettingsbyKey(string key);
        Boolean updateOptionsSettings(List<OptionsMaster> optionsSettings);
        Boolean ImportOptionsSettingsFromFile(List<OptionsMaster> optionsSettings);
        Task<Response<List<OptionsMaster>>> GetAllOptionSettings();

    }
}
