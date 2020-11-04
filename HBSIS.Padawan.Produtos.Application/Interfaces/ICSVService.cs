using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface ICSVService
    {
        Task<byte[]> ExportarCSV();
        Task<Result<CategoriaProduto>> ImportarCSV(IFormFile file);
    }
}
