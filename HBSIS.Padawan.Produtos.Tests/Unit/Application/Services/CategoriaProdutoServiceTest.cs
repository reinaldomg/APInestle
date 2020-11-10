using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators;
using Microsoft.Data.SqlClient.Server;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Application.Services
{
    public class CategoriaProdutoServiceTest
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly ICategoriaProdutoService _categoriaProdutoService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICriarCategoriaProdutoValidator _camposCategoriaProdutoValidator;
        private readonly IIdCategoriaProdutoValidator _idCategoriaProdutoValidator;
        private readonly IAtualizarCategoriaProdutoValidator _updateCategoriaProdutoValidator;
        private const string ID_VALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc3";
        private const string ID_INVALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63cd3";
        private const string NOME_INVALIDO_500 = "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES";
        private const string NOME_CADASTRADO = "Bateria";

        public CategoriaProdutoServiceTest()
        {
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();

            _camposCategoriaProdutoValidator = new CriarCategoriaProdutoValidator(_fornecedorRepository, _categoriaProdutoRepository);
            _idCategoriaProdutoValidator = new IdCategoriaProdutoValidator(_categoriaProdutoRepository);
            _updateCategoriaProdutoValidator = new AtualizarCategoriaProdutoValidator(_categoriaProdutoRepository, _fornecedorRepository);

            _categoriaProdutoService = new CategoriaProdutoService(_categoriaProdutoRepository, _camposCategoriaProdutoValidator, _idCategoriaProdutoValidator, _updateCategoriaProdutoValidator);

            _fornecedorRepository.ExistsByIdAsync(Guid.Parse(ID_VALIDO)).Returns(true);
            _categoriaProdutoRepository.ExistsByNameAsync(NOME_CADASTRADO).Returns(true);
            _categoriaProdutoRepository.ExistsByIdAsync(Guid.Parse(ID_VALIDO)).Returns(true);
        }

        [Fact]
        public async void Deve_Cadastrar_Corretamente_CategoriaProduto_Ao_Passar_Dados_Validos()
        {
            var categoriaproduto = GerarCategoriaProduto();
            var result = await _categoriaProdutoService.CreateAsync(categoriaproduto);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);        
        }

        [Fact]
        public async void Deve_Atualizar_Corretamente_CategoriaProduto_Ao_Passar_Dados_Validos()
        {
            var categoriaproduto = GerarCategoriaProduto();
            categoriaproduto.Id = Guid.Parse(ID_VALIDO);
            var result = await _categoriaProdutoService.UpdateAsync(categoriaproduto);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async void Deve_Deletar_Corretamente_CategoriaProduto_Ao_Passar_Id_Valido()
        {
            var result = await _categoriaProdutoService.DeleteAsync(Guid.Parse(ID_VALIDO));

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async void Deve_Dar_Erro_Ao_Deletar_CategoriaProduto_Com_Id_Invalido()
        {
            var result = await _categoriaProdutoService.DeleteAsync(Guid.Parse(ID_INVALIDO));

            Assert.False(result.IsValid);
            Assert.Equal("Produto não cadastrado", result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("",ID_VALIDO, "Campo Nome obrigatório")]
        [InlineData("Batata", ID_INVALIDO, "Produto não cadastrado")]
        public async void Deve_Dar_Erro_Ao_Atualizar_CategoriaProduto_Com_Dados_Invalidos(string nome, string id, string mensagem)
        {
            var categoriaproduto = GerarCategoriaProduto();
            categoriaproduto.Nome = nome;
            categoriaproduto.Id = Guid.Parse(id);

            var result = await _categoriaProdutoService.UpdateAsync(categoriaproduto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("", ID_VALIDO, "Campo Nome obrigatório")]
        [InlineData("Batata", ID_INVALIDO, "Fornecedor não cadastrado")]
        public async void Deve_Dar_Erro_Ao_Cadastrar_CategoriaProduto_Com_Dados_Invalidos(string nome, string id, string mensagem)
        {
            var categoriaproduto = GerarCategoriaProduto();
            categoriaproduto.Nome = nome;
            categoriaproduto.FornecedorId = Guid.Parse(id);

            var result = await _categoriaProdutoService.CreateAsync(categoriaproduto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        private static CategoriaProduto GerarCategoriaProduto()
        {
            return new CategoriaProduto()
            {
                Nome = "Beterraba",
                FornecedorId = Guid.Parse(ID_VALIDO)
            };
        }
    }
}
