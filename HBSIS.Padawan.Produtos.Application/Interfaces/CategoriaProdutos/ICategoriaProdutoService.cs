using FluentValidation.Results;
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
        Task<ValidationResult> CreateAsync(CategoriaProduto categoriaProduto);
        Task<ValidationResult> UpdateAsync(CategoriaProduto categoriaProduto);
        Task<ValidationResult> DeleteAsync(Guid id);
        Task<IEnumerable<CategoriaProduto>> GetAsync();
    }
}
