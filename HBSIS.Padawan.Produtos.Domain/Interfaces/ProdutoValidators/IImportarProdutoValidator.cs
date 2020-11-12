using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.DTO;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators
{
    public interface IImportarProdutoValidator : IValidator<ProdutoDTO>
    {
    }
}
