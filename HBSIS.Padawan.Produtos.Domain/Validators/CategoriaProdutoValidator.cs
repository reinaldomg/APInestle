using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Validators
{
    public class CategoriaProdutoValidator : ICategoriaProdutoValidator
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public CategoriaProdutoValidator(IFornecedorRepository fornecedorRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _fornecedorRepository = fornecedorRepository;
        }


        public async Task<Result<CategoriaProduto>> CreateValidate(CategoriaProduto categoriaProduto)
        {
            return await ValidarCamposObrigatorios(categoriaProduto); 
        }

        public async Task<Result<CategoriaProduto>> IdValidate(Guid Id)
        {
            return await ExisteCategoria(Id); 
        }

        public async Task<Result<CategoriaProduto>> UpdateValidate(CategoriaProduto categoriaProduto)
        {
            var result = await ExisteCategoria(categoriaProduto.Id);

            if (!result.IsValid)
            {
                return result;
            }
            else
                result = await ValidarCamposObrigatorios(categoriaProduto);
           
            return result;
        }

        private async Task<bool> ExisteFornecedor(Guid fornecedor)
        {
            return await _fornecedorRepository.ExistsByIdAsync(fornecedor);
        }

        private async Task<Result<CategoriaProduto>> ValidarCamposObrigatorios(CategoriaProduto categoriaProduto)
        {
            var result = new Result<CategoriaProduto>();

            if (categoriaProduto.Nome.Length > 500 || categoriaProduto.Nome == string.Empty)
            {
                result.ErrorList.Add("Nome é inválido");
            }
            if(await _categoriaProdutoRepository.ExistsByNameAsync(categoriaProduto.Nome))
            {
                result.ErrorList.Add("Categoria já cadastrada");
            }
            if (categoriaProduto.FornecedorId == Guid.Empty|| !(await ExisteFornecedor(categoriaProduto.FornecedorId)))
            {
                result.ErrorList.Add("Id de referência a Fornecedor é inválido");
            }
            result.IsValid = !result.ErrorList.Any();
            return result;
        }

        private async Task<Result<CategoriaProduto>> ExisteCategoria(Guid id)
        {
            var result = new Result<CategoriaProduto>();

            var response = await _categoriaProdutoRepository.ExistsByIdAsync(id);

            if (!response)
            {
                result.IsValid = false;
                result.ErrorList.Add("Item não encontrado");
            }

            return result;
        }

    }
}
