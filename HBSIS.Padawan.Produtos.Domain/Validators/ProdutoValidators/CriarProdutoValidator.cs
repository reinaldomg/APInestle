using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;

namespace HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators
{
    public class CriarProdutoValidator : AbstractValidator<Produto>, ICriarProdutoValidator
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public CriarProdutoValidator(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;

            ValidarNome();
            ValidarPeso();
            VallidarUnidade();
            ValidarPreco();
            ValidarValidade();
            ValidarCategoriaProduto();
        }

        private void ValidarNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Campo Nome obrigatório")
                .Length(0, 500).WithMessage("Nome excede o tamanho máximo");
        }

        private void ValidarPeso()
        {
            RuleFor(x => x.PesoPorUnidade)
                .NotEqual(0).WithMessage("Campo Peso obrigatório")
                .GreaterThan(0).WithMessage("Campo Peso deve ser um valor positivo");
        }

        private void VallidarUnidade()
        {
            RuleFor(x => x.UnidadePorCaixa)
                .NotEqual(0).WithMessage("Campo Unidade por Caixa obrigatório")
                .GreaterThan(0).WithMessage("Campo Unidade por Caixa deve ser um valor positivo");
        }

        private void ValidarPreco()
        {
            RuleFor(x => x.Preco)
                .NotEqual(0).WithMessage("Campo Preço obrigatório")
                .GreaterThan(0).WithMessage("Campo Preço deve ser um valor positivo");
        }

        private void ValidarValidade()
        {
            RuleFor(x => x.Validade)
                .NotNull().WithMessage("Campo Validade obrigatório");
        }

        private void ValidarCategoriaProduto()
        {
            RuleFor(x => x.CategoriaProdutoId)
               .MustAsync(async (id, _) =>
               {
                   return await _categoriaProdutoRepository.ExistsByIdAsync(id);
               }).WithMessage("CategoriaProduto não cadastrada");
        }
    }
}
