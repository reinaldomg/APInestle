﻿using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.DTO;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;

namespace HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators
{
    public class ImportarProdutoValidator : AbstractValidator<ProdutoDTO>, IImportarProdutoValidator
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public ImportarProdutoValidator(IProdutoRepository produtoRepository,
            ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;

            ValidarNome();
            ValidarPeso();
            VallidarUnidade();
            ValidarPreco();
            ValidarValidade();
            ValidarNomeCategoriaProduto();
        }

        private void ValidarNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Campo Nome obrigatório")
                .Length(0, 500).WithMessage("Nome excede o tamanho máximo")
                .MustAsync(async (nome, _) =>
                {
                    return !await _produtoRepository.ExistsByNameAsync(nome);
                }).WithMessage("Nome de Produto já cadastrado");
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

        private void ValidarNomeCategoriaProduto()
        {
            RuleFor(x => x.NomeCategoriaProduto)
                .NotEmpty().WithMessage("Campo NomeCategoriaProduto obrigatório")
                .MustAsync(async (nome, _) =>
                {
                    return await _categoriaProdutoRepository.ExistsByNameAsync(nome);
                }).WithMessage("CategoriaProduto com esse Nome não cadastrado");
        }
    }
}
