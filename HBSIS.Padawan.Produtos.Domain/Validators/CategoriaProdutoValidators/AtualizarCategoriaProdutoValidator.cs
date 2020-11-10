using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;

namespace HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators
{
    public class AtualizarCategoriaProdutoValidator : AbstractValidator<CategoriaProduto>, IAtualizarCategoriaProdutoValidator
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;

        public AtualizarCategoriaProdutoValidator(ICategoriaProdutoRepository categoriaProdutoRepository, IFornecedorRepository fornecedorRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _fornecedorRepository = fornecedorRepository;

            RuleFor(x => x.Id).SetValidator(new IdCategoriaProdutoValidator(_categoriaProdutoRepository));
            RuleFor(x => x).SetValidator(new CriarCategoriaProdutoValidator(_fornecedorRepository,_categoriaProdutoRepository));
        }
    }
}
