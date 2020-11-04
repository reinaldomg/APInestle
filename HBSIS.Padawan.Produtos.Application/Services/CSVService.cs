using CsvHelper;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class CSVService : ICSVService
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        private readonly IImportarCategoriaProdutoValidator _importarCategoriaProdutoValidator;
        private readonly IFornecedorRepository _fornecedorRepository;

        public CSVService(ICategoriaProdutoRepository categoriaProdutoRepository, IImportarCategoriaProdutoValidator importarCategoriaProdutoValidator, IFornecedorRepository fornecedorRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
            _importarCategoriaProdutoValidator = importarCategoriaProdutoValidator;
            _fornecedorRepository = fornecedorRepository;
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

        public async Task<Result<CategoriaProduto>> ImportarCSV(IFormFile file)
        {           
            var response = new Result<CategoriaProduto>();
            using (var memorystream = new MemoryStream())
            {
                await file.CopyToAsync(memorystream);
                var mem = new MemoryStream(memorystream.ToArray());
                using (var reader = new StreamReader(mem, Encoding.GetEncoding("iso-8859-1")))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.MissingFieldFound = null;

                    var entities = csv.GetRecords<PropImportacao>();

                    var index = 1;
                    foreach (var item in entities)
                    {
                        var result = await _importarCategoriaProdutoValidator.Validar(item.Cnpj, item.Nome);
                        if (result.IsValid)
                        {
                            CriarCategoriaProduto(item);
                        }
                        else
                        {
                            response.ErrorList.Add($"Item: {index}");
                            response.ErrorList.AddRange(result.ErrorList.ToArray());
                        }
                        index++;
                    }
                    return response;
                }
            }   
        }

        private class PropImportacao
        {
            public string Nome { get; set; }
            public string Cnpj { get; set; }
        }

        private async void CriarCategoriaProduto(PropImportacao item)
        {
            var fornecedor = await _fornecedorRepository.GetEntityByCnpjAsync(item.Cnpj);
            var categoriaproduto = new CategoriaProduto()
            {
                Nome = item.Nome,
                FornecedorId = fornecedor.Id
            };
            await _categoriaProdutoRepository.CreateAsync(categoriaproduto);
        }
    }
}
