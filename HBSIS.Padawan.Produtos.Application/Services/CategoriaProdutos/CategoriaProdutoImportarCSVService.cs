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
    public class CategoriaProdutoImportarCSVService : ImportarCSVService<CategoriaProdutoImportar>, ICategoriaProdutoImportarCSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IImportarCategoriaProdutoValidator _importarCategoriaProdutoValidator;

        public CategoriaProdutoImportarCSVService(ICategoriaProdutoRepository categoriaProdutoRepository, IFornecedorRepository fornecedorRepository, IImportarCategoriaProdutoValidator importarCategoriaProdutoValidator)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _fornecedorRepository = fornecedorRepository;
            _importarCategoriaProdutoValidator = importarCategoriaProdutoValidator;
        }

        public override void CriarItemImportado(object obj)
        {
            Importar = new CategoriaProdutoImportar();

            PropertyInfo nome = obj.GetType().GetProperty("Nome");
            Importar.Nome = (String)(nome.GetValue(obj, null));
            PropertyInfo cnpj = obj.GetType().GetProperty("Cnpj");
            Importar.Cnpj = (String)(cnpj.GetValue(obj, null));

            ValidarObjeto(Importar);
        }

        public override void CriarObjetoFinal(CategoriaProdutoImportar item)
        {
            Novoitem = new CategoriaProduto()
            {
                Nome = item.Nome,
                FornecedorId = _fornecedorRepository.GetEntityByCnpjAsync(item.Cnpj).Result.Id
            };

            _categoriaProdutoRepository.CreateAsync((CategoriaProduto)Novoitem);
        }

        public override void ValidarObjeto(CategoriaProdutoImportar item)
        {
            Resultado = _importarCategoriaProdutoValidator.Validate(Importar);
        }

    }
}
