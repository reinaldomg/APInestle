using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository
{
    public class CategoriaProdutoRepository : GenericRepository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await DbSet.Where(x => x.Nome == name).AnyAsync();
        }
    }
}
