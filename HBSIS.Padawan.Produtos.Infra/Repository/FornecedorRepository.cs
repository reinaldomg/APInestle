using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository
{
    public class FornecedorRepository : GenericRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MainContext dbContext) : base(dbContext)
        {
           
        }

        public async Task<bool> GetByCnpj(string cnpj)
        {
            return await DbSet.Where(x => x.Cnpj == cnpj).AnyAsync();
        }

        public async Task<Fornecedor> GetEntityByCnpj(string cnpj)
        {
            return await DbSet.Where(x => x.Cnpj == cnpj).FirstAsync();
        }
    }

}
