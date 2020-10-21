using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
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
            var result = new Result<CategoriaProduto>();
            result = await ValidarCamposObrigatorios(categoriaProduto); 
            return result;
        }

        public async Task<Result<CategoriaProduto>> IdValidate(Guid Id)
        {
            var result = await ExisteCategoria(Id); 
            return result;
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
                result.IsValid = false;
                result.ErrorList.Add("Nome é inválido");
            }
            if (categoriaProduto.FornecedorId == null || !(await ExisteFornecedor(categoriaProduto.FornecedorId)))
            {
                result.IsValid = false;
                result.ErrorList.Add("Id de referência a Fornecedor é inválido");
            }

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
