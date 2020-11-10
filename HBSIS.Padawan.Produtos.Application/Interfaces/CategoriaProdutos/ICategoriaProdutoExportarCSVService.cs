using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces.CategoriaProdutos
{
    public interface ICategoriaProdutoExportarCSVService
    {
        Task<byte[]> ExportarCSV();
    }
}
