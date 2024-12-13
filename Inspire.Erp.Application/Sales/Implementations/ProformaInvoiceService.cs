using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Sales.Interface;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Domain.Entities;
namespace Inspire.Erp.Application.Sales.Implementation
{
    public class ProformaInvoiceService : IProformaInvoice
    {
        private IRepository<ProformaInvoice> repository;
        private IRepository<ProformaInvoiceDetails> detailrepository;

        public ProformaInvoiceService(IRepository<ProformaInvoice> repository, IRepository<ProformaInvoiceDetails> _detailrepository)
        {
            this.repository = repository;
            detailrepository = _detailrepository;
        }
        public IEnumerable<ProformaInvoice> DeleteProformaInvoice(int Id)
        {
            try
            {
                var dataObj = repository.GetAsQueryable().Where(o => o.ProformaInvoiceId == Id).FirstOrDefault();
                dataObj.ProformaInvoiceDelStatus = true;
                var detailData = detailrepository.GetAsQueryable().Where(o => o.ProformaInvoiceDetailsSlNo == Id).ToList();
                detailData.ForEach(elemt =>
                {
                    elemt.ProformaInvoiceDetailsDelStatus = true;
                });
                repository.Update(dataObj);
                detailrepository.UpdateList(detailData);
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProformaInvoice GetProformaInvoiceById(int? id)
        {
            try
            {
                var proformaInvoice = new ProformaInvoice();
                proformaInvoice = repository.GetAsQueryable().FirstOrDefault(x => x.ProformaInvoiceId == id);
                proformaInvoice.ProformaInvoiceDetails = detailrepository.GetAsQueryable().Where(x => x.ProformaInvoiceDetailsSlNo == id).ToList();
                return proformaInvoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProformaInvoice> GetProformaInvoices()
        {
            try
            {
                var detailList = detailrepository.GetAll().ToList();
                var data = repository.GetAll().Select(o => new
                {
                    ProformaInvoice = o,
                    ProformaInvoiceDetails = detailList.Where(a => a.ProformaInvoiceDetailsSlNo == o.ProformaInvoiceId).ToList()
                }).ToList();
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProformaInvoice InsertProformaInvoice(ProformaInvoice proformaInvoice)
        {
            try
            {

                repository.BeginTransaction();
                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    repository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.ProformaInvoiceId) + 1);
                proformaInvoice.ProformaInvoiceId = maxcount;

                repository.Insert(proformaInvoice);
                proformaInvoice.ProformaInvoiceDetails.ForEach(elemt =>
                {
                    elemt.ProformaInvoiceDetailsSlNo = maxcount;
                });
                detailrepository.InsertList(proformaInvoice.ProformaInvoiceDetails);
                repository.TransactionCommit();
                return this.GetProformaInvoiceById(proformaInvoice.ProformaInvoiceId);
            }
            catch (Exception ex)
            {
                repository.TransactionRollback();
                throw ex;
            }
        }

        public ProformaInvoice UpdateProformaInvoice(ProformaInvoice proformaInvoice)
        {
            try
            {
                repository.BeginTransaction();
                repository.Update(proformaInvoice);

                proformaInvoice.ProformaInvoiceDetails.ForEach(elemt =>
                {
                    elemt.ProformaInvoiceDetailsSlNo = proformaInvoice.ProformaInvoiceId;
                });
                detailrepository.UpdateList(proformaInvoice.ProformaInvoiceDetails);
                repository.TransactionCommit();
                return this.GetProformaInvoiceById(proformaInvoice.ProformaInvoiceId);
            }
            catch (Exception ex)
            {
                repository.TransactionRollback();
                throw ex;
            }
        }
    }
}
