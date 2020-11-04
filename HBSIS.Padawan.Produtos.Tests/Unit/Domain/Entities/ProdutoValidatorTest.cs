using Castle.Core.Internal;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators;
using HBSIS.Padawan.Produtos.Domain.Validators.ProdutoValidators;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities
{
    public class ProdutoValidatorTest
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IProdutoValidator _produtoValidator;
        private readonly ICamposProdutoValidator _produtoValidatorCampos;
        private readonly IIdProdutoValidator _produtoValidatorID;
        private readonly IUpdateProdutoValidator _produtoValidatorUpdate;

        private const string ID_PRODUTO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc5";
        private const string ID_VALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc3";
        private const string ID_INVALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc2";
        private const string NOME_INVALIDO_500 = "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES";

        public ProdutoValidatorTest()
        {
            _produtoRepository = Substitute.For<IProdutoRepository>();
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _produtoValidator = new ProdutoValidator(_produtoRepository, _categoriaProdutoRepository);
            _produtoValidatorCampos = new CamposProdutoValidator(_categoriaProdutoRepository);
            _produtoValidatorID = new IdProdutoValidator(_produtoRepository);

            _produtoValidatorUpdate = new UpdateProdutoValidator(_categoriaProdutoRepository, _produtoRepository);
            
            _categoriaProdutoRepository.ExistsByIdAsync(Guid.Parse(ID_VALIDO)).Returns(true);
        }


        [Fact]
        public async void Deve_Criar_Produto_Corretamente_Ao_Passar_Informacoes_Validas()
        {
            var result = await _produtoValidatorCampos.ValidateAsync(GerarProduto());

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("", "Campo Nome obrigatório")]
        [InlineData(NOME_INVALIDO_500, "Nome excede o tamanho máximo")]
        public async void Deve_Dar_Erro_Ao_Passar_Nome_Invalido(string nome, string mensagem)
        {
            var produto = GerarProduto();
            produto.Nome = nome;

            var result = await _produtoValidatorCampos.ValidateAsync(produto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData(0, "Campo Peso obrigatório")]
        [InlineData(-1, "Campo Peso deve ser um valor positivo")]
        public async void Deve_Dar_Erro_Ao_Passar_Peso_Invalido(double peso, string mensagem)
        {
            var produto = GerarProduto();
            produto.PesoPorUnidade = peso;

            var result = await _produtoValidatorCampos.ValidateAsync(produto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData(0, "Campo Unidade por Caixa obrigatório")]
        [InlineData(-1, "Campo Unidade por Caixa deve ser um valor positivo")]
        public async void Deve_Dar_Erro_Ao_Passar_Unidade_Invalida(int unidade, string mensagem)
        {
            var produto = GerarProduto();
            produto.UnidadePorCaixa = unidade;

            var result = await _produtoValidatorCampos.ValidateAsync(produto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData(0, "Campo Preço obrigatório")]
        [InlineData(-1, "Campo Preço deve ser um valor positivo")]
        public async void Deve_Dar_Erro_Ao_Passar_Preco_Invalido(double preco, string mensagem)
        {
            var produto = GerarProduto();
            produto.Preco = preco;

            var result = await _produtoValidatorCampos.ValidateAsync(produto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData(ID_INVALIDO, "CategoriaProduto não cadastrada")]
        [InlineData("", "CategoriaProduto não cadastrada")]
        public async void Deve_Dar_Erro_Ao_Passar_CategoriaProduto_ID_Invalido(string id, string mensagem)
        {
            Guid Id = new Guid();
            bool parse = Guid.TryParse(id, out Id);
            var produto = GerarProduto();

            if (parse)
            {
                produto.CategoriaProdutoId = Id;
            }
            else
            {
                produto.CategoriaProdutoId = Guid.Empty;
            }

            var result = await _produtoValidatorCampos.ValidateAsync(produto);

            Assert.False(result.IsValid);
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
