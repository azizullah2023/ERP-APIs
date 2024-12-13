using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master.Interfaces
{
    public interface IRouteMasterService
    {
        public IEnumerable<RouteMaster> InsertRouteMaster(RouteMaster routeMaster);
        public IEnumerable<RouteMaster> UpdateRouteMaster(RouteMaster routeMaster);
        public IEnumerable<RouteMaster> DeleteRouteMaster(RouteMaster routeMaster);
        public IEnumerable<RouteMaster> GetAllRoute();
        public IEnumerable<RouteMaster> GetAllRouteById(int id);
    }
}
