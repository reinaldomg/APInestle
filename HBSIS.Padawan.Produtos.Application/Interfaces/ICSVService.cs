using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface ICSVService
    {
        public Task<byte[]> ExportarCSV();
        public Task<Result<CategoriaProduto>> ImportarCSV(IFormFile file);
    }
}
