using FluentValidation;
using FluentValidation.Validators;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Domain.Interfaces.FornecedorValidators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HBSIS.Padawan.Produtos.Domain.Validators.FornecedorValidators
{
    public class CamposFornecedorValidator : AbstractValidator<Fornecedor>, ICamposFornecedorValidator 
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public CamposFornecedorValidator(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;

            ValidarNomeFantasia();
            ValidarRazaoSocial();
            ValidarCnpj();
            ValidarEndereco();
            ValidarEmail();
            ValidarTelefone();
        }

        private void ValidarNomeFantasia()
        {
            RuleFor(x => x.NomeFantasia).NotEmpty().WithMessage("Campo Nome Fantasia é obrigatório")
                .MaximumLength(500).WithMessage("Nome Fantasia excede o tamanho máximo");
        }

        private void ValidarRazaoSocial()
        {
            RuleFor(x => x.RazaoSocial).NotEmpty().WithMessage("Campo Razão Social é obrigatório")
                .MaximumLength(500).WithMessage("Razão Social excede o tamanho máximo");
        }

        private void ValidarCnpj()
        {
            RuleFor(x => x.Cnpj).NotEmpty().WithMessage("Campo CNPJ é obrigatório")
                .MaximumLength(14).WithMessage("CNPJ fora dos padrões")
                .MustAsync(async (cnpj, _) =>
                {
                    return !await _fornecedorRepository.ExistsByCnpjAsync(cnpj);
                }).WithMessage("CNPJ já cadastrado");
        }

        private void ValidarEndereco()
        {
            RuleFor(x => x.Endereco).NotEmpty().WithMessage("Campo Endereço é obrigatório")
                .MaximumLength(500).WithMessage("Endereço excede o tamanho máximo");
        }

        private void ValidarEmail()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Campo Email é obrigatório")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Email é inválido");
        }

        private void ValidarTelefone()
        {
            RuleFor(x => x.Telefone).NotEmpty().WithMessage("Campo Telefone é obrigatório")
                .Must((telefone) =>
                {
                    return !ValidadorTelefone(telefone);
                }).WithMessage("Telefone é inválido");
        }

        private bool ValidadorCnpj(string cnpj)
        {
            Regex rx = new Regex(@"^\d{14}");
            if(rx.IsMatch(cnpj))
                return false;
            return true;
        }

        private bool ValidadorTelefone(string telefone)
        {
            Regex rx = new Regex(@"^\(?[\d]{2}\)? ?9? ?[\d]{4}[ -]?[\d]{4}");
            if(rx.IsMatch(telefone))
            return false;
            return true;
        }
    }
}
