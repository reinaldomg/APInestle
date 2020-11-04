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
        Task<Result<CategoriaProduto>> CreateValidate(CategoriaProduto categoriaProduto);
        Task<Result<CategoriaProduto>> IdValidate(Guid Id);
        Task<Result<CategoriaProduto>> UpdateValidate(CategoriaProduto categoriaProduto);
    }
}
