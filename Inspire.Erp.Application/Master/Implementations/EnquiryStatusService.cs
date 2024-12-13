using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master

{
    public class EnquiryStatusService : IEnquiryStatusService
    {
        private IRepository<EnquiryStatus> enquiryStatusrepository;
        public EnquiryStatusService(IRepository<EnquiryStatus> _enquiryStatusrepository)
        {
            enquiryStatusrepository = _enquiryStatusrepository;
        }
        public IEnumerable<EnquiryStatus> InsertEnquiryStatus(EnquiryStatus enquiryStatus)
        {
            bool valid = false;
            try
            {
                valid = true;

                enquiryStatus.EnquiryStatusEnquiryStatusId = Convert.ToInt32(enquiryStatusrepository.GetAsQueryable()
                                                       .Where(x => x.EnquiryStatusEnquiryStatusId > 0)
                                                       .DefaultIfEmpty()
                                                       .Max(o => o == null ? 0 : o.EnquiryStatusEnquiryStatusId)) + 1;
                enquiryStatusrepository.Insert(enquiryStatus);
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
            return this.GetAllEnquiryStatus();
        }
        public IEnumerable<EnquiryStatus> UpdateEnquiryStatus(EnquiryStatus enquiryStatus)
        {
            bool valid = false;
            try
            {
                enquiryStatusrepository.Update(enquiryStatus);
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
            return this.GetAllEnquiryStatus();
        }
        public IEnumerable<EnquiryStatus> DeleteEnquiryStatus(EnquiryStatus enquiryStatus)
        {
            bool valid = false;
            try
            {
                enquiryStatusrepository.Delete(enquiryStatus);
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
            return this.GetAllEnquiryStatus();
        }

        public IEnumerable<EnquiryStatus> GetAllEnquiryStatus()
        {
            IEnumerable<EnquiryStatus> enquiryStatus;
            try
            {
                enquiryStatus = enquiryStatusrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return enquiryStatus;
        }

        public IEnumerable<EnquiryStatus> GetAllEnquiryStatusById(int id)
        {
            IEnumerable<EnquiryStatus> enquiryStatus;
            try
            {
                enquiryStatus = enquiryStatusrepository.GetAsQueryable().Where(k => k.EnquiryStatusEnquiryStatusId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return enquiryStatus;

        }

    }
}
