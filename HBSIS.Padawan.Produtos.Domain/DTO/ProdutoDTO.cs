using System;

namespace HBSIS.Padawan.Produtos.Domain.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int UnidadePorCaixa { get; set; }
        public double PesoPorUnidade { get; set; }
        public DateTime Validade { get; set; }
        public string NomeCategoriaProduto { get; set; }
    }
}
