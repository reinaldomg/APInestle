using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators
{
    public class IdProdutoValidator : AbstractValidator<Guid>, IIdProdutoValidator
    {
        private readonly IProdutoRepository _produtoRepository;

        public IdProdutoValidator(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;

            RuleFor(x => x).MustAsync(async (id, _) =>
            {
                return await _produtoRepository.ExistsByIdAsync(id);
            }).WithMessage("Produto não cadastrado");
        }
    }
}
