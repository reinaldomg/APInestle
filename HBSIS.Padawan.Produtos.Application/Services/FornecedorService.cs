using FluentValidation.Results;
using HBSIS.Padawan.Produtos.Application.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ICriarFornecedorValidator _camposFornecedorValidator;

        public FornecedorService(IFornecedorRepository fornecedorRepository, ICriarFornecedorValidator camposFornecedorValidator)
        {
            _fornecedorRepository = fornecedorRepository;
            _camposFornecedorValidator = camposFornecedorValidator;
        }

        public async Task<ValidationResult> CreateFornecedorAsync(Fornecedor fornecedor) 
        {
            var result = await _camposFornecedorValidator.ValidateAsync(fornecedor);

            if (result.IsValid)
                await _fornecedorRepository.CreateAsync(fornecedor);
            
            return result;
        }
    }
}
