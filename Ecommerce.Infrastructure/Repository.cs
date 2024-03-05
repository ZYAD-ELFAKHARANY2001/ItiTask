using Ecommerce.Application.Contract;
using Ecommerce.Context;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : baseEntity
    {
        private readonly EcommerceDbcontext _ecommerceContext;
        private readonly DbSet<TEntity> _Dbset;
        public Repository(EcommerceDbcontext ecommerceContext)
        {
            _ecommerceContext = ecommerceContext;
            _Dbset = _ecommerceContext.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _Dbset.AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {

            return Task.FromResult(_Dbset.Remove(entity).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            var query = _Dbset.Where(entity => !entity.IsDeleted);

            // Convert the query to asynchronous by returning Task.FromResult
            return Task.FromResult(query);
            //return Task.FromResult(_Dbset.Select(s => !s.IsDeleted));
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _Dbset.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _ecommerceContext.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(_Dbset.Update(entity).Entity);
        }
    }
}
