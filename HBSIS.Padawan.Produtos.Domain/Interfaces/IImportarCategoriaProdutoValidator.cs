using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IImportarCategoriaProdutoValidator
    {
        public Task<Result<CategoriaProduto>> Validar(string cnpj, string nome);
    }
}
