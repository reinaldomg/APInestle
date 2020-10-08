using HBSIS.Padawan.Produtos.Application.Services;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Web.Controllers
{
    [ApiController]
    [Route("Fornecedor")]
    public class FornecedorController : ControllerBase
    {

        private IFornecedorServices _ifornecedorServices;

        public FornecedorController(IFornecedorServices fornecedorServices)
        {
            _ifornecedorServices = fornecedorServices;
        }

        [HttpPost]
        [Route("Criar")]
        public IActionResult Post(Fornecedor fornecedor)
        {
 
            var result = _ifornecedorServices.CreateFornecedor(fornecedor);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.ErrorList);            
        }

    }
}
