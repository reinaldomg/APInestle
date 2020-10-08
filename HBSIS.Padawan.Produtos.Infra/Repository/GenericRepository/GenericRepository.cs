using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository
{
    public abstract class GenericRepository<TEntity>
        : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MainContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        
        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = dbContext.Set<TEntity>();

        }
        
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"A sua {entity} é nula");
            
            _dbSet.Add(entity);

            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"A sua {entity} é nula");
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var del = _dbSet.Find(id);
            _dbSet.Remove(del);
            _dbContext.SaveChanges();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return (Task<IEnumerable<TEntity>>)_dbSet.AsAsyncEnumerable();
        }

        protected IQueryable Query() => _dbSet.AsNoTracking();
    }
}