using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Validators
{
    public class ImportarCategoriaProdutoValidator : IImportarCategoriaProdutoValidator
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public ImportarCategoriaProdutoValidator(IFornecedorRepository fornecedorRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        public async Task<Result<CategoriaProduto>> Validar(string cnpj, string nome)
        {
            var result = new Result<CategoriaProduto>();

            if (!await _fornecedorRepository.ExistsByCnpjAsync(cnpj))
            {
                result.ErrorList.Add($"CNPJ não cadastrado: {cnpj}");
            }
            if (await _categoriaProdutoRepository.ExistsByNameAsync(nome))
            {
                result.ErrorList.Add($"Nome de categoria já cadastrado: {nome}");
            }
            if (nome.Length > 500 || nome == string.Empty)
            {
                result.ErrorList.Add($"Nome é inválido: {nome}");
            }
            result.IsValid = !result.ErrorList.Any();
       
            return result;
        }
    }
}
