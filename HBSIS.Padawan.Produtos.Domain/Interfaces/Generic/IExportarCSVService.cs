using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces.Generic
{
    public interface IExportarCSVService<T>
    {
        Task<byte[]> ExportarCSVAsync(IEnumerable<T> entities);
    }
}
