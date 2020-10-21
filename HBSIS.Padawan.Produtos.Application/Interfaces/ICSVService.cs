using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface ICSVService
    {
        public Task<byte[]> ExportarCSV();
    }
}
