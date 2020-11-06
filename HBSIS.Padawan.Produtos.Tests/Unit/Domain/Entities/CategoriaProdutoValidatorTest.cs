using HBSIS.Padawan.Produtos.Domain.Entities;
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
    public class CategoriaProdutoValidatorTest
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICamposCategoriaProdutoValidator _camposCategoriaProdutoValidator;
        private readonly IIdCategoriaProdutoValidator _idCategoriaProdutoValidator;
        private readonly IUpdateCategoriaProdutoValidator _updateCategoriaProdutoValidator;
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

            _camposCategoriaProdutoValidator = new CamposCategoriaProdutoValidator(_fornecedorRepository, _categoriaProdutoRepository);
            _idCategoriaProdutoValidator = new IdCategoriaProdutoValidator(_categoriaProdutoRepository);
            _updateCategoriaProdutoValidator = new UpdateCategoriaProdutoValidator(_categoriaProdutoRepository, _fornecedorRepository);

            _fornecedorRepository.ExistsByIdAsync(Guid.Parse(ID_VALIDO)).Returns(true);
            _categoriaProdutoRepository.ExistsByNameAsync(NOME_CADASTRADO).Returns(true);
        }

        [Theory]
        [InlineData("", "Campo Nome obrigatório")]
        [InlineData(NOME_INVALIDO_500, "Nome excede o tamanho máximo")]
        [InlineData(NOME_CADASTRADO, "CategoriaProduto já cadastrada")]
        public async void Deve_Dar_Erro_Ao_Passar_Nome_Invalido(string nome, string mensagem)
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = nome,
                FornecedorId = new Guid(ID_VALIDO)
            };

            var result = await _camposCategoriaProdutoValidator.ValidateAsync(categoriaproduto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }

        [Theory]
        [InlineData("", "Fornecedor não deve ser nulo")]
        [InlineData(ID_INVALIDO, "Fornecedor não cadastrado")]
        public async void Deve_Dar_Erro_Ao_Passar_Id_Fornecedor_Invalido(string id, string mensagem)
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

            var result = await _camposCategoriaProdutoValidator.ValidateAsync(categoriaproduto);

            Assert.False(result.IsValid);
            Assert.Equal(mensagem, result.Errors.ElementAt(0).ErrorMessage);
        }


        [Fact]
        public async void Deve_Dar_Certo_Cadastro_Ao_Passar_Valores_Corretos()
        {
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = "Cerveja",
                FornecedorId = new Guid(ID_VALIDO)
            };

            var result = await _camposCategoriaProdutoValidator.ValidateAsync(categoriaproduto);

            Assert.True(result.IsValid);
        }
    }
}
