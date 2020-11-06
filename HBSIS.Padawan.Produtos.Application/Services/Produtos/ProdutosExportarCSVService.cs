using HBSIS.Padawan.Produtos.Application.Interfaces.Generic;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Application.Services.Generic;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Produtos
{
    public class ProdutosExportarCSVService : IProdutosExportarCSVService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IExportarCSVService<Produto> _exportarCSVService;

        public ProdutosExportarCSVService(IProdutoRepository produtoRepository, IExportarCSVService<Produto> exportarCSVService)
        {
            _produtoRepository = produtoRepository;
            _exportarCSVService = exportarCSVService;
        }

        public async Task<byte[]> ExportarCSV()
        {
            return await _exportarCSVService.ExportarCSV(await _produtoRepository.GetAllAsync());
        }
    }
}
