using CsvHelper;
using HBSIS.Padawan.Produtos.Application.Interfaces.Generic;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Generic
{
    public class ExportarCSVService<T> : IExportarCSVService<T>
    {
        public async Task<byte[]> ExportarCSV(IEnumerable<T> entities)
        {
            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory, Encoding.GetEncoding("iso-8859-1")))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = ";";
                csvWriter.WriteHeader(typeof(T));
                csvWriter.NextRecord();
                csvWriter.WriteRecords(entities);

                writer.Flush();
                return memory.ToArray();
            }
        }
    }
}
