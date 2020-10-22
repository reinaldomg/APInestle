using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Validators
{
    public class ImportarCategoriaProdutoValidator : IImportarCategoriaProdutoValidator
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public ImportarCategoriaProdutoValidator(IFornecedorRepository fornecedorRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        public async Task<Result<CategoriaProduto>> Importar(string cnpj, string nome)
        {
            var result = new Result<CategoriaProduto>();

            if (!await _fornecedorRepository.GetByCnpj(cnpj))
            {
                result.IsValid = false;
                result.ErrorList.Add($"CNPJ não cadastrado: {cnpj}");
            }
            if (await _categoriaProdutoRepository.GetByName(nome))
            {
                result.IsValid = false;
                result.ErrorList.Add($"Nome de categoria já cadastrado: {nome}");
            }
            if (nome.Length > 500 || nome == string.Empty)
            {
                result.IsValid = false;
                result.ErrorList.Add($"Nome é inválido: {nome}");
            }
            if (result.IsValid)
            {
                var fornecedor = await _fornecedorRepository.GetEntityByCnpj(cnpj);
                result.Entity = new CategoriaProduto()
                {
                    Nome = nome,
                    FornecedorId = fornecedor.Id
                };
            }
            return result;
        }
    }
}
