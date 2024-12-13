using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Application.Master
{
    public class UnitMasterSevice : IUnitMasterService
    {
        private IRepository<UnitMaster> unitrepository;
        public readonly InspireErpDBContext _Context;
        public UnitMasterSevice(IRepository<UnitMaster> _unitrepository, InspireErpDBContext Context)
        {
            unitrepository = _unitrepository;
            _Context = Context;
        }
        public IEnumerable<UnitMaster> InsertUnit(UnitMaster unitMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = (int)unitrepository.GetAsQueryable().Where(k => k.UnitMasterUnitId != null).Select(x => x.UnitMasterUnitId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                unitMaster.UnitMasterUnitId = mxc;
                unitrepository.Insert(unitMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //unitrepository.Dispose();
            }
            return this.GetAllUnit();
        }
        public IEnumerable<UnitMaster> UpdateUnit(UnitMaster unitMaster)
        {
            bool valid = false;
            try
            {
                unitrepository.Update(unitMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //unitrepository.Dispose();
            }
            return this.GetAllUnit();
        }
        public IEnumerable<UnitMaster> DeleteUnit(UnitMaster unitMaster)
        {
            bool valid = false;
            try
            {
                unitrepository.Delete(unitMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //unitrepository.Dispose();
            }
            return this.GetAllUnit();
        }
        public IEnumerable<UnitMaster> GetAllUnit()
        {
            IEnumerable<UnitMaster> unitMasters;
            try
            {
                unitMasters = unitrepository.GetAsQueryable().Include(x => x.UnitDetails);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //unitrepository.Dispose();
            }
            return unitMasters;
        }
        public IEnumerable<UnitMaster> GetAllUnitById(int id)
        {
            IEnumerable<UnitMaster> unitMasters;
            try
            {
                unitMasters = unitrepository.GetAsQueryable().Where(k => k.UnitMasterUnitId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return unitMasters;

        }


        public async Task<Response<List<GetUnitDetailsMasterList>>> GetUnitDetailsByItemId(long itemId)
        {
            try
            {
                var results = await (from cpod in _Context.UnitDetails
                                     join cpo in _Context.UnitMaster on cpod.UnitDetailsUnitId equals cpo.UnitMasterUnitId
                                     where cpod.UnitDetailsItemId == itemId
                                     select new GetUnitDetailsMasterList
                                     {
                                         UnitMasterUnitId = cpo.UnitMasterUnitId,
                                         UnitDetailsId = cpod.UnitDetailsId,
                                         UnitMasterUnitShortName = cpo.UnitMasterUnitShortName.Trim(),
                                         UnitMasterUnitFullName = cpo.UnitMasterUnitFullName == null ? "" : cpo.UnitMasterUnitFullName.Trim(),
                                         UnitMasterUnitDescription = cpo.UnitMasterUnitDescription == null ? "" : cpo.UnitMasterUnitDescription,
                                         UnitMasterUnitStatus = cpo.UnitMasterUnitStatus == null ? false : cpo.UnitMasterUnitStatus,
                                         UnitDetailsConversionType = cpod.UnitDetailsConversionType == null ? 0 : cpod.UnitDetailsConversionType,
                                         UnitDetailsRate = cpod.UnitDetailsRate == null ? 0 : cpod.UnitDetailsRate
                                     }).ToListAsync();

                return Response<List<GetUnitDetailsMasterList>>.Success(results, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<GetUnitDetailsMasterList>>.Fail(new List<GetUnitDetailsMasterList>(), ex.Message);
            }
        }

    }
}
