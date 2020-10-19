using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class CategoriaProdutoService : ICategoriaProdutoService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly ICategoriaProdutoValidator _categoriaProdutoValidator;

        public CategoriaProdutoService(ICategoriaProdutoRepository categoriaProdutoRepository, ICategoriaProdutoValidator categoriaProdutoValidator)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _categoriaProdutoValidator = categoriaProdutoValidator;
        }

        public async Task<Result<CategoriaProduto>> CreateAsync(CategoriaProduto categoriaProduto)
        {
            var result = await _categoriaProdutoValidator.CategoriaProdutoCreateValidate(categoriaProduto);

            if (result.IsValid)
                await _categoriaProdutoRepository.CreateAsync(categoriaProduto);

            return result;
        }

        public async Task<Result<CategoriaProduto>> DeleteAsync(Guid Id)
        {
            var result = await _categoriaProdutoValidator.CategoriaProdutoIdValidate(Id);

            if (result.IsValid)
                await _categoriaProdutoRepository.DeleteAsync(Id);

            return result;
        }

        public async Task<IEnumerable<CategoriaProduto>> GetAsync()
        {
            return await _categoriaProdutoRepository.GetAllAsync();
        }

        public async Task<Result<CategoriaProduto>> UpdateAsync(CategoriaProduto categoriaProduto)
        {
            var result = await _categoriaProdutoValidator.CategoriaProdutoUpdateValidate(categoriaProduto);

            if (result.IsValid)
                await _categoriaProdutoRepository.UpdateAsync(categoriaProduto);

            return result;

        }
    }
}
