﻿using HBSIS.Padawan.Produtos.Application.Services;
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
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Post(Fornecedor fornecedor)
        {
            var result = _fornecedorService.CreateFornecedor(fornecedor);

            if (result.IsValid)
                return Ok("ok");
            else
                return BadRequest(result.ErrorList);            
        }

    }
}
