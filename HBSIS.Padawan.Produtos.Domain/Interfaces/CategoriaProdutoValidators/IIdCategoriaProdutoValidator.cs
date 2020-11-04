using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using System;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators
{
    public interface IIdCategoriaProdutoValidator : IValidator<Guid>
    {
    }
}
