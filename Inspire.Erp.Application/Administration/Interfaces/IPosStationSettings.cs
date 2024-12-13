using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Administration.Interfaces
{
    public interface IPosStationSettingsServices
    {
        public IEnumerable<PosStationSettings> GetAllCounter();
        public PosStationSettings InsterCounter(PosStationSettings counter);
        public PosStationSettings UpdateCounter(PosStationSettings counter);
        public Task<Response<List<PosStationSettings>>> DeleteCounter(int id);
        public Task<Response<List<PosStationSettings>>> GetCounters();
        public Task<Response<PosStationSettings>> GetCounter(int id);

    }
}

