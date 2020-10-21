using CsvHelper;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class CSVService : ICSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public CSVService(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        public async Task<byte[]> ExportarCSV()
        {
            var entities = await _categoriaProdutoRepository.GetAllAsync();

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory, Encoding.GetEncoding("iso-8859-1")))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))            
            {
                csvWriter.Configuration.Delimiter = ";";
                csvWriter.WriteField("Nome");
                csvWriter.WriteField("FornecedorId");
                csvWriter.WriteField("Id");
                csvWriter.NextRecord();
                foreach(var item in entities)
                {
                    csvWriter.WriteField(item.Nome);
                    csvWriter.WriteField(item.FornecedorId);
                    csvWriter.WriteField(item.Id);
                    csvWriter.NextRecord();
                }
                writer.Flush();
                return memory.ToArray();
            }         
        }
    }
}
