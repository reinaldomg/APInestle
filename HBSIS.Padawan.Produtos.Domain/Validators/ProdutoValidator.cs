using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Validators
{
    public class ProdutoValidator : IProdutoValidator
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public ProdutoValidator(IProdutoRepository produtoRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        public async Task<Result<Produto>> CreateValidate(Produto produto)
        {
            return await ValidarCamposObrigatorios(produto);
        }

        public async Task<Result<Produto>> IdValidate(Guid Id)
        {
            return await ExisteProduto(Id);
        }

        public async Task<Result<Produto>> UpdateValidate(Produto produto)
        {
            var result = await ExisteProduto(produto.Id);

            if (!result.IsValid)
                return result;
            else
                result = await ValidarCamposObrigatorios(produto);

            return result; 
        }

        private async Task<Result<Produto>> ValidarCamposObrigatorios(Produto produto)
        {
            var result = new Result<Produto>();
            if (!(await _categoriaProdutoRepository.ExistsByIdAsync(produto.CategoriaProdutoId)))
                result.ErrorList.Add("Id de CategoriaProduto não cadastrado");
            if (produto.Nome == string.Empty || produto.Nome.Length > 500)
                result.ErrorList.Add("O Nome é inválido");
            if (produto.PesoPorUnidade <= 0)
                result.ErrorList.Add("O Peso por Unidade é inválido");
            if (produto.Preco <= 0)
                result.ErrorList.Add("O Preço é inválido");
            if (produto.UnidadePorCaixa <= 0)
                result.ErrorList.Add("O valor de Unidades por Caixa é inválido");

            result.IsValid = !result.ErrorList.Any();

            return result;
        }

        private async Task<Result<Produto>> ExisteProduto(Guid id)
        {
            var result = new Result<Produto>();
            result.IsValid = await _produtoRepository.ExistsByIdAsync(id);

            if (!result.IsValid)
                result.ErrorList.Add("Produto não cadastrado");

            return result;         
        }
    }
}
