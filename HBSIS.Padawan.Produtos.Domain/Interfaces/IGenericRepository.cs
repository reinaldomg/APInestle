using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>  where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}