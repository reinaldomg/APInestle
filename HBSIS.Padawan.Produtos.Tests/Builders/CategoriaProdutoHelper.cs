using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
    public class CategoriaProdutoHelper
    {
        public static Guid CategoriaProdutoId => Guid.Parse("71cb18ba-7e4f-4f29-144e-08d86ae63dc4"); 
        public static CategoriaProduto CategoriaProdutoBalaJuquinha => new CategoriaProduto()
        {
            FornecedorId = FornecedorHelper.FornecedorId,
            Nome = "Bala Juquinha",
            Id = CategoriaProdutoId
        };

        public static CategoriaProduto CategoriaProdutoBalaSaoJoao => new CategoriaProduto()
        {
            FornecedorId = FornecedorHelper.FornecedorId,
            Nome = "Bala São João",
            Id = Guid.NewGuid()
        };
    }
}
