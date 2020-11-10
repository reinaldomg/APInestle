using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;

namespace HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators
{
    public class CriarCategoriaProdutoValidator : AbstractValidator<CategoriaProduto>, ICriarCategoriaProdutoValidator
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public CriarCategoriaProdutoValidator(IFornecedorRepository fornecedorRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;

            VerificarNome();
            VerificarFornecedor();
            VerificarNomeExistente();
        }

        private void VerificarNome()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo Nome obrigatório")
                .MaximumLength(500).WithMessage("Nome excede o tamanho máximo");
        }

        private void VerificarFornecedor()
        {
            RuleFor(x => x.FornecedorId).NotEmpty().WithMessage("Fornecedor não deve ser nulo")
                .MustAsync(async (id, _) =>
                {
                    return await _fornecedorRepository.ExistsByIdAsync(id);
                }).WithMessage("Fornecedor não cadastrado");
        }

        private void VerificarNomeExistente()
        {
            RuleFor(x => x.Nome)
                .MustAsync(async (nome, _) =>
                {
                    return !await _categoriaProdutoRepository.ExistsByNameAsync(nome);
                }).WithMessage("CategoriaProduto já cadastrada");
        }
    }
}
