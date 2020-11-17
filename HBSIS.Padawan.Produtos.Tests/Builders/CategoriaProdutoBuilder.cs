using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
    public class CategoriaProdutoBuilder
    {
        private Guid _fornecedorId;
        private string _nome;

        public CategoriaProdutoBuilder()
        {
            _fornecedorId = Guid.NewGuid();
            _nome = "Bala Juquinha";
        }

        public CategoriaProdutoBuilder WithFornecedorId(Guid id)
        {
            _fornecedorId = id;
            return this;
        }

        public CategoriaProdutoBuilder Alternative()
        {
            _fornecedorId = Guid.NewGuid();
            _nome = "Bala São João";
            return this;
        }

        public CategoriaProduto Build()
        {
            return new CategoriaProduto()
            {
                Nome = _nome,
                FornecedorId = _fornecedorId,
                Id = Guid.NewGuid()
            };
        }
    }
}
