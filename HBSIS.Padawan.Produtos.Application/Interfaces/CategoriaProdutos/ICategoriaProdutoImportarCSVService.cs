using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos
{
    public interface ICategoriaProdutoImportarCSVService 
    {
        Task<List<string>> ImportarCSV(IFormFile file);
    }
}
