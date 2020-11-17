using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
    public class ProdutoBuilder
    {
        private int _unidadePorCaixa;
        private string _nome;
        private int _pesoPorUnidade;
        private double _preco;
        private DateTime _validade;
        private Guid _categoriaProdutoId;

        public ProdutoBuilder()
        {
            _nome = "Bolacha";
            _categoriaProdutoId = Guid.NewGuid();
            _pesoPorUnidade = 2;
            _unidadePorCaixa = 20;
            _preco = 30.00;
            _validade = new DateTime(2020, 08, 07);
        }

        public ProdutoBuilder WithCategoriaProdutoId(Guid id)
        {
            _categoriaProdutoId = id;
            return this; 
        }

        public ProdutoBuilder Alternative()
        {
            _nome = "Banana";
            _preco = 50.0;
            return this;
        }

        public Produto Build()
        {
            return new Produto()
            {
                Id = Guid.NewGuid(),
                UnidadePorCaixa = _unidadePorCaixa,
                Nome = _nome,
                PesoPorUnidade = _pesoPorUnidade,
                Preco = _preco,
                Validade = _validade,
                CategoriaProdutoId = _categoriaProdutoId
            };
        }
    }
}
