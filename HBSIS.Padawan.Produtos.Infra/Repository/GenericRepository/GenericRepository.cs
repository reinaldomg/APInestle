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
        
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"A sua {entity} é nula");
            
            DbSet.Add(entity);

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
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        protected IQueryable Query() => DbSet.AsNoTracking();
    }
}