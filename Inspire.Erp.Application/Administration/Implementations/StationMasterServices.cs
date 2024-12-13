

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
    public class StationMasterService : IStationMasterServices
    {
        private readonly IRepository<StationMaster> stationMaster;

        public StationMasterService(IRepository<StationMaster> stationMaster)
        {
            this.stationMaster = stationMaster;
        }

        public async Task<Response<List<StationMaster>>> DeleteStationMaster(StationMaster stationMaster)
        {
            try
            {
                await Task.Delay(1000);
                var station = this.stationMaster.GetAll().FirstOrDefault(x => x.StationMasterCode == stationMaster.StationMasterCode);
                station.StationMasterStationName = stationMaster.StationMasterStationName;
                station.StationMasterAddress = stationMaster.StationMasterAddress;
                station.StationMasterCity = stationMaster.StationMasterCity;
                station.StationMasterPostOffice = stationMaster.StationMasterPostOffice;
                station.StationMasterTele1 = stationMaster.StationMasterTele1;
                station.StationMasterTele2 = stationMaster.StationMasterTele2;
                station.StationMasterFax = stationMaster.StationMasterFax;
                station.StationMasterEmail = stationMaster.StationMasterEmail;
                station.StationMasterWebSite = stationMaster.StationMasterWebSite;
                station.StationMasterCountry = stationMaster.StationMasterCountry;
                station.StationMasterLogoPath = stationMaster.StationMasterLogoPath;
                station.StationMasterSignPath = stationMaster.StationMasterSignPath;
                station.StationMasterLogoImg = stationMaster.StationMasterLogoImg;
                station.StationMasterSignImg = stationMaster.StationMasterSignImg;
                station.StationMasterSealImg = stationMaster.StationMasterSealImg;
                station.StationMasterVatNo = stationMaster.StationMasterVatNo;
                station.StationMasterDelStatus = true;
                station.BankName = stationMaster.BankName;
                station.AccountName = stationMaster.AccountName;
                station.AccountNo = stationMaster.AccountNo;
                station.IBAN = stationMaster.IBAN;
                station.SwiftCode = stationMaster.SwiftCode;
                station.BankBranch = stationMaster.BankBranch;
                station.BankCurrency = stationMaster.BankCurrency;
                station.logotype = stationMaster.logotype;
                this.stationMaster.Update(station);
                return await GetStationMasters();
            }
            catch (Exception ex)
            {
                return Response<List<StationMaster>>.Fail(new List<StationMaster>(), ex.Message);
            }
        }

        public async Task<Response<StationMaster>> GetStationMaster(int code)
        {
            try
            {
                await Task.Delay(1000);
                var station = this.stationMaster.GetAll().FirstOrDefault(x => x.StationMasterCode == code);
                return Response<StationMaster>.Fail(station, "Company were deleted successfully");
            }
            catch (Exception ex)
            {
                return Response<StationMaster>.Fail(new StationMaster(), ex.Message);
            }
        }
        public async Task<Response<List<StationMaster>>> GetStationMasters()
        {
            try
            {
                await Task.Delay(1000);
                return Response<List<StationMaster>>.Success(
                    this.stationMaster.GetAll().ToList(),
                    "Data found"
                    );
            }
            catch (Exception ex)
            {
                return Response<List<StationMaster>>.Fail(new List<StationMaster>(), ex.Message);
            }
        }

        public async Task<Response<List<StationMaster>>> InsertStationMaster(StationMaster stationMaster)
        {
            try
            {
                await Task.Delay(1000);
                //stationMaster.StationMasterCode = this.stationMaster.GetAll().Max(x => x.StationMasterCode) + 1;
                this.stationMaster.Insert(stationMaster);
                return await GetStationMasters();
            }
            catch (Exception ex)
            {
                return Response<List<StationMaster>>.Fail(new List<StationMaster>(), ex.Message);
            }
        }

        public async Task<Response<List<StationMaster>>> UpdateStationMaster(StationMaster stationMaster)
        {
            try
            {
                await Task.Delay(1000);
                var station = this.stationMaster.GetAll().FirstOrDefault(x => x.StationMasterCode == stationMaster.StationMasterCode);
                station.StationMasterStationName = stationMaster.StationMasterStationName;
                station.StationMasterAddress = stationMaster.StationMasterAddress;
                station.StationMasterCity = stationMaster.StationMasterCity;
                station.StationMasterPostOffice = stationMaster.StationMasterPostOffice;
                station.StationMasterTele1 = stationMaster.StationMasterTele1;
                station.StationMasterTele2 = stationMaster.StationMasterTele2;
                station.StationMasterFax = stationMaster.StationMasterFax;
                station.StationMasterEmail = stationMaster.StationMasterEmail;
                station.StationMasterWebSite = stationMaster.StationMasterWebSite;
                station.StationMasterCountry = stationMaster.StationMasterCountry;
                station.StationMasterLogoPath = stationMaster.StationMasterLogoPath;
                station.StationMasterSignPath = stationMaster.StationMasterSignPath;
                station.StationMasterLogoImg = stationMaster.StationMasterLogoImg;
                station.StationMasterSignImg = stationMaster.StationMasterSignImg;
                station.StationMasterSealImg = stationMaster.StationMasterSealImg;
                station.StationMasterVatNo = stationMaster.StationMasterVatNo;
                station.StationMasterDelStatus = stationMaster.StationMasterDelStatus;
                station.BankName = stationMaster.BankName;
                station.AccountName = stationMaster.AccountName;
                station.AccountNo = stationMaster.AccountNo;
                station.IBAN = stationMaster.IBAN;
                station.SwiftCode = stationMaster.SwiftCode;
                station.BankBranch = stationMaster.BankBranch;
                station.BankCurrency = stationMaster.BankCurrency;
                station.logotype = stationMaster.logotype;                
                this.stationMaster.Update(station);
                return await GetStationMasters();
            }
            catch (Exception ex)
            {
                return Response<List<StationMaster>>.Fail(new List<StationMaster>(), ex.Message);
            }
        }
        public IEnumerable<StationMaster> GetAllStation()
        {
            IEnumerable<StationMaster> stationMasters;
            try
            {
                stationMasters = this.stationMaster.GetAll().Where(x=>x.StationMasterDelStatus == false).ToList();

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
