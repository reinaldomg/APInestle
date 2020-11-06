using CsvHelper;
using HBSIS.Padawan.Produtos.Application;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaProdutoController : ControllerBase
    {
        private readonly ICategoriaProdutoService _categoriaProdutoService;
        private readonly ICategoriaProdutoExportarCSVService _categoriaProdutoExportarCSVService;
        private readonly ICategoriaProdutoImportarCSVService _categoriaProdutoImportarCSVService;

        public CategoriaProdutoController(ICategoriaProdutoService fornecedorService, ICategoriaProdutoExportarCSVService categoriaProdutoExportarCSVService, ICategoriaProdutoImportarCSVService categoriaProdutoImportarCSVService)
        {
            _categoriaProdutoService = fornecedorService;
            _categoriaProdutoExportarCSVService = categoriaProdutoExportarCSVService;
            _categoriaProdutoImportarCSVService = categoriaProdutoImportarCSVService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> PostAsync(CategoriaProduto fornecedor)
        {
            var result = await _categoriaProdutoService.CreateAsync(fornecedor);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.Errors.Select(x => x.ErrorMessage));
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> PutAsync(CategoriaProduto fornecedor)
        {
            var result = await _categoriaProdutoService.UpdateAsync(fornecedor);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.Errors.Select(x => x.ErrorMessage));
        }

        [HttpDelete]
        [Route("Deletar")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _categoriaProdutoService.DeleteAsync(id);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.Errors.Select(x => x.ErrorMessage));
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoriaProdutoService.GetAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("Exportar")]
        public async Task<IActionResult> ExportAsync()
        {
            try
            {
                var result = await _categoriaProdutoExportarCSVService.ExportarCSV();
                var memory = new MemoryStream(result);
                return new FileStreamResult(memory, "application/csv") { FileDownloadName = "CategoriaProduto.csv" };
            }
            catch
            {
                return BadRequest("Um erro aconteceu ao exportar o arquivo");
            }
        }

        [HttpPost]
        [Route("Importar")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            try
            {
                var result = await _categoriaProdutoImportarCSVService.ImportarCSV(file);
                return Ok(result.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
