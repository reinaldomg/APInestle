using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IProdutoValidator
    {
        Task<Result<Produto>> CreateValidate(Produto produto);
        Task<Result<Produto>> IdValidate(Guid Id);
        Task<Result<Produto>> UpdateValidate(Produto produto);
    }
}
