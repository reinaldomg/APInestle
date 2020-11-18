using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
    public static class FornecedorHelper
    {
        public static Guid FornecedorId => Guid.Parse("71cb18ba-7e4f-4f29-144e-08d86ae63dc3");
        public static Fornecedor FornecedorGugu => new Fornecedor()
        {
            NomeFantasia = "Domingo Legal",
            Cnpj = "10029717532343",
            Email = "guguliberato@gugu.com.br",
            RazaoSocial = "Domingo Triste",
            Endereco = "Rua dos bobos, n 0",
            Telefone = "4340028922",
            Id = FornecedorId
        };
    }
}
