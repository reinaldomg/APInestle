using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HBSIS.Padawan.Produtos.Infra.Mappings
{
    public class ProdutoTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PesoPorUnidade).IsRequired();
            builder.Property(x => x.Preco).IsRequired();
            builder.Property(x => x.UnidadePorCaixa).IsRequired();
            builder.Property(x => x.Validade).IsRequired();
            builder.HasOne(x => x.CategoriaProduto).WithMany().HasForeignKey(x => x.CategoriaProdutoId);
        }
    }
}
