using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master.Interfaces
{
    public interface IModelMasterService
    {
        public IEnumerable<ModelMaster> InsertModel(ModelMaster modelMaster);
        public IEnumerable<ModelMaster> UpdateModel(ModelMaster modelMaster);
        public IEnumerable<ModelMaster> DeleteModel(ModelMaster modelMaster);
        public IEnumerable<ModelMaster> GetAllModel();
        public IEnumerable<ModelMaster> GetAllModelById(int id);

    }
}
