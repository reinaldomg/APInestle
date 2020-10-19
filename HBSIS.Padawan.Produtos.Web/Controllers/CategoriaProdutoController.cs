using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaProdutoController : ControllerBase
    {
        private readonly ICategoriaProdutoService _categoriaProdutoService;

        public CategoriaProdutoController(ICategoriaProdutoService fornecedorService)
        {
            _categoriaProdutoService = fornecedorService;
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

    }
}
