using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.Produtos
{
    public interface IProdutoCSVService
    {
        Task<byte[]> ExportarCSVAsync();
        Task<List<string>> ImportarCSVAsync(IFormFile file);
    }
}
