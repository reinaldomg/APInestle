using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HBSIS.Padawan.Produtos.Domain.Result;

namespace HBSIS.Padawan.Produtos.Domain.Validators
{
    public class FornecedorValidators : Result<Fornecedor>
    {

        public FornecedorValidators(Fornecedor fornecedor)
        {
            base.Entity = fornecedor;
            if (string.IsNullOrEmpty(fornecedor.Cnpj) || fornecedor.Cnpj.Length > 14 || ValidadorCnpj(fornecedor.Cnpj))
            {
                base.IsValid = false;
                base.ErrorList.Add("O CNPJ não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.Endereco) || fornecedor.Endereco.Length > 500)
            {
                base.IsValid = false;
                base.ErrorList.Add("O Endereço não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.NomeFantasia) || fornecedor.NomeFantasia.Length > 500)
            {
                base.IsValid = false;
                base.ErrorList.Add("O Nome Fantasia não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.RazaoSocial) || fornecedor.RazaoSocial.Length > 500)
            {
                base.IsValid = false;
                base.ErrorList.Add("A Razão Social não é válida");
            }
            if (string.IsNullOrEmpty(fornecedor.Email) || fornecedor.Email.Length > 100 /*|| ValidadorEmail(fornecedor.Email)*/)
            {
                base.IsValid = false;
                base.ErrorList.Add("O Email não é válido");
            }
            if (string.IsNullOrEmpty(fornecedor.Telefone) || fornecedor.Telefone.Length > 100 || ValidadorTelefone(fornecedor.Telefone))
            {
                base.IsValid = false;
                base.ErrorList.Add("O Telefone não é válido");
            }
        }

        public FornecedorValidators()
        {
        }

        protected bool ValidadorCnpj(string cnpj)
        {
            string c = Regex.Replace(cnpj, @"\D", "");
            Regex rx = new Regex(@"^\d{14}");
            if (rx.IsMatch(c))
            {
                base.Entity.Cnpj = c;
                return false;
            }
            return true;
        }

        public bool ValidadorEmail(string email)
        {
            Regex rx = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+))\.([A-Za-z]{2,})$");
            if (rx.IsMatch(email))
                return false;

            else return true;
        }

        public bool ValidadorTelefone(string telefone)
        {
            Regex rx = new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$");
            if (rx.IsMatch(telefone))
                return false;

            else return true;
        }
    }
}
