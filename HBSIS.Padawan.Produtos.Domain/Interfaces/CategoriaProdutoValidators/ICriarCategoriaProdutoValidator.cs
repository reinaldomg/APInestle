﻿using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators
{
    public interface ICriarCategoriaProdutoValidator : IValidator<CategoriaProduto>
    {
    }
}
