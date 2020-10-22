using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IImportarCategoriaProdutoValidator
    {
        public Task<Result<CategoriaProduto>> Importar(string cnpj, string nome);
    }
}
