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


        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }


        public async Task<Result<Fornecedor>> CreateFornecedorAsync(Fornecedor fornecedor) 
        {

            var validator = new FornecedorValidator();
            var result = validator.FornecedorValidate(fornecedor);

            if (result.IsValid)
                await _fornecedorRepository.CreateAsync(fornecedor);
            
            return result;
        }


    }
}
