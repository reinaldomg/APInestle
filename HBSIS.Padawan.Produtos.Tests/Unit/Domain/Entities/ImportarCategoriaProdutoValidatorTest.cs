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

        public ImportarCategoriaProdutoValidatorTest()
        {
            _categoriaProdutoRepository = Substitute.For<ICategoriaProdutoRepository>();
            _fornecedorRepository = Substitute.For<IFornecedorRepository>();
            _importarCategoriaProdutoValidator = new ImportarCategoriaProdutoValidator(_fornecedorRepository,_categoriaProdutoRepository);
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
        [InlineData("10029717532343", true)]
        [InlineData("", false)]
        [InlineData("124125235235235",false)]
        public async void Validar_CNPJ_Valido(string cnpj, bool validar)
        {
            var fornecedor = GerarFornecedorValido();

            string nome = "Baleia";

            _fornecedorRepository.GetByCnpj("10029717532343").Returns(true);
            _fornecedorRepository.GetEntityByCnpj("10029717532343").Returns(fornecedor);

            var result = await _importarCategoriaProdutoValidator.Importar(cnpj,nome);

            Assert.Equal(validar, result.IsValid);
            if (!validar)
            {
                Assert.Equal($"CNPJ não cadastrado: {cnpj}", result.ErrorList.SingleOrDefault());
            }
        }

        [Theory]
        [InlineData("Doce",true)]
        [InlineData("Banana",false)]
        public async void Validar_Nome_Valido(string nome, bool validar)
        {
            var fornecedor = GerarFornecedorValido();
            string cnpj = "10029717532343";

            _categoriaProdutoRepository.GetByName("Banana").Returns(true);
            _fornecedorRepository.GetByCnpj("10029717532343").Returns(true);
            _fornecedorRepository.GetEntityByCnpj("10029717532343").Returns(fornecedor);

            var result = await _importarCategoriaProdutoValidator.Importar(cnpj, nome);

            Assert.Equal(validar, result.IsValid);
            if (!validar)
            {
                Assert.Equal($"Nome de categoria já cadastrado: {nome}", result.ErrorList.SingleOrDefault());
            }
        }

        [Theory]
        [InlineData("Doce", true)]
        [InlineData("", false)]
        public async void Validar_Nome_Existe(string nome, bool validar)
        {
            var fornecedor = GerarFornecedorValido();
            string cnpj = "10029717532343";

            _categoriaProdutoRepository.GetByName("Banana").Returns(true);
            _fornecedorRepository.GetByCnpj("10029717532343").Returns(true);
            _fornecedorRepository.GetEntityByCnpj("10029717532343").Returns(fornecedor);

            var result = await _importarCategoriaProdutoValidator.Importar(cnpj, nome);

            Assert.Equal(validar, result.IsValid);
            if (!validar)
            {
                Assert.Equal($"Nome é inválido: {nome}", result.ErrorList.SingleOrDefault());
            }
        }
    }
}
