using HBSIS.Padawan.Produtos.Domain.DTO;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using HBSIS.Padawan.Produtos.Infra.Services.Generic;

namespace HBSIS.Padawan.Produtos.Infra.Services.CategoriaProdutos
{
    public class CategoriaProdutoImportarCSVService : ImportarCSVService<CategoriaProduto, CategoriaProdutoDTO>, ICategoriaProdutoImportarCSVService
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

        public override void SalvarEntidade(CategoriaProdutoDTO item)
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
