using HBSIS.Padawan.Produtos.Domain.DTO;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Infra.Services.Generic;

namespace HBSIS.Padawan.Produtos.Infra.Services.Produtos
{
    public class ProdutosImportarCSVService : ImportarCSVService<Produto, ProdutoDTO>, IProdutosImportarCSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public ProdutosImportarCSVService(IImportarProdutoValidator validator,
            IProdutoRepository repository,
            ICategoriaProdutoRepository categoriaProdutoRepository) : base(validator, repository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        public override void SalvarEntidade(ProdutoDTO item)
        {
            var objeto = new Produto()
            {
                Nome = item.Nome,
                PesoPorUnidade = item.PesoPorUnidade,
                UnidadePorCaixa = item.UnidadePorCaixa,
                Preco = item.Preco,
                Validade = item.Validade,
                CategoriaProdutoId = _categoriaProdutoRepository.GetEntityByNameAsync(item.NomeCategoriaProduto).Result.Id
            };
            _repository.CreateAsync(objeto);
        }
    }
}
