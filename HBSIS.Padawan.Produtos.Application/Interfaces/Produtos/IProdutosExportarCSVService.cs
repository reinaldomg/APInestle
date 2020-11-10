using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.Produtos
{
    public interface IProdutosExportarCSVService
    {
        Task<byte[]> ExportarCSV();
    }
}
