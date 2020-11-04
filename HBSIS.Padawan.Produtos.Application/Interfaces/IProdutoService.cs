using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ValidationResult> CreateAsync(Produto Produto);
        Task<ValidationResult> UpdateAsync(Produto Produto);
        Task<ValidationResult> DeleteAsync(Guid id);
        Task<IEnumerable<Produto>> GetAsync();
    }
}
