using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.DTO;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;

namespace HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators
{
    public class ImportarCategoriaProdutoValidator : AbstractValidator<CategoriaProdutoDTO>, IImportarCategoriaProdutoValidator
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public ImportarCategoriaProdutoValidator(IFornecedorRepository fornecedorRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;

            ValidarNome();
            ValidarCnpj();
        }

        private void ValidarNome()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo Nome é obrigatório")
                .MustAsync(async (nome, _) =>
                {
                    return !await _categoriaProdutoRepository.ExistsByNameAsync(nome);
                }).WithMessage("Nome de CategoriaProduto já cadastrado");
        }

        private void ValidarCnpj()
        {
            RuleFor(x => x.Cnpj).NotEmpty().WithMessage("Campo CNPJ é obrigatório")
                .MustAsync(async (cnpj, _) =>
                {
                    return await _fornecedorRepository.ExistsByCnpjAsync(cnpj);
                }).WithMessage("Fornecedor com esse CNPJ não cadastrado");
        }
    }
}
