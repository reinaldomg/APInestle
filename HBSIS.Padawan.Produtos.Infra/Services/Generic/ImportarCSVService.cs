using CsvHelper;
using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Services.Generic
{
    public abstract class ImportarCSVService<TEntity,IDTO> : IImportarCSVService<TEntity, IDTO> where TEntity : BaseEntity
    {
        protected readonly IValidator<IDTO> _validator;
        protected readonly IGenericRepository<TEntity> _repository;

        protected ImportarCSVService(IValidator<IDTO> validator, IGenericRepository<TEntity> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<List<string>> ImportarCSVAsync(IFormFile file)
        {
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

                    var listaDTO = csv.GetRecords<IDTO>();

                    return ManipularDTOs(listaDTO);
                }
            }
        }
        public abstract void SalvarEntidade(IDTO item);

        private List<string> ManipularDTOs(IEnumerable<IDTO> listaDTOs)
        {
            var index = 1;
            var mensagens = new List<string>();
            foreach (var item in listaDTOs)
            {
                var resultado = _validator.Validate(item);

                if (resultado.IsValid)
                {
                    SalvarEntidade(item);
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
