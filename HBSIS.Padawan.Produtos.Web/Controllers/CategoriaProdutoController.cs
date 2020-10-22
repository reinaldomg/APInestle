using CsvHelper;
using HBSIS.Padawan.Produtos.Application;
using HBSIS.Padawan.Produtos.Application.Interfaces;
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
        private readonly ICSVService _csvService;

        public CategoriaProdutoController(ICategoriaProdutoService fornecedorService, ICSVService csvService)
        {
            _categoriaProdutoService = fornecedorService;
            _csvService = csvService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> PostAsync(CategoriaProduto fornecedor)
        {
            var result = await _categoriaProdutoService.CreateAsync(fornecedor);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.ErrorList);
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> PutAsync(CategoriaProduto fornecedor)
        {
            var result = await _categoriaProdutoService.UpdateAsync(fornecedor);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.ErrorList);
        }

        [HttpDelete]
        [Route("Deletar")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _categoriaProdutoService.DeleteAsync(id);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.ErrorList);
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
                var result = await _csvService.ExportarCSV();
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
                using (var memory = new MemoryStream())
                {
                    await file.CopyToAsync(memory);
                    var result = await _csvService.ImportarCSV(memory.ToArray());
                
                    return Ok(result.ErrorList);
                }
             }
            catch
            {
                return BadRequest("Não foi possível importar o arquivo selecionado");
            }
        }

    }
}
