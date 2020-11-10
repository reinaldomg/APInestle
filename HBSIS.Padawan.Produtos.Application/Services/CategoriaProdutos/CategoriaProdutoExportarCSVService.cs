using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Interfaces.Generic;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos
{
    public class CategoriaProdutoExportarCSVService : ICategoriaProdutoExportarCSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IExportarCSVService<CategoriaProduto> _exportarCSVService;

        public CategoriaProdutoExportarCSVService(ICategoriaProdutoRepository categoriaProdutoRepository, IExportarCSVService<CategoriaProduto> exportarCSVService)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _exportarCSVService = exportarCSVService;
        }

        public async Task<byte[]> ExportarCSV()
        {
            return await _exportarCSVService.ExportarCSV(await _categoriaProdutoRepository.GetAllAsync());
        }
    }
}
