﻿using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;

namespace HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators
{
    public class UpdateProdutoValidator : AbstractValidator<Produto>, IAtualizarProdutoValidator
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public UpdateProdutoValidator(ICategoriaProdutoRepository categoriaProdutoRepository, IProdutoRepository produtoRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _produtoRepository = produtoRepository;

            RuleFor(x => x.Id).SetValidator(new IdProdutoValidator(_produtoRepository));
            RuleFor(x => x).SetValidator(new CriarProdutoValidator(_categoriaProdutoRepository));
        }
    }
}
