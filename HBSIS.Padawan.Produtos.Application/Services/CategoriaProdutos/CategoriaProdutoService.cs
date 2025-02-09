﻿using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.CategoriaProdutoValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.CategoriaProdutos
{
    public class CategoriaProdutoService : ICategoriaProdutoService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly ICriarCategoriaProdutoValidator _camposCategoriaProdutoValidator;
        private readonly IIdCategoriaProdutoValidator _idCategoriaProdutoValidator;
        private readonly IAtualizarCategoriaProdutoValidator _updateCategoriaProdutoValidator;

        public CategoriaProdutoService(ICategoriaProdutoRepository categoriaProdutoRepository, ICriarCategoriaProdutoValidator categoriaProdutoValidator, IIdCategoriaProdutoValidator idCategoriaProdutoValidator, IAtualizarCategoriaProdutoValidator updateCategoriaProdutoValidator)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _camposCategoriaProdutoValidator = categoriaProdutoValidator;
            _idCategoriaProdutoValidator = idCategoriaProdutoValidator;
            _updateCategoriaProdutoValidator = updateCategoriaProdutoValidator;
        }

        public async Task<ValidationResult> CreateAsync(CategoriaProduto categoriaProduto)
        {
            var result = await _camposCategoriaProdutoValidator.ValidateAsync(categoriaProduto);

            if (result.IsValid)
                await _categoriaProdutoRepository.CreateAsync(categoriaProduto);

            return result;
        }

        public async Task<ValidationResult> DeleteAsync(Guid Id)
        {
            var result = await _idCategoriaProdutoValidator.ValidateAsync(Id);

            if (result.IsValid)
                await _categoriaProdutoRepository.DeleteAsync(Id);

            return result;
        }

        public async Task<IEnumerable<CategoriaProduto>> GetAsync()
        {
            return await _categoriaProdutoRepository.GetAllAsync();
        }

        public async Task<ValidationResult> UpdateAsync(CategoriaProduto categoriaProduto)
        {
            var result = await _updateCategoriaProdutoValidator.ValidateAsync(categoriaProduto);

            if (result.IsValid)
                await _categoriaProdutoRepository.UpdateAsync(categoriaProduto);

            return result;
        }
    }
}
