using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository
{
    public class CategoriaProdutoRepository : GenericRepository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(MainContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> GetByName(string name)
        {
            return await DbSet.Where(x => x.Nome == name).AnyAsync();
        }
    }
}
