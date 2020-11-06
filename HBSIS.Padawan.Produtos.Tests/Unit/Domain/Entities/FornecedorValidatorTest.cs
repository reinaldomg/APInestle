using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators;
using HBSIS.Padawan.Produtos.Domain.Validators;
using HBSIS.Padawan.Produtos.Domain.Validators.FornecedorValidators;
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
        private readonly ICamposFornecedorValidator _camposFornecedorValidator;
        private const string CNPJ_CADASTRADO = "10029717532345";
        private const string NOME_INVALIDO_500 = "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES";
        public FornecedorValidatorTest()
        {
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _camposFornecedorValidator = new CamposFornecedorValidator(_fornecedorRepository);

            _fornecedorRepository.ExistsByCnpjAsync(CNPJ_CADASTRADO).Returns(true);
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
        public void Deve_Cadastrar_Corretamente_Fornecedor_Ao_Passar_Dados_Corretamente()
        {
            var fornecedor = GerarFornecedor();

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("", "Campo CNPJ é obrigatório")]
        [InlineData("100297175323435", "CNPJ fora dos padrões")]
        [InlineData(CNPJ_CADASTRADO, "CNPJ já cadastrado")]
        public void Deve_Dar_Erro_Ao_Passar_CNPJ_Inválido(string cnpj, string mensagem)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Cnpj = cnpj;

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("reimg80@gmail", "Email é inválido")]
        [InlineData("reimg80gmail.com.br", "Email é inválido")]
        [InlineData("", "Campo Email é obrigatório")]
        public void Deve_Dar_Erro_Ao_Passar_Email_Invalido(string email, string mensagem)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Email = email;

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("", "Campo Nome Fantasia é obrigatório")]
        [InlineData(NOME_INVALIDO_500, "Nome Fantasia excede o tamanho máximo")]
        public void Deve_Dar_Erro_Ao_Passar_Nome_Invalido(string nome, string mensagem)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.NomeFantasia = nome;

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("43999567213")]
        [InlineData("4330552345")]
        [InlineData("(43)9956-7213")]
        [InlineData("(43)99567213")]
        public void Deve_Cadastrar_Corretamente_Ao_Passar_Telefone_Valido(string telefone)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Telefone = telefone;

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("", "Campo Telefone é obrigatório")]
        [InlineData("(4)999567213", "Telefone é inválido")]
        [InlineData("(4)99956721", "Telefone é inválido")]
        [InlineData("(4)9995672133", "Telefone é inválido")]
        [InlineData("(42)998956-7213332", "Telefone é inválido")]
        public void Deve_Dar_Erro_Ao_Passar__Telefone_Invalido(string telefone, string mensagem)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Telefone = telefone;

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("", "Campo Razão Social é obrigatório")]
        [InlineData(NOME_INVALIDO_500, "Razão Social excede o tamanho máximo")]
        public void Deve_Dar_Erro_Ao_Passar_Razao_Social(string nome, string mensagem)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.RazaoSocial = nome;

            var result = _camposFornecedorValidator.Validate(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }
    }
}
