using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.Generic
{
    public interface IExportarCSVService<T>
    {
        Task<byte[]> ExportarCSV(IEnumerable<T> entities);
    }
}
