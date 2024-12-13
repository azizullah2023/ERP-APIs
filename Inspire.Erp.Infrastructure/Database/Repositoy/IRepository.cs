using IInspire.Erp.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Infrastructure.Database.Repositoy
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetAsQueryable();
        //T Get(long id);
        void Insert(T entity);


        IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        void InsertList(List<T> entity);
        void Update(T entity);

        public void UpdateAsync(T entity);
        void UpdateList(List<T> entity);
        void Delete(T entity);
        void DeleteList(List<T> entity);
        public void SaveChangesAsync();
        void BeginTransaction();
        void TransactionCommit();
        void TransactionRollback();
        Task<int> ExecuteSpNonQuery(string sql, params object[] parameters);
        Task<List<T>> ExecuteSpReader(string sql, params object[] parameters);
        Task<int> ExecuteSpNonQueryWithNoParam(string sql);
        Task<List<T>> ExecuteSpReaderWithNoParam(string sql);
        void Dispose();

        ///  Others Added

        IEnumerable<T> GetBySP(FormattableString query);
        public IEnumerable<T> GetBySPParams(FormattableString query);
        Task<T> GetBySPWithParametersFirstNOSelect(FormattableString query);
        Task<TEntity> GetBySPWithParametersFirst<TEntity>(FormattableString query, Expression<Func<T, TEntity>> select);
        Task<List<TEntity>> GetBySPWithParameters<TEntity>(FormattableString query, Expression<Func<T, TEntity>> select);
        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<T, bool>> predicate, Expression<Func<T, TEntity>> select, List<string> includes = null);
        Task<List<TEntity>> ListSelectAsync<TEntity>(Expression<Func<T, bool>> predicate, Expression<Func<T, TEntity>> select, List<string> includes = null);
        void Commit();
        Task<List<EntityEntry>> TrackChangesAsync();

        ////
    }
}