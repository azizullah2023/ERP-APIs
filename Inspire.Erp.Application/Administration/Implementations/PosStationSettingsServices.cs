

using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Administration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspire.Erp.Domain.Modals;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Administration.Implementations
{
    public class PosStationSettingsSevice : IPosStationSettingsServices
    {
        private readonly IRepository<PosStationSettings> _StationSettingsService;

        public PosStationSettingsSevice(IRepository<PosStationSettings> StationSettings)
        {
            _StationSettingsService = StationSettings;
        }

        public async Task<Response<List<PosStationSettings>>> DeleteCounter(int id)
        {
            try
            {
                _StationSettingsService.BeginTransaction();
                await Task.Delay(1000);
                var counter = _StationSettingsService.GetAll().FirstOrDefault(x => x.Id == id);

                if (counter != null)
                {
                    counter.DelStatus = true;
                }


                _StationSettingsService.Update(counter);

                _StationSettingsService.TransactionCommit();
                return await GetCounters();
            }
            catch (Exception ex)
            {
                _StationSettingsService.TransactionRollback();
                return Response<List<PosStationSettings>>.Fail(new List<PosStationSettings>(), ex.Message);
            }
        }

        public async Task<Response<PosStationSettings>> GetCounter(int code)
        {
            try
            {

                await Task.Delay(1000);
                var station = _StationSettingsService.GetAll().FirstOrDefault(x => x.Id == code);
                return Response<PosStationSettings>.Success(station, "Data found");
            }
            catch (Exception ex)
            {
                return Response<PosStationSettings>.Fail(new PosStationSettings(), ex.Message);
            }
        }
        public async Task<Response<List<PosStationSettings>>> GetCounters()
        {
            try
            {
                await Task.Delay(1000);
                return Response<List<PosStationSettings>>.Success(
                    _StationSettingsService.GetAll().Where(x => x.DelStatus == false).ToList(),
                    "Data found"
                    );
            }
            catch (Exception ex)
            {
                return Response<List<PosStationSettings>>.Fail(new List<PosStationSettings>(), ex.Message);
            }
        }

        public PosStationSettings InsterCounter(PosStationSettings counter)
        {
            try
            {
                _StationSettingsService.BeginTransaction();

                int maxcount = 0;
                maxcount = (
                _StationSettingsService.GetAsQueryable()
                .DefaultIfEmpty().Max(o => o == null ? 0 : (int)o.Id) + 1);
                counter.Id = maxcount;

                _StationSettingsService.Insert(counter);

                _StationSettingsService.TransactionCommit();
                return counter;
            }
            catch (Exception ex)
            {
                _StationSettingsService.TransactionRollback();
                return new PosStationSettings();
            }
        }

        public PosStationSettings UpdateCounter(PosStationSettings counter)
        {
            try
            {
                _StationSettingsService.BeginTransaction();

                _StationSettingsService.Update(counter);

                _StationSettingsService.TransactionCommit();
                return counter;
            }
            catch (Exception ex)
            {
                _StationSettingsService.TransactionRollback();
                return new PosStationSettings();
            }
        }
        public IEnumerable<PosStationSettings> GetAllCounter()
        {
            IEnumerable<PosStationSettings> stationMasters;
            try
            {
                stationMasters = _StationSettingsService.GetAll().Where(x => x.DelStatus == false).ToList();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return stationMasters;
        }

    }
}
