using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.Produtos
{
    public interface IProdutosImportarCSVService
    {
        Task<List<string>> ImportarCSVAsync(IFormFile file);
    }
}
