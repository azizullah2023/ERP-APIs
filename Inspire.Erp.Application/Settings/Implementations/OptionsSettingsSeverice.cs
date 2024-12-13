using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Inspire.Erp.Application.Settings.Implementations
{
    public class OptionsSettingsSeverice : IOptionsSettings
    {
        private IRepository<OptionsMaster> _optionsSettingsRepo;
        public OptionsSettingsSeverice(IRepository<OptionsMaster> optionsSettingsRepo)
        {
            _optionsSettingsRepo = optionsSettingsRepo;
        }

        public async Task<Response<List<OptionsMaster>>> GetAllOptionSettings()
        {
            List <OptionsMaster> optionsMasters = new List < OptionsMaster >();
            try
            {
                optionsMasters = await _optionsSettingsRepo.GetAsQueryable().ToListAsync();
                return Response<List<OptionsMaster>>.Success(optionsMasters, "Data found");
            }catch (Exception ex)
            {
                return Response<List<OptionsMaster>>.Fail(optionsMasters, "Data not found");
            }
        }

        public async Task<IEnumerable<OptionsMaster>> GetOptionsSettings(string search)
        {
            IEnumerable<OptionsMaster> optionsSettings = _optionsSettingsRepo.GetAsQueryable().Where(x => string.IsNullOrEmpty(search)?true: x.OptionsMasterFullDescription.Contains(search));
            return optionsSettings;
        }

        public async Task<Response<List<OptionsMaster>>>  GetOptionsSettingsbyKey(string key)
        {

            IEnumerable<OptionsMaster> optionsSettings;
            try
            {
                optionsSettings = await  _optionsSettingsRepo.GetAsQueryable().Where(k => k.OptionsMasterDescription.Trim() == key).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return Response<List<OptionsMaster>>.Success(optionsSettings.ToList(), "Data found");
        }

        public bool ImportOptionsSettingsFromFile(List<OptionsMaster> optionsSettings)
        {
            try
            {
                if (optionsSettings.Count > 0)
                {
                    _optionsSettingsRepo.InsertList(optionsSettings);
                }
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean updateOptionsSettings(List<OptionsMaster> optionsSettings)
        {
            try
            {
                _optionsSettingsRepo.UpdateList(optionsSettings);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
