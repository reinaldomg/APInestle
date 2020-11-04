using CsvHelper;
using HBSIS.Padawan.Produtos.Application.Interfaces.Produtos;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Produtos
{
    public class ProdutoCSVService : IProdutoCSVService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoCSVService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<byte[]> ExportarCSV()
        {
            var entities = await _produtoRepository.GetAllAsync();

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory, Encoding.GetEncoding("iso-8859-1")))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = ";";
 
                csvWriter.WriteField("Nome");
                csvWriter.WriteField("Preco");
                csvWriter.WriteField("UnidadePorCaixa");
                csvWriter.WriteField("PesoPorUnidade");
                csvWriter.WriteField("Validade");
                csvWriter.WriteField("CategoriaProdutoId");
                csvWriter.NextRecord();

                foreach(var item in entities)
                {
                    csvWriter.WriteField(item.Nome);
                    csvWriter.WriteField(item.Preco);
                    csvWriter.WriteField(item.UnidadePorCaixa);
                    csvWriter.WriteField(item.PesoPorUnidade);
                    csvWriter.WriteField(item.Validade);
                    csvWriter.WriteField(item.CategoriaProdutoId);
                    csvWriter.NextRecord();
                }

                writer.Flush();
                return memory.ToArray();
            }
        }
    }
}
