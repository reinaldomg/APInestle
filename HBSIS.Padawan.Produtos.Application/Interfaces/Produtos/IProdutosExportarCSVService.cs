using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.Produtos
{
    public interface IProdutosExportarCSVService
    {
        Task<byte[]> ExportarCSV();
    }
}
