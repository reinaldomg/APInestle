using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IFornecedorRepository : IGenericRepository<Fornecedor>
    {
        Task<bool> GetByCnpj(string cnpj);
        Task<Fornecedor> GetEntityByCnpj(string cnpj);
    }
}
