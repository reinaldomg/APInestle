using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public interface IFornecedorServices 
    {
        Result<Fornecedor> CreateFornecedor(Fornecedor fornecedor);
    }
}
