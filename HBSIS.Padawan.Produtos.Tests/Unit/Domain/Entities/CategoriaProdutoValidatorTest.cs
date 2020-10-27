using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Validators;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities
{
    public class CategoriaProdutoValidatorTest
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICategoriaProdutoValidator _categoriaProdutoValidator;
        private const string ID_VALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63dc3";
        private const string ID_INVALIDO = "71cb18ba-7e4f-4f29-144e-08d86ae63cd3";
        private const string NOME_INVALIDO_500 = "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES";
        private const string NOME_CADASTRADO = "Bateria";

        public CategoriaProdutoValidatorTest()
        {
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _categoriaProdutoValidator = new CategoriaProdutoValidator(_fornecedorRepository, _categoriaProdutoRepository);

            _fornecedorRepository.ExistsByIdAsync(Guid.Parse(ID_VALIDO)).Returns(true);
            _categoriaProdutoRepository.ExistsByNameAsync(NOME_CADASTRADO).Returns(true);

        }

        [Theory]
        [InlineData("", false)]
        [InlineData(NOME_INVALIDO_500, false)]
        public async void Teste_Nome_Invalido(string nome, bool validar)
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = nome,
                FornecedorId = new Guid(ID_VALIDO)
            };

            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.Equal(validar,result.IsValid);
            Assert.Equal("Nome é inválido", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData(NOME_CADASTRADO, false)]
        public async void Teste_Nome_Cadastrado(string nome, bool validar)
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = nome,
                FornecedorId = new Guid(ID_VALIDO)
            };

            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal("Categoria já cadastrada", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(ID_INVALIDO, false)]
        public async void Teste_ID_Fornecedor_Invalido(string id, bool validar)
        {
            Guid Id = new Guid();
            bool parse = Guid.TryParse(id, out Id);
            CategoriaProduto categoriaproduto;

            if (parse) 
            {
                categoriaproduto = new CategoriaProduto()
                {
                    Nome = "Cerveja",
                    FornecedorId = Id
                };
            }
            else
            {
                categoriaproduto = new CategoriaProduto()
                {
                    Nome = "Cerveja",
                    FornecedorId = Guid.Empty
                };
            }

            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal("Id de referência a Fornecedor é inválido", result.ErrorList.SingleOrDefault());
        }


        [Fact]
        [Trait("Verificar cadastro OK", "(CategoriaProduto)")]
        public async void Deve_Dar_Certo_Cadastro()
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = "Cerveja",
                FornecedorId = new Guid(ID_VALIDO)
            };

            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.True(result.IsValid);
        }
    }
}
