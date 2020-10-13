using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IFornecedorValidator
    {
        public Result<Fornecedor> FornecedorValidate(Fornecedor fornecedor);
    }
}
