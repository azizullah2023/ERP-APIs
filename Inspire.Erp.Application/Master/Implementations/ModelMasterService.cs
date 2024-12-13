using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Master.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Inspire.Erp.Application.Master.Implementations
{
    public class ModelMasterService : IModelMasterService
    {
        private IRepository<ModelMaster> modelrepository;
        public ModelMasterService(IRepository<ModelMaster> _modelrepository)
        {
            modelrepository = _modelrepository;
        }
        public IEnumerable<ModelMaster> InsertModel(ModelMaster modelMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = Convert.ToInt32(modelrepository.GetAsQueryable()
                                  .Where(x => x.ModelMasterId > 0)
                                  .DefaultIfEmpty()
                                  .Max(o => o == null ? 0 : o.ModelMasterId)) + 1;

               modelMaster.ModelMasterId = mxc;

                modelrepository.Insert(modelMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return this.GetAllModel();
        }
        public IEnumerable<ModelMaster> UpdateModel(ModelMaster modelMaster)
        {
            bool valid = false;
            try
            {
                modelrepository.Update(modelMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                
            }
            return this.GetAllModel();
        }
        public IEnumerable<ModelMaster> DeleteModel(ModelMaster modelMaster)
        {
            bool valid = false;
            try
            {
                modelrepository.Delete(modelMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                
            }
            return this.GetAllModel();
        }

        public IEnumerable<ModelMaster> GetAllModel()
        {
            IEnumerable<ModelMaster> modelMasters;
            try
            {
                modelMasters = modelrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                
            }
            return modelMasters;
        }

        public IEnumerable<ModelMaster> GetAllModelById(int id)
        {
            IEnumerable<ModelMaster> modelMasters;
            try
            {
                modelMasters = modelrepository.GetAsQueryable().Where(k => k.ModelMasterId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return modelMasters;

        }

    }
}
