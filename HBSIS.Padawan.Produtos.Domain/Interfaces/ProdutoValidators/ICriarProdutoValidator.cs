using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators
{
    public interface ICriarProdutoValidator : IValidator<Produto>
    {
    }
}
