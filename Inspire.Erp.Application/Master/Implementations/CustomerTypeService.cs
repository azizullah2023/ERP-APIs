using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class CustomerTypeService : ICustomerTypeService
    {
        private IRepository<CustomerType> CustomerTyperepository;
        public CustomerTypeService(IRepository<CustomerType> _CustomerTyperepository)
        {
            CustomerTyperepository = _CustomerTyperepository;
        }
        public IEnumerable<CustomerType> InsertCustType(CustomerType customerType)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = CustomerTyperepository.GetAsQueryable().Where(k => k.CustomerTypeId != null).Select(x => x.CustomerTypeId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                customerType.CustomerTypeId = mxc;
                CustomerTyperepository.Insert(customerType);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllCustType();
        }
        public IEnumerable<CustomerType> UpdateCustType(CustomerType customerType)
        {
            bool valid = false;
            try
            {
                CustomerTyperepository.Update(customerType);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllCustType();
        }
        public IEnumerable<CustomerType> DeleteCustType(CustomerType customerType)
        {
            bool valid = false;
            try
            {
                CustomerTyperepository.Delete(customerType);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllCustType();
        }

        public IEnumerable<CustomerType> GetAllCustType()
        {
            IEnumerable<CustomerType> customerType;
            try
            {
                customerType = CustomerTyperepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return customerType;
        }

        public IEnumerable<CustomerType> GetAllCustTypeById(int id)
        {
            IEnumerable<CustomerType> customerType;
            try
            {
                customerType = CustomerTyperepository.GetAsQueryable().Where(k => k.CustomerTypeId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return customerType;

        }

    }
}
