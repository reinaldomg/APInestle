using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IFornecedorRepository : IGenericRepository<Fornecedor>
    {
        Task<bool> ExistsByCnpjAsync(string cnpj);
        Task<Fornecedor> GetEntityByCnpjAsync(string cnpj);
    }
}
