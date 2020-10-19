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


        public async Task<Result<CategoriaProduto>> CategoriaProdutoCreateValidate(CategoriaProduto categoriaProduto)
        {
            var result = new Result<CategoriaProduto>();
            result = await CamposObrigatoriosValidador(categoriaProduto); 
            return result;
        }

        public async Task<Result<CategoriaProduto>> CategoriaProdutoIdValidate(Guid Id)
        {
            var result = await ExisteCategoriaValidador(Id); 
            return result;
        }

        public async Task<Result<CategoriaProduto>> CategoriaProdutoUpdateValidate(CategoriaProduto categoriaProduto)
        {
            var result = await ExisteCategoriaValidador(categoriaProduto.Id);

            if (!result.IsValid)
            {
                return result;
            }
            else
                result = await CamposObrigatoriosValidador(categoriaProduto);
           
            return result;
        }

        private async Task<bool> ExisteFornecedorValidador(Guid fornecedor)
        {
            return await _fornecedorRepository.ExistsByIdAsync(fornecedor);
        }

        private async Task<Result<CategoriaProduto>> CamposObrigatoriosValidador(CategoriaProduto categoriaProduto)
        {
            var result = new Result<CategoriaProduto>();

            if (categoriaProduto.Nome.Length > 500 || categoriaProduto.Nome == string.Empty)
            {
                result.IsValid = false;
                result.ErrorList.Add("Nome é inválido");
            }
            if (categoriaProduto.FornecedorId == null || !(await ExisteFornecedorValidador(categoriaProduto.FornecedorId)))
            {
                result.IsValid = false;
                result.ErrorList.Add("Id de referência a Fornecedor é inválido");
            }

            return result;
        }

        private async Task<Result<CategoriaProduto>> ExisteCategoriaValidador(Guid id)
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
