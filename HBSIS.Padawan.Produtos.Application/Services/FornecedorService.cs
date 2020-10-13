using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;
using HBSIS.Padawan.Produtos.Domain.Validators;
using HBSIS.Padawan.Produtos.Domain.Result;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Domain.Interfaces;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class FornecedorService : IFornecedorService
    {

        private readonly IFornecedorRepository _fornecedorRepository;


        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }


        public Result<Fornecedor> CreateFornecedor(Fornecedor fornecedor) 
        {

            var validator = new FornecedorValidator();
            var result = validator.FornecedorValidate(fornecedor);

            if (result.IsValid)
                _fornecedorRepository.CreateAsync(fornecedor);
            
            return result;
        }


    }
}
