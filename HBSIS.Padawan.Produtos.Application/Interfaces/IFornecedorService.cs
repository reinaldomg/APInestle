using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface IFornecedorService 
    {
        Task<ValidationResult> CreateFornecedorAsync(Fornecedor fornecedor);
    }
}
