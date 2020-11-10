using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface IFornecedorService 
    {
        Task<ValidationResult> CreateFornecedorAsync(Fornecedor fornecedor);
    }
}
