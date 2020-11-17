using HBSIS.Padawan.Produtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
    public class FornecedorBuilder
    {
        private string _nomeFantasia;
        private string _cnpj;
        private string _email;
        private string _razaoSocial;
        private string _endereco;
        private string _telefone;

        public FornecedorBuilder()
        {
            _nomeFantasia = "Domingo Legal";
            _cnpj = "10029717532343";
            _email = "guguliberato@gugu.com.br";
            _razaoSocial = "Domingo Triste";
            _endereco = "Rua dos bobos, n 0";
            _telefone = "4340028922";
        }

        public Fornecedor Build()
        {
            return new Fornecedor()
            {
                Id = Guid.NewGuid(),
                NomeFantasia = _nomeFantasia,
                Cnpj = _cnpj,
                Email = _email,
                RazaoSocial = _razaoSocial,
                Endereco = _endereco,
                Telefone = _telefone
            };
        }
    }
}
