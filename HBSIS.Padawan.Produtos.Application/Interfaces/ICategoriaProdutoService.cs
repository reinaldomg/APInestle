using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface ICategoriaProdutoService
    {
        public Task<Result<CategoriaProduto>> CreateAsync(CategoriaProduto categoriaProduto);
        public Task<Result<CategoriaProduto>> UpdateAsync(CategoriaProduto categoriaProduto);
        public Task<Result<CategoriaProduto>> DeleteAsync(Guid id);
        public Task<IEnumerable<CategoriaProduto>> GetAsync();
    }
}
