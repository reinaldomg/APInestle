using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Result;
using HBSIS.Padawan.Produtos.Domain.Validators;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorValidator _fornecedorValidator;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IFornecedorValidator fornecedorValidator)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorValidator = fornecedorValidator;
        }

        public async Task<Result<Fornecedor>> CreateFornecedorAsync(Fornecedor fornecedor) 
        {
            var result = _fornecedorValidator.FornecedorValidate(fornecedor);

            if (result.IsValid)
                await _fornecedorRepository.CreateAsync(fornecedor);
            
            return result;
        }
    }
}
