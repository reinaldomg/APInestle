using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Infra.Repository
{
    public class CategoriaProdutoRepository : GenericRepository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(MainContext dbContext) : base(dbContext)
        {
        }
    }
}
