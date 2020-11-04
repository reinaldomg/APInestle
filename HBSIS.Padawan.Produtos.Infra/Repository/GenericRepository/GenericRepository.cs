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
        protected readonly DbSet<TEntity> DbSet;
        
        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }
        
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<bool> ExistsByIdAsync(Guid Id)
        {
            var entity = await DbSet.FindAsync(Id);
            
            if (entity != null) 
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"A sua {entity} é nula");
            DbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"A sua {entity} é nula");
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        protected IQueryable Query() => DbSet.AsNoTracking();
    }
}