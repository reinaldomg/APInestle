using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HBSIS.Padawan.Produtos.Infra.Mappings
{
    public class FornecedorTypeConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RazaoSocial).IsRequired().HasMaxLength(500);
            builder.Property(x => x.NomeFantasia).HasMaxLength(500);
            builder.Property(x => x.Cnpj).IsRequired().HasMaxLength(14);
            builder.Property(x => x.Endereco).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        }
    }
}
