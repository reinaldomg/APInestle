using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos
{
    public class CategoriaProdutoCSVService : ICategoriaProdutoCSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IExportarCSVService<CategoriaProduto> _categoriaProdutoExportarCSVService;
        private readonly ICategoriaProdutoImportarCSVService _categoriaProdutoImportarCSVService;

        public CategoriaProdutoCSVService(
            IExportarCSVService<CategoriaProduto> categoriaProdutoExportarCSVService,
            ICategoriaProdutoImportarCSVService categoriaProdutoImportarCSVService,
            ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _categoriaProdutoExportarCSVService = categoriaProdutoExportarCSVService;
            _categoriaProdutoImportarCSVService = categoriaProdutoImportarCSVService;
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        public async Task<byte[]> ExportarCSVAsync()
        {
            return await _categoriaProdutoExportarCSVService
                .ExportarCSVAsync(await _categoriaProdutoRepository.GetAllAsync());
        }

        public async Task<List<string>> ImportarCSVAsync(IFormFile file)
        {
            return await _categoriaProdutoImportarCSVService.ImportarCSVAsync(file);
        }
    }
}
