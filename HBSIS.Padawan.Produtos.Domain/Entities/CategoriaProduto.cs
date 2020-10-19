using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Entities
{
    public class CategoriaProduto : BaseEntity
    {
        public string Nome { get; set; }
        public Guid FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
