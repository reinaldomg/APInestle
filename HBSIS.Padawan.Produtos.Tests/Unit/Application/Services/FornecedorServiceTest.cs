using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators;
using HBSIS.Padawan.Produtos.Domain.Validators.FornecedorValidators;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Application.Services
{
    public class FornecedorServiceTest
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICriarFornecedorValidator _camposFornecedorValidator;
        private const string CNPJ_CADASTRADO = "10029717532345";
        private const string NOME_INVALIDO_500 = "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES" +
            "500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES 500CARACTERES";

        public FornecedorServiceTest()
        {
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _camposFornecedorValidator = new CamposFornecedorValidator(_fornecedorRepository);
            _fornecedorService = new FornecedorService(_fornecedorRepository, _camposFornecedorValidator);

            _fornecedorRepository.ExistsByCnpjAsync(CNPJ_CADASTRADO).Returns(true);
        }

        [Fact]
        public async void Deve_Criar_Corretamente_Fornecedor_Ao_Passar_Dados_Validos()
        {
            var fornecedor = GerarFornecedor();
            var result = await _fornecedorService.CreateFornecedorAsync(fornecedor);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("", "Campo Nome Fantasia é obrigatório")]
        [InlineData(NOME_INVALIDO_500, "Nome Fantasia excede o tamanho máximo")]
        public async void Deve_Dar_Erro_Ao_Criar_Fornecedor_Com_Nome_Invalido(string nome, string mensagem)
        {
            var fornecedor = GerarFornecedor();
            fornecedor.NomeFantasia = nome;
            var result = await _fornecedorService.CreateFornecedorAsync(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Fact]
        public async void Deve_Dar_Erro_Ao_Criar_Fornecedor_Com_CNPJ_Repetido()
        {
            var fornecedor = GerarFornecedor();
            fornecedor.Cnpj = CNPJ_CADASTRADO;
            var result = await _fornecedorService.CreateFornecedorAsync(fornecedor);

            Assert.False(result.IsValid);
            Assert.Equal("CNPJ já cadastrado", result.Errors.ElementAt(0).ErrorMessage);
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
    }
}
