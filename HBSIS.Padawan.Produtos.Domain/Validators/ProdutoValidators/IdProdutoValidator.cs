using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using System;

namespace HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators
{
    public class IdProdutoValidator : AbstractValidator<Guid>, IIdProdutoValidator
    {
        private readonly IProdutoRepository _produtoRepository;

        public IdProdutoValidator(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;

            ValidarId();
        }

        private void ValidarId()
        {
            RuleFor(x => x).MustAsync(async (id, _) =>
            {
                return await _produtoRepository.ExistsByIdAsync(id);
            }).WithMessage("Produto não cadastrado");
        }
    }
}
