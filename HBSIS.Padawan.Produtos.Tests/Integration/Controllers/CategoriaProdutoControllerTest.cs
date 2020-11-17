using FluentAssertions;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Infra.Repository;
using HBSIS.Padawan.Produtos.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Integration.Controllers
{
    public class CategoriaProdutoControllerTest : BaseControllerTest
    {
        private readonly CategoriaProdutoRepository _categoriaProdutoRepository;
        
        private readonly CategoriaProduto _categoriaProdutoA;
        private readonly CategoriaProduto _categoriaProdutoB;
        private readonly Fornecedor _fornecedor;

        private List<CategoriaProduto> _activeCategoriaProduto;

        public CategoriaProdutoControllerTest() : base("api/CategoriaProduto")
        {
            _fornecedor = new FornecedorBuilder().Build();

            _categoriaProdutoA = new CategoriaProdutoBuilder()
                .WithFornecedorId(_fornecedor.Id).Build();
            _categoriaProdutoB = new CategoriaProdutoBuilder()
                .Alternative().WithFornecedorId(_fornecedor.Id).Build();

            _activeCategoriaProduto = new List<CategoriaProduto> { _categoriaProdutoA };

            Factory.SeedData(_fornecedor, _categoriaProdutoA);

            _categoriaProdutoRepository = new CategoriaProdutoRepository(Factory.GetContext());
        }

        [Fact]
        public async Task Deve_Obter_Todas_As_CategoriasProdutos()
        {
            var response = await Client.GetAsync($"{ControllerUri}/Listar");

            var responseCategoriaProduto = await DescerializeResponse<List<CategoriaProduto>>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            responseCategoriaProduto.Should().HaveCount(_activeCategoriaProduto.Count);
            responseCategoriaProduto.Should().ContainEquivalentOf(_categoriaProdutoA, opt => opt.Excluding(p => p.Fornecedor));
        }

        [Fact]
        public async Task Deve_Cadastrar_Um_Novo_CategoriaProduto()
        {
            var response = await Client.PostAsJsonAsync($"{ControllerUri}/Cadastrar", _categoriaProdutoB);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categoriaProduto = await _categoriaProdutoRepository.GetAllAsync();

            categoriaProduto.Should().HaveCount(_activeCategoriaProduto.Count + 1);
            categoriaProduto.Should().ContainEquivalentOf(_categoriaProdutoB);
        }

        [Fact]
        public async Task Deve_Deletar_Uma_CategoriaProduto()
        {
            var response = await Client.DeleteAsync($"{ControllerUri}/Deletar?id={_categoriaProdutoA.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var itens = await _categoriaProdutoRepository.GetAllAsync();

            itens.Should().BeEmpty();
        }

        [Fact]
        public async Task Deve_Atualizar_Uma_CategoriaProduto()
        {
            _categoriaProdutoA.Nome = "Novo";

            var response = await Client.PutAsJsonAsync($"{ControllerUri}/Atualizar", _categoriaProdutoA);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
