using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.DTO;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators
{
    public interface IImportarCategoriaProdutoValidator : IValidator<CategoriaProdutoDTO>
    {
    }
}
