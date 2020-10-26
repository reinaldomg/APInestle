using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository
{
    public class FornecedorRepository : GenericRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsByCnpjAsync(string cnpj)
        {
            return await DbSet.Where(x => x.Cnpj == cnpj).AnyAsync();
        }

        public async Task<Fornecedor> GetEntityByCnpjAsync(string cnpj)
        {
            return await DbSet.Where(x => x.Cnpj == cnpj).FirstAsync();
        }
    }

}
