using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface ICategoriaProdutoValidator
    {
        public Task<Result<CategoriaProduto>> CategoriaProdutoCreateValidate(CategoriaProduto categoriaProduto);
        public Task<Result<CategoriaProduto>> CategoriaProdutoIdValidate(Guid Id);
        public Task<Result<CategoriaProduto>> CategoriaProdutoUpdateValidate(CategoriaProduto categoriaProduto);
    }
}
