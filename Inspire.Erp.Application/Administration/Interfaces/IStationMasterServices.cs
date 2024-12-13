////using System;
////using System.Collections.Generic;
////using System.Text;

////namespace Inspire.Erp.Application.Administration.Interfaces
////{
////    class IStationMasterServices
////    {
////    }
////}

using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Administration.Interfaces
{
    public interface IStationMasterServices
    {
        public IEnumerable<StationMaster> GetAllStation();
        public Task<Response<List<StationMaster>>> InsertStationMaster(StationMaster stationMaster);
        public Task<Response<List<StationMaster>>> UpdateStationMaster(StationMaster stationMaster);
        public Task<Response<List<StationMaster>>> DeleteStationMaster(StationMaster stationMaster);
        public Task<Response<List<StationMaster>>> GetStationMasters();
        public Task<Response<StationMaster>> GetStationMaster(int id);

    }
}

