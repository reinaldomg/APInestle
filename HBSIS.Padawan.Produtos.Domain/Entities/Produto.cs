using System;

namespace HBSIS.Padawan.Produtos.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int UnidadePorCaixa { get; set; }
        public double PesoPorUnidade { get; set; }
        public DateTime Validade { get; set; }
        public Guid CategoriaProdutoId { get; set; }
        public virtual CategoriaProduto CategoriaProduto { get; set; }
    }
}
