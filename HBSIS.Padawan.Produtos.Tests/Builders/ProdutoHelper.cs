using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
    public class ProdutoHelper
    {
        public static Guid ProdutoId => Guid.Parse("71cb18ba-7e4f-4f29-144e-08d86ae63dc5");
        public static Produto ProdutoBolacha => new Produto()
        {
            Nome = "Bolacha",
            CategoriaProdutoId = CategoriaProdutoHelper.CategoriaProdutoId,
            PesoPorUnidade = 2,
            UnidadePorCaixa = 20,
            Preco = 30.00,
            Validade = new DateTime(2020, 08, 07),
            Id = ProdutoId
        };

        public static Produto ProdutoBanana => new Produto()
        {
            Nome = "Bolacha",
            CategoriaProdutoId = CategoriaProdutoHelper.CategoriaProdutoId,
            PesoPorUnidade = 2,
            UnidadePorCaixa = 20,
            Preco = 30.00,
            Validade = new DateTime(2020, 08, 07),
            Id = Guid.NewGuid()
        };
    }
}
