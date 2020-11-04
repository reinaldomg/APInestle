using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators
{
    public class UpdateCategoriaProdutoValidator : AbstractValidator<CategoriaProduto>, IUpdateCategoriaProdutoValidator
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;

        public UpdateCategoriaProdutoValidator(ICategoriaProdutoRepository categoriaProdutoRepository, IFornecedorRepository fornecedorRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _fornecedorRepository = fornecedorRepository;

            RuleFor(x => x.Id).SetValidator(new IdCategoriaProdutoValidator(_categoriaProdutoRepository));
            RuleFor(x => x).SetValidator(new CamposCategoriaProdutoValidator(_fornecedorRepository,_categoriaProdutoRepository));
        }
    }
}
