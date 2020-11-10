using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Application.Services.Generic;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Entities.Importar;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using System;
using System.Reflection;

namespace HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos
{
    public class CategoriaProdutoImportarCSVService : ImportarCSVService<CategoriaProduto, CategoriaProdutoImportar>, ICategoriaProdutoImportarCSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IImportarCategoriaProdutoValidator _importarCategoriaProdutoValidator;

        public CategoriaProdutoImportarCSVService(ICategoriaProdutoRepository categoriaProdutoRepository,
            IFornecedorRepository fornecedorRepository,
            IImportarCategoriaProdutoValidator importarCategoriaProdutoValidator)
        : base(importarCategoriaProdutoValidator, categoriaProdutoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public override void CriarObjeto(CategoriaProdutoImportar item)
        {
            var objeto = new CategoriaProduto()
            {
                Nome = item.Nome,
                FornecedorId = _fornecedorRepository.GetEntityByCnpjAsync(item.Cnpj).Result.Id
            };

            _categoriaProdutoRepository.CreateAsync(objeto);
        }
    }
}
