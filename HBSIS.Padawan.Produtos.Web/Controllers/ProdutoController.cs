using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoCSVService _produtoCSVService;

        public ProdutoController(IProdutoService produtoService,
            IProdutoCSVService produtoCSVService)
        {
            _produtoService = produtoService;
            _produtoCSVService = produtoCSVService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> PostAsync(Produto produto)
        {
            var result = await _produtoService.CreateAsync(produto);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.Errors.ToString());
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> PutAsync(Produto produto)
        {
            var result = await _produtoService.UpdateAsync(produto);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.Errors.ToList());
        }

        [HttpDelete]
        [Route("Deletar")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _produtoService.DeleteAsync(id);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.Errors.ToString());
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _produtoService.GetAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("Exportar")]
        public async Task<IActionResult> ExportAsync()
        {
            try
            {
                var result = await _produtoCSVService.ExportarCSVAsync();
                var memory = new MemoryStream(result);
                return new FileStreamResult(memory, "application/csv") { FileDownloadName = "Produto.csv" };
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
                var result = await _produtoCSVService.ImportarCSVAsync(file);
                return Ok(result.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
