using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HBSIS.Padawan.Produtos.Domain.Result;
using HBSIS.Padawan.Produtos.Domain.Interfaces;

namespace HBSIS.Padawan.Produtos.Domain.Validators
{
    public class FornecedorValidator : Result<Fornecedor>, IFornecedorValidator
    {

        public Result<Fornecedor> FornecedorValidate(Fornecedor fornecedor)
        {
            var result = new Result<Fornecedor>(fornecedor);
            if (string.IsNullOrEmpty(fornecedor.Cnpj) || fornecedor.Cnpj.Length > 14 || ValidadorCnpj(fornecedor.Cnpj))
            {
                result.IsValid = false;
                result.ErrorList.Add("O CNPJ não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.Endereco) || fornecedor.Endereco.Length > 500)
            {
                result.IsValid = false;
                result.ErrorList.Add("O Endereço não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.NomeFantasia) || fornecedor.NomeFantasia.Length > 500)
            {
                result.IsValid = false;
                result.ErrorList.Add("O Nome Fantasia não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.RazaoSocial) || fornecedor.RazaoSocial.Length > 500)
            {
                result.IsValid = false;
                result.ErrorList.Add("A Razão Social não é válida");
            }
            if (string.IsNullOrEmpty(fornecedor.Email) || fornecedor.Email.Length > 100 || ValidadorEmail(fornecedor.Email))
            {
                result.IsValid = false;
                result.ErrorList.Add("O Email não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.Telefone) || fornecedor.Telefone.Length > 100 || ValidadorTelefone(fornecedor.Telefone))
            {
                result.IsValid = false;
                result.ErrorList.Add("O Telefone não é válido");
            }
            return result;
        }

        protected bool ValidadorCnpj(string cnpj)
        {
            Regex rx = new Regex(@"^\d{14}");
            if (rx.IsMatch(cnpj))
            {
                return false;
            }
            return true;
        }

        private bool ValidadorEmail(string email)
        {
            Regex rx = new Regex(@"^[\w]+@[\w]+.[\w]{3}.?([\w]+)?.([\w]+)?$");
            if (rx.IsMatch(email))
                return false;
            else return true;
        }

        private bool ValidadorTelefone(string telefone)
        {
            Regex rx = new Regex(@"^\(?[\d]{2}\)? ?9? ?[\d]{4}[ -]?[\d]{4}");
            if (rx.IsMatch(telefone))
                return false;
            else return true;
        }
    }
}
