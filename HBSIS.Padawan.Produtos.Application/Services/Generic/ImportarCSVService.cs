using CsvHelper;
using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Application.Interfaces.Generic;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.Generic
{
    public abstract class ImportarCSVService<T> : IImportarCSVService<T>
    {
        protected T Importar;
        protected object Novoitem;
        protected ValidationResult Resultado;

        public async Task<List<string>> ImportarCSV(IFormFile file)
        {
            var r = new List<string>();
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

                    var entities = csv.GetRecords<T>();

                    var index = 1;
                    foreach (var item in entities)
                    {
                        CriarItemImportado(item);
                        
                        if (Resultado.IsValid)
                        {
                            CriarObjetoFinal(Importar);
                        }
                        else
                        {
                            r.Add($"Item: {index}");
                            r.AddRange(Resultado.Errors.Select(x => x.ErrorMessage));
                        }
                        index++;
                    }
                    return r;
                }
            }
        }

        public virtual void CriarItemImportado(object obj)
        {
        }

        public virtual void ValidarObjeto(T item)
        {
        }

        public virtual void CriarObjetoFinal(T item)
        {
        }
    }
}
