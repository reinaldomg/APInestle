using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository
{
    public class FornecedorRepository : GenericRepository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MainContext dbContext) : base(dbContext)
        {
           
        }

    }

}
