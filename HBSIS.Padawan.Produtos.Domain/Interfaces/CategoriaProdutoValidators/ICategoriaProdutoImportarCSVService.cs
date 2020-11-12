using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutos
{
    public interface ICategoriaProdutoImportarCSVService 
    {
        Task<List<string>> ImportarCSVAsync(IFormFile file);
    }
}
