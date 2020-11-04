using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Application.Services.Produtos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Application.Services
{
    public class ProdutoServiceTest
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IProdutoService _produtoService;
        private readonly ICamposProdutoValidator _camposProdutoValidator;
        private readonly IIdProdutoValidator _idProdutoValidator;
        private readonly IUpdateProdutoValidator _updateProdutoValidator;
        private const string ID_PRODUTO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc5";
        private const string ID_VALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc3";
        private const string ID_INVALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc1";
        private const string ID_PRODUTO_EXISTENTE = "71cb18ba-7e4f-4f29-144e-08d86ae63dc6";
        private const string ID_PRODUTO_INEXISTENTE = "71cb18ba-7e4f-4f29-144e-08d86ae63dc7";
        private const string NOME_VALIDO = "Batata";

        public ProdutoServiceTest()
        {
            _produtoRepository = Substitute.For<IProdutoRepository>();
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _camposProdutoValidator = new CamposProdutoValidator(_categoriaProdutoRepository);
            _idProdutoValidator = new IdProdutoValidator(_produtoRepository);
            _updateProdutoValidator = new UpdateProdutoValidator(_categoriaProdutoRepository, _produtoRepository);
            _produtoService = new ProdutoService(_produtoRepository, _camposProdutoValidator, _idProdutoValidator, _updateProdutoValidator);

            _categoriaProdutoRepository.ExistsByIdAsync(Guid.Parse(ID_VALIDO)).Returns(true);
            _produtoRepository.ExistsByIdAsync(Guid.Parse(ID_PRODUTO_EXISTENTE)).Returns(true);
            _produtoRepository.ExistsByIdAsync(Guid.Parse(ID_PRODUTO_INEXISTENTE)).Returns(false);
        }

        [Fact]
        public async void Deve_Criar_Produto_Corretamente_Ao_Inserir_Informacoes_Validas()
        {
            var produto = GerarProduto();
            var result = await _produtoService.CreateAsync(produto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Deve_Atualizar_Produto_Corretamente_Ao_Inserir_Informacoes_Validas()
        {
            var produto = GerarProduto();
            produto.Id = Guid.Parse(ID_PRODUTO_EXISTENTE);

            var result = await _produtoService.UpdateAsync(produto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Deve_Deletar_Produto_Corretamente_Ao_Inserir_Informacoes_Validas()
        {
            var result = await _produtoService.DeleteAsync(Guid.Parse(ID_PRODUTO_EXISTENTE));

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(ID_PRODUTO_INEXISTENTE, false)]
        [InlineData("",false)]
        public async void Deve_Dar_Erro_Ao_Deletar_Produto_Invalido(string id, bool validar)
        {
            var result = new ValidationResult();
            try
            {
                result = await _produtoService.DeleteAsync(Guid.Parse(id));
            }
            catch
            {
                result = await _produtoService.DeleteAsync(Guid.Empty);
            }

            Assert.Equal(validar,result.IsValid);
        }

        [Theory]
        [InlineData(ID_PRODUTO_INEXISTENTE, NOME_VALIDO, false, "Produto não cadastrado")]
        [InlineData(ID_PRODUTO_EXISTENTE, "", false, "Campo Nome obrigatório")]
        public async void Deve_Dar_Erro_Ao_Atualizar_Produto_Invalido(string idproduto, string nome, bool validar, string mensagem)
        {
            var produto = GerarProduto();

            produto.Id = Guid.Parse(idproduto);
            produto.Nome = nome;

            var result = await _produtoService.UpdateAsync(produto);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData(ID_INVALIDO, NOME_VALIDO, false, "CategoriaProduto não cadastrada")]
        [InlineData(ID_VALIDO, "", false, "Campo Nome obrigatório")]
        public async void Deve_Dar_Erro_Ao_Cadastrar_Produto_Invalido(string idcategoriaproduto, string nome, bool validar, string mensagem)
        {
            var produto = GerarProduto();

            produto.CategoriaProdutoId = Guid.Parse(idcategoriaproduto);
            produto.Nome = nome;

            var result = await _produtoService.CreateAsync(produto);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        private static Produto GerarProduto()
        {
            return new Produto()
            {
                Nome = "Bolacha",
                CategoriaProdutoId = Guid.Parse(ID_VALIDO),
                Id = Guid.Parse(ID_PRODUTO),
                PesoPorUnidade = 2,
                UnidadePorCaixa = 20,
                Preco = 30.00,
                Validade = new DateTime(2020, 08, 07)
            };
        }
    }
}
