using HBSIS.Padawan.Produtos.Application.Interfaces;
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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
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
    }
}
