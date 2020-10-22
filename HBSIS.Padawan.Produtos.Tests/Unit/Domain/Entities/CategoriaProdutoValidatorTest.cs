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

        public CategoriaProdutoValidatorTest()
        {
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _categoriaProdutoValidator = new CategoriaProdutoValidator(_fornecedorRepository, _categoriaProdutoRepository);
        }

        [Theory]
        [InlineData("Pamonha", true)]
        [InlineData("", false)]
        [InlineData("500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES", false)]
        public async void Teste_Validar_Nome(string nome, bool validar)
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = nome,
                FornecedorId = new Guid("71cb18ba-7e4f-4f29-144e-08d86ae63dc3")
            };

            _categoriaProdutoRepository.GetByName(Arg.Any<string>()).Returns(false);
            _fornecedorRepository.ExistsByIdAsync(Arg.Any<Guid>()).Returns(true);

            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.Equal(validar,result.IsValid);
            if (!validar)
            {
                Assert.Equal("Nome é inválido", result.ErrorList.SingleOrDefault());
            }
        }

        [Theory]
        [InlineData("",false)]
        [InlineData("71cb18ba-7e4f-4f29-144e-08d86ae63dc3", true)]
        public async void Teste_Validar_ID_Fornecedor(string id, bool validar)
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

            _fornecedorRepository.ExistsByIdAsync(Arg.Any<Guid>()).Returns(true);
            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.Equal(validar, result.IsValid);
            if (!validar)
            {
                Assert.Equal("Id de referência a Fornecedor é inválido", result.ErrorList.SingleOrDefault());
            }
        }


        [Fact]
        [Trait("Verificar cadastro OK", "(CategoriaProduto)")]
        public async void Deve_Dar_Certo_Cadastro()
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = "Cerveja",
                FornecedorId = new Guid("71cb18ba-7e4f-4f29-144e-08d86ae63dc3")
            };

            _fornecedorRepository.ExistsByIdAsync(Arg.Any<Guid>()).Returns(true);

            var result = await _categoriaProdutoValidator.CreateValidate(categoriaproduto);

            Assert.True(result.IsValid);
        }
    }
}
