using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using System;

namespace HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators
{
    public class IdCategoriaProdutoValidator : AbstractValidator<Guid>, IIdCategoriaProdutoValidator
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public IdCategoriaProdutoValidator(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;

            ValidarCategoriaProduto();
        }

        private void ValidarCategoriaProduto()
        {
            RuleFor(x => x).MustAsync(async (id, _) =>
            {
                return await _categoriaProdutoRepository.ExistsByIdAsync(id);
            }).WithMessage("Produto não cadastrado");
        }
    }
}
