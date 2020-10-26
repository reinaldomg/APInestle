using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Validators;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities
{
    public class FornecedorValidatorTest
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorValidator _fornecedorValidator;
        private const string NOME_INVALIDO_500 = "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES";
        public FornecedorValidatorTest()
        {
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _fornecedorValidator = new FornecedorValidator(_fornecedorRepository);
        }

        private static Fornecedor GerarFornecedor()
        {
            return new Fornecedor()
            {
                NomeFantasia = "Domingo Legal",
                Cnpj = "10029717532343",
                Email = "guguliberato@gugu.com.br",
                RazaoSocial = "Domingo Triste",
                Endereco = "Rua dos bobos, n 0",
                Telefone = "4340028922"
            };
        }

        [Fact]
        [Trait("Deve funcionar OK", "(Fornecedor)")]
        public void Deve_Cadastrar_Fornecedor()
        {
            var fornecedor = GerarFornecedor();
            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("100297175323435", false)]
        [InlineData("1002971753234", false)]
        public void Teste_Validar_Erro_CNPJ(string cnpj, bool validar)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Cnpj = cnpj;

            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.Equal(validar,result.IsValid);
        }

        [Theory]
        [InlineData("reimg80@gmail", false)]
        [InlineData("reimg80gmail.com.br", false)]
        [InlineData("reimg90@gmail.com.br.br.br",false)]
        public void Teste_Validar_Email_Invalido(string email, bool validar)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Email = email;

            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.Equal(validar,result.IsValid);
            Assert.Equal("O Email não é válido", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(NOME_INVALIDO_500, false)]
        public void Teste_Validar_Nome_Invalido(string nome, bool validar)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.NomeFantasia = nome;

            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal("O Nome Fantasia não é válido", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData("43999567213",true)]
        [InlineData("4330552345",true)]
        [InlineData("(43)9956-7213",true)]
        [InlineData("(43)99567213",true)]
        public void Teste_Validar_Telefone_Valido(string telefone, bool validar)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Telefone = telefone;

            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.Equal(validar, result.IsValid);
        }

        [Theory]
        [InlineData("(4)999567213", false)]
        [InlineData("(4)99956721", false)]
        [InlineData("(4)9995672133", false)]
        [InlineData("(42)998956-7213332", false)]
        public void Teste_Validar_Telefone_Invalido(string telefone, bool validar)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Telefone = telefone;

            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal("O Telefone não é válido", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData("",false)]
        [InlineData(NOME_INVALIDO_500, false)]
        public void Teste_Validar_Razao_Social(string nome, bool validar)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.RazaoSocial = nome;

            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal("A Razão Social não é válida", result.ErrorList.SingleOrDefault());
        }
    }
}
