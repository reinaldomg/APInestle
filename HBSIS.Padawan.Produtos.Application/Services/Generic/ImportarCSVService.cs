using CsvHelper;
using FluentValidation;
using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Application.Interfaces.Generic;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Generic
{
    public abstract class ImportarCSVService<T,I> : IImportarCSVService<T, I> where T : BaseEntity
    {
        protected readonly IValidator<I> _validator;
        protected readonly IGenericRepository<T> _repository;

        protected ImportarCSVService(IValidator<I> validator, IGenericRepository<T> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<List<string>> ImportarCSV(IFormFile file)
        {
            var mensagens = new List<string>();
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

                    var listaImportados = csv.GetRecords<I>();

                    var index = 1;
                    foreach (var item in listaImportados)
                    {
                        var resultado = _validator.Validate(item);

                        if (resultado.IsValid)
                        {
                            CriarObjeto(item);
                        }
                        else
                        {
                            mensagens.Add($"Item: {index}");
                            mensagens.AddRange(resultado.Errors.Select(x => x.ErrorMessage));
                        }
                        index++;
                    }
                    return mensagens;
                }
            }
        }
        public abstract void CriarObjeto(I item);
    }
}
