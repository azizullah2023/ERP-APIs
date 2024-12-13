
using IInspire.Erp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Infrastructure.Database.Repositoy
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly InspireErpDBContext context;
        private IDbContextTransaction dbContextTransaction;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(InspireErpDBContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void BeginTransaction()
        {
            dbContextTransaction = context.Database.BeginTransaction();

        }

        public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = this.GetAsQueryable().Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public void TransactionCommit()
        {
            dbContextTransaction.Commit();
        }

        public void TransactionRollback()
        {
            dbContextTransaction.Rollback();
        }

        public IQueryable<T> GetAsQueryable()
        {
            return entities.AsQueryable();
        }
        
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }



        public void InsertList(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            context.SaveChanges();
        }

        public void DeleteList(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.RemoveRange(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChangesAsync();
        }

        public void UpdateList(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.UpdateRange(entity);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception Message: " + ex.InnerException.Message);
                }
                throw;
            }
            
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            context.SaveChangesAsync();
        }


        private bool disposed = false;



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> ExecuteSpNonQuery(string sql, params object[] parameters )
        {
            return await context.Database.ExecuteSqlRawAsync("Exec " + sql, parameters);
        }
        public async Task<List<T>> ExecuteSpReader(string sql, params object[] parameters)
        {
            return await entities.FromSqlRaw("Exec " + sql, parameters).ToListAsync();
        }

        public async Task<int> ExecuteSpNonQueryWithNoParam(string sql)
        {
            return await context.Database.ExecuteSqlRawAsync("Exec " + sql);
        }
        public async Task<List<T>> ExecuteSpReaderWithNoParam(string sql)
        {
            return await entities.FromSqlRaw("Exec " + sql).ToListAsync();
        }

        //// others Add

        public IEnumerable<T> GetBySP(FormattableString query)
        {
            return context.Set<T>().FromSqlInterpolated(query);
        }
        public IEnumerable<T> GetBySPParams(FormattableString query)
        {
            try
            {
                return context.Set<T>().FromSqlInterpolated(query);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public async Task<List<TEntity>> GetBySPWithParameters<TEntity>(FormattableString query, Expression<Func<T, TEntity>> select)
        {
            return await context.Set<T>().FromSqlInterpolated(query).Select(select).ToListAsync();
        }
        public async Task<TEntity> GetBySPWithParametersFirst<TEntity>(FormattableString query, Expression<Func<T, TEntity>> select)
        {
            var result = await context.Set<T>().FromSqlInterpolated(query).Select(select).ToListAsync();
            return result.FirstOrDefault();
        }
        public async Task<T> GetBySPWithParametersFirstNOSelect(FormattableString query)
        {
            var result = await context.Set<T>().FromSqlInterpolated(query).ToListAsync();
            return result.FirstOrDefault();
        }
       
        public async Task<List<TEntity>> ListSelectAsync<TEntity>(Expression<Func<T, bool>> predicate, Expression<Func<T, TEntity>> select, List<string> includes = null)
        {
            IQueryable<T> query = entities;
            if (includes != null && includes.Count > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            }
            return await query.Where(predicate).Select(select).ToListAsync();
        }
        public async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<T, bool>> predicate, Expression<Func<T, TEntity>> select, List<string> includes = null)
        {
            IQueryable<T> query = entities;
            if (includes != null && includes.Count > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            }
            return await query.Where(predicate).Select(select).FirstOrDefaultAsync();
        }
        public void Commit()
        {
            context.SaveChanges();
        }
       
        public async Task<List<EntityEntry>> TrackChangesAsync()
        {

            return await Task.FromResult(context.ChangeTracker.Entries().ToList());
        }



        ////

    }
}