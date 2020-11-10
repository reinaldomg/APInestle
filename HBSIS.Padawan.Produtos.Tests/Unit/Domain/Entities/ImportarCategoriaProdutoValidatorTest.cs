using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Entities.Importar;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Validators;
using HBSIS.Padawan.Produtos.Domain.Validators.CategoriaProdutoValidators;
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


        [Theory]
        [InlineData("", "Campo CNPJ é obrigatório")]
        [InlineData(CNPJ_INVALIDO, "Fornecedor com esse CNPJ não cadastrado")]
        public async void Validar_CNPJ_Invalido(string cnpj, string mensagem)
        {
            var categoriaproduto = GerarCategoriaProdutoImportar(cnpj, NOME_VALIDO);
            var result = await _importarCategoriaProdutoValidator.ValidateAsync(categoriaproduto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData(NOME_INVALIDO, "Nome de CategoriaProduto já cadastrado")]
        [InlineData("", "Campo Nome é obrigatório")]
        public async void Validar_Nome_Existe(string nome, string mensagem)
        {
            var categoriaproduto = GerarCategoriaProdutoImportar(CNPJ_VALIDO, nome);
            var result = await _importarCategoriaProdutoValidator.ValidateAsync(categoriaproduto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        private static Fornecedor GerarFornecedorValido()
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

        private static CategoriaProdutoImportar GerarCategoriaProdutoImportar(string cnpj, string nome)
        {
            return new CategoriaProdutoImportar()
            {
                Cnpj = cnpj,
                Nome = nome
            };
        }
    }
}
