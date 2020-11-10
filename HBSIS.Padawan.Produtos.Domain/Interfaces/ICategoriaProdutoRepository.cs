using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface ICategoriaProdutoRepository : IGenericRepository<CategoriaProduto>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
