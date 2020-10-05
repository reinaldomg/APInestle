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
    public class GenericRepository<TEntity>
        : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MainContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        
        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        
        public async Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        protected IQueryable Query() => _dbSet.AsNoTracking();
    }
}