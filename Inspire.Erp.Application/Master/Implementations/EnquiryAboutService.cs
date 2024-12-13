using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master

{
    public class EnquiryAboutService : IEnquiryAboutService
    {
        private IRepository<EnquiryAbout> enquiryAboutrepository;
        public EnquiryAboutService(IRepository<EnquiryAbout> _EnquiryAboutrepository)
        {
            enquiryAboutrepository = _EnquiryAboutrepository;
        }
        public IEnumerable<EnquiryAbout> InsertEnquiryAbout(EnquiryAbout enquiryAbout)
        {
            bool valid = false;
            try
            {
                valid = true;
                
                enquiryAbout.EnquiryAboutEnquiryAboutId = Convert.ToInt32(enquiryAboutrepository.GetAsQueryable()
                                                       .Where(x => x.EnquiryAboutEnquiryAboutId > 0)
                                                       .DefaultIfEmpty()
                                                       .Max(o => o == null ? 0 : o.EnquiryAboutEnquiryAboutId)) + 1;
                enquiryAboutrepository.Insert(enquiryAbout);
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
            return this.GetAllEnquiryAbout();
        }
        public IEnumerable<EnquiryAbout> UpdateEnquiryAbout(EnquiryAbout enquiryAbout)
        {
            bool valid = false;
            try
            {
                enquiryAboutrepository.Update(enquiryAbout);
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
            return this.GetAllEnquiryAbout();
        }
        public IEnumerable<EnquiryAbout> DeleteEnquiryAbout(EnquiryAbout enquiryAbout)
        {
            bool valid = false;
            try
            {
                enquiryAboutrepository.Delete(enquiryAbout);
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
            return this.GetAllEnquiryAbout();
        }

        public IEnumerable<EnquiryAbout> GetAllEnquiryAbout()
        {
            IEnumerable<EnquiryAbout> enquiryAbout;
            try
            {
                enquiryAbout = enquiryAboutrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return enquiryAbout;
        }

        public IEnumerable<EnquiryAbout> GetAllEnquiryAboutById(int id)
        {
            IEnumerable<EnquiryAbout> enquiryAbout;
            try
            {
                enquiryAbout = enquiryAboutrepository.GetAsQueryable().Where(k => k.EnquiryAboutEnquiryAboutId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return enquiryAbout;

        }

    }
}
