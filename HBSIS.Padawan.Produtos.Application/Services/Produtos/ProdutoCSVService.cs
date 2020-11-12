using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Generic;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Produtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Produtos
{
    public class ProdutoCSVService : IProdutoCSVService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IExportarCSVService<Produto> _produtosExportarCSVService;
        private readonly IProdutosImportarCSVService _produtosImportarCSVService;

        public ProdutoCSVService(
            IExportarCSVService<Produto> produtosExportarCSVService,
            IProdutosImportarCSVService produtosImportarCSVService,
            IProdutoRepository produtoRepository)
        {
            _produtosExportarCSVService = produtosExportarCSVService;
            _produtosImportarCSVService = produtosImportarCSVService;
            _produtoRepository = produtoRepository;
        }

        public async Task<byte []> ExportarCSVAsync()
        {
            return await _produtosExportarCSVService.ExportarCSVAsync(await _produtoRepository.GetAllAsync());
        }

        public async Task<List<string>> ImportarCSVAsync(IFormFile file)
        {
            return await _produtosImportarCSVService.ImportarCSVAsync(file);
        } 
    }
}
