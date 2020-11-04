using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos
{
    public interface ICategoriaProdutoService
    {
        Task<Result<CategoriaProduto>> CreateAsync(CategoriaProduto categoriaProduto);
        Task<Result<CategoriaProduto>> UpdateAsync(CategoriaProduto categoriaProduto);
        Task<Result<CategoriaProduto>> DeleteAsync(Guid id);
        Task<IEnumerable<CategoriaProduto>> GetAsync();
    }
}
