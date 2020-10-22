using CsvHelper;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class CSVService : ICSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IImportarCategoriaProdutoValidator _importarCategoriaProdutoValidator;

        public CSVService(ICategoriaProdutoRepository categoriaProdutoRepository, IImportarCategoriaProdutoValidator importarCategoriaProdutoValidator)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _importarCategoriaProdutoValidator = importarCategoriaProdutoValidator;
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

        public async Task<Result<CategoriaProduto>> ImportarCSV(byte[] file)
        {
            
            var response = new Result<CategoriaProduto>();
            using(var memory = new MemoryStream(file))
            using (var reader = new StreamReader(memory, Encoding.GetEncoding("iso-8859-1")))               
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ";";
                csv.Configuration.MissingFieldFound = null;

                var entities = csv.GetRecords<PropImportacao>();

                int index = 1;
                foreach(var item in entities)
                {
                    var result = await _importarCategoriaProdutoValidator.Importar(item.Cnpj, item.Nome);
                    if (result.IsValid)
                    {
                        await _categoriaProdutoRepository.CreateAsync(result.Entity);
                    }
                    else
                    {
                        response.ErrorList.Add($"Item: {index}");
                        response.ErrorList.AddRange(result.ErrorList.ToArray());
                    }
                    index++;
                }
            }
            return response;
        }

        private class PropImportacao
        {
            public string Nome { get; set; }
            public string Cnpj { get; set; }
        }
    }
}
