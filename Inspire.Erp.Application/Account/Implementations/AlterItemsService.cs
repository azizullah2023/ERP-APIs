using AutoMapper.Configuration.Annotations;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.StoreWareHouse.Implementation
{
    public class AlterItemsService : IAlterItemsService
    {
        private IRepository<AltItemMaster> _grnRepository;
        private IRepository<AltItemDetails> _grndetailRepository;
        private readonly InspireErpDBContext _context;

        public AlterItemsService(
            IRepository<AltItemMaster> grnRepository, IRepository<AltItemDetails> grndetailRepository,
            InspireErpDBContext context)
        {
            _grndetailRepository = grndetailRepository;
            _grnRepository = grnRepository;
            _context = context;
        }

        public AltItemMaster save(AltItemMaster grn)
        {
            try
            {
                _grnRepository.BeginTransaction();

                var details = grn.AltItemDetails;

                int id = 1;
                id = (_grnRepository.GetAsQueryable().AsNoTracking().Max(a => (int?)a.DocID) ?? 0) + 1;

                grn.DocID = id;               

                int dtId = 1;
                if (_grndetailRepository.GetAsQueryable().Any())
                {
                    dtId = _grndetailRepository.GetAsQueryable().Max(a => (int?)a.DetID ?? 0) + 1;
                }


                foreach (var item in details)
                {
                    item.DetID = dtId;
                    item.DocID = id;
                    _grndetailRepository.Insert(item);
                    dtId++;
                }
                _grnRepository.Insert(grn);
                _grnRepository.TransactionCommit();
                return this.GetByID(id);
            }
            catch (Exception ex)
            {
                _grnRepository.TransactionRollback();
                throw ex;
            }
        }


        public AltItemMaster update(AltItemMaster grn, List<AltItemDetails> altItemDetails)
        {
            try
            {
                _grnRepository.BeginTransaction();

                var details = altItemDetails;

                _grnRepository.Update(grn);

                int dtId = 1;
                dtId = _grndetailRepository.GetAsQueryable().AsNoTracking().Max(a => (int?)a.DetID ?? 0) + 1;

                foreach (var item in details)
                {
                    if (item.DetID == 0)
                    {
                        item.DetID = dtId;
                        item.DocID = grn.DocID;
                        _grndetailRepository.Insert(item);
                        dtId++;
                    }
                    else
                    {
                        item.DocID = grn.DocID;
                        _grndetailRepository.Update(item);
                    }
                }
                _grnRepository.TransactionCommit();
                return this.GetByID(grn.DocID);
            }
            catch (Exception ex)
            {
                _grnRepository.TransactionRollback();
                throw ex;
            }
        }

        public AltItemMaster GetByID(int id)
        {
            var data = _grnRepository.GetAsQueryable().AsNoTracking().Where(a => a.DocID == id).FirstOrDefault();
            if (data != null)
            {
                data.AltItemDetails = _grndetailRepository.GetAsQueryable().AsNoTracking().Where(a => a.DocID == id).ToList();
            }
            return data;
        }

        public IQueryable GetAll()
        {
            var data = _grnRepository.GetAsQueryable().AsNoTracking();
            return data;
        }

        public IQueryable Delete(int id)
        {
            var data = _grnRepository.GetAsQueryable().AsNoTracking().Where(a => a.DocID == id).FirstOrDefault();
            if (data != null)
            {
                data.AltItemDetails = _grndetailRepository.GetAsQueryable().AsNoTracking().Where(a => a.DocID == id).ToList();
            }
            _grndetailRepository.DeleteList(data.AltItemDetails);
            _grnRepository.Delete(data);
            return this.GetAll();
        }
    }
}
