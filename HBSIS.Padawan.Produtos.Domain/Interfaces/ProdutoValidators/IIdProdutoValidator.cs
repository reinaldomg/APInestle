using FluentValidation;
using System;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators
{
    public interface IIdProdutoValidator : IValidator<Guid>
    {
    }
}
