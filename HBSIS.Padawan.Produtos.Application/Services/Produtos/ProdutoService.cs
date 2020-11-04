using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.ProdutoValidators;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Produtos
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICamposProdutoValidator _produtoValidatorCampos;
        private readonly IIdProdutoValidator _produtoValidatorID;
        private readonly IUpdateProdutoValidator _produtoValidatorUpdate;

        public ProdutoService(IProdutoRepository produtoRepository, ICamposProdutoValidator produtoValidatorCampos, IIdProdutoValidator produtoValidatorID, IUpdateProdutoValidator produtoValidatorUpdate)
        {
            _produtoRepository = produtoRepository;         
            _produtoValidatorCampos = produtoValidatorCampos;
            _produtoValidatorID = produtoValidatorID;
            _produtoValidatorUpdate = produtoValidatorUpdate;
        }

        public async Task<ValidationResult> CreateAsync(Produto produto)
        {
            var result = await _produtoValidatorCampos.ValidateAsync(produto);

            if (result.IsValid)
                await _produtoRepository.CreateAsync(produto);

            return result;
        }

        public async Task<ValidationResult> DeleteAsync(Guid id)
        {
            var result = await _produtoValidatorID.ValidateAsync(id);

            if (result.IsValid)
                await _produtoRepository.DeleteAsync(id);

            return result;
        }

        public async Task<IEnumerable<Produto>> GetAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<ValidationResult> UpdateAsync(Produto produto)
        {
            var result = await _produtoValidatorUpdate.ValidateAsync(produto);

            if (result.IsValid)
                await _produtoRepository.UpdateAsync(produto);

            return result;
        }
    }
}
