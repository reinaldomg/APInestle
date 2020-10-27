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
    public class ImportarCategoriaProdutoValidatorTest
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IImportarCategoriaProdutoValidator _importarCategoriaProdutoValidator;
        private const string CNPJ_VALIDO = "10029717532343";
        private const string CNPJ_INVALIDO = "10029717532344";
        private const string NOME_INVALIDO = "Banana";
        private const string NOME_VALIDO = "Bateria";

        public ImportarCategoriaProdutoValidatorTest()
        {
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _importarCategoriaProdutoValidator = new ImportarCategoriaProdutoValidator(_fornecedorRepository,_categoriaProdutoRepository);

            _categoriaProdutoRepository.ExistsByNameAsync(NOME_INVALIDO).Returns(true);
            _fornecedorRepository.ExistsByCnpjAsync(CNPJ_VALIDO).Returns(true);

            var fornecedor = GerarFornecedorValido();
            _fornecedorRepository.GetEntityByCnpjAsync(CNPJ_VALIDO).Returns(fornecedor);
        }

        public static Fornecedor GerarFornecedorValido()
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

        [Theory]
        [InlineData("", false)]
        [InlineData(CNPJ_INVALIDO,false)]
        public async void Validar_CNPJ_Invalido(string cnpj, bool validar)
        {
            var result = await _importarCategoriaProdutoValidator.Validar(cnpj,NOME_VALIDO);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal($"CNPJ não cadastrado: {cnpj}", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData(NOME_INVALIDO,false)]
        public async void Validar_Nome_Existe(string nome, bool validar)
        {
            var result = await _importarCategoriaProdutoValidator.Validar(CNPJ_VALIDO, nome);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal($"Nome de categoria já cadastrado: {nome}", result.ErrorList.SingleOrDefault());
        }

        [Theory]
        [InlineData("", false)]

        public async void Validar_Nome_Invalido(string nome, bool validar)
        {
            var result = await _importarCategoriaProdutoValidator.Validar(CNPJ_VALIDO, nome);

            Assert.Equal(validar, result.IsValid);
            Assert.Equal($"Nome é inválido: {nome}", result.ErrorList.SingleOrDefault());
        }
    }
}
