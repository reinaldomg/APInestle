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
    public class ProdutoControllerTest : BaseControllerTest
    {
        private readonly ProdutoRepository _produtoRepository;

        private Produto _produtoA;
        private Produto _produtoB;
        private CategoriaProduto _categoriaProduto;
        private Fornecedor _fornecedor;

        private List<Produto> _activeProdutos;

        public ProdutoControllerTest() : base("api/produto")
        {
            _fornecedor = new FornecedorBuilder().Build();

            _categoriaProduto = new CategoriaProdutoBuilder()
                .WithFornecedorId(_fornecedor.Id).Build();

            _produtoA = new ProdutoBuilder()
                .WithCategoriaProdutoId(_categoriaProduto.Id).Build();

            _produtoB = new ProdutoBuilder().Alternative()
                .WithCategoriaProdutoId(_categoriaProduto.Id).Build();

            _activeProdutos = new List<Produto>() { _produtoA };

            Factory.SeedData(_fornecedor, _categoriaProduto, _produtoA);

            _produtoRepository = new ProdutoRepository(Factory.GetContext());
        }

        [Fact]
        public async Task Deve_Listar_Todos_Produtos()
        {
            var response = await Client.GetAsync($"{ControllerUri}/Listar");

            var responseProduto = await DescerializeResponse<List<Produto>>(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            responseProduto.Should().HaveCount(_activeProdutos.Count);
            responseProduto.Should().ContainEquivalentOf(_produtoA, opt => opt.Excluding(p => p.CategoriaProduto));
        }

        [Fact]
        public async Task Deve_Cadastrar_Corretamente_Um_Produto()
        {
            var response = await Client.PostAsJsonAsync($"{ControllerUri}/Cadastrar", _produtoB);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var produto = await _produtoRepository.GetAllAsync();

            produto.Should().HaveCount(_activeProdutos.Count + 1);
            produto.Should().ContainEquivalentOf(_produtoB);
        }

        [Fact]
        public async Task Deve_Deletar_Corretamente_Um_Produto()
        {
            var response = await Client.DeleteAsync($"{ControllerUri}/Deletar?id={_produtoA.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var itens = await _produtoRepository.GetAllAsync();

            itens.Should().BeEmpty();
        }

        [Fact]
        public async Task Deve_Atualizar_Corretamente_Um_Produto()
        {
            _produtoA.Nome = "Banana";

            var response = await Client.PutAsJsonAsync($"{ControllerUri}/Atualizar", _produtoA);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
