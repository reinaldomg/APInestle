using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators
{
    public interface ICamposFornecedorValidator : IValidator<Fornecedor>
    {
    }
}
