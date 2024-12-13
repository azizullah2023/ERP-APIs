using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master.Implementations
{
    public class BankGuaranteeMasterService : IBankGuaranteeMasterService
    {
        private IRepository<BankGuaranteeMaster> bankGuaranteerepository;
        public BankGuaranteeMasterService(IRepository<BankGuaranteeMaster> _bankGuaranteerepository)
        {
            bankGuaranteerepository = _bankGuaranteerepository;
        }
        public IEnumerable<BankGuaranteeMaster> InsertBankGuarantee(BankGuaranteeMaster bankGuaranteeMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int? maxcount = 0;
                maxcount = bankGuaranteerepository.GetAsQueryable().Where(k => k.BankGuaranteeMasterBgid != null).Select(x => x.BankGuaranteeMasterBgid).Max();
                if (maxcount == null)
                {
                    maxcount = 1;
                }
                else
                {
                    maxcount = maxcount + 1;

                }
                bankGuaranteeMaster.BankGuaranteeMasterBgid = maxcount;
                bankGuaranteerepository.Insert(bankGuaranteeMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //bankGuaranteerepository.Dispose();
            }
            return this.GetAllBankGuarantee();
        }
        public IEnumerable<BankGuaranteeMaster> UpdateBankGuarantee(BankGuaranteeMaster bankGuaranteeMaster)
        {
            bool valid = false;
            try
            {
                bankGuaranteerepository.Update(bankGuaranteeMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //bankGuaranteerepository.Dispose();
            }
            return this.GetAllBankGuarantee();
        }
        public IEnumerable<BankGuaranteeMaster> DeleteBankGuarantee(BankGuaranteeMaster bankGuaranteeMaster)
        {
            bool valid = false;
            try
            {
                bankGuaranteerepository.Delete(bankGuaranteeMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //bankGuaranteerepository.Dispose();
            }
            return this.GetAllBankGuarantee();
        }

        public IEnumerable<BankGuaranteeMaster> GetAllBankGuarantee()
        {
            IEnumerable<BankGuaranteeMaster> bankGuaranteeMasters;
            try
            {
                bankGuaranteeMasters = bankGuaranteerepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //bankGuaranteerepository.Dispose();
            }
            return bankGuaranteeMasters;
        }
    }
}
