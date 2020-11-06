using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities.Importar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators
{
    public interface IImportarCategoriaProdutoValidator : IValidator<CategoriaProdutoImportar>
    {
    }
}
